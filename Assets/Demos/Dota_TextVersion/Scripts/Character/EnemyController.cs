using System;
using UnityEngine;
using WYGAS;
using WYGAS.SO;

namespace Demo.Scripts
{
    public class EnemyController : MonoBehaviour
    {
        public AbilitySystemComponent asc { get; private set; }

        public GameplayEffectDefinition defaultAttributeValues;
        public GameplayEffectDefinition defaultPrimaryAttributesScalingEffects;
        public GameplayEffectDefinition defaultHealthRegenerationEffect;

        private void Awake()
        {
            asc = GetComponent<AbilitySystemComponent>();
        }

        private void Start()
        {
            InitializeDefaultAttributes();
            InitializeAttributeClamps();
            InitializePrimaryAttributesScaling();
            InitializePrimaryAttributesPeriodChange();
        }

        public void InitializeDefaultAttributes()
        {
            asc.ApplyGameplayEffect(defaultAttributeValues.CreateSpecInternal());

            var maxHealthAttr = asc.GetAttribute("ATTR_MaxHealth");
            maxHealthAttr.OnPostValueChanged += (float oldValue, float newValue) =>
            {
                var healthChangeGameplayEffectSpec = new GameplayEffectSpec()
                {
                    sourceAS = asc.abilitySystem,
                    targetAS = asc.abilitySystem,
                    durationType = GameplayEffectDurationType.Instant
                };

                var maxHealthDiff = newValue - oldValue;
                var healthModifier = new Modifier()
                {
                    attributeName = asc.GetAttribute("ATTR_Health").attributeName,
                    modifierOperator = ModifierOperator.Add,
                    magnitude = new GameplayEffectMagnitude()
                    {
                        type = MagnitudeType.Constant,
                        constantValue = maxHealthDiff
                    }
                };
                healthChangeGameplayEffectSpec.modifiers.Add(healthModifier);
                asc.ApplyGameplayEffect(healthChangeGameplayEffectSpec);
            };
        }
        
        public void InitializeAttributeClamps()
        {
            var healthAttr = asc.GetAttribute("ATTR_Health");
            healthAttr.onPreBaseValueChanged += (ref float newHealth) =>
            {
                newHealth = Mathf.Clamp(newHealth, 0, asc.GetAttributeValue("ATTR_MaxHealth"));       
            };
        }

        public void InitializePrimaryAttributesScaling()
        {
            var spec = defaultPrimaryAttributesScalingEffects.CreateSpecInternal();
            spec.sourceAS = asc.abilitySystem;
            spec.targetAS = asc.abilitySystem;
            asc.ApplyGameplayEffect(spec);
        }

        public void InitializePrimaryAttributesPeriodChange()
        {
            var spec = defaultHealthRegenerationEffect.CreateSpecInternal();
            spec.sourceAS = asc.abilitySystem;
            spec.targetAS = asc.abilitySystem;
            asc.ApplyGameplayEffect(spec);
        }
    }
}