using System;
using System.Linq;
using Hurricane_Evacuation_Planner.Environment;
using Hurricane_Evacuation_Planner.GraphComponents;
using Hurricane_Evacuation_Planner.GraphComponents.SimulatorGraphComponents;
using Hurricane_Evacuation_Planner.Parser;

namespace Hurricane_Evacuation_Planner
{
    class Program
    {
        static void Main(string[] args)
        {
            var state = FileParser.ParseFile("C:/Users/Tal Barami/Documents/GitHub/Hurricane-Evacuation-Planner/Hurricane-Evacuation-Planner/Hurricane-Evacuation-PlannerTests/resources/file3.txt");
            var maybeBlocked = state.Graph.Edges.OfType<MaybeBlockedEdge>().ToList();

            for (var i = 0; i < 4; i++)
            {
                var blockedEdges = Convert.ToString(i, 2).PadLeft(maybeBlocked.Count, '0').Select(c => c != '0').ToArray();
                for (var j = 0; j < maybeBlocked.Count; j++)
                {
                    maybeBlocked[j].ActuallyBlocked = blockedEdges[j];
                }
                var simulator = new Simulator(state);
                simulator.Start();
            }

            Console.Read();
        }
    }
}
