using System.Collections.Generic;
using System.Linq;
using WYGAS.SO;

namespace WYGAS
{
    public static class GameplayTagRegistry
    {
        private static Dictionary<string, GameplayTag> _map = new Dictionary<string, GameplayTag>();

        public static void Initialize(GameplayTagTable table)
        {
            table.tags.ForEach(tag =>
            {
                string[] parts = tag.Split('.');
                string currentTagName = "";

                for (int i = 0; i < parts.Length; i++)
                {
                    currentTagName = (i == 0) ? parts[i] : currentTagName + "." + parts[i];

                    if (!_map.ContainsKey(currentTagName))
                    {
                        _map[currentTagName] = new GameplayTag(currentTagName);
                    }
                }
            });
        }

        public static GameplayTag Get(string path)
        {
            return _map[path];
        }
    }
}