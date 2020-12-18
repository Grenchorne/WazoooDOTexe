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
        private KeyCode k_fullHeal = KeyCode.Keypad0;
        
        [SerializeField]
        private KeyCode k_damage = KeyCode.Keypad1;

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
            if (GetKeyDown(k_respawn)) respawnHandler.Respawn();
            if (GetKeyDown(k_toggleUnlock_Jump)) unlockHandler.CanJump = !unlockHandler.CanJump;
            if (GetKeyDown(k_toggleUnlock_Attack)) unlockHandler.CanAttack = !unlockHandler.CanAttack;
            if (GetKeyDown(k_toggleUnlock_Hover)) unlockHandler.CanHover = !unlockHandler.CanHover;
            if (GetKeyDown(k_toggleUnlock_HoverJump)) unlockHandler.CanHoverJump = !unlockHandler.CanHoverJump;
            if (GetKeyDown(k_fullHeal)) playerHealth.FullHeal();
            if (GetKeyDown(k_damage)) playerHealth.Damage(gameObject);

            if (spawnCoinOnRightClick && GetMouseButtonDown(1))
            {
                Vector2 pos = mainCam.ScreenToWorldPoint(mousePosition);
                CoinPoolable coin = coinPool.GetCoin();
                coin.Transform.position = pos;
                coin.gameObject.SetActive(true);
            }
        }
    }
}
