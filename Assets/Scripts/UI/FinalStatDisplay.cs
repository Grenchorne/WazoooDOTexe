using System;
using Adhaesii.WazoooDOTexe.Player;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.UI
{
    public class FinalStatDisplay : SerializedMonoBehaviour
    {
        [SerializeField]
        private TMP_Text_Binder<PlayerCurrency> playerCurrency;
        
        [SerializeField]
        private TMP_Text_Binder<TimeDisplay> playerTime;
        
        public void GetStats()
        {
            playerCurrency.Text.text = playerCurrency.Item.Value.ToString();
            playerTime.Text.text = playerTime.Item.FormattedValue;
        }
        
        [Serializable]
        private class TMP_Text_Binder<T>
        {
            [SerializeField]
            private T _item;
            public T Item => _item;


            [SerializeField]
            private TMP_Text _text;
            public TMP_Text Text => _text;
        }
    }
}