using System.Collections.Generic;
using UnityEngine;
using WYGAS.SO;

namespace Demo.Scripts.SO.Projectiles
{
    [CreateAssetMenu(
        fileName = "NewExplosion",
        menuName = "Demo/Projectile/Explosion"
    )]
    public class ExplosionProjectile : ScriptableObject
    {
        public List<GameplayEffectDefinition> onHitEffects;
        
        public float explosionRange;
    }
}