using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public double Reward => Goal ? Agent.Saved : 0;
        public double Utility { get; set; }
        public double Probability { get; set; }

        public Traverse BestMove => ValidMoves.Any()
            ? ValidMoves.Aggregate(validMoves[0], (cur, next) => cur.ExpectedValue() > next.ExpectedValue() ? cur : next)
            : null;

        public bool Goal => Time >= Deadline;
        public List<Traverse> ValidMoves => validMoves ?? (validMoves = Goal
                                                ? new List<Traverse>()
                                                : Graph.Vertex(Agent.Position).Neighbors.Select(v => new Traverse(this, v)).ToList());
        private List<Traverse> validMoves;

        public State(Traverse source, int deadline, double time, IGraph graph, IAgent agent)
        {
            Source = source;
            Deadline = deadline;
            Time = time;
            Graph = graph;
            Agent = agent;
            Probability = 1.0;
            Utility = 0;
        }

        public State(int deadline, IGraph graph, IAgent agent) : this(null, deadline, 0, graph, agent)
        {
        }

        public State(State other) : this(other.Source, other.Deadline, other.Time, other.Graph.Clone(), other.Agent.Clone())
        {
        }
        public bool Match(IState other)
        {
            var edges = Graph.Edges.OfType<MaybeBlockedEdge>().ToList();

            foreach (var edge in edges)
            {
                if (edge.ActuallyBlocked && other.Graph.Edges.Any(e => !(e is MaybeBlockedEdge) && e.Id == edge.Id))
                {
                    return false;
                }
                if (!edge.ActuallyBlocked && other.Graph.Edges.All(e => e.Id != edge.Id))
                {
                    return false;
                }
            }

            return true;
        }

        public IState Clone()
        {
            return new State(this);
        }

        public override string ToString()
        {
            var path = new StringBuilder();
            var src = Source;
            while (src != null)
            {
                path.Insert(0, $"{src} ");
                src = src.OldState.Source;
            }
            return $"Time: {Time}/{Deadline}\n" +
                   $"Utility: {Utility}, Reward: {Reward}, Probability: {Probability}\n" +
                   $"Graph: {Graph}\n" +
                   $"{Agent}\n" +
                   $"Path: {path}";
        }
    }
}
