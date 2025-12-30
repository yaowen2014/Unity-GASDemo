using UnityEngine;
using WYGAS;

namespace Demo.Abilities
{
    public class GA_ShootAbility : GameplayAbility
    {
        public override void Activate(AbilityContext context)
        {
            Debug.Log("GA_ShootAbility Activated!");

            EndAbility(true);
        }
    }
}