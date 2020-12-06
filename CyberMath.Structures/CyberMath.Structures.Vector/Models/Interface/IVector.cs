using System;
using System.Collections.Generic;
using System.Text;

namespace CyberMath.Structures.Vector.Models
{
    public interface IVector
    {
        IPoint FirstPoint { get; }
        IPoint SecondPoint { get; }
        IPoint CalculateCoordinates();
        double CalculateLength();
        double CalculateProjection();
        double CalculateCos();
    }
}
