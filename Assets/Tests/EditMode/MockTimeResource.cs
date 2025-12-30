using WYGAS;

namespace Tests.EditMode
{
    public class MockTimeResource : ITimeSource
    {
        public float now { get; set; }
        
        public float deltaTime { get; set; }
    }
}