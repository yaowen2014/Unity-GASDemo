 using System.Collections.Generic;
using System.Linq;

namespace WYGAS
{
    public class GameplayTagContainer
    {
        private readonly HashSet<GameplayTag> tags = new();

        public void Add(GameplayTag tag) => tags.Add(tag);
        public void AddRange(IEnumerable<GameplayTag> tags) => this.tags.UnionWith(tags);
        
        public void Remove(GameplayTag tag) => tags.Remove(tag);
        
        public void Clear() => tags.Clear();
        
        public bool HasTag(GameplayTag tagToCheck) {
            bool result = false;
            tags.ToList().ForEach(tag =>
            {
                if (tag.name == tagToCheck.name)
                {
                    result = true;
                }
            });
            return result;
        }

        public bool MatchTag(GameplayTag tagToCheck)
        {
            bool result = false;
            tags.ToList().ForEach(tag =>
            {
                if (tag.name == tagToCheck.name || tag.name.StartsWith(tagToCheck.name + '.'))
                {
                    result = true;
                }
            });
            return result;
        }

        public bool HasAny(IEnumerable<GameplayTag> inTags) => inTags.Any(t => tags.Contains(t));
        
        public bool HasAll(IEnumerable<GameplayTag> inTags) => inTags.All(t => tags.Contains(t));
        
        public List<GameplayTag> ToList() => tags.ToList();
    }
}