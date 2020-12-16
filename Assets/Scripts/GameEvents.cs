using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Adhaesii.WazoooDOTexe
{
    public class GameEvents : SerializedMonoBehaviour
    {
        private static GameEvents __instance;
        public static GameEvents Instance
        {
            get
            {
                if (__instance)
                    return __instance;

                __instance = FindObjectOfType<GameEvents>();
                if (__instance)
                    return __instance;

                return __instance = new GameObject("Game Events").AddComponent<GameEvents>();
            }
        }

        // Death
        [SerializeField]
        private UnityEvent OnPlayerDeath;

        public void PlayerDeath() => OnPlayerDeath?.Invoke();
        
        // Respawn
        [SerializeField]
        private UnityEvent OnPlayerRespawn;

        public void PlayerRespawn() => OnPlayerRespawn?.Invoke();
    }
}