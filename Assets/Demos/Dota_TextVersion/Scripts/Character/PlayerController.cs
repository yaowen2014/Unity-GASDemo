using Demo.Scripts;
using UnityEngine;
using WYGAS;
using WYGAS.SO;

namespace Demo
{
    public class PlayerController : MonoBehaviour
    {
        public AbilitySystemComponent asc { get; private set; }
        
        public GameplayEffectDefinition defaultAttributeValues;
        
        public ItemData mockedItemToEquip;
        
        private void Awake()
        {
            asc = GetComponent<AbilitySystemComponent>();
        }

        private void Start()
        {
            asc.ApplyGameplayEffect(defaultAttributeValues.CreateSpecInternal());
            InitializeAttributeClamps();
            InitializeDefaultAttributes();
        }

        public void InitializeAttributeClamps()
        {
            var healthAttr = asc.GetAttribute("ATTR_Health");
            healthAttr.onPreValueChanged += (ref float newHealth) =>
            {
                newHealth = Mathf.Clamp(newHealth, 0, asc.GetAttributeValue("ATTR_MaxHealth"));       
            };
        }
        
        public void InitializeDefaultAttributes()
        {
            asc.ApplyGameplayEffect(defaultAttributeValues.CreateSpecInternal());
        }
        
        public void OnJump()
        {
            asc.TryActivateAbility(asc.GetAbilities()[0]);
        }

        public void OnTestEquip()
        {
            Debug.Log("Test Equip");
            Equip(mockedItemToEquip);
        }
        
        public void Equip(ItemData item)
        {
            item.itemConfig.equipEffects.ForEach(effectDef =>
            {
                var spec = effectDef.CreateSpecInternal();
                spec.SetSetByCallerValue("AttackFromEquip", 10);
                asc.ApplyGameplayEffect(spec);
            });
            item.itemConfig.abilities.ForEach(abilityDef =>
            {
                Debug.Log($"Grant Ability: {abilityDef.GetType().Name}");
                asc.GrantAbility(abilityDef.CreateSpecInternal());
            });
        }
    }
}