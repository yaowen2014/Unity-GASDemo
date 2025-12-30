using System;
using System.Collections.Generic;
using UnityEngine;
using WYGAS;
using WYGAS.SO;

namespace Demo.Scripts.SO
{
    [CreateAssetMenu(
        fileName = "GE_DamageGameplayEffectDef",
        menuName = "Demo/Effects/Damage GameplayEffect Def"
    )]
    public class DamageGameplayEffectDefinition : BasicGameplayEffectDefinition
    {
        public float damageBase = 0;

        public AttributeName healthAttr;

        public List<IDamageRule> damageRules = new List<IDamageRule>();

        public override Type EffectInstanceType => typeof(DamageGameplayEffectSpec);

        public override GameplayEffectSpec CreateSpecInternal()
        {
            var ge = base.CreateSpecInternal() as DamageGameplayEffectSpec;
            
            ge.healthAttr = healthAttr;
            ge.damageBase = damageBase;
            ge.damageRules = damageRules;
            
            return ge;
        }
    }
}