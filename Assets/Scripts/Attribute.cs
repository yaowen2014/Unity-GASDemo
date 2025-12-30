using System;

namespace WYGAS
{
    public class Attribute
    {
        public AttributeName attributeName;
        
        // CurrentValue是这个属性的最终值，currentValue = (baseValue + modification.addValue) * (1 + modification.multiplyValue)
        public float currentValue;
        
        // 所有 Instant 的值修改理论上应该都是直接修改 baseValue
        public float baseValue;
        
        // modification 本质是 baseValue 到 currentValue 的校准值，modification 变化后，需要通过调用 RefreshValue 才会更新 currentValue
        // 所以调用 modification.clear 不会直接引起 currentValue 的变化
        public AttributeModifier modification = new AttributeModifier();
        
        public delegate void onPreValueChangedHandler(ref float newValue);
        public onPreValueChangedHandler onPreValueChanged;
        public onPreValueChangedHandler onPreBaseValueChanged;
        
        // onPostValueChanged params in order
        // - attrName: AttributeName
        // - oldValue: float
        // - newValue: float
        public Action<float, float> OnPostValueChanged;
        
        public void UpdateBaseValue(Modifier modifier)
        {
            var newBaseValue = baseValue;

            switch (modifier.modifierOperator)
            {
                case ModifierOperator.Add:
                    newBaseValue += modifier.GetValue();
                    break;
                case ModifierOperator.Multiply:
                    newBaseValue *= modifier.GetValue();
                    break;
                case ModifierOperator.Override:
                    newBaseValue = modifier.GetValue();
                    break;
            }
            
            onPreBaseValueChanged?.Invoke(ref newBaseValue);
            baseValue = newBaseValue;
        }

        public void UpdateModification(Modifier modifier)
        {
            if (modifier.attributeName != attributeName)
            {
                return;
            }
            
            modification.Apply(modifier);
        }

        public void ApplyModifier(Modifier modifier)
        {
            if (modifier.attributeName != attributeName)
            {
                return;
            }

            if (modifier.IsAggregated)
            {
                UpdateModification(modifier);
            }
            else
            {
                UpdateBaseValue(modifier);
            }

            RefreshValue();
        }

        public void RefreshValue()
        {
            var oldAttrValue = currentValue;
            var newAttrValue = CalculateCurrentValue();
            onPreValueChanged?.Invoke(ref newAttrValue);
            currentValue = newAttrValue;
            if (oldAttrValue != newAttrValue)
            {
                OnPostValueChanged?.Invoke(oldAttrValue, newAttrValue);
            }
        }

        public void ClearModification()
        {
            modification.Clear();
        }

        public float CalculateCurrentValue()
        {
            if (modification.overrideValue != 0)
            {
                return modification.overrideValue;
            }
            
            return (baseValue + modification.addValue) * (1 + modification.multiplyValue);
        }

        public float GetValue()
        {
            return currentValue;
        }

        public string GetNameStr()
        {
            return attributeName.name;
        }
    }
    
    [System.Serializable]
    public class AttributeModifier {
        public float addValue;
        public float multiplyValue;
        public float overrideValue;

        public void Apply(Modifier modifier) {
            switch (modifier.modifierOperator) {
                case ModifierOperator.Add:
                    addValue += modifier.GetValue();
                    break;
                case ModifierOperator.Multiply:
                    multiplyValue += modifier.GetValue();
                    break;
                case ModifierOperator.Override:
                    overrideValue = modifier.GetValue();
                    break;
            }
        }

        public void Clear() {
            addValue = 0;
            multiplyValue = 0;
            overrideValue = 0;
        }
    }
}
