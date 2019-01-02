using System.Collections.Generic;
using Hurricane_Evacuation_Planner.AgentComponents;
using Hurricane_Evacuation_Planner.GraphComponents;

namespace Hurricane_Evacuation_Planner.Environment
{
    public interface IState
    {
        Traverse Source { get; set; }
        int Deadline { get; }
        double Time { get; set; }
        IGraph Graph { get; }
        IAgent Agent { get; }

        bool Goal { get; }
        List<Traverse> ValidMoves { get; }

        IState Clone();
        
        // Util, optimal action (we don't know at first), possible transitions = actions and new states. We know if nearby edge is blocked.
    }
}
