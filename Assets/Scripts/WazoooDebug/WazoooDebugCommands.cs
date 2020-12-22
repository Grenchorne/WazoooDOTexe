using System;
using Adhaesii.WazoooDOTexe.Player;
using Adhaesii.WazoooDOTexe.Pooling;
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

        [SerializeField]
        private KeyCode k_toggleUnlock_Shoot = KeyCode.Alpha4;

        [SerializeField]
        private KeyCode k_fullHeal = KeyCode.Keypad0;
        
        [SerializeField]
        private KeyCode k_damage = KeyCode.Keypad1;

        [SerializeField]
        private KeyCode k_teleport = KeyCode.Z;

        [SerializeField]
        private TeleportPair[] teleportPairs = 
        {
            new TeleportPair(KeyCode.Keypad0, null), 
            new TeleportPair(KeyCode.Keypad1, null), 
            new TeleportPair(KeyCode.Keypad2, null), 
            new TeleportPair(KeyCode.Keypad3, null), 
            new TeleportPair(KeyCode.Keypad4, null), 
            new TeleportPair(KeyCode.Keypad5, null), 
            new TeleportPair(KeyCode.Keypad6, null), 
        };

        [SerializeField]
        private bool spawnCoinOnRightClick = true;

        private PlayerFacade player;
        private PlayerRespawnHandler respawnHandler;
        private PlayerAbilityUnlockHandler unlockHandler;
        private HealthController playerHealth;
        private CoinPool coinPool;
        private Camera mainCam;

        private void Start()
        {
            player = FindObjectOfType<PlayerFacade>();
            respawnHandler = FindObjectOfType<PlayerRespawnHandler>();
            unlockHandler = FindObjectOfType<PlayerAbilityUnlockHandler>();
            playerHealth = player.GetComponent<HealthController>();
            
            coinPool = CoinPool.Instance;
            mainCam = Camera.main;
        }

        private void Update()
        {
            Vector2? mousePos = null;
            
            if (GetKeyDown(k_respawn)) respawnHandler.Respawn();
            if (GetKeyDown(k_toggleUnlock_Jump)) unlockHandler.CanJump = !unlockHandler.CanJump;
            if (GetKeyDown(k_toggleUnlock_Attack)) unlockHandler.CanAttack = !unlockHandler.CanAttack;
            if (GetKeyDown(k_toggleUnlock_Hover)) unlockHandler.CanHover = !unlockHandler.CanHover;
            if (GetKeyDown(k_toggleUnlock_HoverJump)) unlockHandler.CanHoverJump = !unlockHandler.CanHoverJump;
            if (GetKeyDown(k_toggleUnlock_Shoot)) unlockHandler.CanShoot = !unlockHandler.CanShoot;
            if (GetKeyDown(k_fullHeal)) playerHealth.FullHeal();
            if (GetKeyDown(k_damage)) playerHealth.Damage(gameObject);

            if (GetKeyDown(k_teleport))
            {
                setScreenToWorldPoint_();
                teleport_((Vector2)mousePos);
            }

            foreach (TeleportPair teleportPair in teleportPairs)
            {
                if (!GetKeyDown(teleportPair.Key)) continue;
                teleport_(teleportPair.Target.position);
                break;
            }
            
            // mouse-dependant tasks
            if (spawnCoinOnRightClick && GetMouseButtonDown(1))
            {
                setScreenToWorldPoint_();   
                CoinPoolable coin = coinPool.GetCoin();
                coin.Transform.position = (Vector2)mousePos;
                coin.gameObject.SetActive(true);
            }
     

            void setScreenToWorldPoint_()
            {
                if(mousePos == null)
                    mousePos =  mainCam.ScreenToWorldPoint(mousePosition);
            }
            
            void teleport_(Vector2 position)
            {
                player.transform.position = position;
                
                if(player.TryGetComponent(out Rigidbody2D  rigidbody2D))
                    rigidbody2D.velocity = Vector2.zero;
                
                else if (player.TryGetComponent(out Rigidbody rigidbody))
                    rigidbody.velocity = Vector3.zero;
            }
        }

        [Serializable]
        private class TeleportPair
        {
            [SerializeField]
            private KeyCode _key;
            public KeyCode Key => _key;

            [SerializeField]
            private Transform _target;
            public Transform Target => _target;

            public TeleportPair(KeyCode key, Transform target)
            {
                _key = key;
                _target = target;
            }
        }
    }
}
