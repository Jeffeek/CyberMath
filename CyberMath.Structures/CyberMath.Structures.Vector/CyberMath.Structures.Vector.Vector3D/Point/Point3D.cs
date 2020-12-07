using System;
using CyberMath.Structures.Vector.Base.Interface.Point;

namespace CyberMath.Structures.Vector.Vector3D.Point
{
    public class Point3D : IPoint3D
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        public double CalculateDirectionCos()
        {
            throw new NotImplementedException();
        }

        public double CalculateCosA()
        {
            return X / CalculateAbs();
        }

        public double CalculateCosB()
        {
            return Y / CalculateAbs();
        }

        public double CalculateY()
        {
            return Z / CalculateAbs();
        }

        public IPoint3D CalculateCoordinates()
        {
            return new Point3D();
        }

        public double CalculateAbs()
        {
            throw new NotImplementedException();
        }


    }
}
