using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

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
        private float t_jumpCooldown;

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
            _rigidbody2D.velocity = new Vector2(move * moveSpeed, _rigidbody2D.velocity.y); 
                
            // Grounded && !Input? StopMat
            _rigidbody2D.sharedMaterial = _groundCheck.IsGrounded && Mathf.Abs(move) <= 0 ? stopMat: goMat;


            if ((!(move > 0) || FacingRight) && (!(move < 0) || !FacingRight)) return;
            Vector3 localScale = _transform.localScale;
            localScale = new Vector3(localScale.x * -1, localScale.y);
            _transform.localScale = localScale;
        }

        public void Jump()
        {
            if(t_jumpCooldown > 0)
                return;
            
            if(_groundCheck.IsGrounded)
                jump_();
            else if (isHovering)
            {
                _fuelHandler.DepletePip();
                jump_();
            }

            void jump_()
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpSpeed);
                t_jumpCooldown = jumpCooldown;
                isHovering = false;
            }
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
            t_jumpCooldown -= Time.deltaTime;
            _rigidbody2D.isKinematic = isHovering;
        }
    }
}
