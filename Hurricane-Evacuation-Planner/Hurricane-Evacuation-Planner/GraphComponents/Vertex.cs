using System.Collections.Generic;
using Hurricane_Evacuation_Planner.AgentComponents;

namespace Hurricane_Evacuation_Planner.GraphComponents
{
    internal class Vertex : IVertex
    {
        public int Id { get; }
        public string Name => $"V{Id.ToString()}";
        public List<IEdge> Connectors { get; }
        public List<IVertex> Neighbors { get; }
        
        public Vertex(int id)
        {
            Id = id;
            Connectors = new List<IEdge>();
            Neighbors = new List<IVertex>();
        }

        public Vertex(IVertex other) : this(other.Id)
        {
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Vertex)obj);
        }

        protected bool Equals(Vertex other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(Vertex v1, Vertex v2)
        {
            return v1 != null && v1.Equals(v2);
        }

        public static bool operator !=(Vertex v1, Vertex v2)
        {
            return !(v1 == v2);
        }

        public virtual IVertex Clone()
        {
            return new Vertex(this);
        }

        public virtual void Accept(IAgent agent)
        {
        }

        public override string ToString()
        {
            return $"V{Id}";
        }
    }
}
