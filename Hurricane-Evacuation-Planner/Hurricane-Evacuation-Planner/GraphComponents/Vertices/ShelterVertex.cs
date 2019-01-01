using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hurricane_Evacuation_Planner.AgentComponents;

namespace Hurricane_Evacuation_Planner.GraphComponents.Vertices
{
    class ShelterVertex : Vertex
    {
        public ShelterVertex(int id) : base(id)
        {
        }

        public ShelterVertex(IVertex other) : base(other)
        {
        }

        public override void Accept(IAgent agent)
        {
            agent.Saved = agent.Carry;
            agent.Carry = 0;
        }
    }
}
