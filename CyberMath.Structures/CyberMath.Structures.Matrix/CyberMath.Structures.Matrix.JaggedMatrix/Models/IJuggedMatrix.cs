using CyberMath.Structures.Matrix.MatrixBase;

namespace CyberMath.Structures.Matrix.JaggedMatrix.Models
{
    public interface IJuggedMatrix<T> : IMatrixBase<T>
    {
        public IJuggedMatrix<T> SortRows();
        public IJuggedMatrix<T> SortRowsByDescending();
    }
}
