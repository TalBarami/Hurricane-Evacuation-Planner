using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hurricane_Evacuation_Planner.Environment;
using Hurricane_Evacuation_Planner.GraphComponents;

namespace Hurricane_Evacuation_Planner.AgentComponents
{
    public interface IAgent
    {
        IVertex Position { get; }
        int Carry { get; set; }
        int Saved { get; set; }
        IAgent Clone();
        void Visit(IVertex v);
    }
}
