using Adhaesii.WazoooDOTexe.Audio;
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
        
        private HitSpriteFX HitFX { get; set; }

        private bool jumpCancelled;

        [SerializeField, Required]
        private SwordController swordController;

        [SerializeField, Required]
        private PlayerAudioController audioController;

        [SerializeField]
        private GameObject[] hoverGameObjects;

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
            
            peek = new PlayerVerticalPeek(peekSettings);
            peekTransform.SetParent(transform);
        }

        private void OnEnable()
        {
            Mover.OnWalk += audioController.ProcessFootsteps;
            
            Mover.OnHover += audioController.ProcessHover;
            Mover.OnHover += SetHoverFX;
            
            swordController.OnSwing += audioController.PlayAttack;

            HealthController.OnDamage += audioController.PlayHit;
            HealthController.OnDie += audioController.PlayDie;

            HealthController.OnDamage += showHitFX;

            // Game Events
            HealthController.OnDie += () => GameEvents.Instance.PlayerDeath();

        }

        private void OnDisable()
        {
            Mover.OnWalk -= audioController.ProcessFootsteps;
            Mover.OnHover -= audioController.ProcessHover;
            Mover.OnHover -= SetHoverFX;
            
            swordController.OnSwing -= audioController.PlayAttack;
            
            HealthController.OnDamage -= audioController.PlayHit;
            HealthController.OnDie -= audioController.PlayDie;
            
            HealthController.OnDamage -= showHitFX;
            
            // Game Events
            HealthController.OnDie -= () => GameEvents.Instance.PlayerDeath();
        }

        private void showHitFX() => HitFX.ShowFX();

        private void Update()
        {
            peekTransform.localPosition = new Vector3(0, peek.ProcessPeek(Input.Peek, Mover.IsMoving, Time.deltaTime));
        }

        private void FixedUpdate()
        {   
            // xxx Check if can move
            // xxx  // if (Input.Horizontal > 0 || Input.Horizontal < 0) Mover.Move(Input.Horizontal);
            // Move anyway - we check for zero inside the PlayerMover
            Mover.Move(Input.Horizontal);
            
            // Replenish fuel if grounded
            if (GroundCheck.IsGrounded || !Input.Hover)
            {
                FuelHandler.DisableDepletion();
            }

            // Check if can hover -- ensure this is set before jump because a jump-hover depletes fuel
            else if (Abilities.CanHover && Input.Hover && FuelHandler.Fuel > 0)
            {
                Mover.Hover();
                FuelHandler.EnableDepletion();
            }
            
            // Check if can jump
            if (Input.Jump)
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

            // Check if can attack
            if(Input.Attack && Abilities.CanAttack)
                swordController.Attack();
        }

        private void SetHoverFX(bool isHovering)
        {
            foreach (var go in hoverGameObjects)
                if (go)
                    go.SetActive(isHovering);
        }
    }
}