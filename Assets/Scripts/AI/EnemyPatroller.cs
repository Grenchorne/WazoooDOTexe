using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Adhaesii.WazoooDOTexe.Audio;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    public class EnemyPatroller : SerializedMonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 1f;

        [SerializeField]
        private float raycastDistance = 1f;

        [SerializeField]
        private LayerMask blockerLayerMask;

        [SerializeField]
        private bool avoidDrops = true;

        private HealthController HealthController { get; set; }
        private HealthAudio HealthAudio { get; set; }

        private SpriteRenderer SpriteRenderer { get; set; }

        private Transform Transform { get; set; }
        
        private int moveDirection = 1;

        private void Awake()
        {
            HealthController = GetComponent<HealthController>();
            HealthAudio = GetComponentInChildren<HealthAudio>();
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Transform = transform;
        }

        private void OnEnable()
        {
            HealthController.OnDamage += HealthAudio.PlayHit; 
            HealthController.OnDie += HealthAudio.PlayDie;
            HealthController.OnDie += () => gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            HealthController.OnDamage -= HealthAudio.PlayHit; 
            HealthController.OnDie -= HealthAudio.PlayDie;
        }

        private void Update()
        {
            // Blocker immediately to the right
            if (Physics2D.Raycast(transform.position, Vector2.right * moveDirection,
                    raycastDistance, blockerLayerMask))
            {
                flip_();
                return;
            }
            
            // nothing right, down
            if (avoidDrops && !Physics2D.Raycast(transform.position, new Vector2(moveDirection, -1),
                raycastDistance, blockerLayerMask))
            {
                flip_();
                return;
            }
            

            void flip_()
            {
                moveDirection *= -1;
                SpriteRenderer.flipX = !SpriteRenderer.flipX;
            }
        }

        private void FixedUpdate()
        {
            Vector3 position = Transform.position;
            Transform.position = new Vector3(moveDirection * (moveSpeed * Time.fixedDeltaTime) + position.x, position.y, position.z); 
        }
    }
}
