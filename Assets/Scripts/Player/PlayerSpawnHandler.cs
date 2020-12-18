using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Player
{
    public class PlayerSpawnHandler : SerializedMonoBehaviour
    {
        [SerializeField]
        private Transform startingPoint;

        [SerializeField]
        private GameObject player;

        [SerializeField]
        private bool immediateRespawn = true;

        private bool readyToRespawn;
        private Transform lastCheckpoint;


        private void Start()
        {
            lastCheckpoint = startingPoint;
            player.transform.position = startingPoint.position;
        }

        private void HandleDeath()
        {
            player.GetComponent<PlayerMover>().SetMovement(false);
            if (!immediateRespawn) return;
            readyToRespawn = true;
            RespawnPlayer();
        }

        private void RespawnPlayer()
        {
            if (!readyToRespawn)
            {
                Debug.Log("Not Ready to Respawn Player");
                return;
            }
            readyToRespawn = false;
            player.transform.position = lastCheckpoint.position;
            player.GetComponent<HealthController>().FullHeal();
            player.GetComponent<PlayerMover>().SetMovement(true);
        }

        private void OnEnable()
        {
            if(player)
                player.GetComponent<HealthController>().OnDie += HandleDeath;
        }

        private void OnDisable()
        {
            if(player)
                player.GetComponent<HealthController>().OnDie -= HandleDeath;
        }
    }
}
