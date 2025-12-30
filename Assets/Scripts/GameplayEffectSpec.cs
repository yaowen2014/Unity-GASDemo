using System.Collections.Generic;

namespace WYGAS
{
    
    public enum GameplayEffectDurationType
    {
        Instant,
        Infinite,
        Duration
    }
    
    public class GameplayEffectSpec
    {
        public string name;

        public GameplayEffectDurationType durationType;

        // 生成 GameplayEffect 的源技能系统
        public AbilitySystem sourceAS;
        
        // 应用 GameplayEffect 的目标技能系统
        public AbilitySystem targetAS;

        #region DURATION_HANDLE

        public float duration = 0f;

        public float period = 0f;
        
        #endregion
        
        #region TAGS_HANDLE
        
        // 被应用该 effect 的 asc 会附上的 tag，例如中毒，冰冻
        public GameplayTagContainer grantedTags = new GameplayTagContainer();
        
        // 描述该 effect 的 tag，例如弱驱散，负面效果
        public GameplayTagContainer effectTags = new GameplayTagContainer();
        
        #endregion

        #region MODIFIERS_HANDLE
        
        public List<Modifier> modifiers = new List<Modifier>();

        public List<GameplayEffectCalculation> calculations = new List<GameplayEffectCalculation>();
        
        public Dictionary<string, float> setByCallerValues = new Dictionary<string, float>();
        
        #endregion
        
        public GameplayEffectSpec() {}

        public GameplayEffect CreateGameplayEffectInstance()
        {
            return new GameplayEffect(this);
        }

        public List<Modifier> GetBasicModifiers()
        {
            var basicModifiers = new List<Modifier>();
            modifiers.ForEach(modifier =>
            {
                basicModifiers.Add(modifier.ToConstantModifier(this));
            });
            return basicModifiers;
        }

        public float GetSetByCallerValue(string key)
        {
            return setByCallerValues.GetValueOrDefault(key, 0f);
        }

        public void SetSetByCallerValue(string key, float value)
        {
            setByCallerValues[key] = value;
        }

        public bool CanApplyGameplayEffect()
        {
            return true;
        }

        public bool IsPeriodicEffect()
        {
            return period > 0f;
        }

        public bool MatchEffectTags(GameplayTag effectTagToMatch)
        {
            return effectTags.MatchTag(effectTagToMatch);
        }
    }
}