using System;

namespace WYGAS
{
    [Serializable]
    public readonly struct GameplayTag 
    {
        public readonly string name;
        
        public static GameplayTag EmptyTag = new GameplayTag("");

        public GameplayTag(string inName)
        {
            name = inName;
        }
        
        public bool Equals(GameplayTag other) => name == other.name;

        public override string ToString() => name;
    }
}