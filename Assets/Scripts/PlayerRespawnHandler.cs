using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    public class PlayerRespawnHandler : SerializedMonoBehaviour
    {
        [SerializeField]
        private bool useSelf = true;

        [SerializeField, HideIf("useSelf")]
        private Transform player;
        
        [ShowInInspector, ReadOnly]
        public Transform SpawnPoint { get; set; }

        public event Action<Transform> OnRespawn; 

        public void Respawn()
        {
            if (!SpawnPoint)
            {
                Debug.LogError("No spawn point set yet");
                return;
            }

            if (useSelf)
                transform.position = SpawnPoint.position;
            else if (player)
                player.transform.position = SpawnPoint.position;
            
            OnRespawn?.Invoke(SpawnPoint);
        }
    }
}
