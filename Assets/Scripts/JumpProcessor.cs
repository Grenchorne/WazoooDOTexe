using System;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    public class JumpProcessor
    {
        public bool IsJumping { get; private set; }
        public float JumpVelocity { get; private set; }

        public event Action OnJump;
        
        private readonly Settings settings;

        private readonly GroundCheck groundCheck;
        private readonly FuelHandler fuelHandler;

        private bool jumpReleased = true;
        private float t_hold;
        private float t_cooldown;
        
        public JumpProcessor(Settings settings, GroundCheck groundCheck, FuelHandler fuelHandler)
        {
            this.settings = settings;
            this.groundCheck = groundCheck;
            this.fuelHandler = fuelHandler;
        }

        public bool TryJump(bool isHovering)
        {            
            // New Jump -- not jumping, jump released
            if (!IsJumping && jumpReleased)
            {
                // Return if cooldown still continuing, or
                // if not grounded and not hovering
                if (t_cooldown > 0 || (!groundCheck.IsGrounded && !isHovering && groundCheck.TimeSinceLastGrounded > settings.CoyoteTime))
                    return false;

                // Capture the initial input state -- prevents hold to multiple jump
                jumpReleased = false;

                if (isHovering)
                {
                    // deplete pip if hovering
                    fuelHandler.DepletePartialPip();
                }
                
                // initialize the timers
                t_hold = settings.HoldTime;
                t_cooldown = settings.Cooldown;

                // Apply the velocity
                JumpVelocity = settings.Speed;
                
                // Set flag to held
                IsJumping = true;
                
                //Invoke Jump Event
                OnJump?.Invoke();
                
                return true;
            }

            // Continuing Jump -- Check if the hold timer has expired
            if (t_hold <= 0)
            {
                IsJumping = false;
                return false;
            }

            // Apply the velocity
            JumpVelocity = settings.Speed;
            
            //Invoke Jump Event
            OnJump?.Invoke();
            
            return true;
        }

        public bool CancelJump(float velocity)
        {
            if (velocity > 0)
            {
                IsJumping = false;
                return true;
            }
                
            if (t_hold <= 0 )
                return false;
            
            IsJumping = false;
            return true;
        }

        public void ReleaseJump()
        {
            jumpReleased = true;
        }

        public void Tick(float deltaTime)
        {
            // Jump not held - process cooldown
            if (!IsJumping)
                Mathf.Clamp(t_cooldown -= deltaTime, 0, settings.Cooldown);

            // Jump held - process hold timer
            else
            {
                Mathf.Clamp(t_hold -= deltaTime, 0, settings.HoldTime);
                if (t_hold < 0)
                {
                    IsJumping = false;
                }

                else
                {
                    IsJumping = true;
                    JumpVelocity = settings.Speed;
                }
            }

        }

        [Serializable]
        public class Settings
        {
            [SerializeField]
            private float _speed = 10f;
            public float Speed => _speed;

            [SerializeField]
            private float _cooldown = 0.2f;
            public float Cooldown => _cooldown;

            [SerializeField]
            private float _holdTime = 0.15f;
            public float HoldTime => _holdTime;

            [SerializeField]
            private float _coyoteTime = 0.15f;
            public float CoyoteTime => _coyoteTime;
        }
    }
}