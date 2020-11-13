using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Adhaesii.WazoooDOTexe
{
    public class CharacterMover : SerializedMonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpSpeed = 15f;

        [SerializeField] private PhysicsMaterial2D stopMat;
        [SerializeField] private PhysicsMaterial2D goMat;

        private GroundCheck _groundCheck;
        private Rigidbody2D _rigidbody2D;

        private SwordController sword;
        private Transform _transform;

        private FuelHandler _fuelHandler;
        private bool isHovering;

        private bool FacingRight => _transform.localScale.x > 0;

        [SerializeField] private float jumpCooldown = 0.2f;

        
        // jump variables
        enum JumpState{Pressed, Held, None}

        private JumpState jumpState = JumpState.None;
        private float t_jumpCooldown;
        
        //private bool wantsToMove;
        private bool jumpHeld;

        [SerializeField, Tooltip("For how long can the player hold the jump button?")]
        private float jumpTime = 0.3f;
        private float t_jump;
        private float move;

        private void Awake()
        {
            _groundCheck = GetComponentInChildren<GroundCheck>();
            
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _transform = transform;

            sword = GetComponentInChildren<SwordController>();

            _fuelHandler = GetComponent<FuelHandler>();
        }

        private void Start()
        {
            _fuelHandler.OnDeplete += EndHover;
        }

        public void Move(float move)
        {
            this.move = move;

            if ((!(move > 0) || FacingRight) && (!(move < 0) || !FacingRight)) return;
            Vector3 localScale = _transform.localScale;
            localScale = new Vector3(localScale.x * -1, localScale.y);
            _transform.localScale = localScale;
        }

        public void Jump()
        {
            //jumpState = JumpState.Pressed;
        }

        public void Melee() => sword.Attack();

        public void Hover()
        {
            if (!(_fuelHandler.Fuel > 0)) return;
            isHovering = true;
            _fuelHandler.EnableDepletion();
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
            isHovering = true;
        }

        public void EndHover()
        {
            isHovering = false;
            _fuelHandler.DisableDepletion();
        }

        private void Update()
        {
            bool jumpPressed = Keyboard.current.spaceKey.isPressed;
            if (!jumpPressed)
                jumpState = JumpState.None;
            
            float deltaTime = Time.deltaTime; // capture deltatime

            Vector2 velocity = _rigidbody2D.velocity;

            // Hovering?
            _rigidbody2D.gravityScale = isHovering ? 0 : 1;

            //all the move code
            velocity.x = (Mathf.Abs(move) > 0) ? move * moveSpeed : 0;
            
            switch (jumpState)
            {
                case JumpState.Pressed:

                    jumpState = JumpState.Held;
                    // am i grounded or hovering?
                    if (_groundCheck.IsGrounded || isHovering)
                    {
                        // cooldown not met
                        if (t_jumpCooldown > 0)
                        {
                            jumpState = JumpState.None;
                            break;
                        }

                        // set the jump time to default
                        t_jump = jumpTime;
                        t_jumpCooldown = jumpCooldown;

                        // deplete fuel if hovering
                        if (isHovering)
                            _fuelHandler.DepletePip();

                    }
                    else
                        break;

                    goto case JumpState.Held;

                case JumpState.Held:
                    // No more time for jump? cancel it out
                    if (t_jump < 0)
                    {
                        jumpState = JumpState.None;
                        break;
                    }

                    // cancel out the hover
                    isHovering = false;

                    velocity.y = jumpSpeed;
                    t_jump -= deltaTime;
                    break;
                case JumpState.None:
                    if (jumpPressed)
                    {
                        jumpState = JumpState.Pressed;
                        goto case JumpState.Pressed;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            t_jumpCooldown -= deltaTime;

            // apply the velocity back to the rb2d
            _rigidbody2D.velocity = velocity;
        }
    }
}
