using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : SerializedMonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 5f;

        [SerializeField]
        private JumpProcessor.Settings jumpSettings;

        [SerializeField]
        private bool disableHoverOnAscent = true;

        public bool IsHovering { get; private set; }
        public bool IsMoving { get; private set; }

        // components
        private Rigidbody2D RigidBody2D { get; set; }
        private Transform Transform { get; set; }
        private GroundCheck GroundCheck { get; set; }
        
        public  JumpProcessor JumpProcessor { get; set; }
        
        // subscribe to these events
        public event Action<bool> OnHover;
        public event Action<bool> OnWalk;

        public event Action OnIdle; 

        // local members
        private Vector2 velocity;

        private void Awake()
        {
            RigidBody2D = GetComponent<Rigidbody2D>();
            Transform = transform;
            GroundCheck = GetComponentInChildren<GroundCheck>();
            JumpProcessor = new JumpProcessor(jumpSettings, GroundCheck,
                GetComponent<FuelHandler>());
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            JumpProcessor.Tick(deltaTime);
        }

        private void FixedUpdate()
        {
            // Get initial velocity - use our prescribed value if jumping or hovering
            if (!JumpProcessor.IsJumping && !IsHovering)
                velocity.y = RigidBody2D.velocity.y;

            // Disable gravity if hovering
            RigidBody2D.gravityScale = IsHovering ? 0 : 1;

            // Apply movement
            RigidBody2D.velocity = velocity;

            IsMoving = !Mathf.Approximately(velocity.x, 0);
            
            // Reset movement variables
            velocity = Vector2.zero;
            
            // Invoke hover event  
            OnHover?.Invoke(IsHovering);
            
            // Disable hover for next frame - this will be re-flagged by "Hover" if applicable
            IsHovering = false;
        }

        
        public void Move(float move)
        {
            bool wantsToMove = Mathf.Abs(move) > 0;
            
            OnWalk?.Invoke(wantsToMove && !IsHovering && GroundCheck.IsGrounded);
            
            if (Mathf.Approximately(move, 0)) 
                return;
            
            SetFacing(move);

            velocity.x = move * moveSpeed;
        }

        public void Jump()
        {
            if (!JumpProcessor.TryJump(IsHovering)) return;
            velocity.y = JumpProcessor.JumpVelocity;
        }
        
        public void CancelJump()
        {
            if(JumpProcessor.CancelJump(RigidBody2D.velocity.y))
                RigidBody2D.velocity = new Vector2(RigidBody2D.velocity.x, 0f);
        }

        public void ReleaseJump()
        {
            JumpProcessor.ReleaseJump();
        }
        
        public void Hover()
        {
            if(disableHoverOnAscent && RigidBody2D.velocity.y > 0)
                return;
            IsHovering = true;
            velocity.y = 0f;
        }

        private void SetFacing(float move)
        {
            bool facingRight = transform.localScale.x > 0;

            if (move > 0 && facingRight || move < 0 && !facingRight) return;

            Vector3 localScale = Transform.localScale;
            localScale = new Vector3(localScale.x * -1, localScale.y);
            Transform.localScale = localScale;
        }
    }
}
