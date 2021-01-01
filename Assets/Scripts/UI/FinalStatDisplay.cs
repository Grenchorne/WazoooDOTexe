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
        
        [SerializeField]
        private TMP_Text_Binder<PlayerAbilityUnlockHandler> playerAbilities;
        
        [SerializeField]
        private TMP_Text_Binder<KillTracker> playerKillCount;
        
        public void GetStats()
        {
            playerCurrency.Text.text = playerCurrency.Item.Value.ToString();
            playerTime.Text.text = playerTime.Item.FormattedValue;
            playerAbilities.Text.text = playerAbilities.Item.GetUnlockedAbilities().ToString("0");
            playerKillCount.Text.text = playerKillCount.Item.Kills.ToString("00");
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