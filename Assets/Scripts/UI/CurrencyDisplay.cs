using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Adhaesii.WazoooDOTexe.UI
{
    public class CurrencyDisplay : SerializedMonoBehaviour
    {
        [SerializeField]
        private TMP_Text text;

        public UnityEvent OnUpdateText;

        public void UpdateText(int value)
        {
            text.text = value.ToString("0000");
            OnUpdateText?.Invoke();
        }
    }
}
