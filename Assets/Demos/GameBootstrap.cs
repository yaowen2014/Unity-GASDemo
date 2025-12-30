using Demo.Common;
using Demo.Common.SO;
using UnityEngine;
using WYGAS;
using WYGAS.SO;

namespace Demo
{
    public static class GameBootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            GameplayTagTable gameplayTagsTable = Resources.Load<GameplayTagTable>("GameplayTagsTable");
            GameplayTagRegistry.Initialize(gameplayTagsTable);
            
            InputActionToTagTable inputActionToTagTable = Resources.Load<InputActionToTagTable>("InputActionToTagMap");
            InputActionToTagRegistry.Initialize(inputActionToTagTable);
        }
    }
}