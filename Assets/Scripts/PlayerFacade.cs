using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
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

        private bool jumpCancelled;
        
        private void Awake()
        {
            Input = GetComponent<PlayerInput>();
            Mover = GetComponent<PlayerMover>();
            RigidBody2D = GetComponent<Rigidbody2D>();
            GroundCheck = GetComponentInChildren<GroundCheck>();
            FuelHandler = GetComponent<FuelHandler>();
        }

        private void FixedUpdate()
        {   
            // Check if can move
            if (Input.Horizontal > 0 || Input.Horizontal < 0) Mover.Move(Input.Horizontal);
            
            // Replenish fuel if grounded
            if (GroundCheck.IsGrounded || !Input.Hover)
            {
                FuelHandler.DisableDepletion();
            }

            // Check if can hover -- ensure this is set before jump because a jump-hover depletes fuel
            else if (Input.Hover && FuelHandler.Fuel > 0)
            {
                Mover.Hover();
                FuelHandler.EnableDepletion();
            }
            
            // Check if can jump
            if (Input.Jump)
            {
                Mover.Jump();
                jumpCancelled = false;

            }

            else
            {
                Mover.ReleaseJump();

                if (jumpCancelled) return;
                
                Mover.CancelJump();
                jumpCancelled = true;
            }

            
            
            
            // Check if can attack
            
        }
    }
}