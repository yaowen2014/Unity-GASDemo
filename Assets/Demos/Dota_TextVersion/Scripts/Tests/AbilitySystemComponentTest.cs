using System.Collections.Generic;
using Demo.Scripts;
using Demo.Scripts.SO;
using EasyButtons;
using UnityEngine;
using WYGAS;
using WYGAS.SO;

public class AbilitySystemComponentTest : MonoBehaviour
{
    public AbilitySystemComponent playerAsc;
    public AbilitySystemComponent enemyAsc;
    public AttributeName attackAttr;

    private void Awake()
    {
        AssignAbilitySystemComponent();
    }

    private void AssignAbilitySystemComponent()
    {
        if (playerAsc == null)
        {
            if (GetComponent<AbilitySystemComponent>() != null)
            {
                playerAsc = GetComponent<AbilitySystemComponent>();
            }
            else
            {
                playerAsc = gameObject.AddComponent<AbilitySystemComponent>();
            }
        }
    }

    public GameplayEffectDefinition sampleEffectDef;
    [Button]
    public void TestSampleGameplayEffect()
    {
        enemyAsc.ApplyGameplayEffect(sampleEffectDef.CreateSpecInternal());
    }
    
    public GameplayEffectDefinition poisonGameplayEffect;
    [Button]
    public void TestPoison()
    {
        var effect = poisonGameplayEffect.CreateSpecInternal();
        effect.sourceAS = playerAsc.abilitySystem;
        effect.targetAS = enemyAsc.abilitySystem;
        
        enemyAsc.ApplyGameplayEffect(effect);
    }

    public DamageGameplayEffectDefinition normalAttackGameplayEffect;
    [Button]
    public void TestCriticalAttack()
    {
        normalAttackGameplayEffect.damageRules = new List<IDamageRule>();
        var criticalDamageRule = new CriticalDamageRule();
        criticalDamageRule.criticalChance = 0.5f;
        criticalDamageRule.criticalMultiplier = 1f;
        normalAttackGameplayEffect.damageRules.Add(criticalDamageRule);
        var damageEffectSpec = (DamageGameplayEffectSpec)normalAttackGameplayEffect.CreateSpecInternal();
        damageEffectSpec.damageBase = playerAsc.GetAttributeValue(attackAttr);
        damageEffectSpec.sourceAS = playerAsc.abilitySystem;
        damageEffectSpec.targetAS = enemyAsc.abilitySystem;
        enemyAsc.ApplyGameplayEffect(damageEffectSpec);
    }

    public GameplayEffectDefinition addStrengthGameplayEffectDef;
    [Button]
    public void TestAddStrength()
    {
        var addStrengthGameplayEffect = addStrengthGameplayEffectDef.CreateSpecInternal();
        addStrengthGameplayEffect.sourceAS = playerAsc.abilitySystem;
        addStrengthGameplayEffect.targetAS = enemyAsc.abilitySystem;
        enemyAsc.ApplyGameplayEffect(addStrengthGameplayEffect);
    }
    
    public GameplayEffectDefinition addAgilityGameplayEffectDef;
    [Button]
    public void TestAddAgility()
    {
        var addStrengthGameplayEffect = addAgilityGameplayEffectDef.CreateSpecInternal();
        addStrengthGameplayEffect.sourceAS = playerAsc.abilitySystem;
        addStrengthGameplayEffect.targetAS = enemyAsc.abilitySystem;
        enemyAsc.ApplyGameplayEffect(addStrengthGameplayEffect);
    }

    [Button]
    public void TestDispel()
    {
        var tags = new GameplayTagContainer();
        tags.Add(GameplayTagRegistry.Get("Effect.Debuff.Dispellable"));;
        enemyAsc.RemoveGameplayEffectsByTags(tags);
    }
}
