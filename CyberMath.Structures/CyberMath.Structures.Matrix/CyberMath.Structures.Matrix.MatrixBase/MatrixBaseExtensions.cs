using System;
using System.Collections.Generic;
using CyberMath.Structures.MatrixBase.Exceptions;

namespace CyberMath.Structures.MatrixBase
{
    //TODO: unit-test
    
    /// <summary>
    /// Extension methods for <see cref="IMatrixBase{T}"/>
    /// </summary>
    public static class MatrixBaseExtensions
    {
        #region IComparable
        
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
			for(var i = 0; i < matrix.RowsCount; i++)
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
			for(var i = 0; i < matrix.RowsCount; i++)
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
			for(var i = 0; i < matrix.RowsCount; i++)
			{
				if (matrix[i, i] != null)
					sum += matrix[i, i].Value;
			}
			return sum;
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
			for(int i = 0, j = matrix.RowsCount - 1; i < matrix.RowsCount; i++, j--)
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

		#endregion

		#endregion

		#region AvgInRow

		/// <summary>
		/// Returns <see cref="IEnumerable{T}"/> collection with the average of all elements in each row in <see cref="IMatrixBase{T}"/>
		/// </summary>
		/// <param name="matrix">initial matrix</param>
		/// <returns><see cref="IEnumerable{T}"/> collection</returns>
		public static IEnumerable<double> AvgInEachRow(this IMatrixBase<int> matrix)
        {
	        for (var i = 0; i < matrix.RowsCount; i++)
	        {
		        var sum = 0;
		        for (var j = 0; j < matrix.ElementsInRow(i); j++)
		        {
			        sum += matrix[i, j];
		        }

		        yield return (double)sum / matrix.ElementsInRow(i);
	        }
        }

		/// <summary>
		///     <inheritdoc cref="AvgInEachRow(IMatrixBase{int})" />
		/// </summary>
		/// <param name="matrix">
		///     <inheritdoc cref="AvgInEachRow(IMatrixBase{int})" />
		/// </param>
		/// <returns>
		///     <inheritdoc cref="AvgInEachRow(IMatrixBase{int})" />
		/// </returns>
		public static IEnumerable<double> AvgInEachRow(this IMatrixBase<double> matrix)
        {
	        for (var i = 0; i < matrix.RowsCount; i++)
	        {
		        var sum = 0.0d;
		        for (var j = 0; j < matrix.ElementsInRow(i); j++)
		        {
			        sum += matrix[i, j];
		        }

		        yield return sum / matrix.ElementsInRow(i);
	        }
        }

		/// <summary>
		///     <inheritdoc cref="AvgInEachRow(IMatrixBase{int})" />
		/// </summary>
		/// <param name="matrix">
		///     <inheritdoc cref="AvgInEachRow(IMatrixBase{int})" />
		/// </param>
		/// <returns>
		///     <inheritdoc cref="AvgInEachRow(IMatrixBase{int})" />
		/// </returns>
		public static IEnumerable<double> AvgInEachRow(this IMatrixBase<long> matrix)
        {
	        for (var i = 0; i < matrix.RowsCount; i++)
	        {
		        var sum = 0L;
		        for (var j = 0; j < matrix.ElementsInRow(i); j++)
		        {
			        sum += matrix[i, j];
		        }

		        yield return (double)sum / matrix.ElementsInRow(i);
	        }
        }

		/// <summary>
		///     <inheritdoc cref="AvgInEachRow(IMatrixBase{int})" />
		/// </summary>
		/// <param name="matrix">
		///     <inheritdoc cref="AvgInEachRow(IMatrixBase{int})" />
		/// </param>
		/// <returns>
		///     <inheritdoc cref="AvgInEachRow(IMatrixBase{int})" />
		/// </returns>
		public static IEnumerable<decimal> AvgInEachRow(this IMatrixBase<decimal> matrix)
        {
	        for (var i = 0; i < matrix.RowsCount; i++)
	        {
		        var sum = 0.0M;
		        for (var j = 0; j < matrix.ElementsInRow(i); j++)
		        {
			        sum += matrix[i, j];
		        }

		        yield return sum / matrix.ElementsInRow(i);
	        }
        }

