using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    public class DamageSender : SerializedMonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IReceiveDamage dmg))
            {
                dmg.Damage();
            }
        }
    }

    public interface IReceiveDamage
    {
        void Damage();
    }
}
