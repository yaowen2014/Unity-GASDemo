using System;
using System.Collections.Generic;
using UnityEngine;
using WYGAS.SO;

namespace WYGAS
{
    public class AbilitySystem
    {
        public GameObject owner = null;
        
        public void Initialize(List<AttributeName> attrDefs, ITimeSource timeSource, GameObject inOwner)
        {
            time = timeSource;
            attrDefs.ForEach(attrName =>
            {
                var attr = new Attribute();
                attr.attributeName = attrName;
                attr.currentValue = 0;
                attributes.Add(attr);
                attributeNamesMap[attrName.name] = attrName;
                attr.OnPostValueChanged += (float oldValue, float newValue) =>
                {
                    OnAttributeChange(attr, oldValue, newValue);
                };
            });
            
            owner = inOwner;
            
            OnGameplayEffectApplied += UpdateTagsOnEffectChange;
            OnGameplayEffectRemoved += UpdateTagsOnEffectChange;
        }
        public bool TryActivateAbility(GameplayAbilitySpec abilitySpec)
        {
            if (abilitySpec == null)
            {
                return false;
            }
            
            // 检测 Ability 激活条件

            if (abilitySpec.isActive)
            {
                return false;
            }
            
            var abilityInst = (GameplayAbility) Activator.CreateInstance(abilitySpec.abilityInstanceType);
            abilityInst.spec = abilitySpec;

            if (!abilityInst.CheckCost())
            {
                return false;
            }

            if (!abilityInst.CheckCooldown())
            {
                return false;
            }
            
            // 下面开始 Commit Ability

            abilitySpec.isActive = true;
            
            abilityInst.ApplyCost();
            
            abilityInst.ApplyCooldown();
            
            AbilityContext context = new AbilityContext();
            context.actorInfo = new AbilityActorInfo();
            context.actorInfo.owner = owner;
            context.actorInfo.sourceAS = this;
            
            abilityInst.Activate(context);
            
            activeAbilities.Add(abilityInst);

            return true;
        }

        public void CancelAbilitiesByTag(GameplayTag tag)
        {
            var abilitiesToCancel = activeAbilities.FindAll(ability => ability.MatchAbilityTag(tag));
            abilitiesToCancel.ForEach((ability =>
            {
                ability.EndAbility(true);
                activeAbilities.Remove(ability);
            }));
        }
        
        public List<GameplayAbilitySpec> GetAbilities()
        {
            return grantedAbilities;
        }

        public void GrantAbility(GameplayAbilitySpec abilitySpec)
        {
            abilitySpec.sourceAS = this;
            grantedAbilities.Add(abilitySpec);
        }
        
        public float GetAttributeValue(AttributeName attrName)
        {
            var targetAttr = GetAttribute(attrName);

            if (targetAttr == null)
            {
                Debug.LogWarning(($"No Attribute Named ${attrName}"));
                return 0;
            }
            return targetAttr.GetValue();
        }

        public float GetAttributeValue(string attributeNameStr)
        {
            var attrName = attributeNamesMap[attributeNameStr];
            return GetAttributeValue(attrName);
        }

        public Attribute GetAttribute(AttributeName attrName)
        {
            return attributes.Find(attribute => attribute.attributeName == attrName);
        }

        public Attribute GetAttribute(string attributeNameStr)
        {
            var attrName = attributeNamesMap[attributeNameStr];
            return GetAttribute(attrName);
        }

        public void ApplyGameplayEffect(GameplayEffectSpec effectSpec)
        {
            switch (effectSpec.durationType)
            {
                case GameplayEffectDurationType.Infinite:
                case GameplayEffectDurationType.Duration:
                    ApplyAndGenerateDurationGameplayEffect(effectSpec);
                    break;
                case GameplayEffectDurationType.Instant:
                    ApplyInstantGameplayEffect(effectSpec);
                    break;
            }
        }
        
        public ITimeSource time;
        
        public List<Attribute> attributes = new List<Attribute>();

        public List<GameplayEffect> appliedGameplayEffects = new List<GameplayEffect>();

        public Action<GameplayEffect> OnGameplayEffectApplied, OnGameplayEffectRemoved;
        
        // 描述当前游戏对象所包含的 GameplayTag，例如中毒，加速等等
        public GameplayTagContainer tags = new GameplayTagContainer();
        
        private List<GameplayAbilitySpec> grantedAbilities = new List<GameplayAbilitySpec>();
        
        private List<GameplayAbility> activeAbilities = new List<GameplayAbility>();
        
        private Dictionary<string, AttributeName> attributeNamesMap = new Dictionary<string, AttributeName>();

        private bool needToRefreshAggregator = false;

        private void OnAttributeChange(Attribute attr, float oldValue, float newValue)
        {
            needToRefreshAggregator = true;
        }
        public bool HasTag(GameplayTag tag)
        {
            return tags.HasTag(tag);
        }

        public List<GameplayTag> GetTags()
        {
            return tags.ToList();
        }

