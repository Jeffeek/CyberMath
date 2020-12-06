using System;
using System.Collections.Generic;
using System.Text;

namespace CyberMath.Structures.Vector.Models
{
    public sealed class DataPoint<TData> : PointBase
    {
        public TData Data { get; set; }

        public override string ToString() => $"({X};{Y}) - {Data}";

        public DataPoint(int x, int y, TData data) : base(x, y)
        {
            Data = data;
        }
    }
}
