using System;

namespace CyberMath.Structures.Vector.Base.Interface.Point
{
    public interface IPoint : IComparable
    {
        double CalculateDirectionCos();
        double CalculateCosA();
        double CalculateCosB();
        double CalculateAbs();
    }
}
