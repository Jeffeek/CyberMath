using CyberMath.Structures.Matrices.Base;

namespace CyberMath.Structures.Matrices.Matrix
{
    /// <summary>
    /// Interface for vanilla Matrix. Implements <see cref="IMatrixBase{T}"/>
    /// </summary>
    /// <typeparam name="T">ANY</typeparam>
    public interface IMatrix<T> : IMatrixBase<T>
    {
        /// <summary>
        /// Count of columns in <see cref="IMatrix{T}"/>
        /// </summary>
        int ColumnsCount { get; }
        /// <summary>
        /// Creates a new <see cref="IMatrix{T}"/> transposed
        /// </summary>
        /// <returns>New <see cref="IMatrix{T}"/> instance</returns>
        IMatrix<T> Transpose();
    }
}
