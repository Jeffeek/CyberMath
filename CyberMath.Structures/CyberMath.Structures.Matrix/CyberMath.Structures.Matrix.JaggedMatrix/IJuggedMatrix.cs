using CyberMath.Structures.MatrixBase;

namespace CyberMath.Structures.JaggedMatrix
{
    public interface IJuggedMatrix<T> : IMatrixBase<T>
    {
        public IJuggedMatrix<T> SortRows();
        public IJuggedMatrix<T> SortRowsByDescending();
    }
}
