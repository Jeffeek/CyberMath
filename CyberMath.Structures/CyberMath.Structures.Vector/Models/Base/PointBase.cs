using System;
using System.Collections.Generic;
using System.Text;

namespace CyberMath.Structures.Vector.Models
{
    public abstract class PointBase : IPoint
    {
        public int X { get; }
        public int Y { get; }

        public double CalculateDirectionCos()
        {
            return X / CalculateAbs();
        }

        public double CalculateCosA()
        {
            double abs = CalculateAbs();
            return X / abs;
        }

        public double CalculateCosB()
        {
            double abs = CalculateAbs();
            return Y / abs;
        }

        public double CalculateAbs()
        {
            int x = (int)Math.Pow(X, 2);
            int y = (int) Math.Pow(Y, 2);
            return Math.Sqrt(x + y);
        }

        public bool AreOrthogonal(IPoint point)
        {
            int xCalc = X * point.X;
            int yCalc = Y * point.Y;
            return xCalc + yCalc == 0;
        }

        public static IPoint operator +(PointBase firstPoint, PointBase secondPoint)
        {
            return firstPoint.Add(secondPoint);
        }

        public static IPoint operator -(PointBase firstPoint, PointBase secondPoint)
        {
            return firstPoint.Sub(secondPoint);
        }

        public static int operator *(PointBase firstPoint, PointBase secondPoint)
        {
            return firstPoint.ScalarMul(secondPoint);
        }

        public static IPoint operator *(PointBase firstPoint, int number)
        {
            return firstPoint.Mul(number);
        }

        public static bool operator ==(PointBase firstPoint, PointBase secondPoint)
        {
            return firstPoint.Equals(secondPoint);
        }

        public static bool operator !=(PointBase firstPoint, PointBase secondPoint)
        {
            return !firstPoint.Equals(secondPoint);
        }

        public IPoint Sub(IPoint point)
        {
            return new Point(X - point.X, Y - point.Y);
        }

        public IPoint Add(IPoint point)
        {
            return new Point(X + point.X, Y + point.Y);
        }

        public int ScalarMul(IPoint point)
        {
            int x = X * point.X;
            int y = Y * point.Y;
            return x + y;
        }

        public IPoint Mul(int number)
        {
            int x = X * number;
            int y = Y * number;
            return new Point(x, y);
        }

        protected PointBase(int x, int y)
        {
            X = x;
            Y = y;
        }

        public virtual bool Equals(IPoint other)
        {
            return X == other?.X && Y == other?.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((IPoint) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
