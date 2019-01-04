using System;
using System.Linq;
using Hurricane_Evacuation_Planner.GraphComponents.SimulatorGraphComponents;

namespace Hurricane_Evacuation_Planner.Environment
{
    public class Simulator
    {
        public int Deadline { get; }
        public IState InitialState { get; }
        public ValueIteration Policy { get; }

        public Simulator(IState initialInitialState)
        {
            Deadline = initialInitialState.Deadline;
            InitialState = initialInitialState;
            Policy = new ValueIteration(InitialState);
            Policy.UpdateStates();
        }

        public void Start()
        {
            var currentState = InitialState;
            while (!currentState.Goal)
            {
                Console.WriteLine($"Current State: {StateToString(currentState)}");
                Console.WriteLine("Possible transitions:");
                var possibleMoves = string.Join("\n", currentState.ValidMoves.Select(vm => $"\t{vm} -> \t{string.Join("\n\t\t\t", vm.NewStates.Select(StateToString))}"));

                Console.WriteLine(possibleMoves);
                var move = currentState.BestMove;
                Console.WriteLine($"Selected Action: {move} U{move.ExpectedValue()}\n");
                currentState = move.NewStates.First(s => InitialState.Match(s));
            }
            Console.WriteLine($"Final State: {StateToString(currentState)}\n");
            Console.WriteLine("Simulation is over.");

            /*Console.WriteLine();
            Policy.States.ForEach(s => Console.WriteLine($"{s}\n"));*/
            /*Console.ReadLine();*/
        }

        public string StateToString(IState state)
        {
            var position = state.Graph.Vertex(state.Agent.Position);
            var evacuees = InitialState.Graph.Vertices.OfType<EvacuationVertex>()
                .Select(v => v.Equals(state.Graph.Vertex(v)) ? "t" : "f");
            var saved = state.Agent.Saved;
            var blockages = InitialState.Graph.Edges.OfType<MaybeBlockedEdge>()
                .Select(e =>
                {
                    var actualBlockage = e.ActuallyBlocked ? "[B]" : "[F]";
                    var possibleBlockage = state.Graph.Edges.Any(e1 => e1.Id == e.Id)
                            ? state.Graph.Edge(e) is MaybeBlockedEdge ? "U" : "F" : "B";
                    return $"{possibleBlockage}{actualBlockage}";
                });
            var time = (int)state.Time;

            return $"({position}, {string.Join(", ", evacuees)}, {saved}, {string.Join(", ", blockages)}, {time}) U{state.Utility} P{state.Probability}";
        }

    }
}
