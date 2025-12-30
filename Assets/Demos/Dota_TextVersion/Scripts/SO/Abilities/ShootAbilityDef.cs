using System;
using System.Collections.Generic;
using Demo.Abilities;
using Demo.Scripts.SO;
using UnityEngine;
using WYGAS.SO;

namespace Demo.SO.Abilities
{
    [CreateAssetMenu(
        fileName = "NewShootAbilityDef",
        menuName = "DotaDemo/Abilities/ShootAbility"
    )]
    public class GA_ShootAbilityDef : GameplayAbilityDefinition
    {
        public override Type AbilityInstanceType => typeof(GA_ShootAbility);

        public ProjectileDef shootProjectile;
    }
}