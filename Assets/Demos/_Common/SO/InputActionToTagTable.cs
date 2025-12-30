using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.SO
{
    [CreateAssetMenu(menuName = "Demo/InputActionToTagTable")]
    public class InputActionToTagTable : ScriptableObject
    {
        public List<InputActionToTagEntry> inputActionToTagEntries;
    }
}