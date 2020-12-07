using CyberMath.Structures.Vector.Base.Interface.Point;
using CyberMath.Structures.Vector.Base.Interface.Vector;
using CyberMath.Structures.Vector.Vector2D.Point;

namespace CyberMath.Structures.Vector.Vector2D.Vector
{
    public class Vector2D : IVector2D
    {
        public Point2D FirstPoint { get; }
        public Point2D SecondPoint { get; }

        public double CalculateLength()
        {
            throw new System.NotImplementedException();
        }

        public double CalculateProjection()
        {
            throw new System.NotImplementedException();
        }

        public double CalculateCos()
        {
            throw new System.NotImplementedException();
        }

        public IPoint2D CalculateVector()
        {
           
        }
    }
}
