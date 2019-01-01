using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hurricane_Evacuation_Planner.AgentComponents;
using Hurricane_Evacuation_Planner.Environment;
using Hurricane_Evacuation_Planner.GraphComponents;
using Hurricane_Evacuation_Planner.GraphComponents.Vertices;

namespace Hurricane_Evacuation_Planner.Parser
{
    class FileParser
    {
        private const string Vertex = "#V";
        private const string Edge = "#E";
        private const string Evacuees = "#P";
        private const string Start = "#Start";
        private const string Shelter = "#Shelter";
        private const string Deadline = "#Deadline";
        
        private const string Blockage = "B";
        private const string People = "P";

        private const char Whitespace = ' ';
        private const char Comment = ';';

        public IState ParseFile(string path)
        {
            var lines = File.ReadAllLines(path).Select(line => line.Split(Comment)[0].Trim()).Where(line => !string.IsNullOrEmpty(line)).ToList();

            var graph = CreateGraph(lines);

            var start = int.Parse(lines.First(line => line.StartsWith(Start)).Split(Whitespace)[1]);
            var agent = new Agent(graph.Vertex(start));

            var deadline = int.Parse(lines.First(line => line.StartsWith(Deadline)).Split(Whitespace)[1]);
            return new State(graph, agent);
        }

        private IGraph CreateGraph(List<string> lines)
        {
            var verticesCount = int.Parse(lines.First(l => l.StartsWith(Vertex)).Split(Whitespace)[1]);
            var evacuees = lines.Where(l => l.StartsWith(Evacuees)).Select(l => l.Split(Whitespace)).ToDictionary(l => int.Parse(l[1]), l => int.Parse(l[2]));
            var shelter = int.Parse(lines.First(line => line.StartsWith(Shelter)).Split(Whitespace)[1]);

            var vertices = ParseVertices(verticesCount, evacuees, shelter);
            var edges = ParseEdges(lines.Where(l => l.StartsWith(Edge)).Select(l => l.Split(Whitespace)).ToList());
            return new Graph(vertices, edges);
        }


        private List<IVertex> ParseVertices(int count, Dictionary<int, int> evacuees, int shelter)
        {
            var vertices = new List<IVertex>();
            for (var i = 0; i < count; i++)
            {
                var vid = i + 1;

                if (vid == shelter)
                {
                    vertices.Add(new ShelterVertex(vid));
                } else if (evacuees.ContainsKey(vid))
                {
                    vertices.Add(new EvacuationVertex(vid, evacuees[vid]));
                }
                else
                {
                    vertices.Add(new Vertex(vid));
                }
            }

            return vertices;
        }

        private List<IEdge> ParseEdges(List<string[]> edgesData)
        {
            var edges = new List<IEdge>();
            foreach (var eData in edgesData)
            {
                var eid = int.Parse(eData[1]);
                var v1 = int.Parse(eData[2]);
                var v2 = int.Parse(eData[3]);
                var w = int.Parse(eData[4]);
                var b = 0.0f;
                if (eData.Contains(Blockage))
                {
                    b = int.Parse(eData[6]);
                }

                edges.Add(new Edge(eid, v1, v2, w, b));
            }

            return edges;
        }
    }
}
