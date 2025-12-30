using WYGAS;

namespace Demo.Scripts
{
    public interface IDamageRule
    {
        bool CanApply(DamageContext context);
        float Apply(DamageContext context);
    }

    public struct DamageContext
    {
        public AbilitySystem sourceAS;
        public AbilitySystem targetAS;
        public float damageBase;
    }
}