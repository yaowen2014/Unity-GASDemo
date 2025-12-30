namespace WYGAS
{
    public interface ITimeSource
    {
        float now { get; }
        
        float deltaTime { get; }
    }
}