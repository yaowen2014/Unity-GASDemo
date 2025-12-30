using Demo.Scripts.UI;
using UnityEngine;

namespace Demo.Scripts
{
    public class GameUIRoot : MonoBehaviour
    {
        [SerializeField] private EnemyStatusHUD enemyStatusHUD;
        [SerializeField] private EnemyController enemy;
        
        [SerializeField] private PlayerStatusHUD playerStatusHUD;
        [SerializeField] private PlayerController player;

        private void Start()
        {
            playerStatusHUD.Bind(player.asc);
            enemyStatusHUD.Bind(enemy.asc);
        }
    }
}