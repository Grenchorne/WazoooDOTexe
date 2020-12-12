using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Adhaesii.WazoooDOTexe.UI
{
    public class MaskSlider : SerializedMonoBehaviour
    {
        [SerializeField, HideInInspector]
        private float _value;

        [ShowInInspector, PropertyRange(0, 1)]
        public float Value
        {
            get => _value;
            set
            {
                _value = Mathf.Clamp01(value);
                fill.transform.localPosition = Vector2.Lerp(minPos, maxPos, _value);
            }
        }

        [SerializeField]
        private Vector2 minPos;

        [SerializeField]
        private Vector2 maxPos;

        [SerializeField]
        private RectTransform fill;

        [Button, HorizontalGroup("Editor Buttons")]
        private void GetMin()
        {
            if (!fill)
            {
                Debug.LogWarning("Fill not set");
                return;
            }

            minPos = fill.localPosition;
        }

        [Button, HorizontalGroup("Editor Buttons")]
        private void GetMax()
        {
            if (!fill)
            {
                Debug.LogWarning("Fill not set");
                return;
            }
            
            maxPos = fill.localPosition;
        }
    }
}
