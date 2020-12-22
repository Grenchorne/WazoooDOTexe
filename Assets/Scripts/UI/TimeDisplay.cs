using System;
using Sirenix.OdinInspector;
using TMPro;

namespace Adhaesii.WazoooDOTexe.UI
{
    public class TimeDisplay : SerializedMonoBehaviour
    {
        
        private TMP_Text text;

        private float value;

        private void Awake()
        {
            text = GetComponentInChildren<TMP_Text>();
        }

        public void Increment(float amount)
        {
            value += amount;

            string minutes = ((int) value / 60).ToString("00");
            string seconds = (value % 60).ToString("00.00");

            text.text = $"{minutes}:{seconds}";
        }
    }
}