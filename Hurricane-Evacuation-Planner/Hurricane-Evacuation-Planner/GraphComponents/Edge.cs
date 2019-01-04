namespace Hurricane_Evacuation_Planner.GraphComponents
{
    public class Edge : IEdge
    {
        public int Id { get; }
        public string Name => $"E{Id}";
        public int V1 { get; }
        public int V2 { get; }
        public double Weight { get; }

        public Edge(int id, int v1, int v2, double weight)
        {
            Id = id;
            V1 = v1;
            V2 = v2;
            Weight = weight;
        }
        public Edge(int id, IVertex v1, IVertex v2, double weight) : this(id, v1.Id, v2.Id, weight)
        {
        }

        public Edge(Edge other) : this(other.Id, other.V1, other.V2, other.Weight)
        {
        }

        public virtual IEdge Clone()
        {
            return new Edge(this);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Edge)obj);
        }

        protected bool Equals(Edge other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(Edge e1, Edge e2)
        {
            return e1 != null && e1.Equals(e2);
        }

        public static bool operator !=(Edge e1, Edge e2)
        {
            return !(e1 == e2);
        }

        public override string ToString()
        {
            return $"E{Id}({V1},{V2})W{Weight}";
        }
    }
}
