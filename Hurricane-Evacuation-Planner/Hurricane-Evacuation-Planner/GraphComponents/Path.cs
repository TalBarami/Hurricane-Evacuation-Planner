using System.Collections.Generic;
using System.Linq;

namespace Hurricane_Evacuation_Planner.GraphComponents
{
    public class Path
    {
        public List<IVertex> Vertices { get; }
        public List<IEdge> Edges { get; }
        public double Weight { get; }

        public IVertex First => Vertices[0];
        public IVertex Last => Vertices[Vertices.Count - 1];

        private readonly IGraph graph;

        public Path(IGraph graph, List<IEdge> edges)
        {
            Edges = edges;
            Vertices = Edges.Select(e => graph.Vertex(e.V1)).ToList();
            Vertices.Add(graph.Vertex(Edges.Last().V2));
            Weight = Edges.Aggregate(0.0, (sum, cur) => sum + cur.Weight);
            this.graph = graph;
        }

        public override string ToString()
        {
            return string.Join(" -> ", Vertices);
        }
    }
}
