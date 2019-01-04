using Hurricane_Evacuation_Planner.GraphComponents;

namespace Hurricane_Evacuation_Planner.AgentComponents
{
    class Agent : IAgent
    {
        public int Position { get; set; }
        public int Carry { get; set; }
        public int Saved { get; set; }

        public Agent(int position, int carry, int saved)
        {
            Position = position;
            Carry = carry;
            Saved = saved;
        }

        public Agent(IVertex position, int carry, int saved) : this(position.Id, carry, saved)
        {
        }
        public Agent(IVertex position) : this(position.Id, 0, 0)
        {
        }

        public Agent(Agent other) : this(other.Position, other.Carry, other.Saved)
        {
        }

        public void Visit(IVertex v)
        {
            Position = v.Id;
            v.Accept(this);
        }

        public IAgent Clone()
        {
            return new Agent(this);
        }

        public override string ToString()
        {
            return $"Agent({Position},C{Carry},S{Saved})";
        }
    }
}
