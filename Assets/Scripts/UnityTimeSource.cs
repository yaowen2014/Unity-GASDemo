using UnityEngine;

namespace WYGAS
{
    public class UnityTimeSource : ITimeSource
    {
        public float now => Time.time;
        
        public float deltaTime => Time.deltaTime;
    }
}