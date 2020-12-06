using System;
using System.Collections.Generic;
using System.Text;

namespace CyberMath.Structures.Vector.Models
{
    public class Vector : VectorBase
    {
        public Vector(int x1, int y1, int x2, int y2)
        {
            FirstPoint = new Point(x1, y1);
            SecondPoint = new Point(x2, y2);
        }
    }
}
