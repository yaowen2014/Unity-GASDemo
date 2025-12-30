using Mirror;
using UnityEngine;
using WYGAS;

namespace Demos.GASTanks.Scripts.Abilities
{
    public class MirrorFireAbility : GameplayAbility
    {
        public override void Activate(AbilityContext context)
        {
            MirrorTank tank = context.actorInfo.owner.GetComponent<MirrorTank>();
            AbilityTask_RepeatedTask repeatedTask = AbilityTask_RepeatedTask.Create(this, 0.1f);
            repeatedTask.onTrigger += () =>
            {
                GameObject projectile = (GameObject)Object.Instantiate(tank.projectilePrefab,
                    tank.projectileMount.position, tank.projectileMount.rotation);
                NetworkServer.Spawn(projectile);
                tank.animator.SetTrigger("Shoot");
            };
            repeatedTask.Activate();
        }
    }
}