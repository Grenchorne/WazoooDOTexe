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
        
        public bool IsHovering { get; private set; }

        private Rigidbody2D RigidBody2D { get; set; }
        private Transform Transform { get; set; }

        private JumpProcessor jumpProcessor;

        private Vector2 velocity;

        private void Awake()
        {
            RigidBody2D = GetComponent<Rigidbody2D>();
            Transform = transform;
            jumpProcessor = new JumpProcessor(jumpSettings, GetComponentInChildren<GroundCheck>(),
                GetComponent<FuelHandler>());
        }

        private void Update()
        {
            jumpProcessor.Tick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            // Get initial velocity - use our prescribed value if jumping or hovering
            if (!jumpProcessor.IsJumping && !IsHovering)
                velocity.y = RigidBody2D.velocity.y;

            // Disable gravity if hovering
            RigidBody2D.gravityScale = IsHovering ? 0 : 1;

            // Apply movement
            RigidBody2D.velocity = velocity;
            
            // Reset movement variables
            velocity = Vector2.zero;
            IsHovering = false;
        }

        public void Move(float move)
        {
            if (Mathf.Approximately(move, 0)) return;
            
            SetFacing(move);

            velocity.x = move * moveSpeed;
        }

        public void Jump()
        {
            if (!jumpProcessor.TryJump(IsHovering)) return;
            velocity.y = jumpProcessor.JumpVelocity;
        }
        
        public void CancelJump()
        {
            if(jumpProcessor.CancelJump(RigidBody2D.velocity.y))
                RigidBody2D.velocity = new Vector2(RigidBody2D.velocity.x, 0f);
        }

        public void ReleaseJump()
        {
            jumpProcessor.ReleaseJump();
        }

        public void Hover()
        {
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
