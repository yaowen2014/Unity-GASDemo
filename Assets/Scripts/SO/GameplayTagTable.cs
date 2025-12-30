using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace WYGAS.SO
{
    [CreateAssetMenu(menuName = "GAS/GameplayTagTable")]
    public class GameplayTagTable : ScriptableObject
    {
        public List<string> tags;
    }
}