		/// <summary>
		///     <inheritdoc cref="AvgInEachRow(IMatrixBase{int})" />
		/// </summary>
		/// <param name="matrix">
		///     <inheritdoc cref="AvgInEachRow(IMatrixBase{int})" />
		/// </param>
		/// <returns>
		///     <inheritdoc cref="AvgInEachRow(IMatrixBase{int})" />
		/// </returns>
		public static IEnumerable<double> AvgInEachRow(this IMatrixBase<short> matrix)
        {
	        for (var i = 0; i < matrix.RowsCount; i++)
	        {
		        var sum = 0;
		        for (var j = 0; j < matrix.ElementsInRow(i); j++)
		        {
			        sum += matrix[i, j];
		        }

		        yield return (double)sum / matrix.ElementsInRow(i);
	        }
        }

		#endregion

		#region MinInRow

		/// <summary>
		/// Returns <see cref="IEnumerable{T}"/> collection with the minimum element in each row in <see cref="IMatrixBase{T}"/>
		/// </summary>
		/// <param name="matrix">initial matrix</param>
		/// <returns><see cref="IEnumerable{T}"/> collection</returns>
		public static IEnumerable<double> MinInEachRow(this IMatrixBase<int> matrix)
		{
			for (var i = 0; i < matrix.RowsCount; i++)
			{
				var min = matrix[i, 0];
				for (var j = 1; j < matrix.ElementsInRow(i); j++)
				{
					if (min > matrix[i, j])
						min = matrix[i, j];
				}

				yield return min;
			}
		}

        /// <summary>
        ///     <inheritdoc cref="MinInEachRow(IMatrixBase{int})" />
        /// </summary>
        /// <param name="matrix">
        ///     <inheritdoc cref="MinInEachRow(IMatrixBase{int})" />
        /// </param>
        /// <returns>
        ///     <inheritdoc cref="MinInEachRow(IMatrixBase{int})" />
        /// </returns>
        public static IEnumerable<double> MinInEachRow(this IMatrixBase<double> matrix)
		{
			for(var i = 0; i < matrix.RowsCount; i++)
			{
				var min = matrix[i, 0];
				for(var j = 1; j < matrix.ElementsInRow(i); j++)
				{
					if(min > matrix[i, j])
						min = matrix[i, j];
				}

				yield return min;
			}
		}

        /// <summary>
        ///     <inheritdoc cref="MinInEachRow(IMatrixBase{int})" />
        /// </summary>
        /// <param name="matrix">
        ///     <inheritdoc cref="MinInEachRow(IMatrixBase{int})" />
        /// </param>
        /// <returns>
        ///     <inheritdoc cref="MinInEachRow(IMatrixBase{int})" />
        /// </returns>
        public static IEnumerable<long> MinInEachRow(this IMatrixBase<long> matrix)
		{
			for(var i = 0; i < matrix.RowsCount; i++)
			{
				var min = matrix[i, 0];
				for(var j = 1; j < matrix.ElementsInRow(i); j++)
				{
					if(min > matrix[i, j])
						min = matrix[i, j];
				}

				yield return min;
			}
		}

        /// <summary>
        ///     <inheritdoc cref="MinInEachRow(IMatrixBase{int})" />
        /// </summary>
        /// <param name="matrix">
        ///     <inheritdoc cref="MinInEachRow(IMatrixBase{int})" />
        /// </param>
        /// <returns>
        ///     <inheritdoc cref="MinInEachRow(IMatrixBase{int})" />
        /// </returns>
        public static IEnumerable<decimal> MinInEachRow(this IMatrixBase<decimal> matrix)
		{
			for(var i = 0; i < matrix.RowsCount; i++)
			{
				var min = matrix[i, 0];
				for(var j = 1; j < matrix.ElementsInRow(i); j++)
				{
					if(min > matrix[i, j])
						min = matrix[i, j];
				}

				yield return min;
			}
		}

        /// <summary>
        ///     <inheritdoc cref="MinInEachRow(IMatrixBase{int})" />
        /// </summary>
        /// <param name="matrix">
        ///     <inheritdoc cref="MinInEachRow(IMatrixBase{int})" />
        /// </param>
        /// <returns>
        ///     <inheritdoc cref="MinInEachRow(IMatrixBase{int})" />
        /// </returns>
        public static IEnumerable<short> MinInEachRow(this IMatrixBase<short> matrix)
		{
			for(var i = 0; i < matrix.RowsCount; i++)
			{
				var min = matrix[i, 0];
				for(var j = 1; j < matrix.ElementsInRow(i); j++)
				{
					if(min > matrix[i, j])
						min = matrix[i, j];
				}

				yield return min;
			}
		}

