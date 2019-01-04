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
        private const double Discount = 1.0;

        public IState OldState { get; }
        public List<IState> NewStates { get; }
        public IEdge TraversalEdge { get; }

        public Traverse(IState oldState, IVertex dst)
        {
            OldState = oldState;
            TraversalEdge = OldState.Graph.Edge(OldState.Agent.Position, dst);
            NewStates = GeneratePossibleStates(dst);
        }

        public List<IState> GeneratePossibleStates(IVertex dst)
        {
            var result = new List<IState>();
            var unknownEdges = dst.Connectors.OfType<MaybeBlockedEdge>().ToList();
            var length = Math.Pow(2, unknownEdges.Count);

            for (var i = 0; i < length; i++)
            {
                var possibleState = OldState.Clone();
                possibleState.Source = this;
                var blockedEdges = Convert.ToString(i, 2).PadLeft(unknownEdges.Count, '0').Select(c => c != '0').ToArray();
                var stateProbability = 1.0;
                for (var j = 0; j < unknownEdges.Count; j++)
                {
                    if (blockedEdges[j])
                    {
                        stateProbability *= unknownEdges[j].BlockageProbability;
                        possibleState.Graph.Block(unknownEdges[j]);
                    }
                    else
                    {
                        stateProbability *= 1 - unknownEdges[j].BlockageProbability;
                        possibleState.Graph.Free(unknownEdges[j]);
                    }
                }

                possibleState.Probability = stateProbability;
                result.Add(possibleState);
            }

            result.ForEach(r => UpdateState(r, dst));

            return result;
        }

        private void UpdateState(IState state, IVertex v)
        {
            state.Time += TraversalEdge.Weight;
            if (state.Time <= state.Deadline)
            {
                state.Graph.Vertex(v).Accept(state.Agent);
                state.Agent.Visit(state.Graph.Vertex(v));
                state.Graph.Update(v);
            }
        }

        public double ExpectedValue()
        {
            var sum = 0.0;
            foreach (var state in NewStates)
            {
                sum += state.Probability * (OldState.Reward + Discount * state.Utility);
            }

            return sum;
        }

        public override string ToString()
        {
            return TraversalEdge.ToString();
        }
    }
}
