using System;
using System.Collections.Generic;
using System.Linq;
using WYGAS;

namespace Demo.Scripts
{
    public class DamageGameplayEffectSpec : GameplayEffectSpec
    {
        public float damageBase;
        
        public AttributeName healthAttr;
        
        public List<IDamageRule> damageRules;

        public DamageGameplayEffectSpec()
        {
            calculations.Add(new DamageToHealthCalculation());
        }
    }
    
    public class DamageToHealthCalculation : GameplayEffectCalculation
    {
        
        public override List<Modifier> Execute(GameplayEffectSpec gameplayEffect)
        {
            
            DamageGameplayEffectSpec damageEffect = gameplayEffect as DamageGameplayEffectSpec;

            if (damageEffect == null)
            {
                return new List<Modifier>();
            }

            DamageContext damageContext;
            damageContext.sourceAS = gameplayEffect.sourceAS;
            damageContext.targetAS = gameplayEffect.targetAS;
            damageContext.damageBase = damageEffect.damageBase;

            float rawDamage = damageEffect.damageBase;

            damageEffect.damageRules.ForEach((damageRule) =>
            {
                if(damageRule.CanApply(damageContext)) 
                {
                    rawDamage += damageRule.Apply(damageContext);
                }
            });
            
            float finalDamage = DamageReduction(rawDamage, gameplayEffect);
            
            return DamageToHealthModifier(finalDamage, damageEffect, damageEffect.healthAttr);
        }

        public virtual float DamageReduction(float rawDamage, GameplayEffectSpec effectSpec)
        {
            var armor = effectSpec.targetAS.GetAttributeValue("ATTR_Armor");
            var damageReduction = (0.06f * armor) / (1f + 0.06f * armor);
            return rawDamage * (1f - damageReduction);
        }

        public static List<Modifier> DamageToHealthModifier(float damage, GameplayEffectSpec effectSpec, AttributeName healthAttr)
        {
            List<Modifier> modifiers = new List<Modifier>();
            
            var healthModifier = new Modifier
            {
                attributeName = healthAttr,
                IsAggregated = false,
                modifierOperator = ModifierOperator.Add,
                magnitude = new GameplayEffectMagnitude
                {
                    type = MagnitudeType.Constant,
                    constantValue = damage * -1f
                }
            };
            modifiers.Add(healthModifier);
            return modifiers;
        }
    }
}