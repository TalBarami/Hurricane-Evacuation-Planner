using System.Collections.Generic;
using System.Linq;
using Hurricane_Evacuation_Planner.AgentComponents;
using Hurricane_Evacuation_Planner.GraphComponents;
using Hurricane_Evacuation_Planner.GraphComponents.SimulatorGraphComponents;

namespace Hurricane_Evacuation_Planner.Environment
{
    public class State : IState
    {
        public Traverse Source { get; set; }
        public int Deadline { get; }
        public double Time { get; set; }
        public IGraph Graph { get; }
        public IAgent Agent { get; }

        public bool Goal => Time >= Deadline || (!Graph.Vertices.OfType<EvacuationVertex>().Any() && Agent.Carry == 0);

        public List<Traverse> ValidMoves => validMoves ?? (validMoves = Goal
                                                ? new List<Traverse>()
                                                : Agent.Position.Neighbors.Where(v => Time + Graph.Edge(Agent.Position, v).Weight <= Deadline)
                                                    .Select(v => new Traverse(this, v)).ToList());
        private List<Traverse> validMoves;

        public State(Traverse source, int deadline, double time, IGraph graph, IAgent agent)
        {
            Source = source;
            Deadline = deadline;
            Time = time;
            Graph = graph;
            Agent = agent;
        }

        public State(int deadline, IGraph graph, IAgent agent) : this(null, deadline, 0, graph, agent)
        {
        }

        public State(State other) : this(other.Source, other.Deadline, other.Time, other.Graph.Clone(), other.Agent.Clone())
        {
        }

        public IState Clone()
        {
            return new State(this);
        }

        public override string ToString()
        {
            return $"Time: {Time}/{Deadline}\n" +
                   $"Graph: {Graph}\n" +
                   $"Agent: {Agent}";

        }
    }
}
