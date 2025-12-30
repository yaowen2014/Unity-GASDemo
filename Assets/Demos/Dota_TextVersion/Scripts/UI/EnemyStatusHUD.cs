using System;
using TMPro;
using UnityEngine;
using WYGAS;


namespace Demo.Scripts.UI
{
    
    public class EnemyStatusHUD : MonoBehaviour
    {
        private string ATTR_Health = "ATTR_Health";
        private string ATTR_MaxHealth = "ATTR_MaxHealth";
        private string ATTR_Speed = "ATTR_Speed";
        private string ATTR_Strength = "ATTR_Strength";
        private string ATTR_Armor = "ATTR_Armor";
        private string ATTR_Agility = "ATTR_Agility";
            
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI speedText;
        [SerializeField] private TextMeshProUGUI strengthText;
        [SerializeField] private TextMeshProUGUI armorText;
        [SerializeField] private TextMeshProUGUI agilityText;
        [SerializeField] private TextMeshProUGUI appliedGameplayEffectText;
        

        private AbilitySystemComponent asc;
        
        public void Bind(AbilitySystemComponent abilitySystem)
        {
            Unbind();

            asc = abilitySystem;

            // 1. 初始刷新
            RefreshHealth();
            RefreshSpeed();
            RefreshStrength();

            // 2. 事件绑定
            asc.GetAttribute(ATTR_Health)
                .OnPostValueChanged += OnHealthChanged;
            
            asc.GetAttribute(ATTR_MaxHealth)
                .OnPostValueChanged += OnMaxHealthChanged;

            asc.GetAttribute(ATTR_Speed)
                .OnPostValueChanged += OnSpeedChanged;
            
            asc.GetAttribute(ATTR_Strength)
                .OnPostValueChanged += OnStrengthChanged;
            
            asc.GetAttribute(ATTR_Armor)
                .OnPostValueChanged += OnArmorChanged;
            
            asc.GetAttribute(ATTR_Agility)
                .OnPostValueChanged += OnAgilityChanged;
        }

        public string GenerateAppliedGameplayEffectText()
        {
            var effects = asc.GetActiveEffects();
            var effectsText = "";
            
            foreach (var effect in effects)
            {
                effectsText += $"{effect.spec.name},";
            }
            effectsText = effectsText.TrimEnd(',');
            
            return effectsText;
        }

        private void Update()
        {
            appliedGameplayEffectText.text = GenerateAppliedGameplayEffectText();
        }

        private void Unbind()
        {
            if (asc == null) return;

            asc.GetAttribute("ATTR_Health")
                .OnPostValueChanged -= OnHealthChanged;

            asc.GetAttribute("ATTR_Speed")
                .OnPostValueChanged -= OnSpeedChanged;
            
            asc.GetAttribute("ATTR_Strength")
                .OnPostValueChanged -= OnStrengthChanged;

            asc = null;
        }

        private void OnDestroy()
        {
            Unbind();
        }

        private void RefreshHealth()
        {
            healthText.text = $"HP: {asc.GetAttributeValue(ATTR_Health)} / {asc.GetAttributeValue(ATTR_MaxHealth)}";
        }

        private void RefreshSpeed()
        {
            speedText.text = $"SPD: {asc.GetAttributeValue(ATTR_Speed)}";
        }

        private void RefreshStrength()
        {
            strengthText.text = $"STR: {asc.GetAttributeValue(ATTR_Strength)}";
        }

        private void OnHealthChanged(float oldValue, float newValue)
        {
            healthText.text = $"HP: {Math.Floor(newValue)} / {asc.GetAttributeValue(ATTR_MaxHealth)}";
        }

        private void OnMaxHealthChanged(float oldValue, float newValue)
        {
            healthText.text = $"HP: {asc.GetAttributeValue(ATTR_Health)} / {newValue}";
        }

        private void OnSpeedChanged(float oldValue, float newValue)
        {
            speedText.text = $"SPD: {newValue}";
        }

        private void OnStrengthChanged(float oldValue, float newValue)
        {
            strengthText.text = $"STR: {newValue}";
        }
        
        private void OnArmorChanged(float oldValue, float newValue)
        {
            armorText.text = $"ARM: {newValue}";
        }

        private void OnAgilityChanged(float oldValue, float newValue)
        {
            agilityText.text = $"AGI: {newValue}";
        }
    }
}