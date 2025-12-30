using System;
using System.Collections.Generic;
using UnityEngine;
using WYGAS.SO;

namespace WYGAS
{
    public class AbilitySystemComponent : MonoBehaviour
    {
        public AbilitySystem abilitySystem =  new AbilitySystem();

        public List<AttributeName> attributeDefs;

        public void Awake()
        {
            var unityTimeSource = new UnityTimeSource();
            abilitySystem.Initialize(attributeDefs, unityTimeSource, gameObject);
        }

        public void Update()
        {
            abilitySystem.Tick();
        }

        public void GrantAbility(GameplayAbilitySpec abilitySpec)
        {
            abilitySystem.GrantAbility(abilitySpec);
        }

        public bool TryActivateAbility(GameplayAbilitySpec abilitySpec) 
        {
            return abilitySystem.TryActivateAbility(abilitySpec);
        }
        
        public void CancelAbilitiesByTag(GameplayTag tag)
        {
            abilitySystem.CancelAbilitiesByTag(tag);
        }
        
        public List<GameplayAbilitySpec> GetAbilities()
        {
            return abilitySystem.GetAbilities();
        }

        public void ApplyGameplayEffect(GameplayEffectSpec effectSpec)
        {
            abilitySystem.ApplyGameplayEffect(effectSpec);
        }

        public float GetAttributeValue(AttributeName attributeName)
        {
            return abilitySystem.GetAttributeValue(attributeName);
        }

        public float GetAttributeValue(string attributeNameStr)
        {
            return abilitySystem.GetAttributeValue(attributeNameStr);
        }

        public Attribute GetAttribute(AttributeName attributeName)
        {
            return abilitySystem.GetAttribute(attributeName);
        }

        public Attribute GetAttribute(string attributeNameStr)
        {
            return abilitySystem.GetAttribute(attributeNameStr);
        }

        public List<GameplayEffect> GetActiveEffects()
        {
            return abilitySystem.appliedGameplayEffects;
        }

        public void RemoveGameplayEffectsByTags(GameplayTagContainer tags)
        {
            abilitySystem.RemoveGameplayEffectsByTags(tags);
        }

        public List<GameplayTag> GetGrantedTags()
        {
            return abilitySystem.GetTags();
        }
    }
}