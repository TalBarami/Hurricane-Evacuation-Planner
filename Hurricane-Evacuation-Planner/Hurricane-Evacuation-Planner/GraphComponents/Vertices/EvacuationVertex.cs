using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hurricane_Evacuation_Planner.AgentComponents;

namespace Hurricane_Evacuation_Planner.GraphComponents.Vertices
{
    class EvacuationVertex : Vertex
    {
        public int Evacuees { get; private set; }

        public EvacuationVertex(int id, int evacuees) : base(id)
        {
            Evacuees = evacuees;
        }

        public EvacuationVertex(EvacuationVertex other) : this(other.Id, other.Evacuees)
        {
        }

        public override IVertex Clone()
        {
            return Evacuees > 0 ? new EvacuationVertex(this) : new Vertex(this);
        }

        public override void Accept(IAgent agent)
        {
            agent.Carry += Evacuees;
            Evacuees = 0;
        }
    }
}
