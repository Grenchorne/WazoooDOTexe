using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Pooling
{
    public class CoinPoolable : SerializedMonoBehaviour, IPoolable
    {
        public GameObject GameObject => gameObject;
        public Transform Transform => transform;
        public event Action DisableEvent;

        public void OnDisable() => DisableEvent?.Invoke();
    }
}