        private void UpdateTagsOnEffectChange(GameplayEffect changedGameplayEffect)
        {
            tags.Clear();
            var newTags = new List<GameplayTag>();
            appliedGameplayEffects.ForEach(gameplayEffect =>
            {
                var effectGrantedTags = gameplayEffect.spec.grantedTags.ToList();
                effectGrantedTags.ForEach(tag => newTags.Add(tag));
            });
            
            tags.AddRange(newTags);
        }

        public List<Attribute> GetAttributes()
        {
            return attributes;
        }

        public void Tick()
        {
            TryApplyPeriodGameplayEffects();
            UpdateDurationGameplayEffectsExpirations();
            if (needToRefreshAggregator)
            {
                RefreshAttributesAggregator();
                needToRefreshAggregator = false;
            }

            TickActiveAbilities();
        }

        private void TickActiveAbilities()
        {
            activeAbilities.ForEach(activeAbility =>
            {
                activeAbility.tasks.ForEach(abilityTask =>
                {
                    if (abilityTask.IsActive())
                    {
                        abilityTask.Tick(time.deltaTime);
                    }
                });
            });
        }

        private void UpdateDurationGameplayEffectsExpirations()
        {
            List<GameplayEffect> expiredEffects = new List<GameplayEffect>();
            appliedGameplayEffects.ForEach(effect =>
            {
                if (effect.IsExpired(time.now))
                {
                    expiredEffects.Add(effect);
                }
            });
            
            expiredEffects.ForEach(effect => RemoveGameplayEffect(effect));

            if (expiredEffects.Count > 0)
            {
                needToRefreshAggregator = true;
            }
        }

        // 重新计算所有非 Instant 的 GameplayEffect 产生的 Attribute Aggregator 值
        private void RefreshAttributesAggregator()
        {
            attributes.ForEach(attribute => attribute.ClearModification());
            appliedGameplayEffects.ForEach(effect =>
            {
                // 周期性效果不在这里计算
                if (effect.spec.IsPeriodicEffect()) return;
                ApplyBasicModifiersInGameplayEffect(effect.spec);
            });
            attributes.ForEach(attribute => attribute.RefreshValue());
        }
        
        private void ApplyInstantGameplayEffect(GameplayEffectSpec effectSpec)
        {
            ApplyBasicModifiersInGameplayEffect(effectSpec);
        }
        
        private void ApplyAndGenerateDurationGameplayEffect(GameplayEffectSpec effectSpec)
        {
            ApplyBasicModifiersInGameplayEffect(effectSpec);
            var ge = GenerateDurationalGameplayEffectInstance(effectSpec);
            OnGameplayEffectApplied?.Invoke(ge);
        }

        private GameplayEffect GenerateDurationalGameplayEffectInstance(GameplayEffectSpec effectSpec)
        {
            var ge = effectSpec.CreateGameplayEffectInstance();
            appliedGameplayEffects.Add(ge);
            ge.effectStartTime = time.now;
            if (effectSpec.period != 0)
            {
                ge.lastPeriodTriggerTime = time.now;
            }
            return ge;
        }

        private void ApplyBasicModifiersInGameplayEffect(GameplayEffectSpec effectSpec)
        {
            var modifiersToProcess = new List<Modifier>();
            modifiersToProcess.AddRange(effectSpec.GetBasicModifiers());
            effectSpec.calculations.ForEach(calculation => modifiersToProcess.AddRange(calculation.Execute(effectSpec)));
            
            var attributes = GetAttributes();
            attributes.ForEach(attribute =>
            {
                modifiersToProcess.ForEach(modifier =>
                {
                    if (attribute.attributeName == modifier.attributeName)
                    {
                        attribute.ApplyModifier(modifier);
                    }
                });
            });
        }

        private void RemoveGameplayEffect(GameplayEffect ge)
        {
            if (!appliedGameplayEffects.Contains(ge))
            {
                return;
            }

            appliedGameplayEffects.Remove(ge);
            OnGameplayEffectRemoved?.Invoke(ge);
        }

        public void RemoveGameplayEffectsByTags(GameplayTagContainer tagsToRemove)
        {
            tagsToRemove.ToList().ForEach(RemoveGameplayEffectByTag);
        }

        public void RemoveGameplayEffectByTag(GameplayTag tagToRemove)
        {
            var effectsToRemove = appliedGameplayEffects.FindAll(appliedGameplayEffect => appliedGameplayEffect.spec.MatchEffectTags(tagToRemove));
            effectsToRemove.ForEach(RemoveGameplayEffect);
        }

        private void TryApplyPeriodGameplayEffects()
        {
            appliedGameplayEffects.ForEach(gameEffect =>
            {
                if (gameEffect.ShouldApplyPeriod(time.now))
                {
                    ApplyBasicModifiersInGameplayEffect(gameEffect.spec);
                    gameEffect.lastPeriodTriggerTime = time.now;
                }
            });
        }

        public GameplayAbilitySpec GetAbilitySpecByHandle(GameplayAbilitySpecHandle handle)
        {
            return grantedAbilities.Find(abilitySpec => abilitySpec.handle == handle);
        }
    }
}