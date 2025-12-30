using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace WYGAS.SO
{
    public abstract class GameplayAbilityDefinition : ScriptableObject
    {
        public abstract Type AbilityInstanceType { get; }

        public GameplayEffectDefinition costEffectDef;
        public GameplayEffectDefinition cooldownEffectDef;
        public GameplayTagRef cooldownTag;
        public List<GameplayTagRef> abilityTags;

        public GameplayAbilitySpec CreateSpecInternal()
        {
            var abilitySpec = new GameplayAbilitySpec();

            if (costEffectDef != null)
            {
                abilitySpec.costEffectSpec = costEffectDef.CreateSpecInternal();
            }

            if (cooldownEffectDef != null)
            {
                abilitySpec.cooldownEffectSpec = cooldownEffectDef.CreateSpecInternal();
                abilitySpec.cooldownTag = GameplayTagRegistry.Get(cooldownTag.Path);
            }

            abilityTags.ForEach(abilityTag =>
            {
                abilitySpec.abilityTags.Add(GameplayTagRegistry.Get(abilityTag.Path));
            });
            abilitySpec.abilityInstanceType = AbilityInstanceType;

            return abilitySpec;
        }
    }
}