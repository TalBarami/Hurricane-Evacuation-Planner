using Hurricane_Evacuation_Planner.Environment;
using Hurricane_Evacuation_Planner.Parser;

namespace Hurricane_Evacuation_Planner
{
    class Program
    {
        static void Main(string[] args)
        {
            var state = FileParser.ParseFile("C:/Users/Tal Barami/Documents/GitHub/Hurricane-Evacuation-Planner/Hurricane-Evacuation-Planner/Hurricane-Evacuation-PlannerTests/resources/file1.txt");
            var simulator = new Simulator(state);
        }
    }
}
