using System.Collections.Generic;
using Mirror;

namespace WYGAS
{
    public class AbilitySystemComponentMirror : NetworkBehaviour
    {
        public AbilitySystem abilitySystem =  new AbilitySystem();
        
        private int nextAbilitySpecHandle = 1;
        
        private SyncDictionary<string, float> _syncAttributes = new SyncDictionary<string, float>();
        
        public List<AttributeName> attributeDefs;

        public void Awake()
        {
            var unityTimeSource = new UnityTimeSource();
            abilitySystem.Initialize(attributeDefs, unityTimeSource, gameObject);

            if (!isClientOnly)
            {
                ClientInitializeAttributesSync();
            }
            else
            {
                ServerInitializeAttributesSync();
            }
        }

        public void ClientInitializeAttributesSync()
        {
            foreach (var attribute in abilitySystem.attributes)
            {
                _syncAttributes[attribute.GetNameStr()] = attribute.currentValue;
                _syncAttributes.OnChange += (op, key, value) =>
                {
                    var attribute = abilitySystem.GetAttribute(key);
                    attribute.baseValue = value;
                    attribute.currentValue = value;
                };
            }
        }

        public void ServerInitializeAttributesSync()
        {
            foreach (var attribute in abilitySystem.attributes)
            {
                _syncAttributes[attribute.GetNameStr()] = attribute.currentValue;
                attribute.OnPostValueChanged += (float oldValue, float newValue) =>
                {
                    _syncAttributes[attribute.GetNameStr()] = newValue;
                };
            }
        }

        public void Update()
        {
            abilitySystem.Tick();
        }

        public void GrantAbility(GameplayAbilitySpec abilitySpec)
        {
            abilitySystem.GrantAbility(abilitySpec);
        }

        public bool TryActivateAbility(GameplayAbilitySpec abilitySpec) 
        {
            if (!isServer)
            {
                ServerRPC_TryActivateAbility(abilitySpec.handle);
                return true;
            }
            return abilitySystem.TryActivateAbility(abilitySpec);
        }

        public void ServerRPC_TryActivateAbility(GameplayAbilitySpecHandle abilitySpecHandle)
        {
            TryActivateAbility(abilitySystem.GetAbilitySpecByHandle(abilitySpecHandle));
        }

        public void ServerRPC_GrantAbility(GameplayAbilitySpec abilitySpec)
        {
            abilitySpec.handle = new GameplayAbilitySpecHandle() { Value = nextAbilitySpecHandle++};
            GrantAbility(abilitySpec);
        }
        
        public void CancelAbilitiesByTag(GameplayTag tag)
        {
            abilitySystem.CancelAbilitiesByTag(tag);
        }
        
        public List<GameplayAbilitySpec> GetAbilities()
        {
            return abilitySystem.GetAbilities();
        }

        public void ApplyGameplayEffect(GameplayEffectSpec effectSpec)
        {
            abilitySystem.ApplyGameplayEffect(effectSpec);
        }

        public float GetAttributeValue(AttributeName attributeName)
        {
            return abilitySystem.GetAttributeValue(attributeName);
        }

        public float GetAttributeValue(string attributeNameStr)
        {
            return abilitySystem.GetAttributeValue(attributeNameStr);
        }

        public Attribute GetAttribute(AttributeName attributeName)
        {
            return abilitySystem.GetAttribute(attributeName);
        }

        public Attribute GetAttribute(string attributeNameStr)
        {
            return abilitySystem.GetAttribute(attributeNameStr);
        }

        public List<GameplayEffect> GetActiveEffects()
        {
            return abilitySystem.appliedGameplayEffects;
        }

        public void RemoveGameplayEffectsByTags(GameplayTagContainer tags)
        {
            abilitySystem.RemoveGameplayEffectsByTags(tags);
        }

        public List<GameplayTag> GetGrantedTags()
        {
            return abilitySystem.GetTags();
        }
    }
}