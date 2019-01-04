using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hurricane_Evacuation_Planner.GraphComponents.SimulatorGraphComponents;

namespace Hurricane_Evacuation_Planner.GraphComponents
{
    class Graph : IGraph
    {
        public List<IVertex> Vertices { get; }
        public List<IEdge> Edges { get; }
        public IVertex Vertex(int id) => Vertices.First(v => v.Id == id);
        public IVertex Vertex(IVertex v) => Vertex(v.Id);
        public IEdge Edge(int id) => Edges.First(e => e.Id == id);
        public IEdge Edge(IEdge e) => Edge(e.Id);

        public IEdge Edge(IVertex v1, IVertex v2) => Edge(v1.Id, v2.Id);
        public IEdge Edge(IVertex v1, int v2) => Edge(v1.Id, v2);
        public IEdge Edge(int v1, IVertex v2) => Edge(v1, v2.Id);
        public IEdge Edge(int v1, int v2) => Edges.First(e => (e.V1 == v1 || e.V2 == v1) && (e.V1 == v2 || e.V2 == v2));

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
            e = Edge(e);
            var v1 = Vertex(e.V1);
            var v2 = Vertex(e.V2);
            v1.Neighbors.Remove(v2);
            v2.Neighbors.Remove(v1);
            v1.Connectors.Remove(e);
            v2.Connectors.Remove(e);
            Edges.Remove(e);
            Edges.Sort((e1, e2) => e1.Id.CompareTo(e2.Id));
        }

        public void Free(IEdge e)
        {
            Block(e);
            e = new Edge(e.Id, e.V1, e.V2, e.Weight);
            var v1 = Vertex(e.V1);
            var v2 = Vertex(e.V2);
            v1.Neighbors.Add(v2);
            v2.Neighbors.Add(v1);
            v1.Connectors.Add(e);
            v2.Connectors.Add(e);
            Edges.Add(e);
            Edges.Sort((e1, e2) => e1.Id.CompareTo(e2.Id));
        }

        public void Update(IVertex v)
        {
            v = Vertex(v);
            if (v is EvacuationVertex ev && ev.Evacuees == 0)
            {
                var newV = v.Clone();
                v.Neighbors.ForEach(n =>
                {
                    n.Neighbors.Remove(v);
                    n.Neighbors.Add(newV);
                    newV.Neighbors.Add(n);
                });
                Vertices.Remove(v);
                Vertices.Add(newV);
                Vertices.Sort((v1, v2) => v1.Id.CompareTo(v2.Id));
            }
        }

        public IGraph Clone()
        {
            return new Graph(this);
        }
        
        public override string ToString()
        {
            return $"Vertices: {string.Join(", ", Vertices)}; Edges: {string.Join(", ", Edges)}";
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
