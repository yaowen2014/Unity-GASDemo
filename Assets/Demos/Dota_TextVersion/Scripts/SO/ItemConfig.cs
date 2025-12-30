using System.Collections.Generic;
using UnityEngine;
using WYGAS.SO;

public class ItemConfig : MonoBehaviour
{
    public string name;

    public int maxStack = 0;
    
    public List<GameplayAbilityDefinition> abilities;
    
    public List<GameplayEffectDefinition> equipEffects;
}
