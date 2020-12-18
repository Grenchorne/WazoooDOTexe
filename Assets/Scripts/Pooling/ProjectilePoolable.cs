using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Adhaesii.WazoooDOTexe.Pooling
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class ProjectilePoolable : SerializedMonoBehaviour, IPoolable
    {
        public GameObject GameObject => gameObject;
        public Transform Transform => transform;
        public event Action DisableEvent;
        public void OnDisable() => DisableEvent?.Invoke();
        
        [SerializeField]
        private UnityEvent collideEvent;

        [SerializeField]
        private float lifetime = 3f;
        private float t_lifetime;
        
        private Rigidbody2D RigidBody2D { get; set; }
        private SpriteRenderer SpriteRenderer { get; set; }

        private Vector2 velocity;
        private bool constantVelocity;

        private void Awake()
        {
            RigidBody2D = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out HealthController healthController))
                healthController.Damage(gameObject);
            
            velocity = Vector2.zero;
            
            collideEvent?.Invoke();
            GameObject.SetActive(false);
        }

        public void Get(Vector2 position, Vector2 velocity, bool isKinematic = true, bool constantVelocity = true)
        {
            // HACK
            // flip facingRight because of the sprite we're using
            SpriteRenderer.flipX = velocity.x > 0;
            GameObject.SetActive(true);
            Transform.position = position;
            
            this.velocity = velocity;
            this.constantVelocity = constantVelocity;
            RigidBody2D.isKinematic = isKinematic;


            if (!constantVelocity)
                RigidBody2D.velocity = velocity;

            t_lifetime = lifetime;
        }

        private void FixedUpdate()
        {
            t_lifetime -= Time.fixedDeltaTime;
            if (t_lifetime <= 0)
            {
                gameObject.SetActive(false);
                return;
            }
            
            if (constantVelocity)
                RigidBody2D.velocity = velocity;
        }
    }
}