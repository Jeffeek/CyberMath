using CyberMath.Extensions;
using CyberMath.Structures.JaggedMatrix;
using CyberMath.Structures.MatrixBase;
using CyberMath.Structures.MatrixBase.Exceptions;
using System;
using System.Linq;
using System.Text;

namespace CyberMath.Structures.MatrixExtensions.JaggedMatrix
{
    /// <summary>
    /// Extension methods for <see cref="ValueType"/> <see cref="IJuggedMatrix{T}"/>
    /// </summary>
    public static class ValueTypeJuggedMatrixExtensions
    {
        //TODO: unit-test
        #region NOT nullable

        #region Int32

        #region Math

        /// <summary>
        /// Returns the add <see cref="IJuggedMatrix{T}"/> <paramref name="first"/> and <see cref="IJuggedMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IJuggedMatrix<int> Add(this IJuggedMatrix<int> first, IJuggedMatrix<int> second)
        {
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");
            for (var i = 0; i < first.RowsCount; i++)
            {
                if (first.ElementsInRow(i) != second.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<int>(first.RowsCount, first.GetCountPerRow().ToArray());
            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = first[i, j] + second[i, j];
                }
            }

            return juggedMatrix;
        }

        /// <summary>
        /// Returns the subtraction <see cref="IJuggedMatrix{T}"/> <paramref name="first"/> and <see cref="IJuggedMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IJuggedMatrix<int> Sub(this IJuggedMatrix<int> first, IJuggedMatrix<int> second)
        {
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");
            for (var i = 0; i < first.RowsCount; i++)
            {
                if (first.ElementsInRow(i) != second.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<int>(first.RowsCount, first.GetCountPerRow().ToArray());
            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = first[i, j] - second[i, j];
                }
            }

            return juggedMatrix;
        }

        /// <summary>
        /// Returns the multiplication <see cref="IJuggedMatrix{T}"/> <paramref name="matrix"/> on <see cref="Int32"/> <paramref name="number"/>
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result multiplication of matrix <paramref name="matrix"/> on <paramref name="number"/></returns>
        public static IJuggedMatrix<int> MulOnNumber(this IJuggedMatrix<int> matrix, int number)
        {
            var juggedMatrix = new JuggedMatrix<int>(matrix.RowsCount, matrix.GetCountPerRow().ToArray());
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = matrix[i, j] * number;
                }
            }

