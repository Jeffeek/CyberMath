using CyberMath.Structures.MatrixBase;

namespace CyberMath.Structures.Matrix
{
    public interface IMatrix<T> : IMatrixBase<T>
    {
        int ColumnsCount { get; }
        IMatrix<T> Transpose();
    }
}
