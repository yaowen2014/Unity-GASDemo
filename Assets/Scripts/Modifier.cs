using System;
using UnityEngine;

namespace WYGAS
{
    [Serializable]

    public enum ModifierOperator
    {
        Add,
        Multiply,
        Override
    }
    
    [Serializable]
    public class Modifier
    {
        public float GetValue()
        {
            if (magnitude.type == MagnitudeType.Constant)
            {
                return magnitude.constantValue;
            } 

            return 0;
        }

        public Modifier ToConstantModifier(GameplayEffectSpec spec)
        {
            if (magnitude.type == MagnitudeType.Constant) return this;

            float constantValue = 0;
            
            if (magnitude.type == MagnitudeType.SetByCaller)
            {
                constantValue = spec.GetSetByCallerValue(magnitude.setByCallerKey);

            } else if (magnitude.type == MagnitudeType.AttributeBased)
            {
                constantValue = spec.sourceAS.GetAttributeValue(magnitude.attributeName) * magnitude.attributeCoefficient;
            }
            return new Modifier()
            {
                attributeName = attributeName,
                modifierOperator = modifierOperator,
                IsAggregated = IsAggregated,
                magnitude = new GameplayEffectMagnitude()
                {
                    type = MagnitudeType.Constant,
                    constantValue = constantValue,
                }
            };
        }

        public bool IsValueSetByCaller()
        {
            return magnitude.type == MagnitudeType.SetByCaller;
        }
        
        [SerializeReference] public AttributeName attributeName;

        public ModifierOperator modifierOperator;

        public GameplayEffectMagnitude magnitude;

        // 描述是否参与属性公式
        // True => 参与属性公式，移除回滚（例如冰冻，修改速度属性，但是冰冻效果结束后回滚）
        // False => 驱动属性变化，不回滚（例如中毒，直接修改 Damage）
        public bool IsAggregated;
    }
}