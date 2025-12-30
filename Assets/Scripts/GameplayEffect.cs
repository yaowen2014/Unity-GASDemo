using System.Collections.Generic;

namespace WYGAS
{
    public class GameplayEffect
    {
        public GameplayEffectSpec spec;

        public float lastPeriodTriggerTime = 0f;
        
        public float effectStartTime = 0;

        public GameplayEffect(GameplayEffectSpec spec)
        {
            this.spec = spec;
        }

        public bool IsExpired(float now)
        {
            if (spec.durationType == GameplayEffectDurationType.Duration)
            {
                return effectStartTime + spec.duration <= now;
            }

            return false;
        }

        public bool ShouldApplyPeriod(float now)
        {
            if (spec.period <= 0) return false;
            return lastPeriodTriggerTime + spec.period <= now;
        }
        
    }
}