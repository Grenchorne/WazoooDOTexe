using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Adhaesii.WazoooDOTexe.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerHealthPickup : SerializedMonoBehaviour
    {
        [SerializeField]
        private int healAmount = 1;

        [SerializeField]
        private UnityEvent OnPickUp;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerFacade player = FindObjectOfType<PlayerFacade>();

            if (!player) return;
            if (!other.TryGetComponent(out PlayerFacade pf)) return;
            if (pf != player) return; 
            player.GetComponent<HealthController>().FullHeal();
            OnPickUp?.Invoke();
        }
    }
}
