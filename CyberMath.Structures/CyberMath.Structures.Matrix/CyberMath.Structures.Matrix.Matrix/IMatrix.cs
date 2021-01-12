using CyberMath.Structures.MatrixBase;

namespace CyberMath.Structures.Matrix
{
    //TODO: summary
    public interface IMatrix<T> : IMatrixBase<T>
    {
        int ColumnsCount { get; }
        IMatrix<T> Transpose();
    }
}
