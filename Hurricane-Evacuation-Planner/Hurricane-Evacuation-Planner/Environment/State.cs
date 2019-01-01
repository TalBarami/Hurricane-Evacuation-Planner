using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hurricane_Evacuation_Planner.AgentComponents;
using Hurricane_Evacuation_Planner.GraphComponents;

namespace Hurricane_Evacuation_Planner.Environment
{
    class State : IState
    {
        public double Time { get; set; }
        public IGraph Graph { get; }
        public IAgent Agent { get; }

        public State(double time, IGraph graph, IAgent agent)
        {
            Time = time;
            Graph = graph;
            Agent = agent;
        }

        public State(IGraph graph, IAgent agent) : this(0, graph, agent)
        {
        }

        public State(State other) : this(other.Time, other.Graph.Clone(), other.Agent.Clone())
        {
        }

        public IState Clone()
        {
            return new State(this);
        }
    }
}
