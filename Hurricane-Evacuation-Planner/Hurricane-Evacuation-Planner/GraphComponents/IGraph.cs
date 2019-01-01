using System.Collections.Generic;

namespace Hurricane_Evacuation_Planner.GraphComponents
{
    public interface IGraph
    {
        List<IVertex> Vertices { get; }
        List<IEdge> Edges { get; }

        IVertex Vertex(int id);
        IVertex Vertex(IVertex v);
        IEdge Edge(int id);
        IEdge Edge(IEdge e);
        IEdge Edge(IVertex v1, IVertex v2);

        List<Path> Dfs(IVertex src, IVertex dst);
        IGraph Block(IEdge e);
        IGraph Clone();
    }
}
