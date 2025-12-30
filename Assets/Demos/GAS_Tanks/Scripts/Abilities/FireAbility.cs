using UnityEngine;
using WYGAS;

namespace Demos.GASTanks.Scripts.Abilities
{
    public class FireAbility : GameplayAbility
    {
        public override void Activate(AbilityContext context)
        {
            Tank tank = context.actorInfo.owner.GetComponent<Tank>();
            AbilityTask_RepeatedTask repeatedTask = AbilityTask_RepeatedTask.Create(this, 0.1f);
            repeatedTask.onTrigger += () =>
            {
                GameObject projectile = (GameObject)Object.Instantiate(tank.projectilePrefab,
                    tank.projectileMount.position, tank.projectileMount.rotation);
                tank.animator.SetTrigger("Shoot");
            };
            repeatedTask.Activate();
        }
    }
}