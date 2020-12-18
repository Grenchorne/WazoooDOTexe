using Adhaesii.WazoooDOTexe.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Spawning
{
    public class RespawnTrigger : SerializedMonoBehaviour
    {
        [SerializeField]
        private Transform respawnPoint;

        [SerializeField]
        private string playerTag = "Player";

        private void Reset() => GetComponent<Collider2D>().isTrigger = true;

        [Button]
        private void CreateSpawnPointInCentre()
        {
            Collider2D col = GetComponent<Collider2D>();
            respawnPoint = new GameObject("SpawnPoint").transform;
            respawnPoint.SetParent(transform);
            respawnPoint.position = col.bounds.center;
        }

        [Button]
        private void AssignPointFromChild() => respawnPoint = transform.GetChild(0);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.CompareTag(playerTag))
                return;

            PlayerRespawnHandler playerRespawnHandler = FindObjectOfType<PlayerRespawnHandler>();
            if(respawnPoint)
                playerRespawnHandler.SpawnPoint = respawnPoint;
            else
                Debug.LogError("Respawn point not set on " + gameObject.name);
        }
    }
}
