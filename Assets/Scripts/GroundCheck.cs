using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    [RequireComponent(typeof(Collider2D))]
    public class GroundCheck : SerializedMonoBehaviour
    {
        [SerializeField] private LayerMask groundMask;
        private List<Collider2D> collisions = new List<Collider2D>();

        [ShowInInspector] private int CollCount => collisions.Count; 
            
        public bool IsGrounded => collisions.Count > 0;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (groundMask == (groundMask | 1 << other.gameObject.layer) && !collisions.Contains(other))
                collisions.Add(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (groundMask == (groundMask | 1 << other.gameObject.layer) && collisions.Contains(other))
                collisions.Remove(other);
        }
    }
}
