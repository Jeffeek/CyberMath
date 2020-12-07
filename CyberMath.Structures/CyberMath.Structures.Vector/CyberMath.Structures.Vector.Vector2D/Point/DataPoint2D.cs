
using CyberMath.Structures.Vector.Base.Interface;
using CyberMath.Structures.Vector.Base.Interface.Point;

namespace CyberMath.Structures.Vector.Vector2D.Point
{
    public class DataPoint2D<TData> : IPoint2D
    {
        public TData Data { get; }
        public int X { get; }
        public int Y { get; }

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
            throw new System.NotImplementedException();
        }

        public double CalculateCosB()
        {
            throw new System.NotImplementedException();
        }

        public double CalculateAbs()
        {
            throw new System.NotImplementedException();
        }

    }
}
