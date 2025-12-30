using System;
using System.Collections.Generic;
using UnityEngine;

namespace WYGAS.SO
{
    
    [CreateAssetMenu(
        fileName = "GE_NewBasicGameplayEffectDef",
        menuName = "GAS/Effects/Basic GameplayEffect Def"
    )]
    public class BasicGameplayEffectDefinition : GameplayEffectDefinition
    {
        public override Type EffectInstanceType => typeof(GameplayEffectSpec);
        
        public GameplayEffectDurationType durationType;

        public float period;

        public float duration;
        
        public List<Modifier> modifiers;

        public List<GameplayEffectCalculation> calculations;

        public List<GameplayTagRef> grantedTags;
        public List<GameplayTagRef> effectTags;

        public override GameplayEffectSpec CreateSpecInternal()
        {
            var effectSpec = (GameplayEffectSpec)Activator.CreateInstance(EffectInstanceType);

            effectSpec.name = name;

            effectSpec.durationType = durationType;
            effectSpec.period = period;
            effectSpec.duration = duration;
            
            effectSpec.modifiers.AddRange(modifiers);
            effectSpec.calculations.AddRange(calculations);

            grantedTags.ForEach(grantedTag => effectSpec.grantedTags.Add(GameplayTagRegistry.Get(grantedTag.Path)));
            effectTags.ForEach(effectTag => effectSpec.effectTags.Add(GameplayTagRegistry.Get(effectTag.Path)));
            
            return effectSpec;
        }
    }
}