using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using WYGAS;

namespace Demo.Scripts.UI
{
    public class PlayerStatusHUD : MonoBehaviour
    {
        [FormerlySerializedAs("AttackAttr")] [SerializeField] private AttributeName attackAttr;
        [SerializeField] private AttributeName chargesAttr;
        [SerializeField] private AttributeName maxChargesAttr;
        
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI attackText;
        [SerializeField] private TextMeshProUGUI chargeText;
        [SerializeField] private TextMeshProUGUI tagsText;

        private AbilitySystemComponent asc;
        
        public void Bind(AbilitySystemComponent abilitySystem)
        {
            Unbind();

            asc = abilitySystem;

            // 1. 初始刷新
            RefreshAttack();
            RefreshCharges();

            // 2. 事件绑定
            asc.GetAttribute(attackAttr)
                .OnPostValueChanged += OnAttackChanged;
            
            asc.GetAttribute(chargesAttr)
                .OnPostValueChanged += OnChargesChanged;
            
            asc.GetAttribute(maxChargesAttr)
                .OnPostValueChanged += OnMaxChargesChanged;
        }
        
        private void Update()
        {
            tagsText.text = $"Tags: {GenerateAppliedGameplayEffectText()}";
        }

        public string GenerateAppliedGameplayEffectText()
        {
            var tags = asc.GetGrantedTags();
            var tagsStr = "";
            
            foreach (var tag in tags)
            {
                tagsStr += $"{tag.name},";
            }
            tagsStr = tagsStr.TrimEnd(',');
            
            return tagsStr;
        }

        private void Unbind()
        {
            if (asc == null) return;

            asc.GetAttribute(attackAttr)
                .OnPostValueChanged -= OnAttackChanged;

            asc = null;
        }

        private void OnDestroy()
        {
            Unbind();
        }

        private void RefreshAttack()
        {
            attackText.text = $"Attack: {asc.GetAttributeValue(attackAttr)}";
        }
        private void RefreshCharges()
        {
            chargeText.text = $"Charges: {asc.GetAttributeValue(chargesAttr)}/{asc.GetAttributeValue(maxChargesAttr)}";
        }

        private void OnAttackChanged(float oldValue, float newValue)
        {
            attackText.text = $"Attack: {asc.GetAttributeValue(attackAttr)}";
        }
        
        private void OnChargesChanged(float oldValue, float newValue)
        {
            chargeText.text = $"Charge: {newValue}/{asc.GetAttributeValue(maxChargesAttr)}";
        }
        
        private void OnMaxChargesChanged(float oldValue, float newValue)
        {
            chargeText.text = $"Charge: {asc.GetAttribute(chargesAttr)}/{newValue}";
        }
    }
}