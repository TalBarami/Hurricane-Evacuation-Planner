﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hurricane_Evacuation_Planner.GraphComponents.SimulatorGraphComponents
{
    public class MaybeBlockedEdge : Edge
    {
        public double BlockageProbability { get; set; }
        public bool ActuallyBlocked { get; set; }
        public MaybeBlockedEdge(int id, int v1, int v2, double weight, double blockageProbability) : base(id, v1, v2, weight)
        {
            BlockageProbability = blockageProbability;
        }

        public MaybeBlockedEdge(int id, IVertex v1, IVertex v2, double weight, double blockageProbability) : this(id, v1.Id, v2.Id, weight, blockageProbability)
        {
        }

        public MaybeBlockedEdge(MaybeBlockedEdge other) : this(other.Id, other.V1, other.V2, other.Weight, other.BlockageProbability)
        {
        }

        public override string ToString()
        {
            var b = ActuallyBlocked ? "(B)" : "(F)";
            return $"{base.ToString()}B{BlockageProbability}{b}";
        }

        public override IEdge Clone()
        {
            return BlockageProbability > 0 ? new MaybeBlockedEdge(this) : base.Clone();
        }
    }
}
