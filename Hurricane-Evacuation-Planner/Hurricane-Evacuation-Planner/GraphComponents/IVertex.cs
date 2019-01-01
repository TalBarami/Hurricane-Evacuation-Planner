using System.Collections.Generic;
using Hurricane_Evacuation_Planner.AgentComponents;

namespace Hurricane_Evacuation_Planner.GraphComponents
{
    public interface IVertex
    {
        int Id { get; }
        string Name { get; }
        List<IEdge> Connectors { get; }
        List<IVertex> Neighbors { get; }
        IVertex Clone();
        void Accept(IAgent agent);
    }
}
