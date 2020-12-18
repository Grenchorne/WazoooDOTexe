using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Adhaesii.WazoooDOTexe.Pooling
{
    public class CoinPool : PoolBase<CoinPool>
    {
        public CoinPoolable GetCoin() => pool.Get() as CoinPoolable;
    }
}
