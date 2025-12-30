using System;
using UnityEngine;

namespace WYGAS.SO
{
    [Serializable]
    public struct GameplayTagRef
    {
        [SerializeField]
        private string tagPath;

        public string Path => tagPath;
    }
}