using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Interactibles
{
    public class Gate : SerializedMonoBehaviour
    {
        [SerializeField]
        private float boxColliderCloseHeight = 6f;

        [SerializeField]
        private float spriteRendererCloseHeight = 6f;

        [SerializeField]
        private float transitionTime = 1.5f;

        [SerializeField, HideInInspector]
        private float _openAmount;

        [ShowInInspector]
        public float OpenAmount
        {
            get => _openAmount;
            set
            {
                if(!BoxCollider2D || !SpriteRenderer)
                    AssignComponents();
                _openAmount = Mathf.Clamp01(value);

                Vector2 newBoxSize = BoxCollider2D.size;
                newBoxSize = new Vector2(newBoxSize.x, boxColliderCloseHeight - boxColliderCloseHeight * _openAmount);
                BoxCollider2D.size = newBoxSize;
                BoxCollider2D.offset = new Vector2(BoxCollider2D.offset.x, newBoxSize.y * 0.5f); 
                
                Vector2 spriteSize = SpriteRenderer.size;
                SpriteRenderer.size = new Vector2(spriteSize.x,
                    spriteRendererCloseHeight - spriteRendererCloseHeight * _openAmount);

            }
        }

        private BoxCollider2D BoxCollider2D { get; set; }
        private SpriteRenderer SpriteRenderer { get; set; }

        [Button]
        public void Open() => SetClosed(false);
        
        [Button]
        public void Close() => SetClosed(true);

        public void SetClosed(bool setClosed)
        {
            StopCoroutine(_());
            StartCoroutine(_());
            IEnumerator _()
            {
                float modifier = setClosed ? -1 : 1;
                WaitForEndOfFrame wait = new WaitForEndOfFrame();

                do
                {
                    OpenAmount += (transitionTime * Time.deltaTime) * modifier;
                    yield return wait;
                } while (setClosed ? !Mathf.Approximately(OpenAmount, 0) : !Mathf.Approximately(OpenAmount, 1));

                OpenAmount = setClosed ? 0 : 1;

            }
        }

        private void Awake() => AssignComponents();

        private void AssignComponents()
        {
            BoxCollider2D = GetComponentInChildren<BoxCollider2D>();
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }
}
