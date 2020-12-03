using CyberMath.Structures.Matrix.MatrixBase;

namespace CyberMath.Structures.Matrix.Matrix.Models
{
    public interface IMatrix<T> : IMatrixBase<T>
    {
        int ColumnsCount { get; }
        IMatrix<T> Transpose();
    }
}
