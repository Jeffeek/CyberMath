using CyberMath.Structures.Matrix;
using System;

namespace CyberMath.Structures.MatrixExtensions.Matrix
{
    /// <summary>
    /// Extension methods for <see cref="IMatrix{T}"/> sum for <see langword="Generic"/></summary>
    public static class GenericMatrixExtensions
    {
        #region DiagonalSum

        #region int

        /// <summary>
        /// Calculate sum of diagonal elements. Only if <see cref="IMatrix{T}"/> is square
        /// </summary>
        /// <typeparam name="TSource">Generic type</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="selector">Selector for sum</param>
        /// <returns>Main diagonal sum</returns>
        public static int DiagonalSum<TSource>(this IMatrix<TSource> matrix, Func<TSource, int> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            if (!matrix.IsSquare) throw new ArgumentException("Diagonal sum can be calculated only for square matrix (ColumnsCount = RowsCount)");
            int sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                sum += selector(matrix[i, i]);
            }
            return sum;
        }

        /// <summary>
        /// Calculate sum of diagonal elements. Only if <see cref="IMatrix{T}"/> is square
        /// </summary>
        /// <typeparam name="TSource">Generic type</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="selector">Selector for sum</param>
        /// <returns>Main diagonal sum</returns>
        public static int DiagonalSum<TSource>(this IMatrix<TSource> matrix, Func<TSource, int?> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            if (!matrix.IsSquare) throw new ArgumentException("Diagonal sum can be calculated only for square matrix (ColumnsCount = RowsCount)");
            int sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                if (matrix[i, i] != null)
                    sum += selector(matrix[i, i]).Value;
            }
            return sum;
        }

        #endregion

        #region double

        /// <summary>
        /// Calculate sum of diagonal elements. Only if <see cref="IMatrix{T}"/> is square
        /// </summary>
        /// <typeparam name="TSource">Generic type</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="selector">Selector for sum</param>
        /// <returns>Main diagonal sum</returns>
        public static double DiagonalSum<TSource>(this IMatrix<TSource> matrix, Func<TSource, double> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            if (!matrix.IsSquare) throw new ArgumentException("Diagonal sum can be calculated only for square matrix (ColumnsCount = RowsCount)");
            double sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                sum += selector(matrix[i, i]);
            }
            return sum;
        }

        /// <summary>
        /// Calculate sum of diagonal elements. Only if <see cref="IMatrix{T}"/> is square
        /// </summary>
        /// <typeparam name="TSource">Generic type</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="selector">Selector for sum</param>
        /// <returns>Main diagonal sum</returns>
        public static double DiagonalSum<TSource>(this IMatrix<TSource> matrix, Func<TSource, double?> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            if (!matrix.IsSquare) throw new ArgumentException("Diagonal sum can be calculated only for square matrix (ColumnsCount = RowsCount)");
            double sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                if (matrix[i, i] != null)
                    sum += selector(matrix[i, i]).Value;
            }
            return sum;
        }

        #endregion

        #region decimal

        /// <summary>
        /// Calculate sum of diagonal elements. Only if <see cref="IMatrix{T}"/> is square
        /// </summary>
        /// <typeparam name="TSource">Generic type</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="selector">Selector for sum</param>
        /// <returns>Main diagonal sum</returns>
        public static decimal DiagonalSum<TSource>(this IMatrix<TSource> matrix, Func<TSource, decimal> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            if (!matrix.IsSquare) throw new ArgumentException("Diagonal sum can be calculated only for square matrix (ColumnsCount = RowsCount)");
            decimal sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                sum += selector(matrix[i, i]);
            }
            return sum;
        }

        /// <summary>
        /// Calculate sum of diagonal elements. Only if <see cref="IMatrix{T}"/> is square
        /// </summary>
        /// <typeparam name="TSource">Generic type</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="selector">Selector for sum</param>
        /// <returns>Main diagonal sum</returns>
        public static decimal? DiagonalSum<TSource>(this Matrix<TSource> matrix, Func<TSource, decimal?> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            if (!matrix.IsSquare) throw new ArgumentException("Diagonal sum can be calculated only for square matrix (ColumnsCount = RowsCount)");
            decimal sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                if (matrix[i, i] != null)
                    sum += selector(matrix[i, i]).Value;
            }
            return sum;
        }

        #endregion

        #endregion
    }
}
