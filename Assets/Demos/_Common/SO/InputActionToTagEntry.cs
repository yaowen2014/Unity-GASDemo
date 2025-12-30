using System;
using WYGAS.SO;

namespace Demo.Common.SO
{
    [Serializable]
    public class InputActionToTagEntry
    {
        public string inputActionName;
        public GameplayTagRef tag;
    }
}