using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
            var state = FileParser.ParseFile(
                "C:/Users/Tal Barami/Documents/GitHub/Hurricane-Evacuation-Planner/Hurricane-Evacuation-Planner/Hurricane-Evacuation-PlannerTests/resources/file1.txt");
            var avg = 0.0;
            var times = 10000;
            var simulator = new Simulator(state);

            for (var i = 0; i < times; i++)
            {
                simulator.Start();
                avg += simulator.Score;
            }
            Console.WriteLine($"Average score: {avg / times} ; Initial state utility: {state.Utility}");
            Console.Read();
        }
    }
}
