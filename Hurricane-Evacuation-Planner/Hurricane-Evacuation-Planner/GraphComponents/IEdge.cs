namespace Hurricane_Evacuation_Planner.GraphComponents
{
    public interface IEdge
    {
        int Id { get; }
        string Name { get; }
        int V1 { get; }
        int V2 { get; }
        double Weight { get; }
    }
}
