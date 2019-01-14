using System;
using System.Linq;
using Hurricane_Evacuation_Planner.GraphComponents.SimulatorGraphComponents;

namespace Hurricane_Evacuation_Planner.Environment
{
    public class Simulator
    {
        public int Deadline { get; }
        public IState InitialState { get; }
        public IState FinalState { get; private set; }
        public ValueIteration Policy { get; }
        public double Score => FinalState.Agent.Saved;

        private bool debug;
        private static readonly Random Random = new Random(123);

        public Simulator(IState initialInitialState, bool debug)
        {
            this.debug = debug;
            Deadline = initialInitialState.Deadline;
            InitialState = initialInitialState;
            Policy = new ValueIteration(InitialState);
            Policy.UpdateStates();
        }
        public Simulator(IState initialInitialState) : this(initialInitialState, false)
        {
        }

        public void Start()
        {
            InitialState.Graph.Edges.OfType<MaybeBlockedEdge>().ToList().ForEach(e => e.ActuallyBlocked = Random.NextDouble() <= e.BlockageProbability);
            var currentState = InitialState;
            while (!currentState.Goal)
            {
                Print($"Current State: {StateToString(currentState)}");
                Print("Possible transitions:");
                var possibleMoves = string.Join("\n", currentState.ValidMoves.Select(vm => $"\t{vm} -> \t{string.Join("\n\t\t\t", vm.NewStates.Select(StateToString))}"));

                Print(possibleMoves);
                var move = currentState.BestMove;
                Print($"Selected Action: {move} U{move.ExpectedValue()}\n");
                currentState = move.NewStates.First(s => InitialState.Match(s));
            }

            FinalState = currentState;
            Print($"Final State: {StateToString(FinalState)}\n");
            Print("Simulation is over.");
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

            return $"({position}, {string.Join(", ", evacuees)}, {saved}, {string.Join(", ", blockages)}, {time}) U{state.Utility} P{state.Probability} C{state.Agent.Carry}";
        }

        public void Print(string s = "")
        {
            if (debug)
            {
                Console.WriteLine(s);
            }
        }

    }
}
