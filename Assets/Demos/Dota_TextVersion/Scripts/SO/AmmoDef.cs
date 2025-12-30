using UnityEngine;

namespace Demo.Scripts.SO
{
    [CreateAssetMenu(
        fileName = "NewAmmo",
        menuName = "Demo/Projectile/Ammo"
    )]
    public class AmmoDef : ScriptableObject
    {
        public int stackAmount;

        public ProjectileDef projectileType;
    }
}