using System;
using Sirenix.OdinInspector;

namespace Adhaesii.WazoooDOTexe.Player
{
    public class PlayerCurrency : SerializedMonoBehaviour
    {
        public event Action<int> OnUpdate;

        private int _value;
        public int Value => _value;
        
        [Button]
        public void Add(int amount)
        {
            _value += amount;
            OnUpdate?.Invoke(_value);
        }

        [Button]
        public void Subtract(int amount)
        {
            _value -= amount;
            OnUpdate?.Invoke(_value);
        }
    }
}
