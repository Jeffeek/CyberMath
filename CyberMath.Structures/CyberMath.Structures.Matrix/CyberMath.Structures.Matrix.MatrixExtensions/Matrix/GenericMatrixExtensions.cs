using System;
using CyberMath.Structures.Matrix.Matrix.Models;

namespace CyberMath.Structures.Matrix.MatrixExtensions.Matrix
{
    public static class GenericMatrixExtensions
    {
        #region Sum

        #region int

        public static int Sum<TSource>(this Matrix<TSource> matrix, Func<TSource, int> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            int sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    sum += selector(matrix[i, j]);
                }
            }

            return sum;
        }

        public static int Sum<TSource>(this Matrix<TSource> matrix, Func<TSource, int?> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            int sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (matrix[i, j] != null)
                        sum += selector(matrix[i, j]).Value;
                }
            }

            return sum;
        }

        #endregion

        #region double

        public static double Sum<TSource>(this Matrix<TSource> matrix, Func<TSource, double> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            double sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    sum += selector(matrix[i, j]);
                }
            }

            return sum;
        }

        public static double Sum<TSource>(this Matrix<TSource> matrix, Func<TSource, double?> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            double sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (matrix[i, j] != null)
                        sum += selector(matrix[i, j]).Value;
                }
            }

            return sum;
        }

        #endregion

        #region decimal

        public static decimal Sum<TSource>(this Matrix<TSource> matrix, Func<TSource, decimal> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            decimal sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    sum += selector(matrix[i, j]);
                }
            }

            return sum;
        }

        public static decimal? Sum<TSource>(this Matrix<TSource> matrix, Func<TSource, decimal?> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            decimal sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (matrix[i, j] != null)
                        sum += selector(matrix[i, j]).Value;
                }
            }

            return sum;
        }

        #endregion

        #endregion

        #region DiagonalSum

        #region int

        public static int DiagonalSum<TSource>(this Matrix<TSource> matrix, Func<TSource, int> selector)
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

        public static int DiagonalSum<TSource>(this Matrix<TSource> matrix, Func<TSource, int?> selector)
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

        public static double DiagonalSum<TSource>(this Matrix<TSource> matrix, Func<TSource, double> selector)
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

        public static double DiagonalSum<TSource>(this Matrix<TSource> matrix, Func<TSource, double?> selector)
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

        public static decimal DiagonalSum<TSource>(this Matrix<TSource> matrix, Func<TSource, decimal> selector)
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
