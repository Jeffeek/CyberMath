﻿#region Using namespaces

using System;
using CyberMath.Extensions;
using CyberMath.Structures.Matrices.Base.Exceptions;
using CyberMath.Structures.Matrices.Matrix;

#endregion

namespace CyberMath.Structures.Matrices.Extensions
{
    /// <summary>
    ///     Extension methods for ValueType for <see cref="IMatrix{T}"/>
    ///     Supported types: <see cref="int"/>, <see cref="long"/>, <see cref="double"/>, <see cref="decimal"/>,
    ///     <see cref="string"/> and <see cref="Nullable{T}"/>
    /// </summary>
    public static class ValueTypeMatrixExtension
    {
        #region Not Nullable

        #region Int32

        #region Math

        /// <summary>
        ///     Returns the add <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<int> Add(this IMatrix<int> first, IMatrix<int> second)
        {
            if (first.ColumnsCount != second.ColumnsCount)
                throw new
                    MatrixInvalidOperationException("Can't add second matrix to matrix. Count of columns should be the same");

            if (first.RowsCount != second.RowsCount)
                throw new
                    MatrixInvalidOperationException("Can't add second matrix to matrix. Count of rows should be the same");

            var matrix = new Matrix<int>(first.RowsCount, first.ColumnsCount);

            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ColumnsCount; j++) matrix[i, j] = first[i, j] + second[i, j];
            }

