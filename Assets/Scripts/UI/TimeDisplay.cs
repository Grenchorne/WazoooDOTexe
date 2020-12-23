using System;
using Sirenix.OdinInspector;
using TMPro;

namespace Adhaesii.WazoooDOTexe.UI
{
    public class TimeDisplay : SerializedMonoBehaviour
    {
        private TMP_Text text;

        private float _value;
        public float Value => _value;

        public string FormattedValue
        {
            get
            {
                string minutes = ((int) _value / 60).ToString("00");
                string seconds = (_value % 60).ToString("00.00");
                
                return $"{minutes}:{seconds}";
            }
        }
        

        private void Awake()
        {
            text = GetComponentInChildren<TMP_Text>();
        }

        public void Increment(float amount)
        {
            _value += amount;
            text.text = FormattedValue;
        }
    }
}