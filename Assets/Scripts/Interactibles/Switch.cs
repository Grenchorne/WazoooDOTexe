using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Adhaesii.WazoooDOTexe.Interactibles
{
    [RequireComponent(typeof(Collider2D))]
    public class Switch : SerializedMonoBehaviour
    {
        [SerializeField]
        private string activationTag = "PlayerWeapon";

        public UnityEvent activationEvents;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(activationTag))
                activationEvents?.Invoke();
        }  
    }
}
