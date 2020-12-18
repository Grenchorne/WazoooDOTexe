using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Pooling
{
    [RequireComponent(typeof(Pool))]
    public class ProjectilePool : PoolBase<ProjectilePool>
    {
        public ProjectilePoolable Get(Vector2 position, Vector2 velocity, bool isKinematic = true,
            bool constantVelocity = true)
        {
            ProjectilePoolable projectile = pool.Get() as ProjectilePoolable;
            
            if (projectile == null)
            {
                throw new UnityException($"could not get {typeof(ProjectilePoolable)} from {name}");
            }
            
            projectile.Get(position, velocity, isKinematic, constantVelocity);
            return projectile;
        }
    }
}