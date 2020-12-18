using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Adhaesii.WazoooDOTexe.Pooling
{
    [RequireComponent(typeof(Pool))]
    public class CoinPool : SerializedMonoBehaviour
    {
        private static CoinPool __instance;
        public static CoinPool Instance
        {
            get
            {
                if (__instance)
                    return __instance;

                __instance = FindObjectOfType<CoinPool>();
                
                if (__instance)
                    return __instance;

                return __instance = new GameObject("Coin Pool").AddComponent<CoinPool>();
            }       
        }
        
        private Pool pool;

        private void Awake() => pool = GetComponent<Pool>();

        public CoinPoolable GetCoin() => pool.Get() as CoinPoolable;
    }
}
