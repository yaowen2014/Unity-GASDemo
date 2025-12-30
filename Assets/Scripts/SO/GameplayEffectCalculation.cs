using System.Collections.Generic;
using UnityEngine;

namespace WYGAS
{
    public abstract class GameplayEffectCalculation : ScriptableObject
    {
        public abstract List<Modifier> Execute(GameplayEffectSpec effectSpec);
    }
}