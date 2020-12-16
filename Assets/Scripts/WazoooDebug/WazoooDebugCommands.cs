using Sirenix.OdinInspector;
using static UnityEngine.Input;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.WazoooDebug
{
    public class WazoooDebugCommands : SerializedMonoBehaviour
    {
        [SerializeField]
        private KeyCode k_respawn = KeyCode.R;
        
        [SerializeField]
        private KeyCode k_toggleUnlock_Jump = KeyCode.Alpha0;
        
        [SerializeField]
        private KeyCode k_toggleUnlock_Attack = KeyCode.Alpha1;
        
        [SerializeField]
        private KeyCode k_toggleUnlock_Hover = KeyCode.Alpha2;
        
        [SerializeField]
        private KeyCode k_toggleUnlock_HoverJump = KeyCode.Alpha3;

        
        private PlayerRespawnHandler respawnHandler;
        private PlayerAbilityUnlockHandler unlockHandler;

        private void Start()
        {
            respawnHandler = FindObjectOfType<PlayerRespawnHandler>();
            unlockHandler = FindObjectOfType<PlayerAbilityUnlockHandler>();
        }

        private void Update()
        {
            if(GetKeyDown(k_respawn))
                respawnHandler.Respawn();
            if (GetKeyDown(k_toggleUnlock_Jump))
                unlockHandler.CanJump = !unlockHandler.CanJump;
            if (GetKeyDown(k_toggleUnlock_Attack))
                unlockHandler.CanAttack = !unlockHandler.CanAttack;
            if (GetKeyDown(k_toggleUnlock_Hover))
                unlockHandler.CanHover= !unlockHandler.CanHover;
            if (GetKeyDown(k_toggleUnlock_HoverJump))
                unlockHandler.CanHoverJump= !unlockHandler.CanHoverJump;
        }
    }
}
