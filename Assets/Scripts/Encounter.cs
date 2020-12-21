using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Adhaesii.WazoooDOTexe
{
    [RequireComponent(typeof(Collider2D))]
    public class Encounter : SerializedMonoBehaviour
    {
        [SerializeField]
        private UnityEvent OnInitiateEncounter;
        
        [SerializeField]
        private UnityEvent OnCompleteEncounter;

        [SerializeField]
        private string playerTag = "Player";

        private bool initiated;
        private bool completed;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag(playerTag))
                Initiate();
        }

        [Button]
        public void Initiate()
        {
            if (initiated)
                return;

            initiated = true;
            OnInitiateEncounter?.Invoke();
        }

        [Button]
        public void Complete()
        {
            if(completed)
                return;
            completed = true;
            OnCompleteEncounter?.Invoke();
        }
    }
}
