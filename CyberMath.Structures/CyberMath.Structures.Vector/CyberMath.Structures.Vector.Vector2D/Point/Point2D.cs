using CyberMath.Structures.Vector.Base.Interface.Point;

namespace CyberMath.Structures.Vector.Vector2D.Point
{
    public class Point2D : IPoint2D
    {
        public int X { get; }
        public int Y { get; }

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public IPoint2D CalculateCoordinates()
        {
            return new Point2D();
        }

        public int CompareTo(object? obj)
        {
            throw new System.NotImplementedException();
        }

        public double CalculateDirectionCos()
        {
            throw new System.NotImplementedException();
        }

        public double CalculateCosA()
        {
            return X / CalculateAbs();
        }

        public double CalculateCosB()
        {
            return Y / CalculateAbs();
        }

        public double CalculateAbs()
        {
            throw new System.NotImplementedException();
        }
    }
}
