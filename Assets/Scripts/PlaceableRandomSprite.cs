using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    public class PlaceableRandomSprite : SerializedMonoBehaviour
    {
        [SerializeField, InlineEditor]
        private RandomSpriteSelection sprites;

        [SerializeField, HideIf("useSelf")]
        private SpriteRenderer target;

        [SerializeField]
        private bool useSelf = true;

        [SerializeField]
        private RandomPoint randomPoint = RandomPoint.Reset;
        
        enum RandomPoint
        {
            Reset,
            Awake,
            Start,
            Enable
        }

        private void Reset()
        {
            if (randomPoint == RandomPoint.Reset)
                AssignRandomSprite();
        }

        private void Awake()
        {
            if (randomPoint == RandomPoint.Awake)
                AssignRandomSprite();
        }

        private void Start()
        {
            if (randomPoint == RandomPoint.Start)
                AssignRandomSprite();
        }

        private void OnEnable()
        {
            if (randomPoint == RandomPoint.Enable)
                AssignRandomSprite();
        }

        [Button]
        private void AssignRandomSprite()
        {
            if (!sprites)
                return;
            SpriteRenderer sr = null;

            if (useSelf)
                TryGetComponent(out sr);
            else if (target) sr = target;

            if(!sr)
                Debug.LogWarning("SpriteRenderer not found");

            sr.sprite = sprites.GetRandom();
        }
    }
}
