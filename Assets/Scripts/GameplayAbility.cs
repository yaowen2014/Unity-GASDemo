using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace WYGAS
{
    public abstract class GameplayAbility
    {
        public GameplayAbilitySpec spec;
        
        public List<GameplayAbilityTask> tasks = new();

        public abstract void Activate(AbilityContext context);

        public void EndAbility(bool wasCancelled)
        {
            isEnded = true;

            spec.isActive = false;
            
            foreach (var task in tasks)
            {
                task.EndTask();
            }
            tasks.Clear();
            spec.isActive = false;

            if (wasCancelled)
            {
                OnCanceled();
            }
            else
            {
                OnEnded();
            }

            // TODO: 结束所有 Task

            // TODO: 移除 Owned Tags

            // TODO: 处理冷却

            // TODO: 通知 ASC
        }
    
        public virtual bool CheckCost()
        {
            var costEffectSpec = spec.costEffectSpec;
            var abilitySystem = spec.sourceAS;
            if (costEffectSpec == null) return true;

            bool result = true;
            costEffectSpec.modifiers.ForEach(modifier =>
            {
                if (abilitySystem.GetAttributeValue(modifier.attributeName) +
                    modifier.ToConstantModifier(costEffectSpec).GetValue() < 0)
                {
                    result = false;
                }
            });
            return result;

        }

        public virtual void ApplyCost()
        {
            if (spec.costEffectSpec == null) return;
            
            spec.sourceAS.ApplyGameplayEffect(spec.costEffectSpec);
        }

        public virtual bool CheckCooldown()
        {
            if (spec.cooldownEffectSpec == null) return true;

            return !spec.sourceAS.HasTag(spec.cooldownTag);
        }

        public virtual void ApplyCooldown()
        {
            if (spec.cooldownEffectSpec == null) return;
            
            spec.sourceAS.ApplyGameplayEffect(spec.cooldownEffectSpec);
        }
        
        public T CreateTask<T>() where T : GameplayAbilityTask, new()
        {
            var task = new T();
            task.Initialize(this);
            tasks.Add(task);
            return task;
        }

        public virtual void OnEnded()
        {
            // Need To Implemented
        }

        public virtual void OnCanceled()
        {
            // Need To Implemented
        }

        public bool MatchAbilityTag(GameplayTag abilityTagToMatch)
        {
            return spec.abilityTags.MatchTag(abilityTagToMatch);
        }

        private bool isEnded = false;
    }
    
    public sealed class AbilityContext
    {
        public AbilityActorInfo actorInfo;
        public Vector3 targetPoint;
    }
    
    public sealed class AbilityActorInfo
    {
        public GameObject owner;          
        public AbilitySystem sourceAS;
    }
}