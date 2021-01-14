using System;

namespace CyberMath.Structures.MatrixBase
{
    //TODO: unit-test
    /// <summary>
    /// Extension methods for <see cref="IMatrixBase{T}"/>
    /// </summary>
    public static class MatrixBaseExtensions
    {
        /// <summary>
        /// Returns <see cref="bool"/> value if element at [<paramref name="i"/>, <paramref name="j"/>] is max in <see cref="IMatrixBase{IComparable}"/> matrix column at index <paramref name="j"/>
        /// </summary>
        /// <typeparam name="T">IComparable type</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="i">Row index</param>
        /// <param name="j">Column index</param>
        /// <returns></returns>
        public static bool IsMaxInColumn<T>(this IMatrixBase<T> matrix, int i, int j) where T : IComparable
        {
            for (var k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] != null && matrix[i, j] != null)
                    if (matrix[k, j].CompareTo(matrix[i, j]) == 1)
                        return false;
            }
            return true;
        }

        /// <summary>
        /// Returns <see cref="bool"/> value if element at [<paramref name="i"/>, <paramref name="j"/>] is min in <see cref="IMatrixBase{IComparable}"/> matrix row at index <paramref name="i"/>
        /// </summary>
        /// <typeparam name="T">IComparable type</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="i">Row index</param>
        /// <param name="j">Column index</param>
        /// <returns></returns>
        public static bool IsMinInRow<T>(this IMatrixBase<T> matrix, int i, int j) where T : IComparable
        {
            for (var k = 0; k < matrix.ElementsInRow(i); k++)
            {
                if (matrix[i, k] != null && matrix[i, j] != null)
                    if (matrix[i, k].CompareTo(matrix[i, j]) == -1)
                        return false;
            }
            return true;
        }
    }
}
