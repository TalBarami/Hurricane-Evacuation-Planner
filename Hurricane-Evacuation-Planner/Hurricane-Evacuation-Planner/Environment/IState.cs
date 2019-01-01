using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hurricane_Evacuation_Planner.AgentComponents;
using Hurricane_Evacuation_Planner.GraphComponents;

namespace Hurricane_Evacuation_Planner.Environment
{
    interface IState
    {
        double Time { get; set; }
        IGraph Graph { get; }
        IAgent Agent { get; }
        IState Clone();
        // Util, optimal action (we don't know at first), possible transitions = actions and new states. We know if nearby edge is blocked.
    }
}
