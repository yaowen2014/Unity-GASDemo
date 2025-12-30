using System;
using UnityEngine;

namespace WYGAS
{
    public enum MagnitudeType
    {
        Constant,
        SetByCaller,
        AttributeBased,
    }
    
    [Serializable]
    public class GameplayEffectMagnitude
    {
        public MagnitudeType type;

        public float constantValue;
        public string setByCallerKey;
        public AttributeName attributeName;
        public float attributeCoefficient;
    }
}