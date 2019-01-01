using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hurricane_Evacuation_Planner.Environment;
using Hurricane_Evacuation_Planner.GraphComponents;

namespace Hurricane_Evacuation_Planner.AgentComponents
{
    class Agent : IAgent
    {
        public IVertex Position { get; set; }
        public int Carry { get; set; }
        public int Saved { get; set; }

        public Agent(IVertex position, int carry, int saved)
        {
            Position = position;
            Carry = carry;
            Saved = saved;
        }
        public Agent(IVertex position) : this(position, 0, 0)
        {
        }

        public Agent(Agent other) : this(other.Position, other.Carry, other.Saved)
        {
        }

        public void Visit(IVertex v)
        {
            Position = v;
            v.Accept(this);
        }

        public IAgent Clone()
        {
            return new Agent(this);
        }
    }
}
