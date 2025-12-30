using System.Collections.Generic;
using UnityEngine;
using WYGAS.SO;

namespace Demo.Scripts.SO.Projectiles
{
    [CreateAssetMenu(
        fileName = "NewBullet",
        menuName = "Demo/Projectile/Bullet"
    )]
    public class BulletProjectile : ProjectileDef
    {
        public List<GameplayEffectDefinition> onHitEffects;
    }
}