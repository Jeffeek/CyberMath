using System;
using System.Collections.Generic;
using System.Text;

namespace CyberMath.Structures.Vector.Models
{
    public class DataVector<TData> : VectorBase
    {
        public DataVector(int x1, int y1, TData data1, int x2, int y2, TData data2)
        {
            FirstPoint = new DataPoint<TData>(x1, x2, data1);
            SecondPoint = new DataPoint<TData>(x2, y2, data2);
        }
    }
}
