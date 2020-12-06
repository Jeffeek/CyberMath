using System;
using System.Collections.Generic;
using System.Text;

namespace CyberMath.Structures.Vector.Models
{
    public interface IPoint : IEquatable<IPoint>
    {
        int X { get; }
        int Y { get; }
        double CalculateDirectionCos();
        double CalculateCosA();
        double CalculateCosB();
        double CalculateAbs();
        bool AreOrthogonal(IPoint point);
        IPoint Sub(IPoint point);
        IPoint Add(IPoint point);
        IPoint Mul(int number);
        int ScalarMul(IPoint point);
    }
}
