using System;
using System.Collections.Generic;
using System.Linq;

namespace Hurricane_Evacuation_Planner.Environment
{
    public class StatesGenerator
    {
        public List<IState> States { get; private set; }

        public StatesGenerator(IState baseState)
        {
            States = new List<IState>();
            var stack = new Stack<IState>();
            stack.Push(baseState);
            while (stack.Any())
            {
                var current = stack.Pop();
                States.Add(current);
                current.ValidMoves.ForEach(move => move.NewStates.ForEach(stack.Push));
            }
        }
    }
}
