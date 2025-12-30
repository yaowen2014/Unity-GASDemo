using System;
using Demos.GASTanks.Scripts.Abilities;
using UnityEngine;
using WYGAS.SO;

namespace Demos.GASTanks.Scripts.SO
{
    [CreateAssetMenu(
        fileName = "GA_NewFireAbilityDef",
        menuName = "TankDemo/Abilities/FireAbility"
    )]
    public class FireAbilityDefinition : GameplayAbilityDefinition
    {
        public override Type AbilityInstanceType => typeof(FireAbility);
    }
}