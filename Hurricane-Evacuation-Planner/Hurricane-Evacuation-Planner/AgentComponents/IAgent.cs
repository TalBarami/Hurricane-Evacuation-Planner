using Hurricane_Evacuation_Planner.GraphComponents;

namespace Hurricane_Evacuation_Planner.AgentComponents
{
    public interface IAgent
    {
        int Position { get; }
        int Carry { get; set; }
        int Saved { get; set; }
        IAgent Clone();
        void Visit(IVertex v);
    }
}
