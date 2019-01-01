using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hurricane_Evacuation_Planner.Environment;
using Hurricane_Evacuation_Planner.GraphComponents;

namespace Hurricane_Evacuation_Planner.AgentComponents
{
    class Traverse
    {
        public IState OldState { get; }
        public IState NewState { get; }
        public IEdge TraversalEdge { get; }

        public Traverse(IState oldState, IVertex dst)
        {
            OldState = oldState;
            NewState = OldState.Clone();

            var graph = NewState.Graph;
            var agent = NewState.Agent;
            TraversalEdge = graph.Edge(agent.Position, dst);
            agent.Visit(graph.Vertex(dst));
            NewState.Time += TraversalEdge.Weight;
        }
    }
}
