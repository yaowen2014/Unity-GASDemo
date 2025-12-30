using System;
using WYGAS;

namespace Demos.GASTanks.Scripts.Abilities
{
    public class AbilityTask_RepeatedTask : GameplayAbilityTask
    {
        public Action onTrigger;
        
        private float interval;
        private float elapsed;
        
        public static AbilityTask_RepeatedTask Create(GameplayAbility ability, float interval)
        {
            var task = ability.CreateTask<AbilityTask_RepeatedTask>();
            task.interval = interval;
            return task;
        }

        protected override void OnActivate()
        {
            elapsed = 0;
        }

        public override void Tick(float deltaTime)
        {
            elapsed += deltaTime;

            if (elapsed >= interval)
            {
                elapsed -= interval;
                onTrigger?.Invoke();
            }
        }
    }
}