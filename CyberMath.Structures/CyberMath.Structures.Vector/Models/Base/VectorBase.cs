using System;
using System.Collections.Generic;
using System.Text;

namespace CyberMath.Structures.Vector.Models
{
    public abstract class VectorBase : IVector
    {
        public IPoint FirstPoint { get; protected set; }
        public IPoint SecondPoint { get; protected set; }

        protected VectorBase()
        {
            
        }

        public virtual IPoint CalculateCoordinates()
        {
            int x = SecondPoint.X - FirstPoint.X;
            int y = SecondPoint.Y - FirstPoint.Y;
            return new Point(x, y);
        }

        public double CalculateLength()
        {
            int xCalculation = SecondPoint.X - FirstPoint.X;
            xCalculation = (int)Math.Pow(xCalculation, 2);
            int yCalculation = SecondPoint.Y - FirstPoint.Y;
            yCalculation = (int)Math.Pow(yCalculation, 2);
            int supSqrtCalculation = xCalculation + yCalculation;
            return Math.Sqrt(supSqrtCalculation);
        }

        public double CalculateProjection()
        {
            return FirstPoint.ScalarMul(SecondPoint) / SecondPoint.CalculateAbs();
        }

        public double CalculateCos()
        {
            double mul = FirstPoint.ScalarMul(SecondPoint);
            double x = FirstPoint.CalculateAbs();
            double y = SecondPoint.CalculateAbs();
            return mul / (x * y);
        }
    }
}
