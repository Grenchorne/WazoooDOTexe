using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    [RequireComponent(typeof(Collider2D))]
    public class KillTrigger : SerializedMonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private string playerTag = "Player";

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_layerMask != (_layerMask | 1 << other.gameObject.layer)) return;
            
            if (other.CompareTag(playerTag))
            {
                other.transform.position = Vector3.zero;        
                if(other.TryGetComponent(out Rigidbody2D rb))
                    rb.velocity = Vector2.zero;
            }

            else
                Destroy(other.gameObject);
        }
    }
}
