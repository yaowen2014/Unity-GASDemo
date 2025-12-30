using System;
using Demos.GASTanks.Scripts.Abilities;
using UnityEngine;
using WYGAS.SO;

namespace Demos.GASTanks.Scripts.SO
{
    [CreateAssetMenu(
        fileName = "GA_NewFireAbilityDef",
        menuName = "TankDemo/Abilities/MirrorFireAbility"
    )]
    public class MirrorFireAbilityDefinition : GameplayAbilityDefinition
    {
        public override Type AbilityInstanceType => typeof(MirrorFireAbility);
    }
}