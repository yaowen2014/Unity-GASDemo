using System;
using UnityEngine;

namespace WYGAS
{
    [CreateAssetMenu(
        fileName = "NewAttributeName",
        menuName = "GAS/Attribute Definition"
    )]
    [Serializable]
    public class AttributeName : ScriptableObject { }
}