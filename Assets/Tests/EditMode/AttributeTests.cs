using System.Collections.Generic;
using NUnit.Framework;
using Tests.EditMode;
using WYGAS;

public class AttributeTests
{
    /*
    public Attribute createMockAttribute(AttributeName attrName, float defaultValue = 0)
    {
        var mockedAttribute = new Attribute();
        mockedAttribute.attributeName = attrName;
        mockedAttribute.currentValue = defaultValue;
        return mockedAttribute;
    }

    public GameplayEffect CreateMockInstantGameplayEffect(List<Modifier> modifiers)
    {
        var mockedGameplayEffect = new GameplayEffect();
        mockedGameplayEffect.durationType = GameplayEffectDurationType.Instant;
        mockedGameplayEffect.modifiers = modifiers;
        return mockedGameplayEffect;
    }

    public GameplayEffect CreateMockDurationGameplayEffectWithModifier(List<Modifier> modifiers, float duration, float period)
    {
        var mockedGameplayEffect = new GameplayEffect();
        mockedGameplayEffect.durationType = GameplayEffectDurationType.Duration;
        mockedGameplayEffect.modifiers = modifiers;
        mockedGameplayEffect.duration = duration;
        mockedGameplayEffect.period = period;
        return mockedGameplayEffect;
    }

    public GameplayEffect CreateMockDurationGameplayEffectWithGrantedTags(List<GameplayTag> grantedTags, float duration)
    {
        var mockedGameplayEffect = new GameplayEffect();
        mockedGameplayEffect.durationType = GameplayEffectDurationType.Duration;
        mockedGameplayEffect.grantedTags.AddRange(grantedTags);
        mockedGameplayEffect.duration = duration;
        return mockedGameplayEffect;
    }

    public Modifier CreateMockBasicModifier(ModifierOperator op, AttributeName attrName, float value)
    {
        var mockedModifier = new Modifier();
        mockedModifier.modifierOperator = op;
        mockedModifier.attributeName = attrName;
        mockedModifier.magnitude.constantValue = value;
        return mockedModifier;
    }
    
    [Test]
    public void ApplyInstantGE_WithBasicModifier_Test()
    {
        AbilitySystem asc = new AbilitySystem();
        asc.Initialize();
        var mockedAttrName = new AttributeName();
        asc.attributes.Add(createMockAttribute(mockedAttrName));
        var mockedModifier = CreateMockBasicModifier(ModifierOperator.Add, mockedAttrName, 1);
        var gameplayEffect = CreateMockInstantGameplayEffect(new List<Modifier> {mockedModifier });
        asc.ApplyGameplayEffect(gameplayEffect);
        Assert.AreEqual(1, asc.GetAttributeValue(mockedAttrName));
    }

    [Test]
    public void ApplyDurationGE_WithBasicModifier_Test()
    {
        AbilitySystem asc = new AbilitySystem();
        asc.Initialize();
        var time = new MockTimeResource();
        asc.time = time;
        var mockedAttrName = new AttributeName();
        asc.attributes.Add(createMockAttribute(mockedAttrName));
        var mockedModifier = CreateMockBasicModifier(ModifierOperator.Add, mockedAttrName, 1);
        var gameplayEffect = CreateMockDurationGameplayEffectWithModifier(new List<Modifier> {mockedModifier }, 3, 1);
        time.now = 0;
        asc.ApplyGameplayEffect(gameplayEffect);
        
        time.now = 1;
        asc.Tick();
        Assert.AreEqual(1, asc.GetAttributeValue(mockedAttrName));

        time.now = 1.1f;
        asc.Tick();
        Assert.AreEqual(1, asc.GetAttributeValue(mockedAttrName));

        time.now = 2;
        asc.Tick();
        Assert.AreEqual(2, asc.GetAttributeValue(mockedAttrName));
        
        time.now = 3;
        asc.Tick();
        Assert.AreEqual(3, asc.GetAttributeValue(mockedAttrName));
        
        time.now = 4;
        asc.Tick();
        Assert.AreEqual(3, asc.GetAttributeValue(mockedAttrName));
    }
    
    [Test]
    public void ApplyDurationGE_WithGrantedTags_Test()
    {
        AbilitySystem asc = new AbilitySystem();
        asc.Initialize();
        var time = new MockTimeResource();
        asc.time = time;
        
        var mockedAttrName = new AttributeName();
        asc.attributes.Add(createMockAttribute(mockedAttrName));
        
        var gameplayTags = new List<GameplayTag> {new GameplayTag("tag1")};
        var gameplayEffect = CreateMockDurationGameplayEffectWithGrantedTags(gameplayTags, 2);
        
        asc.ApplyGameplayEffect(gameplayEffect);
        Assert.IsTrue(asc.HasTag(gameplayTags[0]));
        
        time.now = 1;
        asc.Tick();
        Assert.IsTrue(asc.HasTag(gameplayTags[0]));
        
        time.now = 3;
        asc.Tick();
        Assert.IsFalse(asc.HasTag(gameplayTags[0]));
    }
    */
}