		#endregion

		#region MinInRow

		/// <summary>
		///     Returns <see cref="IEnumerable{T}" /> collection with the maximum element in each row in
		///     <see cref="IMatrixBase{T}" />
		/// </summary>
		/// <param name="matrix">initial matrix</param>
		/// <returns><see cref="IEnumerable{T}" /> collection</returns>
		public static IEnumerable<double> MaxInEachRow(this IMatrixBase<int> matrix)
		{
			for(var i = 0; i < matrix.RowsCount; i++)
			{
				var max = matrix[i, 0];
				for(var j = 1; j < matrix.ElementsInRow(i); j++)
				{
					if(max > matrix[i, j])
						max = matrix[i, j];
				}

				yield return max;
			}
		}

        /// <summary>
        ///     <inheritdoc cref="MaxInEachRow(IMatrixBase{int})" />
        /// </summary>
        /// <param name="matrix">
        ///     <inheritdoc cref="MaxInEachRow(IMatrixBase{int})" />
        /// </param>
        /// <returns>
        ///     <inheritdoc cref="MaxInEachRow(IMatrixBase{int})" />
        /// </returns>
        public static IEnumerable<double> MaxInEachRow(this IMatrixBase<double> matrix)
		{
			for(var i = 0; i < matrix.RowsCount; i++)
			{
				var max = matrix[i, 0];
				for(var j = 1; j < matrix.ElementsInRow(i); j++)
				{
					if(max > matrix[i, j])
						max = matrix[i, j];
				}

				yield return max;
			}
		}

        /// <summary>
        ///     <inheritdoc cref="MaxInEachRow(IMatrixBase{int})" />
        /// </summary>
        /// <param name="matrix">
        ///     <inheritdoc cref="MaxInEachRow(IMatrixBase{int})" />
        /// </param>
        /// <returns>
        ///     <inheritdoc cref="MaxInEachRow(IMatrixBase{int})" />
        /// </returns>
        public static IEnumerable<long> MaxInEachRow(this IMatrixBase<long> matrix)
		{
			for(var i = 0; i < matrix.RowsCount; i++)
			{
				var max = matrix[i, 0];
				for(var j = 1; j < matrix.ElementsInRow(i); j++)
				{
					if(max > matrix[i, j])
						max = matrix[i, j];
				}

				yield return max;
			}
		}

        /// <summary>
        ///     <inheritdoc cref="MaxInEachRow(IMatrixBase{int})" />
        /// </summary>
        /// <param name="matrix">
        ///     <inheritdoc cref="MaxInEachRow(IMatrixBase{int})" />
        /// </param>
        /// <returns>
        ///     <inheritdoc cref="MaxInEachRow(IMatrixBase{int})" />
        /// </returns>
        public static IEnumerable<decimal> MaxInEachRow(this IMatrixBase<decimal> matrix)
		{
			for(var i = 0; i < matrix.RowsCount; i++)
			{
				var max = matrix[i, 0];
				for(var j = 1; j < matrix.ElementsInRow(i); j++)
				{
					if(max > matrix[i, j])
						max = matrix[i, j];
				}

				yield return max;
			}
		}

        /// <summary>
        ///     <inheritdoc cref="MaxInEachRow(IMatrixBase{int})" />
        /// </summary>
        /// <param name="matrix">
        ///     <inheritdoc cref="MaxInEachRow(IMatrixBase{int})" />
        /// </param>
        /// <returns>
        ///     <inheritdoc cref="MaxInEachRow(IMatrixBase{int})" />
        /// </returns>
        public static IEnumerable<short> MaxInEachRow(this IMatrixBase<short> matrix)
		{
			for(var i = 0; i < matrix.RowsCount; i++)
			{
				var max = matrix[i, 0];
				for(var j = 1; j < matrix.ElementsInRow(i); j++)
				{
					if(max > matrix[i, j])
						max = matrix[i, j];
				}

				yield return max;
			}
		}

		#endregion
	}
}