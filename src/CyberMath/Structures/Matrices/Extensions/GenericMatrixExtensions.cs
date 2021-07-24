#region Using namespaces

using System;
using CyberMath.Structures.Matrices.Matrix;

#endregion

namespace CyberMath.Structures.Matrices.Extensions
{
    /// <summary>
    ///     Extension methods only for <see cref="IMatrix{T}"/>
    /// </summary>
    public static class GenericMatrixExtensions
    {
        #region Removing Rows and Columns

        /// <summary>
        ///     Creates a new <see cref="IMatrix{T}"/> matrix without column at <paramref name="columnIndex"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="columnIndex">Column index to remove</param>
        /// <returns>A new <see cref="IMatrix{T}"/> matrix without column at <paramref name="columnIndex"/></returns>
        public static IMatrix<T> CreateMatrixWithoutColumn<T>(this IMatrix<T> matrix, int columnIndex)
        {
            if (ReferenceEquals(matrix, null)) throw new ArgumentNullException(nameof(matrix));
            if (columnIndex < 0 || columnIndex >= matrix.ColumnsCount) throw new ArgumentException("invalid column index");

            var result = new Matrix<T>(matrix.RowsCount, matrix.ColumnsCount - 1);

            result.ProcessFunctionOverData((i, j) =>
                                               result[i, j] = j < columnIndex ? matrix[i, j] : matrix[i, j + 1]);

            return result;
        }

        /// <summary>
        ///     Creates a new <see cref="IMatrix{T}"/> matrix without row at <paramref name="rowIndex"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="rowIndex">Column index to remove</param>
        /// <returns>A new <see cref="IMatrix{T}"/> matrix without row at <paramref name="rowIndex"/></returns>
        public static IMatrix<T> CreateMatrixWithoutRow<T>(this IMatrix<T> matrix, int rowIndex)
        {
            if (ReferenceEquals(matrix, null)) throw new ArgumentNullException(nameof(matrix));
            if (rowIndex < 0 || rowIndex >= matrix.RowsCount) throw new ArgumentException("invalid row index");

            var result = new Matrix<T>(matrix.RowsCount - 1, matrix.ColumnsCount);

            result.ProcessFunctionOverData((i, j) =>
                                               result[i, j] = i < rowIndex ? matrix[i, j] : matrix[i + 1, j]);

            return result;
        }

        #endregion
    }
}