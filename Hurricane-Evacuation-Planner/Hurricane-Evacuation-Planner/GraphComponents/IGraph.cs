using System.Collections.Generic;
using Hurricane_Evacuation_Planner.GraphComponents.SimulatorGraphComponents;

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
        IEdge Edge(IVertex v1, int v2);
        IEdge Edge(int v1, IVertex v2);
        IEdge Edge(int v1, int v2);

        List<Path> Dfs(IVertex src, IVertex dst);
        void Block(IEdge e);
        void Free(IEdge edge);
        void Update(IVertex v);
        IGraph Clone();
    }
}
