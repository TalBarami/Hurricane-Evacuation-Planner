using System;
using System.Collections.Generic;
using System.Linq;
using Hurricane_Evacuation_Planner.Environment;
using Hurricane_Evacuation_Planner.GraphComponents;
using Hurricane_Evacuation_Planner.GraphComponents.SimulatorGraphComponents;

namespace Hurricane_Evacuation_Planner.AgentComponents
{
    public class Traverse
    {
        public IState OldState { get; }
        public List<IState> NewStates { get; }
        public IEdge TraversalEdge { get; }

        public Traverse(IState oldState, IVertex dst)
        {
            OldState = oldState;
            TraversalEdge = OldState.Graph.Edge(OldState.Agent.Position, dst);
            NewStates = GeneratePossibleStates(dst);

            /*NewState = OldState.Clone();
            NewState.Source = this;
            
            var graph = NewState.Graph;
            var agent = NewState.Agent;
            TraversalEdge = graph.Edge(agent.Position, dst);
            agent.Visit(graph.Vertex(dst));
            NewState.Time += TraversalEdge.Weight;*/
        }

        public List<IState> GeneratePossibleStates(IVertex dst)
        {
            var result = new List<IState>();
            var unknownEdges = dst.Connectors.OfType<MaybeBlockedEdge>().ToList();
            var length = Math.Pow(2, unknownEdges.Count);

            for (var i = 0; i < length; i++)
            {
                var possibleState = OldState.Clone();
                var blockedEdges = Convert.ToString(i, 2).PadLeft(unknownEdges.Count, '0').Select(c => c != '0').ToArray();

                for (var j = 0; j < unknownEdges.Count; j++)
                {
                    if (blockedEdges[j])
                    {
                        possibleState.Graph.Block(unknownEdges[j]);
                    }
                    else
                    {
                        unknownEdges[j].BlockageProbability = 0;
                    }
                }
                result.Add(possibleState);
            }

            result.ForEach(r => UpdateState(r, dst));

            return result;
        }

        public void UpdateState(IState state, IVertex v)
        {
            state.Agent.Visit(state.Graph.Vertex(v));
            state.Time += TraversalEdge.Weight;
        }

        public override string ToString()
        {
            return $"Traverse from {OldState.Agent.Position} to {NewStates[0].Agent.Position} at cost {TraversalEdge.Weight}";
        }
    }
}
