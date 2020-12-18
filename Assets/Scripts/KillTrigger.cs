using Adhaesii.WazoooDOTexe.Audio;
using Adhaesii.WazoooDOTexe.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    [RequireComponent(typeof(Collider2D))]
    public class KillTrigger : SerializedMonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private string playerTag = "Player";

        [SerializeField]
        private int damage = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_layerMask != (_layerMask | 1 << other.gameObject.layer)) return;
            
            if (other.CompareTag(playerTag))
            {
                if(other.TryGetComponent(out HealthController health))
                    health.Damage(gameObject);
                
                if (other.TryGetComponent(out PlayerRespawnHandler respawnHandler))
                    respawnHandler.Respawn();
                
                else
                {
                    other.transform.position = Vector3.zero;
                    if(other.TryGetComponent(out Rigidbody2D rb))
                        rb.velocity = Vector2.zero;
                }
            }

            else
                Destroy(other.gameObject);
        }
    }
}
