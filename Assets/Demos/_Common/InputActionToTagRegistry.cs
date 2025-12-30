using System.Collections.Generic;
using Demo.Common.SO;
using WYGAS;

namespace Demo.Common
{
    public class InputActionToTagRegistry
    {
        private static Dictionary<string, GameplayTag> _map = new Dictionary<string, GameplayTag>();

        public static void Initialize(InputActionToTagTable table)
        {
            table.inputActionToTagEntries.ForEach(entry =>
            {
                var key = entry.inputActionName;

                if (!_map.ContainsKey(key))
                {
                    _map[key] = GameplayTagRegistry.Get(entry.tag.Path);
                }
            });
        }

        public static GameplayTag Get(string actionName)
        {
            if (!_map.ContainsKey(actionName))
            {
                return GameplayTag.EmptyTag;
            }
            return _map[actionName];
        }
    }
}