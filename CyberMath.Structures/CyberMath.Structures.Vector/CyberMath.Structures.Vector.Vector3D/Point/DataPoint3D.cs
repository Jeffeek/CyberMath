using System;
using System.Collections.Generic;
using System.Text;
using CyberMath.Structures.Vector.Base.Interface.Point;

namespace CyberMath.Structures.Vector.Vector3D.Point
{
    public class DataPoint3D<TData> : IPoint3D
    {
        public TData Data { get; }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }

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

        public double CalculateAbs()
        {
            throw new NotImplementedException();
        }

        public double CalculateY()
        {
            throw new NotImplementedException();
        }
    }
}
