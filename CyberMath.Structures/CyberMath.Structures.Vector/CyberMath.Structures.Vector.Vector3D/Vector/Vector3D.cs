using System;
using CyberMath.Structures.Vector.Base.Interface.Point;
using CyberMath.Structures.Vector.Base.Interface.Vector;

namespace CyberMath.Structures.Vector.Vector3D.Vector
{
    public class Vector3D : IVector3D
    {
        public IPoint3D FirstPoint { get; }
        public IPoint3D SecondPoint { get; }
        public IPoint3D ThirdPoint { get; }

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
