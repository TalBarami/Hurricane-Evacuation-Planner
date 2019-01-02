using Hurricane_Evacuation_Planner.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hurricane_Evacuation_PlannerTests.Parser
{
    [TestClass()]
    public class FileParserTests
    {
        [TestMethod()]
        public void ParseFileTest()
        {
            var state = FileParser.ParseFile("../../resources/file1.txt");
        }
    }
}