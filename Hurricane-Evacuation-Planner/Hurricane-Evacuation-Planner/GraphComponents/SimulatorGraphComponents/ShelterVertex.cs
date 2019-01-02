using Hurricane_Evacuation_Planner.AgentComponents;

namespace Hurricane_Evacuation_Planner.GraphComponents.SimulatorGraphComponents
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

        public override IVertex Clone()
        {
            return new ShelterVertex(this);
        }

        public override string ToString()
        {
            return $"{base.ToString()}S";
        }
    }
}
