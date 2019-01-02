using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hurricane_Evacuation_Planner.GraphComponents
{
    class Graph : IGraph
    {
        public List<IVertex> Vertices { get; }
        public List<IEdge> Edges { get; }
        public IVertex Vertex(int id) => Vertices[id - 1];
        public IVertex Vertex(IVertex v) => Vertex(v.Id);
        public IEdge Edge(int id) => Edges[id - 1];
        public IEdge Edge(IEdge e) => Edge(e.Id);

        public IEdge Edge(IVertex v1, IVertex v2) =>
            Edges.First(e => (e.V1 == v1.Id || e.V2 == v1.Id) && (e.V1 == v2.Id || e.V2 == v2.Id));

        public Graph(List<IVertex> vertices, List<IEdge> edges)
        {
            Vertices = vertices;
            Edges = edges;

            edges.ForEach(e =>
            {
                var v1 = Vertex(e.V1);
                var v2 = Vertex(e.V2);
                v1.Neighbors.Add(v2);
                v2.Neighbors.Add(v1);
                v1.Connectors.Add(e);
                v2.Connectors.Add(e);
            });
        }

        private Graph(IGraph other) : this(other.Vertices.Select(v => v.Clone()).ToList(), other.Edges.Select(e => e.Clone()).ToList())
        {
        }

        public void Block(IEdge e)
        {
            RemoveEdge(e);
        }

        public IGraph Clone()
        {
            return new Graph(this);
        }

        private void RemoveEdge(IEdge e)
        {
            var v1 = Vertex(e.V1);
            var v2 = Vertex(e.V2);
            v1.Neighbors.Remove(v2);
            v2.Neighbors.Remove(v1);
            v1.Connectors.Remove(e);
            v2.Connectors.Remove(e);
            Edges.Remove(e);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Vertices: {string.Join(", ", Vertices)}");
            sb.AppendLine($"Edges: {string.Join(", ", Edges)}");
            return sb.ToString();
        }

        public List<Path> Dfs(IVertex src, IVertex dst)
        {
            if (src.Equals(dst))
            {
                return new List<Path> { new Path(this, new List<IEdge>{new Edge(-1, src, src, 0)}) };
            }
            var lst = new List<Path>();
            var queue = new Queue<QueueItem>();
            queue.Enqueue(new QueueItem(src, new List<IEdge>()));
            while (queue.Count > 0)
            {
                var currentItem = queue.Dequeue();
                foreach (var edge in currentItem.Node.Connectors)
                {
                    if (!currentItem.Visited.Contains(edge))
                    {
                        var visited = new List<IEdge>(currentItem.Visited) {edge};
                        if (edge.V2 == dst.Id)
                        {
                            lst.Add(new Path(this, visited));
                        }
                        else
                        {
                            queue.Enqueue(new QueueItem(Vertex(edge.V2), visited));
                        }
                    }
                }
            }

            return lst;
        }

        private class QueueItem
        {

            public IVertex Node { get; }
            public List<IEdge> Visited { get; }

            public QueueItem(IVertex node, List<IEdge> visited)
            {
                Node = node;
                Visited = visited;
            }

        }
    }
}
