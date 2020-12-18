using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Adhaesii.WazoooDOTexe.Player
{
    public class PlayerRespawnHandler : SerializedMonoBehaviour
    {
        [SerializeField]
        private bool useSelf = true;

        [SerializeField, HideIf("useSelf")]
        private Transform player;
        
        [ShowInInspector, ReadOnly]
        public Transform SpawnPoint { get; set; }

        [SerializeField]
        private float respawnTime = 2f;

        public event Action<Transform> OnRespawn;

        [SerializeField]
        private UnityEvent DespawnEvents;
        
        [SerializeField]
        private UnityEvent RespawnEvents;

        public void Respawn()
        {
            if (!SpawnPoint)
            {
                Debug.LogError("No spawn point set yet");
                return;
            }

            if (useSelf)
                StartCoroutine(processRespawn_(transform));
            else if (player)
                StartCoroutine(processRespawn_(player.transform));

            IEnumerator processRespawn_(Transform tx)
            {
                DespawnEvents?.Invoke();
                
                yield return new WaitForSeconds(respawnTime);
                
                RespawnEvents?.Invoke();

                tx.position = SpawnPoint.position;
                
                OnRespawn?.Invoke(SpawnPoint);
                
                GameEvents.Instance.PlayerRespawn();
            }
        }
    }
}
