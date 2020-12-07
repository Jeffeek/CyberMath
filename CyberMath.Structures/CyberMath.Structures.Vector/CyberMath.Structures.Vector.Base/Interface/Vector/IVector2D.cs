using CyberMath.Structures.Vector.Base.Interface.Point;

namespace CyberMath.Structures.Vector.Base.Interface.Vector
{
    public interface IVector2D : IVector
    {
        IPoint2D CalculateVector();
    }
}
