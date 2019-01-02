using Hurricane_Evacuation_Planner.AgentComponents;

namespace Hurricane_Evacuation_Planner.GraphComponents.SimulatorGraphComponents
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
            return Evacuees > 0 ? new EvacuationVertex(this) : base.Clone();
        }

        public override void Accept(IAgent agent)
        {
            agent.Carry += Evacuees;
            Evacuees = 0;
        }

        public override string ToString()
        {
            return $"{base.ToString()}P{Evacuees}";
        }
    }
}