            return juggedMatrix;
        }

        #endregion

        #region Creation

        /// <summary>
        /// Creates new <see cref="IJuggedMatrix{T}"/> identity matrix.
        /// <para></para>
        /// <example>
        /// n = 3
        /// <para/>
        /// matrix = <para/>
        /// {<para/>
        ///     {1,0,0},<para/>
        ///     {0,1,0},<para/>
        ///     {0,0,1}<para/>
        /// }
        /// </example>
        /// </summary>
        /// <param name="n">Count of rows and columns</param>
        /// <returns>Identity <see cref="IJuggedMatrix{T}"/> matrix</returns>
        public static IJuggedMatrix<int> CreateIdentityMatrix(int n)
        {
            var result = new JuggedMatrix<int>(n, Enumerable.Repeat(n, n).ToArray());
            for (var i = 0; i < n; i++)
            {
                result[i, i] = 1;
            }
            return result;
        }

        #endregion

        #region Sum Operations

        /// <summary>
        /// Calculates sum of all numbers in main diagonal in <see cref="IJuggedMatrix{T}"/>
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static int DiagonalSum(this IJuggedMatrix<int> juggedMatrix)
        {
            if (!juggedMatrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0;
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
                sum += juggedMatrix[i, i];
            return sum;
        }

        /// <summary>
        /// Calculates sum of all saddle points in matrix
        /// </summary>
        /// <param name="juggedMatrix"></param>
        /// <returns>Sum of all saddle points in matrix</returns>
        public static int SumSaddlePoints(this IJuggedMatrix<int> juggedMatrix)
        {
            var sum = 0;
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    if (!juggedMatrix.IsMinInRow(i, j) || !juggedMatrix.IsMaxInColumn(i, j))
                        continue;
                    sum += juggedMatrix[i, j];
                }
            }

            return sum;
        }

        #endregion

        #region Fill

        /// <summary>
        /// Fills <paramref name="juggedMatrix"/> with randomly numbers
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IJuggedMatrix<int> juggedMatrix, int min = -50, int max = 50)
        {
            var rnd = new Random();
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = rnd.Next(min, max + 1);
                }
            }
        }

        #endregion

        #endregion

        #region Long

        #region Math

        /// <summary>
        /// Returns the add <see cref="IJuggedMatrix{T}"/> <paramref name="first"/> and <see cref="IJuggedMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IJuggedMatrix<long> Add(this IJuggedMatrix<long> first, IJuggedMatrix<long> second)
        {
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");
            for (var i = 0; i < first.RowsCount; i++)
            {
                if (first.ElementsInRow(i) != second.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<long>(first.RowsCount, first.GetCountPerRow().ToArray());
            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = first[i, j] + second[i, j];
                }
            }

            return juggedMatrix;
        }

        /// <summary>
        /// Returns the subtraction <see cref="IJuggedMatrix{T}"/> <paramref name="first"/> and <see cref="IJuggedMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IJuggedMatrix<long> Sub(this IJuggedMatrix<long> first, IJuggedMatrix<long> second)
        {
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");
            for (var i = 0; i < first.RowsCount; i++)
            {
                if (first.ElementsInRow(i) != second.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<long>(first.RowsCount, first.GetCountPerRow().ToArray());
            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = first[i, j] - second[i, j];
                }
            }

            return juggedMatrix;
        }

        /// <summary>
        /// Returns the multiplication <see cref="IJuggedMatrix{T}"/> <paramref name="matrix"/> on <see cref="Int64"/> <paramref name="number"/>
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result multiplication of matrix <paramref name="matrix"/> on <paramref name="number"/></returns>
        public static IJuggedMatrix<long> MulOnNumber(this IJuggedMatrix<long> matrix, long number)
        {
            var juggedMatrix = new JuggedMatrix<long>(matrix.RowsCount, matrix.GetCountPerRow().ToArray());
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = matrix[i, j] * number;
                }
            }

            return juggedMatrix;
        }

        #endregion

        #region Sum Operations

        /// <summary>
        /// Calculates sum of all numbers in main diagonal in <see cref="IJuggedMatrix{T}"/>
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static long DiagonalSum(this IJuggedMatrix<long> juggedMatrix)
        {
            if (!juggedMatrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            long sum = 0;
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
                sum += juggedMatrix[i, i];
            return sum;
        }

        /// <summary>
        /// Calculates sum of all saddle points in matrix
        /// </summary>
        /// <param name="juggedMatrix"></param>
        /// <returns>Sum of all saddle points in matrix</returns>
        public static long SumSaddlePoints(this IJuggedMatrix<long> juggedMatrix)
        {
            long sum = 0;
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    if (!juggedMatrix.IsMinInRow(i, j) || !juggedMatrix.IsMaxInColumn(i, j))
                        continue;
                    sum += juggedMatrix[i, j];
                }
            }

            return sum;
        }

        #endregion

        #region Fill

        /// <summary>
        /// Fills <paramref name="juggedMatrix"/> with randomly numbers
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IJuggedMatrix<long> juggedMatrix, long min = -50, long max = 50)
        {
            var rnd = new Random();
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = rnd.NextLong(min, max + 1);
                }
            }
        }

        #endregion

        #endregion

        #region Double

        #region Math

        /// <summary>
        /// Returns the add <see cref="IJuggedMatrix{T}"/> <paramref name="first"/> and <see cref="IJuggedMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IJuggedMatrix<double> Add(this IJuggedMatrix<double> first, IJuggedMatrix<double> second)
        {
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");
            for (var i = 0; i < first.RowsCount; i++)
            {
                if (first.ElementsInRow(i) != second.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<double>(first.RowsCount, first.GetCountPerRow().ToArray());
            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = first[i, j] + second[i, j];
                }
            }

            return juggedMatrix;
        }

        /// <summary>
        /// Returns the subtraction <see cref="IJuggedMatrix{T}"/> <paramref name="first"/> and <see cref="IJuggedMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IJuggedMatrix<double> Sub(this IJuggedMatrix<double> first, IJuggedMatrix<double> second)
        {
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");
            for (var i = 0; i < first.RowsCount; i++)
            {
                if (first.ElementsInRow(i) != second.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<double>(first.RowsCount, first.GetCountPerRow().ToArray());
            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = first[i, j] - second[i, j];
                }
            }

            return juggedMatrix;
        }

        /// <summary>
        /// Returns the multiplication <see cref="IJuggedMatrix{T}"/> <paramref name="matrix"/> on <see cref="Double"/> <paramref name="number"/>
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result multiplication of matrix <paramref name="matrix"/> on <paramref name="number"/></returns>
        public static IJuggedMatrix<double> MulOnNumber(this IJuggedMatrix<double> matrix, double number)
        {
            var juggedMatrix = new JuggedMatrix<double>(matrix.RowsCount, matrix.GetCountPerRow().ToArray());
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = matrix[i, j] * number;
                }
            }

            return juggedMatrix;
        }

        #endregion

        #region Sum Operations

        /// <summary>
        /// Calculates sum of all numbers in main diagonal in <see cref="IJuggedMatrix{T}"/>
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static double DiagonalSum(this IJuggedMatrix<double> juggedMatrix)
        {
            if (!juggedMatrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            double sum = 0;
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
                sum += juggedMatrix[i, i];
            return sum;
        }

        /// <summary>
        /// Calculates sum of all saddle points in matrix
        /// </summary>
        /// <param name="juggedMatrix"></param>
        /// <returns>Sum of all saddle points in matrix</returns>
        public static double SumSaddlePoints(this IJuggedMatrix<double> juggedMatrix)
        {
            double sum = 0;
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    if (!juggedMatrix.IsMinInRow(i, j) || !juggedMatrix.IsMaxInColumn(i, j))
                        continue;
                    sum += juggedMatrix[i, j];
                }
            }

            return sum;
        }

        #endregion

        #region Fill

        /// <summary>
        /// Fills <paramref name="juggedMatrix"/> with randomly numbers
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IJuggedMatrix<double> juggedMatrix, double min = -50, double max = 50)
        {
            var rnd = new Random();
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = rnd.NextDouble(min, max + 1);
                }
            }
        }

        #endregion

        #endregion

        #region Decimal

        #region Math

        /// <summary>
        /// Returns the add <see cref="IJuggedMatrix{T}"/> <paramref name="first"/> and <see cref="IJuggedMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IJuggedMatrix<decimal> Add(this IJuggedMatrix<decimal> first, IJuggedMatrix<decimal> second)
        {
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");
            for (var i = 0; i < first.RowsCount; i++)
            {
                if (first.ElementsInRow(i) != second.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<decimal>(first.RowsCount, first.GetCountPerRow().ToArray());
            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = first[i, j] + second[i, j];
                }
            }

            return juggedMatrix;
        }

        /// <summary>
        /// Returns the subtraction <see cref="IJuggedMatrix{T}"/> <paramref name="first"/> and <see cref="IJuggedMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IJuggedMatrix<decimal> Sub(this IJuggedMatrix<decimal> first, IJuggedMatrix<decimal> second)
        {
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");
            for (var i = 0; i < first.RowsCount; i++)
            {
                if (first.ElementsInRow(i) != second.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<decimal>(first.RowsCount, first.GetCountPerRow().ToArray());
            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = first[i, j] - second[i, j];
                }
            }

            return juggedMatrix;
        }

        /// <summary>
        /// Returns the multiplication <see cref="IJuggedMatrix{T}"/> <paramref name="matrix"/> on <see cref="Decimal"/> <paramref name="number"/>
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result multiplication of matrix <paramref name="matrix"/> on <paramref name="number"/></returns>
        public static IJuggedMatrix<decimal> MulOnNumber(this IJuggedMatrix<decimal> matrix, decimal number)
        {
            var juggedMatrix = new JuggedMatrix<decimal>(matrix.RowsCount, matrix.GetCountPerRow().ToArray());
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = matrix[i, j] * number;
                }
            }

            return juggedMatrix;
        }

        #endregion

        #region Sum Operations

        /// <summary>
        /// Calculates sum of all numbers in main diagonal in <see cref="IJuggedMatrix{T}"/>
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static decimal DiagonalSum(this IJuggedMatrix<decimal> juggedMatrix)
        {
            if (!juggedMatrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            decimal sum = 0;
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
                sum += juggedMatrix[i, i];
            return sum;
        }

        /// <summary>
        /// Calculates sum of all saddle points in matrix
        /// </summary>
        /// <param name="juggedMatrix"></param>
        /// <returns>Sum of all saddle points in matrix</returns>
        public static decimal SumSaddlePoints(this IJuggedMatrix<decimal> juggedMatrix)
        {
            decimal sum = 0;
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    if (!juggedMatrix.IsMinInRow(i, j) || !juggedMatrix.IsMaxInColumn(i, j))
                        continue;
                    sum += juggedMatrix[i, j];
                }
            }

            return sum;
        }

        #endregion

        #region Fill

        /// <summary>
        /// Fills <paramref name="juggedMatrix"/> with randomly numbers
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
                {
                    juggedMatrix[i, j] = (decimal)rnd.NextDouble((double)min, (double)max + 1);
                }
            }
        }

        #endregion

        #endregion

        #region String

        #region Math

        /// <summary>
        /// Returns the add <see cref="IJuggedMatrix{T}"/> <paramref name="first"/> and <see cref="IJuggedMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IJuggedMatrix<string> Add(this IJuggedMatrix<string> first, IJuggedMatrix<string> second)
        {
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");
            for (var i = 0; i < first.RowsCount; i++)
            {
                if (first.ElementsInRow(i) != second.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<string>(first.RowsCount, first.GetCountPerRow().ToArray());
            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = first[i, j] + second[i, j];
                }
            }

            return juggedMatrix;
        }

        /// <summary>
        /// Returns the subtraction <see cref="IJuggedMatrix{T}"/> <paramref name="first"/> and <see cref="IJuggedMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IJuggedMatrix<string> Sub(this IJuggedMatrix<string> first, IJuggedMatrix<string> second)
        {
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");
            for (var i = 0; i < first.RowsCount; i++)
            {
                if (first.ElementsInRow(i) != second.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<string>(first.RowsCount, first.GetCountPerRow().ToArray());
            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = string.Concat(first[i, j].Except(second[i, j]));
                }
            }

            return juggedMatrix;
        }

        /// <summary>
        /// Returns the multiplication <see cref="IJuggedMatrix{T}"/> <paramref name="matrix"/> on <see cref="Int32"/> <paramref name="number"/>
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result multiplication of matrix <paramref name="matrix"/> on <paramref name="number"/></returns>
        public static IJuggedMatrix<string> MulOnNumber(this IJuggedMatrix<string> matrix, int number)
        {
            var juggedMatrix = new JuggedMatrix<string>(matrix.RowsCount, matrix.GetCountPerRow().ToArray());
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = matrix[i, j].Concat(number);
                }
            }

            return juggedMatrix;
        }

        #endregion

        #region Sum Operations

        /// <summary>
        /// Calculates sum of all numbers in main diagonal in <see cref="IJuggedMatrix{T}"/>
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static string DiagonalSum(this IJuggedMatrix<string> juggedMatrix)
        {
            if (!juggedMatrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sb = new StringBuilder();
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
                sb.Append(juggedMatrix[i, i]).Append(" ");
            return sb.ToString();
        }

        
        public static string Sum(this IJuggedMatrix<string> juggedMatrix)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    sb.Append(juggedMatrix[i, j]).Append(" ");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Calculates sum of all saddle points in matrix
        /// </summary>
        /// <param name="juggedMatrix"></param>
        /// <returns>Sum of all saddle points in matrix</returns>
        public static string SumSaddlePoints(this IJuggedMatrix<string> juggedMatrix)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    if (!juggedMatrix.IsMinInRow(i, j) || !juggedMatrix.IsMaxInColumn(i, j))
                        continue;
                    sb.Append(juggedMatrix[i, j]).Append(" ");
                }
            }

            return sb.ToString();
        }

        #endregion

        #endregion

        #endregion

        #region Nullable

        #region Int32?

        #region Math

        /// <summary>
        /// Returns the add <see cref="IJuggedMatrix{T}"/> <paramref name="first"/> and <see cref="IJuggedMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IJuggedMatrix<int?> Add(this IJuggedMatrix<int?> first, IJuggedMatrix<int?> second)
        {
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");
            for (var i = 0; i < first.RowsCount; i++)
            {
                if (first.ElementsInRow(i) != second.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<int?>(first.RowsCount, first.GetCountPerRow().ToArray());
            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ElementsInRow(i); j++)
                {
                    if (first[i, j] != null && second[i, j] != null)
                        juggedMatrix[i, j] = first[i, j].Value + second[i, j].Value;
                }
            }

            return juggedMatrix;
        }

        /// <summary>
        /// Returns the subtraction <see cref="IJuggedMatrix{T}"/> <paramref name="first"/> and <see cref="IJuggedMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IJuggedMatrix<int?> Sub(this IJuggedMatrix<int?> first, IJuggedMatrix<int?> second)
        {
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");
            for (var i = 0; i < first.RowsCount; i++)
            {
                if (first.ElementsInRow(i) != second.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<int?>(first.RowsCount, first.GetCountPerRow().ToArray());
            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ElementsInRow(i); j++)
                {
                    if (first[i, j] != null && second[i, j] != null)
                        juggedMatrix[i, j] = first[i, j].Value - second[i, j].Value;
                }
            }

            return juggedMatrix;
        }

        /// <summary>
        /// Returns the multiplication <see cref="IJuggedMatrix{T}"/> <paramref name="juggedMatrix"/> on <see cref="Int32"/> <paramref name="number"/>
        /// </summary>
        /// <param name="juggedMatrix">First</param>
        /// <param name="number"></param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result multiplication of matrix <paramref name="juggedMatrix"/> on <paramref name="number"/></returns>
        public static IJuggedMatrix<int?> MulOnNumber(this IJuggedMatrix<int?> juggedMatrix, int number)
        {
            var newJuggedMatrix = new JuggedMatrix<int?>(juggedMatrix.RowsCount, juggedMatrix.GetCountPerRow().ToArray());
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

        #region Creation

        /// <summary>
        /// Creates new <see cref="IJuggedMatrix{T}"/> identity matrix.
        /// <para></para>
        /// <example>
        /// n = 3
        /// <para/>
        /// matrix = <para/>
        /// {<para/>
        ///     {1,null,0},<para/>
        ///     {null,1,null},<para/>
        ///     {0,null,1}<para/>
        /// }
        /// </example>
        /// </summary>
        /// <param name="n">Count of rows and columns</param>
        /// <returns>Identity <see cref="IJuggedMatrix{T}"/> matrix</returns>
        public static IJuggedMatrix<int?> CreateIdentityMatrix(int n, bool includeNull = false)
        {
            var result = new JuggedMatrix<int?>(n, Enumerable.Repeat(n, n).ToArray());
            for (var i = 0; i < n; i++)
            {
                result[i, i] = 1;
                if (includeNull && i % 2 == 0)
                    result[i, i] = null;
            }
            return result;
        }

        #endregion

        #region Sum Operations

        /// <summary>
        /// Calculates sum of all numbers in main diagonal in <see cref="IJuggedMatrix{T}"/>
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static int DiagonalSum(this IJuggedMatrix<int?> juggedMatrix)
        {
            if (!juggedMatrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0;
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
                if (juggedMatrix[i, i] != null)
                    sum += juggedMatrix[i, i].Value;

            return sum;
        }

        /// <summary>
        /// Calculates sum of all numbers in <see cref="IJuggedMatrix{T}"/>
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static int Sum(this IJuggedMatrix<int?> juggedMatrix)
        {
            var sum = 0;
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    if (juggedMatrix[i, j] != null)
                        sum += juggedMatrix[i, j].Value;
                }
            }

            return sum;
        }

        #endregion

        #region Fill

        /// <summary>
        /// Fills <paramref name="juggedMatrix"/> with randomly numbers
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
        /// Returns the add <see cref="IJuggedMatrix{T}"/> <paramref name="first"/> and <see cref="IJuggedMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IJuggedMatrix<long?> Add(this IJuggedMatrix<long?> first, IJuggedMatrix<long?> second)
        {
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");
            for (var i = 0; i < first.RowsCount; i++)
            {
                if (first.ElementsInRow(i) != second.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<long?>(first.RowsCount, first.GetCountPerRow().ToArray());
            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ElementsInRow(i); j++)
                {
                    if (first[i, j] != null && second[i, j] != null)
                        juggedMatrix[i, j] = first[i, j].Value + second[i, j].Value;
                }
            }

            return juggedMatrix;
        }

        /// <summary>
        /// Returns the subtraction <see cref="IJuggedMatrix{T}"/> <paramref name="first"/> and <see cref="IJuggedMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IJuggedMatrix<long?> Sub(this IJuggedMatrix<long?> first, IJuggedMatrix<long?> second)
        {
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");
            for (var i = 0; i < first.RowsCount; i++)
            {
                if (first.ElementsInRow(i) != second.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<long?>(first.RowsCount, first.GetCountPerRow().ToArray());
            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ElementsInRow(i); j++)
                {
                    if (first[i, j] != null && second[i, j] != null)
                        juggedMatrix[i, j] = first[i, j].Value - second[i, j].Value;
                }
            }

            return juggedMatrix;
        }

        /// <summary>
        /// Returns the multiplication <see cref="IJuggedMatrix{T}"/> <paramref name="juggedMatrix"/> on <see cref="Int64"/> <paramref name="number"/>
        /// </summary>
        /// <param name="juggedMatrix">First</param>
        /// <param name="number"></param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result multiplication of matrix <paramref name="juggedMatrix"/> on <paramref name="number"/></returns>
        public static IJuggedMatrix<long?> MulOnNumber(this IJuggedMatrix<long?> juggedMatrix, long number)
        {
            var newJuggedMatrix = new JuggedMatrix<long?>(juggedMatrix.RowsCount, juggedMatrix.GetCountPerRow().ToArray());
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

        #region Sum Operations

        /// <summary>
        /// Calculates sum of all numbers in main diagonal in <see cref="IJuggedMatrix{T}"/>
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static long DiagonalSum(this IJuggedMatrix<long?> juggedMatrix)
        {
            if (!juggedMatrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            long sum = 0;
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
                if (juggedMatrix[i, i] != null)
                    sum += juggedMatrix[i, i].Value;

            return sum;
        }

        /// <summary>
        /// Calculates sum of all numbers in <see cref="IJuggedMatrix{T}"/>
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static double Sum(this IJuggedMatrix<long?> juggedMatrix)
        {
            var sum = 0.0d;
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    if (juggedMatrix[i, j] != null)
                        sum += juggedMatrix[i, j].Value;
                }
            }

            return sum;
        }

        #endregion

        #region Fill

        /// <summary>
        /// Fills <paramref name="juggedMatrix"/> with randomly numbers
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
                        juggedMatrix[i, j] = rnd.NextLong(min, max + 1);
                }
            }
        }

        #endregion

        #endregion

        #region double?

        #region Math

        /// <summary>
        /// Returns the add <see cref="IJuggedMatrix{T}"/> <paramref name="first"/> and <see cref="IJuggedMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IJuggedMatrix<double?> Add(this IJuggedMatrix<double?> first, IJuggedMatrix<double?> second)
        {
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");
            for (var i = 0; i < first.RowsCount; i++)
            {
                if (first.ElementsInRow(i) != second.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<double?>(first.RowsCount, first.GetCountPerRow().ToArray());
            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ElementsInRow(i); j++)
                {
                    if (first[i, j] != null && second[i, j] != null)
                        juggedMatrix[i, j] = first[i, j].Value + second[i, j].Value;
                }
            }

            return juggedMatrix;
        }

        /// <summary>
        /// Returns the subtraction <see cref="IJuggedMatrix{T}"/> <paramref name="first"/> and <see cref="IJuggedMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IJuggedMatrix<double?> Sub(this IJuggedMatrix<double?> first, IJuggedMatrix<double?> second)
        {
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");
            for (var i = 0; i < first.RowsCount; i++)
            {
                if (first.ElementsInRow(i) != second.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<double?>(first.RowsCount, first.GetCountPerRow().ToArray());
            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ElementsInRow(i); j++)
                {
                    if (first[i, j] != null && second[i, j] != null)
                        juggedMatrix[i, j] = first[i, j].Value - second[i, j].Value;
                }
            }

            return juggedMatrix;
        }

        /// <summary>
        /// Returns the multiplication <see cref="IJuggedMatrix{T}"/> <paramref name="juggedMatrix"/> on <see cref="Double"/> <paramref name="number"/>
        /// </summary>
        /// <param name="juggedMatrix">First</param>
        /// <param name="number"></param>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> - the result multiplication of matrix <paramref name="juggedMatrix"/> on <paramref name="number"/></returns>
        public static IJuggedMatrix<double?> MulOnNumber(this IJuggedMatrix<double?> juggedMatrix, double number)
        {
            var newJuggedMatrix = new JuggedMatrix<double?>(juggedMatrix.RowsCount, juggedMatrix.GetCountPerRow().ToArray());
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

        #region Sum Operations

        /// <summary>
        /// Calculates sum of all numbers in main diagonal in <see cref="IJuggedMatrix{T}"/>
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static double DiagonalSum(this IJuggedMatrix<double?> juggedMatrix)
        {
            if (!juggedMatrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sum = 0.0d;
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
                if (juggedMatrix[i, i] != null)
                    sum += juggedMatrix[i, i].Value;

            return sum;
        }

        /// <summary>
        /// Calculates sum of all numbers in <see cref="IJuggedMatrix{T}"/>
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static double Sum(this IJuggedMatrix<double?> juggedMatrix)
        {
            var sum = 0.0d;
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    if (juggedMatrix[i, j] != null)
                        sum += juggedMatrix[i, j].Value;
                }
            }

            return sum;
        }

        #endregion

        #region Fill
        
        /// <summary>
        /// Fills <paramref name="juggedMatrix"/> with randomly numbers
        /// </summary>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IJuggedMatrix<double?> juggedMatrix, double min = -50.0, double max = 50.0d)
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