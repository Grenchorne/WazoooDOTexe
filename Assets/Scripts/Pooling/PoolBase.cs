using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Pooling
{
    [RequireComponent(typeof(Pool))]
    public abstract class PoolBase<T> : Singleton<T> where T : PoolBase<T>
    {
        protected Pool pool;

        protected void Awake() => pool = GetComponent<Pool>();
    }
}