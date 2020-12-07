namespace CyberMath.Structures.Vector.Base.Interface.Point
{
    public interface IPoint3D : IPoint
    {
        int X { get; }
        int Y { get; }
        int Z { get; }
        double CalculateY();
    }
}
