using MatrixBase;

namespace CyberMath.Matrix.Models
{
    public interface IMatrix<T> : IMatrixBase<T>
    {
        int RowsCount { get; }
        int ColumnsCount { get; }
    }
}
