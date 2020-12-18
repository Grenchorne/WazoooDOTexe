using Adhaesii.WazoooDOTexe.Pooling;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Player
{
    public class PlayerRangedController : SerializedMonoBehaviour
    {
        [SerializeField]
        private float projectileSpeed = 4f;

        [SerializeField, Required]
        private Transform projectileOrigin;

        [SerializeField]
        private float cooldown = 0.7f;
        private float t_cooldown;

        [SerializeField]
        private bool projectileIsKinematic = true;

        [SerializeField]
        private bool constantVelocity = true;
        
        private ProjectilePool projectilePool;
        
        private void Awake() => projectilePool = ProjectilePool.Instance;

        private void Update() => t_cooldown -= Time.deltaTime;

        public void Shoot(bool facingRight)
        {
            if(t_cooldown > 0)
                return;

            t_cooldown = cooldown;
            projectilePool.Get(projectileOrigin.position,
                facingRight ? Vector2.right * projectileSpeed : Vector2.left * projectileSpeed, projectileIsKinematic,
                constantVelocity);
        }
    }
}