using System;
using System.Collections;
using System.Collections.Generic;
using Adhaesii.WazoooDOTexe.Audio;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Adhaesii.WazoooDOTexe
{
    public class HealthController : SerializedMonoBehaviour, IReceiveDamage
    {
        [SerializeField]
        private int startingHealth = 4;

        [SerializeField]
        private int _maxHealth = 4;
        public int MaxHealth => _maxHealth;

        [SerializeField]
        private float cooldownBetweenHits = 0.5f;

        [SerializeField]
        private float damageBounceForce = 15f;

        [SerializeField]
        private ForceMode2D damageBounceForceMode = ForceMode2D.Impulse;

        [SerializeField]
        private bool damageLocksMovement = true;

        [SerializeField]
        private float damageBounceMovementDisableCooldown = 0.075f;

        public event Action OnDamage;
        public event Action OnDie;
        public event Action OnUpdateHealth; 
        
        private int _health;
        public int Health => _health;

        private PlayerMover playerMover;
        private Rigidbody2D rigidBody2D;

        private float t_cooldown;

        private void Awake()
        {
            playerMover = GetComponent<PlayerMover>();
            rigidBody2D = GetComponent<Rigidbody2D>();
        }

        private void Start() => _health = startingHealth;

        private void Update() => t_cooldown -= Time.deltaTime;

        public void Damage(GameObject source)
        {
            if(t_cooldown > 0)
                return;

            _health--;
            
            OnUpdateHealth?.Invoke();
            
            t_cooldown = cooldownBetweenHits;

            if (_health <= 0)
            {
                _health = 0;
                OnDie?.Invoke();
                return;
            }
            
            OnDamage?.Invoke();
            
            // temporarily lock out of movement
            StartCoroutine(_());
            
            IEnumerator _()
            {
                if(damageLocksMovement)
                    playerMover.SetMovement(false);
                
                rigidBody2D.velocity = Vector2.zero;
                bool sourceIsLeft = transform.position.x > source.transform.position.x;
                rigidBody2D.AddForce((sourceIsLeft ? Vector2.right : Vector2.left) * damageBounceForce,
                    damageBounceForceMode);
                
                yield return new WaitForSeconds(damageBounceMovementDisableCooldown);
                
                if(damageLocksMovement)
                    playerMover.SetMovement(true);
            }
        }

        public void FullHeal()
        {
            _health = _maxHealth;
            OnUpdateHealth?.Invoke();
        }
    }
}
