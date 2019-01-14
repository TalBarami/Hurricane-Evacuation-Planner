using System;
using System.Collections.Generic;
using System.Linq;
using Hurricane_Evacuation_Planner.GraphComponents.SimulatorGraphComponents;

namespace Hurricane_Evacuation_Planner.Environment
{
    public class ValueIteration
    {
        private const double Epsilon = 0.000001;

        public IState InitialState { get; }
        public List<IState> States { get; }
        public double MaximumStates { get; }
        public ValueIteration(IState initialState)
        {
            States = new List<IState>();
            InitialState = initialState;
            MaximumStates = InitialState.Graph.Vertices.Count * // position
                            Math.Pow(2, InitialState.Graph.Vertices.OfType<EvacuationVertex>().Count()) * // evacuees?
                            Math.Pow(2, InitialState.Graph.Vertices.OfType<EvacuationVertex>().Count()) * // saved
                            Math.Pow(3, InitialState.Graph.Edges.OfType<MaybeBlockedEdge>().Count()) * // blocked?
                            InitialState.Deadline; // deadline
        }

        public void UpdateStates()
        {
            GenerateStates();
            UpdateUtilities();
        }

        private void GenerateStates()
        {
            var stack = new Stack<IState>();
            stack.Push(InitialState);
            while (stack.Any())
            {
                var current = stack.Pop();
                States.Add(current);
                current.ValidMoves.ForEach(move => move.NewStates.ForEach(stack.Push));
            }
        }

        private void UpdateUtilities()
        {
            foreach (var state in States.Where(s => s.Goal))
            {
                state.Utility = state.Reward;
            }

            var converged = false;
            while (!converged)
            {
                converged = true;
                foreach (var state in States.Where(s => !s.Goal))
                {
                    var newUtility = state.ValidMoves.Aggregate(0.0, (current, action) =>
                        action.ExpectedValue() > current ? action.ExpectedValue() : current);
                    if (Math.Abs(newUtility - state.Utility) > Epsilon)
                    {
                        converged = false;
                    }
                    state.Utility = newUtility;
                }
            }
        }
    }
}
