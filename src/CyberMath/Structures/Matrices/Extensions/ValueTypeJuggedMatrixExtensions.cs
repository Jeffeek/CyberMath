#region Using derectives

using System;
using System.Linq;
using CyberMath.Extensions;
using CyberMath.Structures.Matrices.Base.Exceptions;
using CyberMath.Structures.Matrices.Jagged_Matrix;

#endregion

namespace CyberMath.Structures.Matrices.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="ValueType" /> <see cref="IJuggedMatrix{T}" />
    /// </summary>
    public static class ValueTypeJuggedMatrixExtensions
	{
		//TODO: unit-test

		#region NOT nullable

		#region Int32

		#region Math

        /// <summary>
        ///     Returns the add <see cref="IJuggedMatrix{T}" /> <paramref name="first" /> and <see cref="IJuggedMatrix{T}" />
        ///     <paramref name="second" />
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result sum of matrices <paramref name="first" /> and
        ///     <paramref name="second" />
        /// </returns>
        public static IJuggedMatrix<int> Add(this IJuggedMatrix<int> first, IJuggedMatrix<int> second)
		{
			if (first.RowsCount != second.RowsCount)
				throw new
					MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");

			for (var i = 0; i < first.RowsCount; i++)
			{
				if (first.ElementsInRow(i) != second.ElementsInRow(i))
					throw new
						MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
			}

			var juggedMatrix = new JuggedMatrix<int>(first.RowsCount, first.CountOnEachRow().ToArray());
			for (var i = 0; i < first.RowsCount; i++)
			{
				for (var j = 0; j < first.ElementsInRow(i); j++) juggedMatrix[i, j] = first[i, j] + second[i, j];
			}

			return juggedMatrix;
		}

        /// <summary>
        ///     Returns the subtraction <see cref="IJuggedMatrix{T}" /> <paramref name="first" /> and
        ///     <see cref="IJuggedMatrix{T}" /> <paramref name="second" />
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result subtraction of matrices <paramref name="first" /> and
        ///     <paramref name="second" />
        /// </returns>
        public static IJuggedMatrix<int> Sub(this IJuggedMatrix<int> first, IJuggedMatrix<int> second)
		{
			if (first.RowsCount != second.RowsCount)
				throw new
					MatrixInvalidOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");

			for (var i = 0; i < first.RowsCount; i++)
			{
				if (first.ElementsInRow(i) != second.ElementsInRow(i))
					throw new
						MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
			}

			var juggedMatrix = new JuggedMatrix<int>(first.RowsCount, first.CountOnEachRow().ToArray());
			for (var i = 0; i < first.RowsCount; i++)
			{
				for (var j = 0; j < first.ElementsInRow(i); j++) juggedMatrix[i, j] = first[i, j] - second[i, j];
			}

			return juggedMatrix;
		}

        /// <summary>
        ///     Returns the multiplication <see cref="IJuggedMatrix{T}" /> <paramref name="matrix" /> on <see cref="int" />
        ///     <paramref name="number" />
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result multiplication of matrix <paramref name="matrix" /> on
        ///     <paramref name="number" />
        /// </returns>
        public static IJuggedMatrix<int> MulOnNumber(this IJuggedMatrix<int> matrix, int number)
		{
			var juggedMatrix = new JuggedMatrix<int>(matrix.RowsCount, matrix.CountOnEachRow().ToArray());
			for (var i = 0; i < matrix.RowsCount; i++)
			{
				for (var j = 0; j < matrix.ElementsInRow(i); j++) juggedMatrix[i, j] = matrix[i, j] * number;
			}

			return juggedMatrix;
		}

		#endregion

		#region Fill

        /// <summary>
        ///     Fills <paramref name="juggedMatrix" /> with randomly numbers
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IJuggedMatrix<int> juggedMatrix, int min = -50, int max = 50)
		{
			var rnd = new Random();
			for (var i = 0; i < juggedMatrix.RowsCount; i++)
			{
				for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++) juggedMatrix[i, j] = rnd.Next(min, max);
			}
		}

		#endregion

		#endregion

		#region Long

		#region Math

        /// <summary>
        ///     Returns the add <see cref="IJuggedMatrix{T}" /> <paramref name="first" /> and <see cref="IJuggedMatrix{T}" />
        ///     <paramref name="second" />
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result sum of matrices <paramref name="first" /> and
        ///     <paramref name="second" />
        /// </returns>
        public static IJuggedMatrix<long> Add(this IJuggedMatrix<long> first, IJuggedMatrix<long> second)
		{
			if (first.RowsCount != second.RowsCount)
				throw new
					MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");

			for (var i = 0; i < first.RowsCount; i++)
			{
				if (first.ElementsInRow(i) != second.ElementsInRow(i))
					throw new
						MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
			}

			var juggedMatrix = new JuggedMatrix<long>(first.RowsCount, first.CountOnEachRow().ToArray());
			for (var i = 0; i < first.RowsCount; i++)
			{
				for (var j = 0; j < first.ElementsInRow(i); j++) juggedMatrix[i, j] = first[i, j] + second[i, j];
			}

			return juggedMatrix;
		}

        /// <summary>
        ///     Returns the subtraction <see cref="IJuggedMatrix{T}" /> <paramref name="first" /> and
        ///     <see cref="IJuggedMatrix{T}" /> <paramref name="second" />
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result subtraction of matrices <paramref name="first" /> and
        ///     <paramref name="second" />
        /// </returns>
        public static IJuggedMatrix<long> Sub(this IJuggedMatrix<long> first, IJuggedMatrix<long> second)
		{
			if (first.RowsCount != second.RowsCount)
				throw new
					MatrixInvalidOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");

			for (var i = 0; i < first.RowsCount; i++)
			{
				if (first.ElementsInRow(i) != second.ElementsInRow(i))
					throw new
						MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
			}

			var juggedMatrix = new JuggedMatrix<long>(first.RowsCount, first.CountOnEachRow().ToArray());
			for (var i = 0; i < first.RowsCount; i++)
			{
				for (var j = 0; j < first.ElementsInRow(i); j++) juggedMatrix[i, j] = first[i, j] - second[i, j];
			}

			return juggedMatrix;
		}

        /// <summary>
        ///     Returns the multiplication <see cref="IJuggedMatrix{T}" /> <paramref name="matrix" /> on <see cref="long" />
        ///     <paramref name="number" />
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result multiplication of matrix <paramref name="matrix" /> on
        ///     <paramref name="number" />
        /// </returns>
        public static IJuggedMatrix<long> MulOnNumber(this IJuggedMatrix<long> matrix, long number)
		{
			var juggedMatrix = new JuggedMatrix<long>(matrix.RowsCount, matrix.CountOnEachRow().ToArray());
			for (var i = 0; i < matrix.RowsCount; i++)
			{
				for (var j = 0; j < matrix.ElementsInRow(i); j++) juggedMatrix[i, j] = matrix[i, j] * number;
			}

			return juggedMatrix;
		}

		#endregion

		#region Fill

        /// <summary>
        ///     Fills <paramref name="juggedMatrix" /> with randomly numbers
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IJuggedMatrix<long> juggedMatrix, long min = -50, long max = 50)
		{
			var rnd = new Random();
			for (var i = 0; i < juggedMatrix.RowsCount; i++)
			{
				for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++) juggedMatrix[i, j] = rnd.NextLong(min, max);
			}
		}

		#endregion

		#endregion

		#region Double

		#region Math

        /// <summary>
        ///     Returns the add <see cref="IJuggedMatrix{T}" /> <paramref name="first" /> and <see cref="IJuggedMatrix{T}" />
        ///     <paramref name="second" />
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result sum of matrices <paramref name="first" /> and
        ///     <paramref name="second" />
        /// </returns>
        public static IJuggedMatrix<double> Add(this IJuggedMatrix<double> first, IJuggedMatrix<double> second)
		{
			if (first.RowsCount != second.RowsCount)
				throw new
					MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");

			for (var i = 0; i < first.RowsCount; i++)
			{
				if (first.ElementsInRow(i) != second.ElementsInRow(i))
					throw new
						MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
			}

			var juggedMatrix = new JuggedMatrix<double>(first.RowsCount, first.CountOnEachRow().ToArray());
			for (var i = 0; i < first.RowsCount; i++)
			{
				for (var j = 0; j < first.ElementsInRow(i); j++) juggedMatrix[i, j] = first[i, j] + second[i, j];
			}

			return juggedMatrix;
		}

        /// <summary>
        ///     Returns the subtraction <see cref="IJuggedMatrix{T}" /> <paramref name="first" /> and
        ///     <see cref="IJuggedMatrix{T}" /> <paramref name="second" />
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result subtraction of matrices <paramref name="first" /> and
        ///     <paramref name="second" />
        /// </returns>
        public static IJuggedMatrix<double> Sub(this IJuggedMatrix<double> first, IJuggedMatrix<double> second)
		{
			if (first.RowsCount != second.RowsCount)
				throw new
					MatrixInvalidOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");

			for (var i = 0; i < first.RowsCount; i++)
			{
				if (first.ElementsInRow(i) != second.ElementsInRow(i))
					throw new
						MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
			}

			var juggedMatrix = new JuggedMatrix<double>(first.RowsCount, first.CountOnEachRow().ToArray());
			for (var i = 0; i < first.RowsCount; i++)
			{
				for (var j = 0; j < first.ElementsInRow(i); j++) juggedMatrix[i, j] = first[i, j] - second[i, j];
			}

			return juggedMatrix;
		}

        /// <summary>
        ///     Returns the multiplication <see cref="IJuggedMatrix{T}" /> <paramref name="matrix" /> on <see cref="double" />
        ///     <paramref name="number" />
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result multiplication of matrix <paramref name="matrix" /> on
        ///     <paramref name="number" />
        /// </returns>
        public static IJuggedMatrix<double> MulOnNumber(this IJuggedMatrix<double> matrix, double number)
		{
			var juggedMatrix = new JuggedMatrix<double>(matrix.RowsCount, matrix.CountOnEachRow().ToArray());
			for (var i = 0; i < matrix.RowsCount; i++)
			{
				for (var j = 0; j < matrix.ElementsInRow(i); j++) juggedMatrix[i, j] = matrix[i, j] * number;
			}

			return juggedMatrix;
		}

		#endregion

		#region Fill

        /// <summary>
        ///     Fills <paramref name="juggedMatrix" /> with randomly numbers
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IJuggedMatrix<double> juggedMatrix, double min = -50, double max = 50)
		{
			var rnd = new Random();
			for (var i = 0; i < juggedMatrix.RowsCount; i++)
			{
				for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++) juggedMatrix[i, j] = rnd.NextDouble(min, max);
			}
		}

		#endregion

		#endregion

		#region Decimal

		#region Math

        /// <summary>
        ///     Returns the add <see cref="IJuggedMatrix{T}" /> <paramref name="first" /> and <see cref="IJuggedMatrix{T}" />
        ///     <paramref name="second" />
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result sum of matrices <paramref name="first" /> and
        ///     <paramref name="second" />
        /// </returns>
        public static IJuggedMatrix<decimal> Add(this IJuggedMatrix<decimal> first, IJuggedMatrix<decimal> second)
		{
			if (first.RowsCount != second.RowsCount)
				throw new
					MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");

			for (var i = 0; i < first.RowsCount; i++)
			{
				if (first.ElementsInRow(i) != second.ElementsInRow(i))
					throw new
						MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
			}

			var juggedMatrix = new JuggedMatrix<decimal>(first.RowsCount, first.CountOnEachRow().ToArray());
			for (var i = 0; i < first.RowsCount; i++)
			{
				for (var j = 0; j < first.ElementsInRow(i); j++) juggedMatrix[i, j] = first[i, j] + second[i, j];
			}

			return juggedMatrix;
		}

        /// <summary>
        ///     Returns the subtraction <see cref="IJuggedMatrix{T}" /> <paramref name="first" /> and
        ///     <see cref="IJuggedMatrix{T}" /> <paramref name="second" />
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result subtraction of matrices <paramref name="first" /> and
        ///     <paramref name="second" />
        /// </returns>
        public static IJuggedMatrix<decimal> Sub(this IJuggedMatrix<decimal> first, IJuggedMatrix<decimal> second)
		{
			if (first.RowsCount != second.RowsCount)
				throw new
					MatrixInvalidOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");

			for (var i = 0; i < first.RowsCount; i++)
			{
				if (first.ElementsInRow(i) != second.ElementsInRow(i))
					throw new
						MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
			}

			var juggedMatrix = new JuggedMatrix<decimal>(first.RowsCount, first.CountOnEachRow().ToArray());
			for (var i = 0; i < first.RowsCount; i++)
			{
				for (var j = 0; j < first.ElementsInRow(i); j++) juggedMatrix[i, j] = first[i, j] - second[i, j];
			}

			return juggedMatrix;
		}

        /// <summary>
        ///     Returns the multiplication <see cref="IJuggedMatrix{T}" /> <paramref name="matrix" /> on <see cref="decimal" />
        ///     <paramref name="number" />
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result multiplication of matrix <paramref name="matrix" /> on
        ///     <paramref name="number" />
        /// </returns>
        public static IJuggedMatrix<decimal> MulOnNumber(this IJuggedMatrix<decimal> matrix, decimal number)
		{
			var juggedMatrix = new JuggedMatrix<decimal>(matrix.RowsCount, matrix.CountOnEachRow().ToArray());
			for (var i = 0; i < matrix.RowsCount; i++)
			{
				for (var j = 0; j < matrix.ElementsInRow(i); j++) juggedMatrix[i, j] = matrix[i, j] * number;
			}

			return juggedMatrix;
		}

		#endregion

		#region Fill

        /// <summary>
        ///     Fills <paramref name="juggedMatrix" /> with randomly numbers
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IJuggedMatrix<decimal> juggedMatrix, decimal min = -50, decimal max = 50)
		{
			var rnd = new Random();
			for (var i = 0; i < juggedMatrix.RowsCount; i++)
			{
				for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
					juggedMatrix[i, j] = (decimal)rnd.NextDouble((double)min, (double)max);
			}
		}

		#endregion

		#endregion

		#endregion

		#region Nullable

		#region String

		#region Math

        /// <summary>
        ///     Returns the add <see cref="IJuggedMatrix{T}" /> <paramref name="first" /> and <see cref="IJuggedMatrix{T}" />
        ///     <paramref name="second" />
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result sum of matrices <paramref name="first" /> and
        ///     <paramref name="second" />
        /// </returns>
        public static IJuggedMatrix<string> Add(this IJuggedMatrix<string> first, IJuggedMatrix<string> second)
		{
			if (first.RowsCount != second.RowsCount)
				throw new
					MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");

			for (var i = 0; i < first.RowsCount; i++)
			{
				if (first.ElementsInRow(i) != second.ElementsInRow(i))
					throw new
						MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
			}

			var juggedMatrix = new JuggedMatrix<string>(first.RowsCount, first.CountOnEachRow().ToArray());
			for (var i = 0; i < first.RowsCount; i++)
			{
				for (var j = 0; j < first.ElementsInRow(i); j++) juggedMatrix[i, j] = first[i, j] + second[i, j];
			}

			return juggedMatrix;
		}

        /// <summary>
        ///     Returns the subtraction <see cref="IJuggedMatrix{T}" /> <paramref name="first" /> and
        ///     <see cref="IJuggedMatrix{T}" /> <paramref name="second" />
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result subtraction of matrices <paramref name="first" /> and
        ///     <paramref name="second" />
        /// </returns>
        public static IJuggedMatrix<string> Sub(this IJuggedMatrix<string> first, IJuggedMatrix<string> second)
		{
			if (first.RowsCount != second.RowsCount)
				throw new
					MatrixInvalidOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");

			for (var i = 0; i < first.RowsCount; i++)
			{
				if (first.ElementsInRow(i) != second.ElementsInRow(i))
					throw new
						MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
			}

			var juggedMatrix = new JuggedMatrix<string>(first.RowsCount, first.CountOnEachRow().ToArray());
			for (var i = 0; i < first.RowsCount; i++)
			{
				for (var j = 0; j < first.ElementsInRow(i); j++)
					juggedMatrix[i, j] = string.Concat(first[i, j].Except(second[i, j]));
			}

			return juggedMatrix;
		}

        /// <summary>
        ///     Returns the multiplication <see cref="IJuggedMatrix{T}" /> <paramref name="matrix" /> on <see cref="int" />
        ///     <paramref name="number" />
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result multiplication of matrix <paramref name="matrix" /> on
        ///     <paramref name="number" />
        /// </returns>
        public static IJuggedMatrix<string> MulOnNumber(this IJuggedMatrix<string> matrix, int number)
		{
			var juggedMatrix = new JuggedMatrix<string>(matrix.RowsCount, matrix.CountOnEachRow().ToArray());
			for (var i = 0; i < matrix.RowsCount; i++)
			{
				for (var j = 0; j < matrix.ElementsInRow(i); j++) juggedMatrix[i, j] = matrix[i, j].Concat(number);
			}

			return juggedMatrix;
		}

		#endregion

		#endregion

		#region Int32?

		#region Math

        /// <summary>
        ///     Returns the add <see cref="IJuggedMatrix{T}" /> <paramref name="first" /> and <see cref="IJuggedMatrix{T}" />
        ///     <paramref name="second" />
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result sum of matrices <paramref name="first" /> and
        ///     <paramref name="second" />
        /// </returns>
        public static IJuggedMatrix<int?> Add(this IJuggedMatrix<int?> first, IJuggedMatrix<int?> second)
		{
			if (first.RowsCount != second.RowsCount)
				throw new
					MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");

			for (var i = 0; i < first.RowsCount; i++)
			{
				if (first.ElementsInRow(i) != second.ElementsInRow(i))
					throw new
						MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
			}

			var juggedMatrix = new JuggedMatrix<int?>(first.RowsCount, first.CountOnEachRow().ToArray());
			for (var i = 0; i < first.RowsCount; i++)
			{
				for (var j = 0; j < first.ElementsInRow(i); j++)
				{
					if (first[i, j] != null &&
					    second[i, j] != null)
						juggedMatrix[i, j] = first[i, j].Value + second[i, j].Value;
				}
			}

			return juggedMatrix;
		}

        /// <summary>
        ///     Returns the subtraction <see cref="IJuggedMatrix{T}" /> <paramref name="first" /> and
        ///     <see cref="IJuggedMatrix{T}" /> <paramref name="second" />
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result subtraction of matrices <paramref name="first" /> and
        ///     <paramref name="second" />
        /// </returns>
        public static IJuggedMatrix<int?> Sub(this IJuggedMatrix<int?> first, IJuggedMatrix<int?> second)
		{
			if (first.RowsCount != second.RowsCount)
				throw new
					MatrixInvalidOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");

			for (var i = 0; i < first.RowsCount; i++)
			{
				if (first.ElementsInRow(i) != second.ElementsInRow(i))
					throw new
						MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
			}

			var juggedMatrix = new JuggedMatrix<int?>(first.RowsCount, first.CountOnEachRow().ToArray());
			for (var i = 0; i < first.RowsCount; i++)
			{
				for (var j = 0; j < first.ElementsInRow(i); j++)
				{
					if (first[i, j] != null &&
					    second[i, j] != null)
						juggedMatrix[i, j] = first[i, j].Value - second[i, j].Value;
				}
			}

			return juggedMatrix;
		}

        /// <summary>
        ///     Returns the multiplication <see cref="IJuggedMatrix{T}" /> <paramref name="juggedMatrix" /> on <see cref="int" />
        ///     <paramref name="number" />
        /// </summary>
        /// <param name="juggedMatrix">First</param>
        /// <param name="number"></param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result multiplication of matrix <paramref name="juggedMatrix" /> on
        ///     <paramref name="number" />
        /// </returns>
        public static IJuggedMatrix<int?> MulOnNumber(this IJuggedMatrix<int?> juggedMatrix, int number)
		{
			var newJuggedMatrix =
				new JuggedMatrix<int?>(juggedMatrix.RowsCount, juggedMatrix.CountOnEachRow().ToArray());

			for (var i = 0; i < juggedMatrix.RowsCount; i++)
			{
				for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
				{
					if (juggedMatrix[i, j] != null)
						newJuggedMatrix[i, j] = juggedMatrix[i, j].Value * number;
				}
			}

			return newJuggedMatrix;
		}

		#endregion

		#region Fill

        /// <summary>
        ///     Fills <paramref name="juggedMatrix" /> with randomly numbers
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IJuggedMatrix<int?> juggedMatrix, int min = -50, int max = 50)
		{
			var rnd = new Random();
			for (var i = 0; i < juggedMatrix.RowsCount; i++)
			{
				for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
				{
					if (juggedMatrix[i, j] != null)
						juggedMatrix[i, j] = rnd.Next(min, max + 1);
				}
			}
		}

		#endregion

		#endregion

		#region long?

		#region Math

        /// <summary>
        ///     Returns the add <see cref="IJuggedMatrix{T}" /> <paramref name="first" /> and <see cref="IJuggedMatrix{T}" />
        ///     <paramref name="second" />
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result sum of matrices <paramref name="first" /> and
        ///     <paramref name="second" />
        /// </returns>
        public static IJuggedMatrix<long?> Add(this IJuggedMatrix<long?> first, IJuggedMatrix<long?> second)
		{
			if (first.RowsCount != second.RowsCount)
				throw new
					MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");

			for (var i = 0; i < first.RowsCount; i++)
			{
				if (first.ElementsInRow(i) != second.ElementsInRow(i))
					throw new
						MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
			}

			var juggedMatrix = new JuggedMatrix<long?>(first.RowsCount, first.CountOnEachRow().ToArray());
			for (var i = 0; i < first.RowsCount; i++)
			{
				for (var j = 0; j < first.ElementsInRow(i); j++)
				{
					if (first[i, j] != null &&
					    second[i, j] != null)
						juggedMatrix[i, j] = first[i, j].Value + second[i, j].Value;
				}
			}

			return juggedMatrix;
		}

        /// <summary>
        ///     Returns the subtraction <see cref="IJuggedMatrix{T}" /> <paramref name="first" /> and
        ///     <see cref="IJuggedMatrix{T}" /> <paramref name="second" />
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result subtraction of matrices <paramref name="first" /> and
        ///     <paramref name="second" />
        /// </returns>
        public static IJuggedMatrix<long?> Sub(this IJuggedMatrix<long?> first, IJuggedMatrix<long?> second)
		{
			if (first.RowsCount != second.RowsCount)
				throw new
					MatrixInvalidOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");

			for (var i = 0; i < first.RowsCount; i++)
			{
				if (first.ElementsInRow(i) != second.ElementsInRow(i))
					throw new
						MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
			}

			var juggedMatrix = new JuggedMatrix<long?>(first.RowsCount, first.CountOnEachRow().ToArray());
			for (var i = 0; i < first.RowsCount; i++)
			{
				for (var j = 0; j < first.ElementsInRow(i); j++)
				{
					if (first[i, j] != null &&
					    second[i, j] != null)
						juggedMatrix[i, j] = first[i, j].Value - second[i, j].Value;
				}
			}

			return juggedMatrix;
		}

        /// <summary>
        ///     Returns the multiplication <see cref="IJuggedMatrix{T}" /> <paramref name="juggedMatrix" /> on <see cref="long" />
        ///     <paramref name="number" />
        /// </summary>
        /// <param name="juggedMatrix">First</param>
        /// <param name="number"></param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result multiplication of matrix <paramref name="juggedMatrix" /> on
        ///     <paramref name="number" />
        /// </returns>
        public static IJuggedMatrix<long?> MulOnNumber(this IJuggedMatrix<long?> juggedMatrix, long number)
		{
			var newJuggedMatrix =
				new JuggedMatrix<long?>(juggedMatrix.RowsCount, juggedMatrix.CountOnEachRow().ToArray());

			for (var i = 0; i < juggedMatrix.RowsCount; i++)
			{
				for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
				{
					if (juggedMatrix[i, j] != null)
						newJuggedMatrix[i, j] = juggedMatrix[i, j].Value * number;
				}
			}

			return newJuggedMatrix;
		}

		#endregion

		#region Fill

        /// <summary>
        ///     Fills <paramref name="juggedMatrix" /> with randomly numbers
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IJuggedMatrix<long?> juggedMatrix, long min = -50, long max = 50)
		{
			var rnd = new Random();
			for (var i = 0; i < juggedMatrix.RowsCount; i++)
			{
				for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
				{
					if (juggedMatrix[i, j] != null)
						juggedMatrix[i, j] = rnd.NextLong(min, max);
				}
			}
		}

		#endregion

		#endregion

		#region double?

		#region Math

        /// <summary>
        ///     Returns the add <see cref="IJuggedMatrix{T}" /> <paramref name="first" /> and <see cref="IJuggedMatrix{T}" />
        ///     <paramref name="second" />
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result sum of matrices <paramref name="first" /> and
        ///     <paramref name="second" />
        /// </returns>
        public static IJuggedMatrix<double?> Add(this IJuggedMatrix<double?> first, IJuggedMatrix<double?> second)
		{
			if (first.RowsCount != second.RowsCount)
				throw new
					MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");

			for (var i = 0; i < first.RowsCount; i++)
			{
				if (first.ElementsInRow(i) != second.ElementsInRow(i))
					throw new
						MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
			}

			var juggedMatrix = new JuggedMatrix<double?>(first.RowsCount, first.CountOnEachRow().ToArray());
			for (var i = 0; i < first.RowsCount; i++)
			{
				for (var j = 0; j < first.ElementsInRow(i); j++)
				{
					if (first[i, j] != null &&
					    second[i, j] != null)
						juggedMatrix[i, j] = first[i, j].Value + second[i, j].Value;
				}
			}

			return juggedMatrix;
		}

        /// <summary>
        ///     Returns the subtraction <see cref="IJuggedMatrix{T}" /> <paramref name="first" /> and
        ///     <see cref="IJuggedMatrix{T}" /> <paramref name="second" />
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result subtraction of matrices <paramref name="first" /> and
        ///     <paramref name="second" />
        /// </returns>
        public static IJuggedMatrix<double?> Sub(this IJuggedMatrix<double?> first, IJuggedMatrix<double?> second)
		{
			if (first.RowsCount != second.RowsCount)
				throw new
					MatrixInvalidOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");

			for (var i = 0; i < first.RowsCount; i++)
			{
				if (first.ElementsInRow(i) != second.ElementsInRow(i))
					throw new
						MatrixInvalidOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
			}

			var juggedMatrix = new JuggedMatrix<double?>(first.RowsCount, first.CountOnEachRow().ToArray());
			for (var i = 0; i < first.RowsCount; i++)
			{
				for (var j = 0; j < first.ElementsInRow(i); j++)
				{
					if (first[i, j] != null &&
					    second[i, j] != null)
						juggedMatrix[i, j] = first[i, j].Value - second[i, j].Value;
				}
			}

			return juggedMatrix;
		}

        /// <summary>
        ///     Returns the multiplication <see cref="IJuggedMatrix{T}" /> <paramref name="juggedMatrix" /> on
        ///     <see cref="double" /> <paramref name="number" />
        /// </summary>
        /// <param name="juggedMatrix">First</param>
        /// <param name="number"></param>
        /// <returns>
        ///     New <see cref="IJuggedMatrix{T}" /> - the result multiplication of matrix <paramref name="juggedMatrix" /> on
        ///     <paramref name="number" />
        /// </returns>
        public static IJuggedMatrix<double?> MulOnNumber(this IJuggedMatrix<double?> juggedMatrix, double number)
		{
			var newJuggedMatrix =
				new JuggedMatrix<double?>(juggedMatrix.RowsCount, juggedMatrix.CountOnEachRow().ToArray());

			for (var i = 0; i < juggedMatrix.RowsCount; i++)
			{
				for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
				{
					if (juggedMatrix[i, j] != null)
						newJuggedMatrix[i, j] = juggedMatrix[i, j].Value * number;
				}
			}

			return newJuggedMatrix;
		}

		#endregion

		#region Fill

        /// <summary>
        ///     Fills <paramref name="juggedMatrix" /> with randomly numbers
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IJuggedMatrix<double?> juggedMatrix, double min = -50.0,
                                        double max = 50.0d)
		{
			var rnd = new Random();
			for (var i = 0; i < juggedMatrix.RowsCount; i++)
			{
				for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
				{
					if (juggedMatrix[i, j] != null)
						juggedMatrix[i, j] = rnd.NextDouble(min, max + 1);
				}
			}
		}

		#endregion

		#endregion

		#endregion
	}
}