using CyberMath.Structures.MatrixBase.Exceptions;
using System;
using System.Text;

namespace CyberMath.Structures.MatrixBase
{
    //TODO: unit-test

    /// <summary>
    /// Extension methods for <see cref="IMatrixBase{T}"/>
    /// </summary>
    public static class MatrixBaseExtensions
    {
        #region Saddling

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

        /// <summary>
        /// Returns <see cref="bool"/> value if element at [<paramref name="i"/>, <paramref name="j"/>] is max in <see cref="IMatrixBase{IComparable}"/> matrix column at index <paramref name="j"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="i">Row index</param>
        /// <param name="j">Column index</param>
        /// <returns></returns>
        public static bool IsMaxInColumn(this IMatrixBase<int?> matrix, int i, int j)
        {
            for (var k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] != null && matrix[i, j] != null)
                    if (matrix[k, j] > (matrix[i, j]))
                        return false;
            }
            return true;
        }

        /// <summary>
        /// Returns <see cref="bool"/> value if element at [<paramref name="i"/>, <paramref name="j"/>] is min in <see cref="IMatrixBase{IComparable}"/> matrix row at index <paramref name="i"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="i">Row index</param>
        /// <param name="j">Column index</param>
        /// <returns></returns>
        public static bool IsMinInRow(this IMatrixBase<int?> matrix, int i, int j)
        {
            for (var k = 0; k < matrix.ElementsInRow(i); k++)
            {
                if (matrix[i, k] != null && matrix[i, j] != null)
                    if (matrix[i, k] < (matrix[i, j]))
                        return false;
            }
            return true;
        }

        /// <summary>
        /// Returns <see cref="bool"/> value if element at [<paramref name="i"/>, <paramref name="j"/>] is max in <see cref="IMatrixBase{IComparable}"/> matrix column at index <paramref name="j"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="i">Row index</param>
        /// <param name="j">Column index</param>
        /// <returns></returns>
        public static bool IsMaxInColumn(this IMatrixBase<long?> matrix, int i, int j)
        {
            for (var k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] != null && matrix[i, j] != null)
                    if (matrix[k, j] > (matrix[i, j]))
                        return false;
            }
            return true;
        }

        /// <summary>
        /// Returns <see cref="bool"/> value if element at [<paramref name="i"/>, <paramref name="j"/>] is min in <see cref="IMatrixBase{IComparable}"/> matrix row at index <paramref name="i"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="i">Row index</param>
        /// <param name="j">Column index</param>
        /// <returns></returns>
        public static bool IsMinInRow(this IMatrixBase<long?> matrix, int i, int j)
        {
            for (var k = 0; k < matrix.ElementsInRow(i); k++)
            {
                if (matrix[i, k] != null && matrix[i, j] != null)
                    if (matrix[i, k] < (matrix[i, j]))
                        return false;
            }
            return true;
        }

        /// <summary>
        /// Returns <see cref="bool"/> value if element at [<paramref name="i"/>, <paramref name="j"/>] is max in <see cref="IMatrixBase{IComparable}"/> matrix column at index <paramref name="j"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="i">Row index</param>
        /// <param name="j">Column index</param>
        /// <returns></returns>
        public static bool IsMaxInColumn(this IMatrixBase<double?> matrix, int i, int j)
        {
            for (var k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] != null && matrix[i, j] != null)
                    if (matrix[k, j] > (matrix[i, j]))
                        return false;
            }
            return true;
        }

        /// <summary>
        /// Returns <see cref="bool"/> value if element at [<paramref name="i"/>, <paramref name="j"/>] is min in <see cref="IMatrixBase{IComparable}"/> matrix row at index <paramref name="i"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="i">Row index</param>
        /// <param name="j">Column index</param>
        /// <returns></returns>
        public static bool IsMinInRow(this IMatrixBase<double?> matrix, int i, int j)
        {
            for (var k = 0; k < matrix.ElementsInRow(i); k++)
            {
                if (matrix[i, k] != null && matrix[i, j] != null)
                    if (matrix[i, k] < (matrix[i, j]))
                        return false;
            }
            return true;
        }

        #endregion

        #region Sum

        #region Not Nullable

        #region Main diagonal

        /// <summary>
        /// Calculates sum of all numbers in main diagonal in <see cref="IMatrixBase{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static int DiagonalSum(this IMatrixBase<int> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0;
            for (var i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="DiagonalSum(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></returns>
        public static long DiagonalSum(this IMatrixBase<long> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0L;
            for (var i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="DiagonalSum(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></returns>
        public static double DiagonalSum(this IMatrixBase<double> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0D;
            for (var i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="DiagonalSum(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></returns>
        public static decimal DiagonalSum(this IMatrixBase<decimal> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0M;
            for (var i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="DiagonalSum(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></returns>
        public static int DiagonalSum(this IMatrixBase<short> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0;
            for (var i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        #endregion

        #region Side diagonal

        /// <summary>
        /// Calculates sum of all numbers of side diagonal in <see cref="IMatrixBase{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static int SideDiagonalSum(this IMatrixBase<int> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0;
            for (int i = 0, j = matrix.RowsCount - 1; i < matrix.RowsCount; i++, j--)
                sum += matrix[i, j];
            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="SideDiagonalSum(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="SideDiagonalSum(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></returns>
        public static long SideDiagonalSum(this IMatrixBase<long> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0L;
            for (int i = 0, j = matrix.RowsCount - 1; i < matrix.RowsCount; i++, j--)
                sum += matrix[i, j];
            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="SideDiagonalSum(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="SideDiagonalSum(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></returns>
        public static double SideDiagonalSum(this IMatrixBase<double> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0D;
            for (int i = 0, j = matrix.RowsCount - 1; i < matrix.RowsCount; i++, j--)
                sum += matrix[i, j];
            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="DiagonalSum(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></returns>
        public static decimal SideDiagonalSum(this IMatrixBase<decimal> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0M;
            for (int i = 0, j = matrix.RowsCount - 1; i < matrix.RowsCount; i++, j--)
                sum += matrix[i, j];
            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="DiagonalSum(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></returns>
        public static int SideDiagonalSum(this IMatrixBase<short> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0;
            for (int i = 0, j = matrix.RowsCount - 1; i < matrix.RowsCount; i++, j--)
                sum += matrix[i, j];
            return sum;
        }

        #endregion

        #region Saddle points

        /// <summary>
        /// Calculates sum of all saddle points in matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>Sum of all saddle points in matrix</returns>
        public static int SumSaddlePoints(this IMatrixBase<int> matrix)
        {
            var sum = 0;
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ElementsInRow(i); j++)
                {
                    if (!matrix.IsMinInRow(i, j) || !matrix.IsMaxInColumn(i, j)) continue;
                    sum += matrix[i, j];
                }
            }

            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="SumSaddlePoints(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="SumSaddlePoints(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="SumSaddlePoints(IMatrixBase{int})"/></returns>
        public static long SumSaddlePoints(this IMatrixBase<long> matrix)
        {
            var sum = 0L;
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ElementsInRow(i); j++)
                {
                    if (!matrix.IsMinInRow(i, j) || !matrix.IsMaxInColumn(i, j)) continue;
                    sum += matrix[i, j];
                }
            }

            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="SumSaddlePoints(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="SumSaddlePoints(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="SumSaddlePoints(IMatrixBase{int})"/></returns>
        public static double SumSaddlePoints(this IMatrixBase<double> matrix)
        {
            var sum = 0D;
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ElementsInRow(i); j++)
                {
                    if (!matrix.IsMinInRow(i, j) || !matrix.IsMaxInColumn(i, j)) continue;
                    sum += matrix[i, j];
                }
            }

            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="SumSaddlePoints(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="SumSaddlePoints(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="SumSaddlePoints(IMatrixBase{int})"/></returns>
        public static decimal SumSaddlePoints(this IMatrixBase<decimal> matrix)
        {
            var sum = 0M;
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ElementsInRow(i); j++)
                {
                    if (!matrix.IsMinInRow(i, j) || !matrix.IsMaxInColumn(i, j)) continue;
                    sum += matrix[i, j];
                }
            }

            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="SumSaddlePoints(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="SumSaddlePoints(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="SumSaddlePoints(IMatrixBase{int})"/></returns>
        public static int SumSaddlePoints(this IMatrixBase<short> matrix)
        {
            var sum = 0;
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ElementsInRow(i); j++)
                {
                    if (!matrix.IsMinInRow(i, j) || !matrix.IsMaxInColumn(i, j)) continue;
                    sum += matrix[i, j];
                }
            }

            return sum;
        }

        #endregion

        #endregion

        #region Nullable

        #region Main diagonal

        /// <summary>
        /// Calculates sum of all numbers in main diagonal in <see cref="IMatrixBase{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static int DiagonalSum(this IMatrixBase<int?> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0;
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                if (matrix[i, i] != null)
                    sum += matrix[i, i].Value;
            }

            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="DiagonalSum(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></returns>
        public static long DiagonalSum(this IMatrixBase<long?> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0L;
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                if (matrix[i, i] != null)
                    sum += matrix[i, i].Value;
            }
            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="DiagonalSum(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></returns>
        public static double DiagonalSum(this IMatrixBase<double?> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0D;
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                if (matrix[i, i] != null)
                    sum += matrix[i, i].Value;
            }
            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="DiagonalSum(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></returns>
        public static decimal DiagonalSum(this IMatrixBase<decimal?> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0M;
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                if (matrix[i, i] != null)
                    sum += matrix[i, i].Value;
            }
            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="DiagonalSum(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></returns>
        public static int DiagonalSum(this IMatrixBase<short?> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0;
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                if (matrix[i, i] != null)
                    sum += matrix[i, i].Value;
            }
            return sum;
        }

        /// <summary>
        /// Calculates sum of all string in main diagonal in <see cref="IMatrixBase{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static string DiagonalSum(this IMatrixBase<string> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sb = new StringBuilder();
            for (var i = 0; i < matrix.RowsCount - 1; i++)
                sb.Append(matrix[i, i]).Append(' ');

            sb.Append(matrix[matrix.RowsCount - 1, matrix.RowsCount - 1]);
            return sb.ToString();
        }

        #endregion

        #region Side diagonal

        /// <summary>
        /// Calculates sum of all numbers of side diagonal in <see cref="IMatrixBase{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static int SideDiagonalSum(this IMatrixBase<int?> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0;
            for (int i = 0, j = matrix.RowsCount - 1; i < matrix.RowsCount; i++, j--)
            {
                if (matrix[i, j] != null)
                    sum += matrix[i, j].Value;
            }
            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="SideDiagonalSum(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="SideDiagonalSum(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></returns>
        public static long SideDiagonalSum(this IMatrixBase<long?> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0L;
            for (int i = 0, j = matrix.RowsCount - 1; i < matrix.RowsCount; i++, j--)
            {
                if (matrix[i, j] != null)
                    sum += matrix[i, j].Value;
            }

            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="SideDiagonalSum(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="SideDiagonalSum(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></returns>
        public static double SideDiagonalSum(this IMatrixBase<double?> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0D;
            for (int i = 0, j = matrix.RowsCount - 1; i < matrix.RowsCount; i++, j--)
            {
                if (matrix[i, j] != null)
                    sum += matrix[i, j].Value;
            }
            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="DiagonalSum(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></returns>
        public static decimal SideDiagonalSum(this IMatrixBase<decimal?> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0M;
            for (int i = 0, j = matrix.RowsCount - 1; i < matrix.RowsCount; i++, j--)
            {
                if (matrix[i, j] != null)
                    sum += matrix[i, j].Value;
            }
            return sum;
        }

        /// <summary>
        /// <inheritdoc cref="DiagonalSum(IMatrixBase{int})"/>
        /// </summary>
        /// <param name="matrix"><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></param>
        /// <returns><inheritdoc cref="DiagonalSum(IMatrixBase{int})"/></returns>
        public static int SideDiagonalSum(this IMatrixBase<short?> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0;
            for (int i = 0, j = matrix.RowsCount - 1; i < matrix.RowsCount; i++, j--)
            {
                if (matrix[i, j] != null)
                    sum += matrix[i, j].Value;
            }
            return sum;
        }

        #endregion

        /// <summary>
        /// Calculates sum of all string in <see cref="IMatrixBase{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static string Sum(this IMatrixBase<string> matrix)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ElementsInRow(i); j++)
                {
                    sb.Append(matrix[i, j]).Append(' ');
                }
            }

            return sb.ToString().TrimEnd();
        }

        #endregion

        #region Generic

        #region int

        /// <summary>
        /// Calculate sum of diagonal elements. Only if <see cref="IMatrixBase{T}"/> is square
        /// </summary>
        /// <typeparam name="TSource">Generic type</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="selector">Selector for sum</param>
        /// <returns>Main diagonal sum</returns>
        public static int DiagonalSum<TSource>(this IMatrixBase<TSource> matrix, Func<TSource, int> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            if (!matrix.IsSquare) throw new ArgumentException("Diagonal sum can be calculated only for square matrix (ColumnsCount = RowsCount)");
            var sum = 0;
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                sum += selector(matrix[i, i]);
            }
            return sum;
        }

        /// <summary>
        /// Calculate sum of diagonal elements. Only if <see cref="IMatrixBase{T}"/> is square
        /// </summary>
        /// <typeparam name="TSource">Generic type</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="selector">Selector for sum</param>
        /// <returns>Main diagonal sum</returns>
        public static int DiagonalSum<TSource>(this IMatrixBase<TSource> matrix, Func<TSource, int?> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            if (!matrix.IsSquare) throw new ArgumentException("Diagonal sum can be calculated only for square matrix (ColumnsCount = RowsCount)");
            var sum = 0;
            for (var i = 0; i < matrix.RowsCount; i++)
            {
	            if(matrix[i, i] == null) continue;
	            var value = selector(matrix[i, i]);
	            if(value.HasValue)
		            sum += value.Value;
            }
            return sum;
        }

        #endregion

        #region double

        /// <summary>
        /// Calculate sum of diagonal elements. Only if <see cref="IMatrixBase{T}"/> is square
        /// </summary>
        /// <typeparam name="TSource">Generic type</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="selector">Selector for sum</param>
        /// <returns>Main diagonal sum</returns>
        public static double DiagonalSum<TSource>(this IMatrixBase<TSource> matrix, Func<TSource, double> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            if (!matrix.IsSquare) throw new ArgumentException("Diagonal sum can be calculated only for square matrix (ColumnsCount = RowsCount)");
            double sum = 0;
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                sum += selector(matrix[i, i]);
            }
            return sum;
        }

        /// <summary>
        /// Calculate sum of diagonal elements. Only if <see cref="IMatrixBase{T}"/> is square
        /// </summary>
        /// <typeparam name="TSource">Generic type</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="selector">Selector for sum</param>
        /// <returns>Main diagonal sum</returns>
        public static double DiagonalSum<TSource>(this IMatrixBase<TSource> matrix, Func<TSource, double?> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            if (!matrix.IsSquare) throw new ArgumentException("Diagonal sum can be calculated only for square matrix (ColumnsCount = RowsCount)");
            double sum = 0;
            for (var i = 0; i < matrix.RowsCount; i++)
            {
	            if (matrix[i, i] == null) continue;
	            var value = selector(matrix[i, i]);
	            if (value.HasValue)
		            sum += value.Value;
            }
            return sum;
        }

        #endregion

        #region decimal

        /// <summary>
        /// Calculate sum of diagonal elements. Only if <see cref="IMatrixBase{T}"/> is square
        /// </summary>
        /// <typeparam name="TSource">Generic type</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="selector">Selector for sum</param>
        /// <returns>Main diagonal sum</returns>
        public static decimal DiagonalSum<TSource>(this IMatrixBase<TSource> matrix, Func<TSource, decimal> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            if (!matrix.IsSquare) throw new ArgumentException("Diagonal sum can be calculated only for square matrix (ColumnsCount = RowsCount)");
            decimal sum = 0;
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                sum += selector(matrix[i, i]);
            }
            return sum;
        }

        /// <summary>
        /// Calculate sum of diagonal elements. Only if <see cref="IMatrixBase{T}"/> is square
        /// </summary>
        /// <typeparam name="TSource">Generic type</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="selector">Selector for sum</param>
        /// <returns>Main diagonal sum</returns>
        public static decimal? DiagonalSum<TSource>(this IMatrixBase<TSource> matrix, Func<TSource, decimal?> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            if (!matrix.IsSquare) throw new ArgumentException("Diagonal sum can be calculated only for square matrix (ColumnsCount = RowsCount)");
            decimal sum = 0;
            for (var i = 0; i < matrix.RowsCount; i++)
            {
	            if (matrix[i, i] == null) continue;
	            var value = selector(matrix[i, i]);
	            if (value.HasValue)
		            sum += value.Value;
            }
            return sum;
        }

        #endregion

        #endregion

        #endregion
    }
}