using System;
using System.Collections.Generic;
using System.Linq;

namespace Hurricane_Evacuation_Planner.Environment
{
    public class ValueIteration
    {
        private const int Iterations = 100;

        public IState InitialState { get; }
        public List<IState> States { get; }

        public ValueIteration(IState initialState)
        {
            States = new List<IState>();
            InitialState = initialState;
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
            for (var i = 0; i < Iterations; i++)
            {
                foreach (var state in States)
                {
                    if (!state.Goal)
                    {
                        state.Utility = state.ValidMoves.Aggregate(0.0, (current, action) =>
                            action.ExpectedValue() > current ? action.ExpectedValue() : current);
                    }
                }
            }
        }
    }
}
