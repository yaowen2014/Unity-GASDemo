using System;
using UnityEngine;

namespace WYGAS.SO
{
    public abstract class GameplayEffectDefinition : ScriptableObject
    {
        public abstract Type EffectInstanceType { get; }

        public abstract GameplayEffectSpec CreateSpecInternal();
    }
}