            return matrix;
        }

        /// <summary>
        ///     Returns the multiplied <see cref="IMatrix{T}"/> <paramref name="first"/> by <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result of multiplying <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<int> Multiplication(this IMatrix<int> first, IMatrix<int> second)
        {
            if (first.ColumnsCount == second.RowsCount) return first.InternalMul(second);
            if (second.ColumnsCount == first.RowsCount) return second.InternalMul(first);

            throw new MatrixInvalidOperationException("Multiplication of this matrices is not possible");
        }

        /// <summary>
        ///     Returns the subtraction <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<int> Sub(this IMatrix<int> first, IMatrix<int> second)
        {
            if (first.ColumnsCount != second.ColumnsCount)
                throw new
                    MatrixInvalidOperationException("Can't sub second matrix to matrix. Count of columns should be the same");

            if (first.RowsCount != second.RowsCount)
                throw new
                    MatrixInvalidOperationException("Can't sub second matrix to matrix. Count of rows should be the same");

            var matrix = new Matrix<int>(first.RowsCount, first.ColumnsCount);

            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ColumnsCount; j++) matrix[i, j] = first[i, j] - second[i, j];
            }

            return matrix;
        }

        /// <summary>
        ///     Returns the multiplication <see cref="IMatrix{T}"/> <paramref name="matrix"/> on <see cref="int"/>
        ///     <paramref name="number"/>
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result multiplication of matrix <paramref name="matrix"/> on
        ///     <paramref name="number"/>
        /// </returns>
        public static IMatrix<int> MulOnNumber(this IMatrix<int> matrix, int number)
        {
            var newMatrix = new Matrix<int>(matrix.RowsCount, matrix.ColumnsCount);

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++) newMatrix[i, j] = matrix[i, j] * number;
            }

            return newMatrix;
        }

        #endregion

        #region Operations

        /// <summary>
        ///     Calculates determinant for <see cref="int"/> <see cref="IMatrix{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Determinant</returns>
        public static int CalculateDeterminant(this IMatrix<int> matrix)
        {
            if (!matrix.IsSquare)
                throw new InvalidOperationException("Determinant can be calculated only for square matrix");

            if (matrix.ColumnsCount == 2) return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            var result = 0;

            for (var j = 0; j < matrix.ColumnsCount; j++)
                result += (j % 2 == 1 ? 1 : -1)
                          * matrix[1, j]
                          * (matrix.CreateMatrixWithoutColumn(j)
                                   ?.CreateMatrixWithoutRow(1)).CalculateDeterminant();

            return result;
        }

        /// <summary>
        ///     Creates inverted matrix from <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>New inverted <see cref="IMatrix{T}"/></returns>
        public static IMatrix<double> CreateInvertibleMatrix(this IMatrix<int> matrix)
        {
            if (!matrix.IsSquare)
                return null;

            var determinant = matrix.CalculateDeterminant();

            IMatrix<double> result = new Matrix<double>(matrix.RowsCount, matrix.ColumnsCount);
            var resultMatrixRef = result;

            matrix.ProcessFunctionOverData((i, j) =>
                                           {
                                               resultMatrixRef[i, j] = Math.Round((i + j) % 2 == 1
                                                                                      ? -1
                                                                                      : 1 * matrix.CalculateMinor(i, j) / determinant,
                                                                                  2);
                                           });

            result = result.Transpose();

            return result;
        }

        /// <summary>
        ///     Calculates minor for <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Minor</returns>
        public static double CalculateMinor(this IMatrix<int> matrix, int i, int j) =>
            (matrix.CreateMatrixWithoutColumn(j)
                   ?.CreateMatrixWithoutRow(i)).CalculateDeterminant();

        #endregion

        #region Internal Mul

        private static IMatrix<int> InternalMul(this IMatrix<int> matrix, IMatrix<int> b)
        {
            var result = new Matrix<int>(matrix.RowsCount, b.ColumnsCount);

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < b.ColumnsCount; j++)
                {
                    for (var k = 0; k < b.RowsCount; k++) result[i, j] += matrix[i, k] * b[k, j];
                }
            }

            return result;
        }

        #endregion

        #region Fill

        /// <summary>
        ///     Fills <paramref name="matrix"/> with randomly numbers
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IMatrix<int> matrix, int min = -50, int max = 50)
        {
            var rnd = new Random();

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++) matrix[i, j] = rnd.Next(min, max);
            }
        }

        #endregion

        #endregion

        #region Long

        #region Math

        /// <summary>
        ///     Returns the multiplied <see cref="IMatrix{T}"/> <paramref name="first"/> by <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result of multiplying <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<long> Multiplication(this IMatrix<long> first, IMatrix<long> second)
        {
            if (first.ColumnsCount == second.RowsCount) return first.InternalMul(second);
            if (second.ColumnsCount == first.RowsCount) return second.InternalMul(first);

            throw new MatrixInvalidOperationException("Multiplication of this matrices is not possible");
        }

        /// <summary>
        ///     Returns the add <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<long> Add(this IMatrix<long> first, IMatrix<long> second)
        {
            if (first.ColumnsCount != second.ColumnsCount)
                throw new
                    MatrixInvalidOperationException("Can't add second matrix to matrix. Count of columns should be the same");

            if (first.RowsCount != second.RowsCount)
                throw new
                    MatrixInvalidOperationException("Can't add second matrix to matrix. Count of rows should be the same");

            var matrix = new Matrix<long>(first.RowsCount, first.ColumnsCount);

            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ColumnsCount; j++) matrix[i, j] = first[i, j] + second[i, j];
            }

            return matrix;
        }

        /// <summary>
        ///     Returns the subtraction <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<long> Sub(this IMatrix<long> first, IMatrix<long> second)
        {
            if (first.ColumnsCount != second.ColumnsCount)
                throw new
                    MatrixInvalidOperationException("Can't sub second matrix to matrix. Count of columns should be the same");

            if (first.RowsCount != second.RowsCount)
                throw new
                    MatrixInvalidOperationException("Can't sub second matrix to matrix. Count of rows should be the same");

            var matrix = new Matrix<long>(first.RowsCount, first.ColumnsCount);

            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ColumnsCount; j++) matrix[i, j] = first[i, j] - second[i, j];
            }

            return matrix;
        }

        /// <summary>
        ///     Returns the multiplication <see cref="IMatrix{T}"/> <paramref name="matrix"/> on <see cref="int"/>
        ///     <paramref name="number"/>
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result multiplication of matrix <paramref name="matrix"/> on
        ///     <paramref name="number"/>
        /// </returns>
        public static IMatrix<long> MulOnNumber(this IMatrix<long> matrix, long number)
        {
            var newMatrix = new Matrix<long>(matrix.RowsCount, matrix.ColumnsCount);

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++) newMatrix[i, j] = matrix[i, j] * number;
            }

            return newMatrix;
        }

        #endregion

        #region Operations

        /// <summary>
        ///     Calculates determinant for <see cref="long"/> <see cref="IMatrix{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Determinant</returns>
        public static long CalculateDeterminant(this IMatrix<long> matrix)
        {
            if (!matrix.IsSquare)
                throw new InvalidOperationException("Determinant can be calculated only for square matrix");

            if (matrix.ColumnsCount == 2) return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            long result = 0;

            for (var j = 0; j < matrix.ColumnsCount; j++)
                result += (j % 2 == 1 ? 1 : -1)
                          * matrix[1, j]
                          * (matrix.CreateMatrixWithoutColumn(j)
                                   ?.CreateMatrixWithoutRow(1)).CalculateDeterminant();

            return result;
        }

        /// <summary>
        ///     Creates inverted matrix from <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>New inverted <see cref="IMatrix{T}"/></returns>
        public static IMatrix<double> CreateInvertibleMatrix(this IMatrix<long> matrix)
        {
            if (!matrix.IsSquare)
                return null;

            var determinant = matrix.CalculateDeterminant();

            IMatrix<double> result = new Matrix<double>(matrix.RowsCount, matrix.ColumnsCount);
            var resultMatrixRef = result;

            matrix.ProcessFunctionOverData((i, j) =>
                                           {
                                               resultMatrixRef[i, j] = Math.Round((i + j) % 2 == 1
                                                                                      ? -1
                                                                                      : 1 * matrix.CalculateMinor(i, j) / determinant,
                                                                                  2);
                                           });

            result = result.Transpose();

            return result;
        }

        /// <summary>
        ///     Calculates minor for <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Minor</returns>
        public static double CalculateMinor(this IMatrix<long> matrix, int i, int j) =>
            (matrix.CreateMatrixWithoutColumn(j)
                   ?.CreateMatrixWithoutRow(i)).CalculateDeterminant();

        #endregion

        #region Internal Mul

        private static IMatrix<long> InternalMul(this IMatrix<long> matrix, IMatrix<long> second)
        {
            var result = new Matrix<long>(matrix.RowsCount, second.ColumnsCount);

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < second.ColumnsCount; j++)
                {
                    for (var k = 0; k < second.RowsCount; k++) result[i, j] += matrix[i, k] * second[k, j];
                }
            }

            return result;
        }

        #endregion

        #region Fill

        /// <summary>
        ///     Fills <paramref name="matrix"/> with randomly numbers
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IMatrix<long> matrix, long min = -50, long max = 50)
        {
            var rnd = new Random();

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++) matrix[i, j] = rnd.NextLong(min, max);
            }
        }

        #endregion

        #endregion

        #region Double

        #region Math

        /// <summary>
        ///     Returns the multiplied <see cref="IMatrix{T}"/> <paramref name="first"/> by <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result of multiplying <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<double> Multiplication(this IMatrix<double> first, IMatrix<double> second)
        {
            if (first.ColumnsCount == second.RowsCount) return first.InternalMul(second);
            if (second.ColumnsCount == first.RowsCount) return second.InternalMul(first);

            throw new MatrixInvalidOperationException("Multiplication of this matrices is not possible");
        }

        /// <summary>
        ///     Returns the add <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<double> Add(this IMatrix<double> first, IMatrix<double> second)
        {
            if (first.ColumnsCount != second.ColumnsCount)
                throw new
                    MatrixInvalidOperationException("Can't add second matrix to matrix. Count of columns should be the same");

            if (first.RowsCount != second.RowsCount)
                throw new
                    MatrixInvalidOperationException("Can't add second matrix to matrix. Count of rows should be the same");

            var matrix = new Matrix<double>(first.RowsCount, first.ColumnsCount);

            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ColumnsCount; j++) matrix[i, j] = first[i, j] + second[i, j];
            }

            return matrix;
        }

        /// <summary>
        ///     Returns the subtraction <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<double> Sub(this IMatrix<double> first, IMatrix<double> second)
        {
            if (first.ColumnsCount != second.ColumnsCount)
                throw new
                    MatrixInvalidOperationException("Can't sub second matrix to matrix. Count of columns should be the same");

            if (first.RowsCount != second.RowsCount)
                throw new
                    MatrixInvalidOperationException("Can't sub second matrix to matrix. Count of rows should be the same");

            var matrix = new Matrix<double>(first.RowsCount, first.ColumnsCount);

            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ColumnsCount; j++) matrix[i, j] = first[i, j] - second[i, j];
            }

            return matrix;
        }

        /// <summary>
        ///     Returns the multiplication <see cref="IMatrix{T}"/> <paramref name="matrix"/> on <see cref="int"/>
        ///     <paramref name="number"/>
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result multiplication of matrix <paramref name="matrix"/> on
        ///     <paramref name="number"/>
        /// </returns>
        public static IMatrix<double> MulOnNumber(this IMatrix<double> matrix, double number)
        {
            var newMatrix = new Matrix<double>(matrix.RowsCount, matrix.ColumnsCount);

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++) newMatrix[i, j] = matrix[i, j] * number;
            }

            return newMatrix;
        }

        #endregion

        #region Operations

        /// <summary>
        ///     Calculates determinant for <see cref="long"/> <see cref="IMatrix{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Determinant</returns>
        public static double CalculateDeterminant(this IMatrix<double> matrix)
        {
            if (!matrix.IsSquare)
                throw new InvalidOperationException("Determinant can be calculated only for square matrix");

            if (matrix.ColumnsCount == 2) return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            double result = 0;

            for (var j = 0; j < matrix.ColumnsCount; j++)
                result += (j % 2 == 1 ? 1 : -1)
                          * matrix[1, j]
                          * (matrix.CreateMatrixWithoutColumn(j)
                                   ?.CreateMatrixWithoutRow(1)).CalculateDeterminant();

            return result;
        }

        /// <summary>
        ///     Creates inverted matrix from <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>New inverted <see cref="IMatrix{T}"/></returns>
        public static IMatrix<double> CreateInvertibleMatrix(this IMatrix<double> matrix)
        {
            if (!matrix.IsSquare)
                return null;

            var determinant = matrix.CalculateDeterminant();

            IMatrix<double> result = new Matrix<double>(matrix.RowsCount, matrix.ColumnsCount);
            var resultMatrixRef = result;

            matrix.ProcessFunctionOverData((i, j) =>
                                           {
                                               resultMatrixRef[i, j] = Math.Round((i + j) % 2 == 1
                                                                                      ? -1
                                                                                      : 1 * matrix.CalculateMinor(i, j) / determinant,
                                                                                  2);
                                           });

            result = result.Transpose();

            return result;
        }

        /// <summary>
        ///     Calculates minor for <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Minor</returns>
        public static double CalculateMinor(this IMatrix<double> matrix, int i, int j) =>
            (matrix.CreateMatrixWithoutColumn(j)
                   ?.CreateMatrixWithoutRow(i)).CalculateDeterminant();

        #endregion

        #region Internal Mul

        private static IMatrix<double> InternalMul(this IMatrix<double> matrix, IMatrix<double> second)
        {
            var result = new Matrix<double>(matrix.RowsCount, second.ColumnsCount);

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < second.ColumnsCount; j++)
                {
                    for (var k = 0; k < second.RowsCount; k++) result[i, j] += matrix[i, k] * second[k, j];
                }
            }

            return result;
        }

        #endregion

        #region Fill

        /// <summary>
        ///     Fills <paramref name="matrix"/> with randomly numbers
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IMatrix<double> matrix, double min = -50d, double max = 50d)
        {
            var rnd = new Random();

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++) matrix[i, j] = rnd.NextDouble(min, max);
            }
        }

        #endregion

        #endregion

        #region Decimal

        #region Math

        /// <summary>
        ///     Returns the multiplied <see cref="IMatrix{T}"/> <paramref name="first"/> by <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result of multiplying <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<decimal> Multiplication(this IMatrix<decimal> first, IMatrix<decimal> second)
        {
            if (first.ColumnsCount == second.RowsCount) return first.InternalMul(second);
            if (second.ColumnsCount == first.RowsCount) return second.InternalMul(first);

            throw new MatrixInvalidOperationException("Multiplication of this matrices is not possible");
        }

        /// <summary>
        ///     Returns the add <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<decimal> Add(this IMatrix<decimal> first, IMatrix<decimal> second)
        {
            if (first.ColumnsCount != second.ColumnsCount)
                throw new
                    MatrixInvalidOperationException("Can't add second matrix to matrix. Count of columns should be the same");

            if (first.RowsCount != second.RowsCount)
                throw new
                    MatrixInvalidOperationException("Can't add second matrix to matrix. Count of rows should be the same");

            var matrix = new Matrix<decimal>(first.RowsCount, first.ColumnsCount);

            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ColumnsCount; j++) matrix[i, j] = first[i, j] + second[i, j];
            }

            return matrix;
        }

        /// <summary>
        ///     Returns the subtraction <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<decimal> Sub(this IMatrix<decimal> first, IMatrix<decimal> second)
        {
            if (first.ColumnsCount != second.ColumnsCount)
                throw new
                    MatrixInvalidOperationException("Can't sub second matrix to matrix. Count of columns should be the same");

            if (first.RowsCount != second.RowsCount)
                throw new
                    MatrixInvalidOperationException("Can't sub second matrix to matrix. Count of rows should be the same");

            var matrix = new Matrix<decimal>(first.RowsCount, first.ColumnsCount);

            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ColumnsCount; j++) matrix[i, j] = first[i, j] - second[i, j];
            }

            return matrix;
        }

        /// <summary>
        ///     Returns the multiplication <see cref="IMatrix{T}"/> <paramref name="matrix"/> on <see cref="int"/>
        ///     <paramref name="number"/>
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result multiplication of matrix <paramref name="matrix"/> on
        ///     <paramref name="number"/>
        /// </returns>
        public static IMatrix<decimal> MulOnNumber(this IMatrix<decimal> matrix, decimal number)
        {
            var newMatrix = new Matrix<decimal>(matrix.RowsCount, matrix.ColumnsCount);

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++) newMatrix[i, j] = matrix[i, j] * number;
            }

            return newMatrix;
        }

        #endregion

        #region Operations

        /// <summary>
        ///     Calculates determinant for <see cref="decimal"/> <see cref="IMatrix{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Determinant</returns>
        public static decimal CalculateDeterminant(this IMatrix<decimal> matrix)
        {
            if (!matrix.IsSquare)
                throw new InvalidOperationException("Determinant can be calculated only for square matrix");

            if (matrix.ColumnsCount == 2) return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            decimal result = 0;

            for (var j = 0; j < matrix.ColumnsCount; j++)
                result += (j % 2 == 1 ? 1 : -1)
                          * matrix[1, j]
                          * (matrix.CreateMatrixWithoutColumn(j)
                                   ?.CreateMatrixWithoutRow(1)).CalculateDeterminant();

            return result;
        }

        /// <summary>
        ///     Creates inverted matrix from <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>New inverted <see cref="IMatrix{T}"/></returns>
        public static IMatrix<decimal> CreateInvertibleMatrix(this IMatrix<decimal> matrix)
        {
            if (!matrix.IsSquare)
                return null;

            var determinant = matrix.CalculateDeterminant();

            IMatrix<decimal> result = new Matrix<decimal>(matrix.RowsCount, matrix.ColumnsCount);
            var resultMatrixRef = result;

            matrix.ProcessFunctionOverData((i, j) =>
                                           {
                                               resultMatrixRef[i, j] = Math.Round((i + j) % 2 == 1
                                                                                      ? -1
                                                                                      : 1 * matrix.CalculateMinor(i, j) / determinant,
                                                                                  4);
                                           });

            result = result.Transpose();

            return result;
        }

        /// <summary>
        ///     Calculates minor for <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Minor</returns>
        public static decimal CalculateMinor(this IMatrix<decimal> matrix, int i, int j) =>
            (matrix.CreateMatrixWithoutColumn(j)
                   ?.CreateMatrixWithoutRow(i)).CalculateDeterminant();

        #endregion

        #region Internal Mul

        private static IMatrix<decimal> InternalMul(this IMatrix<decimal> matrix, IMatrix<decimal> second)
        {
            var result = new Matrix<decimal>(matrix.RowsCount, second.ColumnsCount);

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < second.ColumnsCount; j++)
                {
                    for (var k = 0; k < second.RowsCount; k++) result[i, j] += matrix[i, k] * second[k, j];
                }
            }

            return result;
        }

        #endregion

        #region Fill

        /// <summary>
        ///     Fills <paramref name="matrix"/> with randomly numbers
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IMatrix<decimal> matrix, decimal min = -50, decimal max = 50)
        {
            var rnd = new Random();

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++)
                    matrix[i, j] = (decimal)rnd.NextDouble((double)min, (double)max);
            }
        }

        #endregion

        #endregion

        #endregion

        #region Nullable

        #region string

        #region Math

        /// <summary>
        ///     Returns the add <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<string> Add(this IMatrix<string> first, IMatrix<string> second)
        {
            if (first.ColumnsCount != second.ColumnsCount)
                throw new
                    MatrixInvalidOperationException("Can't add second matrix to matrix. Count of columns should be the same");

            if (first.RowsCount != second.RowsCount)
                throw new
                    MatrixInvalidOperationException("Can't add second matrix to matrix. Count of rows should be the same");

            var matrix = new Matrix<string>(first.RowsCount, first.ColumnsCount);

            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ColumnsCount; j++) matrix[i, j] = first[i, j] + second[i, j];
            }

            return matrix;
        }

        /// <summary>
        ///     Returns the multiplication <see cref="IMatrix{T}"/> <paramref name="matrix"/> on <see cref="int"/>
        ///     <paramref name="number"/>
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result multiplication of matrix <paramref name="matrix"/> on
        ///     <paramref name="number"/>
        /// </returns>
        public static IMatrix<string> MulOnNumber(this IMatrix<string> matrix, int number)
        {
            var newMatrix = new Matrix<string>(matrix.RowsCount, matrix.ColumnsCount);

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++)
                    newMatrix[i, j] = matrix[i, j]
                        .Concat(number);
            }

            return newMatrix;
        }

        #endregion

        #region Fill

        /// <summary>
        ///     Fills <paramref name="matrix"/> with randomly strings with <see cref="Guid"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="guid">GUID object</param>
        /// <param name="length">Length of string</param>
        public static void FillRandomly(this IMatrix<string> matrix, Guid guid, int length = 10)
        {
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++)
                    matrix[i, j] = guid.ToString()
                                       .Substring(0, length);
            }
        }

        #endregion

        #endregion

        #region Int32?

        #region Math

        /// <summary>
        ///     Returns the multiplied <see cref="IMatrix{T}"/> <paramref name="first"/> by <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result of multiplying <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<int?> Multiplication(this IMatrix<int?> first, IMatrix<int?> second)
        {
            if (first.ColumnsCount == second.RowsCount) return first.InternalMul(second);
            if (second.ColumnsCount == first.RowsCount) return second.InternalMul(first);

            throw new MatrixInvalidOperationException("Multiplication of this matrices is not possible");
        }

        /// <summary>
        ///     Returns the add <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<int?> Add(this IMatrix<int?> first, IMatrix<int?> second)
        {
            if (first.ColumnsCount != second.ColumnsCount)
                throw new
                    MatrixInvalidOperationException("Can't add second matrix to matrix. Count of columns should be the same");

            if (first.RowsCount != second.RowsCount)
                throw new
                    MatrixInvalidOperationException("Can't add second matrix to matrix. Count of rows should be the same");

            var matrix = new Matrix<int?>(first.RowsCount, first.ColumnsCount);

            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ColumnsCount; j++)
                    if (first[i, j] != null && second[i, j] != null)
                        matrix[i, j] = first[i, j] + second[i, j];
                    else
                        matrix[i, j] = null;
            }

            return matrix;
        }

        /// <summary>
        ///     Returns the subtraction <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<int?> Sub(this IMatrix<int?> first, IMatrix<int?> second)
        {
            if (first.ColumnsCount != second.ColumnsCount)
                throw new
                    MatrixInvalidOperationException("Can't sub second matrix to matrix. Count of columns should be the same");

            if (first.RowsCount != second.RowsCount)
                throw new
                    MatrixInvalidOperationException("Can't sub second matrix to matrix. Count of rows should be the same");

            var matrix = new Matrix<int?>(first.RowsCount, first.ColumnsCount);

            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ColumnsCount; j++)
                    if (first[i, j] != null && second[i, j] != null)
                        matrix[i, j] = first[i, j] - second[i, j];
                    else
                        matrix[i, j] = null;
            }

            return matrix;
        }

        /// <summary>
        ///     Returns the multiplication <see cref="IMatrix{T}"/> <paramref name="matrix"/> on <see cref="int"/>
        ///     <paramref name="number"/>
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result multiplication of matrix <paramref name="matrix"/> on
        ///     <paramref name="number"/>
        /// </returns>
        public static IMatrix<int?> MulOnNumber(this IMatrix<int?> matrix, int number)
        {
            var newMatrix = new Matrix<int?>(matrix.RowsCount, matrix.ColumnsCount);

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++)
                    if (newMatrix[i, j] != null)
                        newMatrix[i, j] = matrix[i, j] * number;
            }

            return newMatrix;
        }

        #endregion

        #region Operations

        /// <summary>
        ///     Calculates determinant for <seealso cref="Nullable{T}"/> <see cref="int"/> <see cref="IMatrix{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Determinant</returns>
        public static int CalculateDeterminant(this IMatrix<int?> matrix)
        {
            if (!matrix.IsSquare)
                throw new InvalidOperationException("Determinant can be calculated only for square matrix");

            if (matrix.ColumnsCount == 2)
            {
                if (matrix[0, 0] != null && matrix[1, 1] != null && matrix[0, 1] != null && matrix[1, 0] != null)
                    return matrix[0, 0]
                               .Value
                           * matrix[1, 1]
                               .Value
                           - matrix[0, 1]
                               .Value
                           * matrix[1, 0]
                               .Value;

                return 0;
            }

            var result = 0;

            for (var j = 0; j < matrix.ColumnsCount; j++)
                result += (j % 2 == 1 ? 1 : -1) * matrix[1, j]
                          ?? 0
                          * (matrix.CreateMatrixWithoutColumn(j)
                                   ?.CreateMatrixWithoutRow(1)).CalculateDeterminant();

            return result;
        }

        /// <summary>
        ///     Creates inverted matrix from <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>New inverted <see cref="IMatrix{T}"/></returns>
        public static IMatrix<double?> CreateInvertibleMatrix(this IMatrix<int?> matrix)
        {
            if (!matrix.IsSquare)
                return null;

            var determinant = matrix.CalculateDeterminant();

            IMatrix<double?> result = new Matrix<double?>(matrix.RowsCount, matrix.ColumnsCount);
            var resultMatrixRef = result;

            matrix.ProcessFunctionOverData((i, j) =>
                                           {
                                               resultMatrixRef[i, j] =
                                                   Math
                                                       .Round((i + j) % 2 == 1 ? -1 : 1 * matrix.CalculateMinor(i, j) / determinant,
                                                              2);
                                           });

            result = result.Transpose();

            return result;
        }

        /// <summary>
        ///     Calculates minor for <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Minor</returns>
        public static double CalculateMinor(this IMatrix<int?> matrix, int i, int j) =>
            (matrix.CreateMatrixWithoutColumn(j)
                   ?.CreateMatrixWithoutRow(i)).CalculateDeterminant();

        #endregion

        #region Internal Mul

        private static IMatrix<int?> InternalMul(this IMatrix<int?> matrix, IMatrix<int?> second)
        {
            var result = new Matrix<int?>(matrix.RowsCount, second.ColumnsCount);

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < second.ColumnsCount; j++)
                {
                    for (var k = 0; k < second.RowsCount; k++)
                        if (matrix[i, k] != null && matrix[k, j] != null)
                            result[i, j] += matrix[i, k] * second[k, j];
                        else
                            break;
                }
            }

            return result;
        }

        #endregion

        #region Fill

        public static void FillRandomly(this IMatrix<int?> matrix,
                                        int min = -50,
                                        int max = 50,
                                        bool includeNull = false)
        {
            var rnd = new Random();

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (i + j * i / j - i + j % 2 == 0 && includeNull)
                    {
                        matrix[i, j] = null;

                        continue;
                    }

                    matrix[i, j] = rnd.Next(min, max);
                }
            }
        }

        #endregion

        #endregion

        #region Int64?

        #region Math

        /// <summary>
        ///     Returns the multiplied <see cref="IMatrix{T}"/> <paramref name="first"/> by <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result of multiplying <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<long?> Multiplication(this IMatrix<long?> first, IMatrix<long?> second)
        {
            if (first.ColumnsCount == second.RowsCount) return first.InternalMul(second);
            if (second.ColumnsCount == first.RowsCount) return second.InternalMul(first);

            throw new MatrixInvalidOperationException("Multiplication of this matrices is not possible");
        }

        /// <summary>
        ///     Returns the add <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<long?> Add(this IMatrix<long?> first, IMatrix<long?> second)
        {
            if (first.ColumnsCount != second.ColumnsCount)
                throw new
                    MatrixInvalidOperationException("Can't add second matrix to matrix. Count of columns should be the same");

            if (first.RowsCount != second.RowsCount)
                throw new
                    MatrixInvalidOperationException("Can't add second matrix to matrix. Count of rows should be the same");

            var matrix = new Matrix<long?>(first.RowsCount, first.ColumnsCount);

            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ColumnsCount; j++)
                    if (first[i, j] != null && second[i, j] != null)
                        matrix[i, j] = first[i, j] + second[i, j];
                    else
                        matrix[i, j] = null;
            }

            return matrix;
        }

        /// <summary>
        ///     Returns the subtraction <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<long?> Sub(this IMatrix<long?> first, IMatrix<long?> second)
        {
            if (first.ColumnsCount != second.ColumnsCount)
                throw new
                    MatrixInvalidOperationException("Can't sub second matrix to matrix. Count of columns should be the same");

            if (first.RowsCount != second.RowsCount)
                throw new
                    MatrixInvalidOperationException("Can't sub second matrix to matrix. Count of rows should be the same");

            var matrix = new Matrix<long?>(first.RowsCount, first.ColumnsCount);

            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ColumnsCount; j++)
                    if (first[i, j] != null && second[i, j] != null)
                        matrix[i, j] = first[i, j] - second[i, j];
                    else
                        matrix[i, j] = null;
            }

            return matrix;
        }

        /// <summary>
        ///     Returns the multiplication <see cref="IMatrix{T}"/> <paramref name="matrix"/> on <see cref="int"/>
        ///     <paramref name="number"/>
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result multiplication of matrix <paramref name="matrix"/> on
        ///     <paramref name="number"/>
        /// </returns>
        public static IMatrix<long?> MulOnNumber(this IMatrix<long?> matrix, long number)
        {
            var newMatrix = new Matrix<long?>(matrix.RowsCount, matrix.ColumnsCount);

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++)
                    if (matrix[i, j] != null)
                        newMatrix[i, j] = matrix[i, j] * number;
            }

            return newMatrix;
        }

        #endregion

        #region Operations

        /// <summary>
        ///     Calculates determinant for <seealso cref="Nullable{T}"/> <see cref="long"/> <see cref="IMatrix{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Determinant</returns>
        public static long CalculateDeterminant(this IMatrix<long?> matrix)
        {
            if (!matrix.IsSquare)
                throw new InvalidOperationException("Determinant can be calculated only for square matrix");

            if (matrix.ColumnsCount == 2)
            {
                if (matrix[0, 0] != null && matrix[1, 1] != null && matrix[0, 1] != null && matrix[1, 0] != null)
                    return matrix[0, 0]
                               .Value
                           * matrix[1, 1]
                               .Value
                           - matrix[0, 1]
                               .Value
                           * matrix[1, 0]
                               .Value;

                return 0;
            }

            long result = 0;

            for (var j = 0; j < matrix.ColumnsCount; j++)
                result += (j % 2 == 1 ? 1 : -1) * matrix[1, j] == null
                              ? 0
                              : matrix[1, j]
                                    .Value
                                * (matrix.CreateMatrixWithoutColumn(j)
                                         ?.CreateMatrixWithoutRow(1)).CalculateDeterminant();

            return result;
        }

        /// <summary>
        ///     Creates inverted matrix from <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>New inverted <see cref="IMatrix{T}"/></returns>
        public static IMatrix<double> CreateInvertibleMatrix(this IMatrix<long?> matrix)
        {
            if (!matrix.IsSquare)
                throw new InvalidOperationException("Creating invertible matrix is possible only for square matrix");

            var determinant = matrix.CalculateDeterminant();

            IMatrix<double> result = new Matrix<double>(matrix.RowsCount, matrix.ColumnsCount);
            var resultMatrixRef = result;

            matrix.ProcessFunctionOverData((i, j) =>
                                           {
                                               resultMatrixRef[i, j] = Math.Round((i + j) % 2 == 1
                                                                                      ? -1
                                                                                      : 1 * matrix.CalculateMinor(i, j) / determinant,
                                                                                  2);
                                           });

            result = result.Transpose();

            return result;
        }

        /// <summary>
        ///     Calculates minor for <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Minor</returns>
        public static double CalculateMinor(this IMatrix<long?> matrix, int i, int j) =>
            (matrix.CreateMatrixWithoutColumn(j)
                   ?.CreateMatrixWithoutRow(i)).CalculateDeterminant();

        #endregion

        #region Internal Mul

        private static IMatrix<long?> InternalMul(this IMatrix<long?> matrix, IMatrix<long?> second)
        {
            var result = new Matrix<long?>(matrix.RowsCount, second.ColumnsCount);

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < second.ColumnsCount; j++)
                {
                    for (var k = 0; k < second.RowsCount; k++)
                        if (matrix[i, k] == null && second[k, j] == null)
                            matrix[i, k] = null;
                        else
                            result[i, j] += matrix[i, k] * second[k, j];
                }
            }

            return result;
        }

        #endregion

        #region Fill

        public static void FillRandomly(this IMatrix<long?> matrix,
                                        long min = -50,
                                        long max = 50,
                                        bool includeNull = false)
        {
            var rnd = new Random();

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (i + j * i / j - i + j % 2 == 0 && includeNull)
                    {
                        matrix[i, j] = null;

                        continue;
                    }

                    matrix[i, j] = rnd.Next((int)min, (int)max);
                }
            }
        }

        #endregion

        #endregion

        #region Double?

        #region Math

        /// <summary>
        ///     Returns the multiplied <see cref="IMatrix{T}"/> <paramref name="first"/> by <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result of multiplying <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<double?> Multiplication(this IMatrix<double?> first, IMatrix<double?> second)
        {
            if (first.ColumnsCount == second.RowsCount) return first.InternalMul(second);
            if (second.ColumnsCount == first.RowsCount) return second.InternalMul(first);

            throw new MatrixInvalidOperationException("Multiplication of this matrices is not possible");
        }

        /// <summary>
        ///     Returns the add <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<double?> Add(this IMatrix<double?> first, IMatrix<double?> second)
        {
            if (first.ColumnsCount != second.ColumnsCount)
                throw new
                    MatrixInvalidOperationException("Can't add second matrix to matrix. Count of columns should be the same");

            if (first.RowsCount != second.RowsCount)
                throw new
                    MatrixInvalidOperationException("Can't add second matrix to matrix. Count of rows should be the same");

            var matrix = new Matrix<double?>(first.RowsCount, first.ColumnsCount);

            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ColumnsCount; j++)
                    if (first[i, j] != null && second[i, j] != null)
                        matrix[i, j] = first[i, j] + second[i, j];
                    else
                        matrix[i, j] = null;
            }

            return matrix;
        }

        /// <summary>
        ///     Returns the subtraction <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/>
        ///     <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>
        ///     New <see cref="IMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and
        ///     <paramref name="second"/>
        /// </returns>
        public static IMatrix<double?> Sub(this IMatrix<double?> first, IMatrix<double?> second)
        {
            if (first.ColumnsCount != second.ColumnsCount)
                throw new
                    MatrixInvalidOperationException("Can't sub second matrix to matrix. Count of columns should be the same");

            if (first.RowsCount != second.RowsCount)
                throw new
                    MatrixInvalidOperationException("Can't sub second matrix to matrix. Count of rows should be the same");

            var matrix = new Matrix<double?>(first.RowsCount, first.ColumnsCount);

            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ColumnsCount; j++)
                    if (first[i, j] != null && second[i, j] != null)
                        matrix[i, j] = first[i, j] - second[i, j];
                    else
                        matrix[i, j] = null;
            }

            return matrix;
        }

        /// <summary>
        ///     <inheritdoc cref="MulOnNumber(IMatrix{int},int)"/>
        /// </summary>
        /// <param name="first"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static IMatrix<double?> MulOnNumber(this IMatrix<double?> first, double number)
        {
            var matrix = new Matrix<double?>(first.RowsCount, first.ColumnsCount);

            for (var i = 0; i < first.RowsCount; i++)
            {
                for (var j = 0; j < first.ColumnsCount; j++)
                    if (matrix[i, j] != null)
                        matrix[i, j] = first[i, j] * number;
                    else
                        matrix[i, j] = null;
            }

            return matrix;
        }

        #endregion

        #region Operations

        /// <summary>
        ///     Calculates determinant for <seealso cref="Nullable{T}"/> <see cref="double"/> <see cref="IMatrix{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Determinant</returns>
        public static double CalculateDeterminant(this IMatrix<double?> matrix)
        {
            if (!matrix.IsSquare)
                throw new InvalidOperationException("Determinant can be calculated only for square matrix");

            if (matrix.ColumnsCount == 2)
            {
                if (matrix[0, 0] != null && matrix[1, 1] != null && matrix[0, 1] != null && matrix[1, 0] != null)
                    return matrix[0, 0]
                               .Value
                           * matrix[1, 1]
                               .Value
                           - matrix[0, 1]
                               .Value
                           * matrix[1, 0]
                               .Value;

                return 0;
            }

            double result = 0;

            for (var j = 0; j < matrix.ColumnsCount; j++)
                result += (j % 2 == 1 ? 1 : -1) * matrix[1, j]
                          ?? 0
                          * (matrix.CreateMatrixWithoutColumn(j)
                                   ?.CreateMatrixWithoutRow(1)).CalculateDeterminant();

            return result;
        }

        /// <summary>
        ///     Creates inverted matrix from <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>New inverted <see cref="IMatrix{T}"/></returns>
        public static IMatrix<double?> CreateInvertibleMatrix(this IMatrix<double?> matrix)
        {
            if (!matrix.IsSquare)
                return null;

            var determinant = matrix.CalculateDeterminant();

            IMatrix<double?> result = new Matrix<double?>(matrix.RowsCount, matrix.ColumnsCount);
            var resultMatrixRef = result;

            matrix.ProcessFunctionOverData((i, j) =>
                                           {
                                               resultMatrixRef[i, j] = Math.Round((i + j) % 2 == 1
                                                                                      ? -1
                                                                                      : 1 * matrix.CalculateMinor(i, j) / determinant,
                                                                                  2);
                                           });

            result = result.Transpose();

            return result;
        }

        /// <summary>
        ///     Calculates minor for <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Minor</returns>
        public static double CalculateMinor(this IMatrix<double?> matrix, int i, int j) =>
            (matrix.CreateMatrixWithoutColumn(j)
                   ?.CreateMatrixWithoutRow(i)).CalculateDeterminant();

        #endregion

        #region Internal Mul

        private static IMatrix<double?> InternalMul(this IMatrix<double?> matrix, IMatrix<double?> second)
        {
            var result = new Matrix<double?>(matrix.RowsCount, second.ColumnsCount);

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < second.ColumnsCount; j++)
                {
                    for (var k = 0; k < second.RowsCount; k++)
                        if (matrix[i, k] != null && second[k, j] != null)
                            result[i, j] += matrix[i, k] * second[k, j];
                        else
                            break;
                }
            }

            return result;
        }

        #endregion

        #region Fill

        public static void FillRandomly(this IMatrix<double?> matrix,
                                        double min = -50d,
                                        double max = 50d,
                                        bool includeNull = false)
        {
            var rnd = new Random();

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++)
                {
                    var value = rnd.NextDouble(min, max);

                    if (i + j * i / j - i + j % 2 == 0 && includeNull)
                    {
                        matrix[i, j] = null;

                        continue;
                    }

                    matrix[i, j] = value;
                }
            }
        }

        #endregion

        #endregion

        #endregion
    }
}