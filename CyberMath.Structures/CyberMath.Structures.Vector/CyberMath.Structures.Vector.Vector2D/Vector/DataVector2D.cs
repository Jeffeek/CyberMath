using CyberMath.Structures.Vector.Base;
using CyberMath.Structures.Vector.Base.Base;
using CyberMath.Structures.Vector.Base.Interface;
using CyberMath.Structures.Vector.Base.Interface.Point;
using CyberMath.Structures.Vector.Base.Interface.Vector;
using CyberMath.Structures.Vector.Vector2D.Point;

namespace CyberMath.Structures.Vector.Vector2D.Vector
{
    public class DataVector2D<TData> : IVector2D
    {
        public DataPoint2D<TData> FirstPoint { get; }
        public DataPoint2D<TData> SecondPoint { get; }

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
            throw new System.NotImplementedException();
        }
    }
}
