using System;
using CyberMath.Structures.Vector.Base.Interface.Point;
using CyberMath.Structures.Vector.Base.Interface.Vector;
using CyberMath.Structures.Vector.Vector3D.Point;

namespace CyberMath.Structures.Vector.Vector3D.Vector
{
    public class DataVector3D<TData> : IVector3D
    {
        public DataPoint3D<TData> FirstPoint { get; }
        public DataPoint3D<TData> SecondPoint { get; }
        public DataPoint3D<TData> ThirdPoint { get; }

        public double CalculateLength()
        {
            throw new NotImplementedException();
        }

        public double CalculateProjection()
        {
            throw new NotImplementedException();
        }

        public double CalculateCos()
        {
            throw new NotImplementedException();
        }

        public IPoint3D CalculateVector()
        {
            throw new NotImplementedException();
        }
    }
}
