using Adhaesii.WazoooDOTexe.Audio;
using Adhaesii.WazoooDOTexe.WazoooInput.MonoBehaviours;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Player
{
    [RequireComponent(typeof(PlayerInput), typeof(PlayerMover), typeof(Rigidbody2D))]
    [RequireComponent(typeof(FuelHandler))]
    public class PlayerFacade : SerializedMonoBehaviour
    {
        private PlayerInput Input { get; set; }
        private PlayerMover Mover { get; set; }
        private Rigidbody2D RigidBody2D { get; set; }
        private GroundCheck GroundCheck { get; set; }
        
        private FuelHandler FuelHandler { get; set; }
        
        private HealthController HealthController { get; set; }
        private PlayerRespawnHandler RespawnHandler { get; set; }
        
        private PlayerAbilityUnlockHandler Abilities { get; set; }
        
        private PlayerRangedController RangedController { get; set; }
        
        private WazoooVoice WazoooVoice { get; set; }
        
        private HitSpriteFX HitFX { get; set; }

        private bool jumpCancelled;

        [SerializeField, Required]
        private PlayerMeleeController playerMeleeController;

        [SerializeField, Required]
        private PlayerAudioController audioController;

        [SerializeField]
        private GameObject[] hoverGameObjects;

        [SerializeField]
        private bool shootConsumesFuel = true;

        [SerializeField]
        private PlayerVerticalPeek.Settings peekSettings;

        [SerializeField]
        private Transform peekTransform;
        
        private PlayerVerticalPeek peek;
        
        private void Awake()
        {
            Input = GetComponent<PlayerInput>();
            Mover = GetComponent<PlayerMover>();
            RigidBody2D = GetComponent<Rigidbody2D>();
            GroundCheck = GetComponentInChildren<GroundCheck>();
            FuelHandler = GetComponent<FuelHandler>();
            HealthController = GetComponent<HealthController>();
            RespawnHandler = GetComponent<PlayerRespawnHandler>();
            Abilities = GetComponent<PlayerAbilityUnlockHandler>();
            HitFX = GetComponent<HitSpriteFX>();
            RangedController = GetComponent<PlayerRangedController>();

            WazoooVoice = GetComponentInChildren<WazoooVoice>();
            
            peek = new PlayerVerticalPeek(peekSettings);
            peekTransform.SetParent(transform);
        }

        private void OnEnable()
        {
            Mover.OnWalk += audioController.ProcessFootsteps;
            
            Mover.OnHover += audioController.ProcessHover;
            Mover.OnHover += SetHoverFX;
            
            playerMeleeController.OnSwing += audioController.PlayAttack;

            HealthController.OnDamage += audioController.PlayHit;
            HealthController.OnDie += audioController.PlayDie;

            HealthController.OnDamage += showHitFX;

            // Game Events
            HealthController.OnDie += () => GameEvents.Instance.PlayerDeath();
            HealthController.OnDie += () => gameObject.SetActive(false);
            
            // Wazooo voice!

            HealthController.OnDamage += () => WazoooVoice.PlayHurt();
            HealthController.OnDie += () => WazoooVoice.PlayDeath();
            RespawnHandler.OnRespawn += (_) => WazoooVoice.PlayDespawn();
            
            EnemySensor.Instance.OnSeeBoss += (_) => WazoooVoice.PlaySeeBoss();
            EnemySensor.Instance.OnSeeNewEnemy += (_) => WazoooVoice.PlaySeeEnemy();
            EnemySensor.Instance.OnEnemyDie += (_) => WazoooVoice.PlayKillEnemy();
            EnemySensor.Instance.OnBossDie += (_) => WazoooVoice.PlayKillBoss();

            Abilities.OnUnlockHover += () => WazoooVoice.PlayUnlock(default);
            Abilities.OnUnlockAttack += () => WazoooVoice.PlayUnlock(default);
            Abilities.OnUnlockHoverJump += () => WazoooVoice.PlayUnlock(default);
            Abilities.OnUnlockShoot += () => WazoooVoice.PlayUnlock(default);

        }

        private void OnDisable()
        {
            Mover.OnWalk -= audioController.ProcessFootsteps;
            Mover.OnHover -= audioController.ProcessHover;
            Mover.OnHover -= SetHoverFX;
            
            playerMeleeController.OnSwing -= audioController.PlayAttack;
            
            HealthController.OnDamage -= audioController.PlayHit;
            HealthController.OnDie -= audioController.PlayDie;
            
            HealthController.OnDamage -= showHitFX;
            
            // Game Events
            HealthController.OnDie -= () => GameEvents.Instance.PlayerDeath();
        }

        private void showHitFX() => HitFX.ShowFX();

        private void Update()
        {
            peekTransform.localPosition = new Vector3(0, peek.ProcessPeek(Input.Vertical.Value, Mover.IsMoving, Time.deltaTime));
            
            // Check if can attack
            if(Input.MeleeAttack.Down && Abilities.CanAttack)
                playerMeleeController.Attack();

            if (Input.RangedAttack.Down && Abilities.CanShoot)
            {
                if (shootConsumesFuel)
                {
                    if (FuelHandler.HasPip)
                    {
                        if (RangedController.Shoot(transform.localScale.x > 0))
                            FuelHandler.DepleteFullPip();
                    }
//                    else
//                        Debug.Log("no fuel");
                }
                else
                    RangedController.Shoot(transform.localScale.x > 0); 
            }
        }

        private void FixedUpdate()
        {   
            // xxx Check if can move
            // xxx  // if (Input.Horizontal > 0 || Input.Horizontal < 0) Mover.Move(Input.Horizontal);
            // Move anyway - we check for zero inside the PlayerMover
            Mover.Move(Input.Horizontal.Value);
            
            // Replenish fuel if grounded
            if (GroundCheck.IsGrounded || !Input.Hover.Held)
            {
                FuelHandler.DisableDepletion();
            }

            // Check if can hover -- ensure this is set before jump because a jump-hover depletes fuel
            else if (Abilities.CanHover && Input.Hover.Held && FuelHandler.Fuel > 0)
            {
                Mover.Hover();
                FuelHandler.EnableDepletion();
            }
            
            // Check if can jump
            if (Input.Jump.Held)
            {
                Mover.Jump(Abilities.CanHoverJump);
                jumpCancelled = false;
            }

            else
            {
                Mover.ReleaseJump();

                if (!jumpCancelled)
                {
                    Mover.CancelJump();
                    jumpCancelled = true;
                }
            }
        }

        private void SetHoverFX(bool isHovering)
        {
            foreach (var go in hoverGameObjects)
                if (go)
                    go.SetActive(isHovering);
        }
    }
}