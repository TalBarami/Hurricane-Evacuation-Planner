using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hurricane_Evacuation_Planner.GraphComponents.SimulatorGraphComponents
{
    class MaybeBlockedEdge : Edge
    {
        public double BlockageProbability { get; set; }
        public bool ActuallyBlocked { get; }
        public MaybeBlockedEdge(int id, int v1, int v2, double weight, double blockageProbability) : base(id, v1, v2, weight)
        {
            BlockageProbability = blockageProbability;
            ActuallyBlocked = new Random().NextDouble() <= blockageProbability;
        }

        public MaybeBlockedEdge(int id, IVertex v1, IVertex v2, double weight, double blockageProbability) : this(id, v1.Id, v2.Id, weight, blockageProbability)
        {
        }

        public MaybeBlockedEdge(MaybeBlockedEdge other) : this(other.Id, other.V1, other.V2, other.Weight, other.BlockageProbability)
        {
        }

        public override string ToString()
        {
            return $"{base.ToString()}B{BlockageProbability}";
        }

        public override IEdge Clone()
        {
            return BlockageProbability > 0 ? new MaybeBlockedEdge(this) : base.Clone();
        }
    }
}
