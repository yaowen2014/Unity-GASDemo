using System;

namespace WYGAS
{
    public class GameplayAbilitySpec
    {
        public GameplayAbilitySpecHandle handle;
        
        public bool isActive;

        public AbilitySystem sourceAS;
        
        public Type abilityInstanceType;

        public GameplayEffectSpec costEffectSpec;

        public GameplayEffectSpec cooldownEffectSpec;

        public GameplayTag cooldownTag;
        
        public GameplayTagContainer abilityTags = new GameplayTagContainer();
        
        public GameplayAbility CreateInstance()
        {
            var ability = (GameplayAbility)Activator.CreateInstance(abilityInstanceType);

            return ability;
        }
    }
    
    [Serializable]
    public struct GameplayAbilitySpecHandle: IEquatable<GameplayAbilitySpecHandle>
    {
        public int Value;

        public bool IsValid => Value > 0;

        public bool Equals(GameplayAbilitySpecHandle other)
            => Value == other.Value;

        public override bool Equals(object obj)
            => obj is GameplayAbilitySpecHandle other && Equals(other);

        public override int GetHashCode()
            => Value;

        public static bool operator ==(GameplayAbilitySpecHandle a, GameplayAbilitySpecHandle b)
            => a.Value == b.Value;

        public static bool operator !=(GameplayAbilitySpecHandle a, GameplayAbilitySpecHandle b)
            => a.Value != b.Value;

        public override string ToString()
            => $"AbilitySpecHandle({Value})";
    }
}