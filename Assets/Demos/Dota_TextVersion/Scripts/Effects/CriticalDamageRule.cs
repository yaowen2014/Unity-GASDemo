using UnityEngine;
using System;
namespace Demo.Scripts.SO
{
    public class CriticalDamageRule : IDamageRule
    {
        public float criticalChance = 0;
        public float criticalMultiplier = 0;
        
        public bool CanApply(DamageContext context)
        {
            return true;
        }

        public float Apply(DamageContext context)
        {
            if (criticalChance < 0 || criticalChance > 1)
            {
                return 0;
            }
            
            var roll = UnityEngine.Random.value;
            if (roll < criticalChance)
            {
                return context.damageBase * criticalMultiplier;
            }

            return 0;
        }
     }
}