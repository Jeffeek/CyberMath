using CyberMath.Structures.Vector.Base.Interface.Point;

namespace CyberMath.Structures.Vector.Base.Interface.Vector
{
    public interface IVector3D : IVector
    {
        IPoint3D CalculateVector();
    }
}
