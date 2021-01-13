﻿using CyberMath.Extensions;
using CyberMath.Structures.Matrix;
using CyberMath.Structures.MatrixBase.Exceptions;
using CyberMath.Structures.MatrixBase;
using System;
using System.Text;

namespace CyberMath.Structures.MatrixExtensions.Matrix
{
    /// <summary>
    /// Extension methods for ValueType for <see cref="IMatrix{T}"/>
    /// Support types: <see cref="Int32"/>, <see cref="Int64"/>, <see cref="Double"/>, <see cref="Decimal"/>, <see cref="String"/> and <see cref="Nullable{T}"/>
    /// </summary>
    public static class ValueTypeMatrixExtension
    {
        #region Int32

        #region Math

        /// <summary>
        /// Returns the multiplied <see cref="IMatrix{T}"/> <paramref name="first"/> by <see cref="IMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IMatrix{T}"/> - the result of multiplying <paramref name="first"/> and <paramref name="second"/></returns>
        public static IMatrix<int> Multiplication(this IMatrix<int> first, IMatrix<int> second)
        {
            if (first.ColumnsCount == second.RowsCount) return first.InternalMulAtoB(second);
            if (second.ColumnsCount == first.RowsCount) return first.InternalMulBtoA(second);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        /// <summary>
        /// Returns the add <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IMatrix<int> Add(this IMatrix<int> first, IMatrix<int> second)
        {
            if (first.ColumnsCount != second.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<int>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    matrix[i, j] = first[i, j] + second[i, j];
                }
            }

            return matrix;
        }

        /// <summary>
        /// Returns the subtraction <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IMatrix<int> Sub(this IMatrix<int> first, IMatrix<int> second)
        {
            if (first.ColumnsCount != second.ColumnsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of columns should be the same");
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<int>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    matrix[i, j] = first[i, j] - second[i, j];
                }
            }

            return matrix;
        }

        /// <summary>
        /// Returns the multiplication <see cref="IMatrix{T}"/> <paramref name="matrix"/> on <see cref="Int32"/> <paramref name="number"/>
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>New <see cref="IMatrix{T}"/> - the result multiplication of matrix <paramref name="matrix"/> on <paramref name="number"/></returns>
        public static IMatrix<int> MulOnNumber(this Matrix<int> matrix, int number)
        {
            var newMatrix = new Matrix<int>(matrix.RowsCount, matrix.ColumnsCount);
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    newMatrix[i, j] = matrix[i, j] * number;
                }
            }

            return newMatrix;
        }

        #endregion

        #region Creation

        /// <summary>
        /// Creates new <see cref="IMatrix{T}"/> identity matrix.
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
        /// <returns>Identity <see cref="IMatrix{T}"/> matrix</returns>
        public static IMatrix<int> CreateIdentityMatrix(int n)
        {
            var result = new Matrix<int>(n, n);
            for (var i = 0; i < n; i++)
            {
                result[i, i] = 1;
            }
            return result;
        }

        #endregion

        #region Operations

        /// <summary>
        /// Calculates determinant for <see cref="Int32"/> <see cref="IMatrix{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Determinant</returns>
        public static int CalculateDeterminant(this IMatrix<int> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new InvalidOperationException(
                    "Determinant can be calculated only for square matrix");
            }
            if (matrix.ColumnsCount == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            int result = 0;
            for (var j = 0; j < matrix.ColumnsCount; j++)
            {
                result += (j % 2 == 1 ? 1 : -1) * matrix[1, j] *
                          ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<int>)?.CreateMatrixWithoutRow(1) as IMatrix<int>).CalculateDeterminant();
            }
            return result;
        }

        /// <summary>
        /// Creates inverted matrix from <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>New inverted <see cref="IMatrix{T}"/></returns>
        public static IMatrix<double> CreateInvertibleMatrix(this IMatrix<int> matrix)
        {
            if (!matrix.IsSquare)
                return null;
            var determinant = matrix.CalculateDeterminant();

            IMatrix<double> result = new Matrix<double>(matrix.RowsCount, matrix.ColumnsCount);
            matrix.ProcessFunctionOverData((i, j) =>
            {
                result[i, j] = Math.Round((i + j) % 2 == 1 ? -1 : 1 *
                    matrix.CalculateMinor(i, j) / determinant, 2);
            });

            result = result.Transpose();
            return result;
        }

        /// <summary>
        /// Calculates minor for <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Minor</returns>
        public static double CalculateMinor(this IMatrix<int> matrix, int i, int j) => ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<int>)?.CreateMatrixWithoutRow(i) as IMatrix<int>).CalculateDeterminant();

        #endregion

        #region Sum Operations

        /// <summary>
        /// Calculates sum of all numbers in main diagonal in <see cref="IMatrix{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static int DiagonalSum(this IMatrix<int> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            int sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        /// <summary>
        /// Calculates sum of all saddle points in matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>Sum of all saddle points in matrix</returns>
        public static int SumSaddlePoints(this IMatrix<int> matrix)
        {
            int sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (!matrix.IsMinInRow(i, j) || !matrix.IsMaxInColumn(i, j))
                        continue;
                    sum += matrix[i, j];
                }
            }

            return sum;
        }

        #endregion

        #region Internal Mul

        private static IMatrix<int> InternalMulAtoB(this IMatrix<int> matrix, IMatrix<int> b)
        {
            var result = new Matrix<int>(matrix.RowsCount, b.ColumnsCount);
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < b.ColumnsCount; j++)
                {
                    for (int k = 0; k < b.RowsCount; k++)
                    {
                        result[i, j] += matrix[i, k] * b[k, j];
                    }
                }
            }

            return result;
        }

        private static IMatrix<int> InternalMulBtoA(this IMatrix<int> matrix, IMatrix<int> b)
        {
            var result = new Matrix<int>(b.RowsCount, matrix.ColumnsCount);
            for (int i = 0; i < b.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    for (int k = 0; k < matrix.RowsCount; k++)
                    {
                        result[i, j] += b[i, k] * matrix[k, j];
                    }
                }
            }

            return matrix;
        }

        #endregion

        #region Fill

        /// <summary>
        /// Fills <paramref name="matrix"/> with randomly numbers
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IMatrix<int> matrix, int min = -50, int max = 50)
        {
            var rnd = new Random();
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    matrix[i, j] = rnd.Next(min, max + 1);
                }
            }
        }

        #endregion

        #endregion

        #region Long

        #region Math

        /// <summary>
        /// Returns the multiplied <see cref="IMatrix{T}"/> <paramref name="first"/> by <see cref="IMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IMatrix{T}"/> - the result of multiplying <paramref name="first"/> and <paramref name="second"/></returns>
        public static IMatrix<long> Multiplication(this IMatrix<long> first, IMatrix<long> second)
        {
            if (first.ColumnsCount == second.RowsCount) return first.InternalMulAtoB(second);
            if (second.ColumnsCount == first.RowsCount) return first.InternalMulBtoA(second);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        /// <summary>
        /// Returns the add <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IMatrix<long> Add(this IMatrix<long> first, IMatrix<long> second)
        {
            if (first.ColumnsCount != second.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<long>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    matrix[i, j] = first[i, j] + second[i, j];
                }
            }

            return matrix;
        }

        /// <summary>
        /// Returns the subtraction <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IMatrix<long> Sub(this IMatrix<long> first, IMatrix<long> second)
        {
            if (first.ColumnsCount != second.ColumnsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of columns should be the same");
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<long>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    matrix[i, j] = first[i, j] - second[i, j];
                }
            }

            return matrix;
        }

        /// <summary>
        /// Returns the multiplication <see cref="IMatrix{T}"/> <paramref name="matrix"/> on <see cref="Int32"/> <paramref name="number"/>
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>New <see cref="IMatrix{T}"/> - the result multiplication of matrix <paramref name="matrix"/> on <paramref name="number"/></returns>
        public static IMatrix<long> MulOnNumber(this IMatrix<long> matrix, long number)
        {
            var newMatrix = new Matrix<long>(matrix.RowsCount, matrix.ColumnsCount);
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    newMatrix[i, j] = matrix[i, j] * number;
                }
            }

            return newMatrix;
        }

        #endregion

        #region Operations

        /// <summary>
        /// Calculates determinant for <see cref="Int64"/> <see cref="IMatrix{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Determinant</returns>
        public static long CalculateDeterminant(this IMatrix<long> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new InvalidOperationException(
                    "Determinant can be calculated only for square matrix");
            }
            if (matrix.ColumnsCount == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            long result = 0;
            for (var j = 0; j < matrix.ColumnsCount; j++)
            {
                result += (j % 2 == 1 ? 1 : -1) * matrix[1, j] *
                          ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<long>)?.CreateMatrixWithoutRow(1) as IMatrix<long>).CalculateDeterminant();
            }
            return result;
        }

        /// <summary>
        /// Creates inverted matrix from <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>New inverted <see cref="IMatrix{T}"/></returns>
        public static IMatrix<double> CreateInvertibleMatrix(this IMatrix<long> matrix)
        {
            if (!matrix.IsSquare)
                return null;
            var determinant = matrix.CalculateDeterminant();

            IMatrix<double> result = new Matrix<double>(matrix.RowsCount, matrix.ColumnsCount);
            matrix.ProcessFunctionOverData((i, j) =>
            {
                result[i, j] = Math.Round((i + j) % 2 == 1 ? -1 : 1 *
                    matrix.CalculateMinor(i, j) / determinant, 2);
            });

            result = result.Transpose();
            return result;
        }

        /// <summary>
        /// Calculates minor for <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Minor</returns>
        public static double CalculateMinor(this IMatrix<long> matrix, int i, int j) => ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<long>)?.CreateMatrixWithoutRow(i) as IMatrix<long>).CalculateDeterminant();

        #endregion

        #region Sum Operations

        /// <summary>
        /// Calculates sum of all numbers in main diagonal in <see cref="IMatrix{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static long DiagonalSum(this IMatrix<long> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            long sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        /// <summary>
        /// Calculates sum of all saddle points in matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>Sum of all saddle points in matrix</returns>
        public static long SumSaddlePoints(this IMatrix<long> matrix)
        {
            long sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (!matrix.IsMinInRow(i, j) || !matrix.IsMaxInColumn(i, j)) continue;
                    sum += matrix[i, j];
                }
            }

            return sum;
        }

        #endregion

        #region Internal Mul

        private static IMatrix<long> InternalMulAtoB(this IMatrix<long> matrix, IMatrix<long> second)
        {
            var result = new Matrix<long>(matrix.RowsCount, second.ColumnsCount);
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < second.ColumnsCount; j++)
                {
                    for (int k = 0; k < second.RowsCount; k++)
                    {
                        result[i, j] += matrix[i, k] * second[k, j];
                    }
                }
            }

            return result;
        }

        private static IMatrix<long> InternalMulBtoA(this IMatrix<long> matrix, IMatrix<long> second)
        {
            var result = new Matrix<long>(second.RowsCount, matrix.ColumnsCount);
            for (int i = 0; i < second.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    for (int k = 0; k < matrix.RowsCount; k++)
                    {
                        result[i, j] += second[i, k] * matrix[k, j];
                    }
                }
            }

            return matrix;
        }

        #endregion

        #region Fill

        /// <summary>
        /// Fills <paramref name="matrix"/> with randomly numbers
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IMatrix<long> matrix, long min = -50, long max = 50)
        {
            var rnd = new Random();
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    matrix[i, j] = rnd.NextLong(min, max);
                }
            }
        }

        #endregion

        #endregion

        #region Double

        #region Math

        /// <summary>
        /// Returns the multiplied <see cref="IMatrix{T}"/> <paramref name="first"/> by <see cref="IMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IMatrix{T}"/> - the result of multiplying <paramref name="first"/> and <paramref name="second"/></returns>
        public static IMatrix<double> Multiplication(this IMatrix<double> first, IMatrix<double> second)
        {
            if (first.ColumnsCount == second.RowsCount) return first.InternalMulAtoB(second);
            if (second.ColumnsCount == first.RowsCount) return first.InternalMulBtoA(second);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        /// <summary>
        /// Returns the add <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IMatrix<double> Add(this IMatrix<double> first, IMatrix<double> second)
        {
            if (first.ColumnsCount != second.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<double>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    matrix[i, j] = first[i, j] + second[i, j];
                }
            }

            return matrix;
        }

        /// <summary>
        /// Returns the subtraction <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IMatrix{T}"/> - the result subtraction of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IMatrix<double> Sub(this IMatrix<double> first, IMatrix<double> second)
        {
            if (first.ColumnsCount != second.ColumnsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of columns should be the same");
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<double>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    matrix[i, j] = first[i, j] - second[i, j];
                }
            }

            return matrix;
        }

        /// <summary>
        /// Returns the multiplication <see cref="IMatrix{T}"/> <paramref name="matrix"/> on <see cref="Int32"/> <paramref name="number"/>
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>New <see cref="IMatrix{T}"/> - the result multiplication of matrix <paramref name="matrix"/> on <paramref name="number"/></returns>
        public static IMatrix<double> MulOnNumber(this IMatrix<double> matrix, double number)
        {
            var newMatrix = new Matrix<double>(matrix.RowsCount, matrix.ColumnsCount);
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    newMatrix[i, j] = matrix[i, j] * number;
                }
            }

            return newMatrix;
        }

        #endregion

        #region Operations

        /// <summary>
        /// Calculates determinant for <see cref="Int64"/> <see cref="IMatrix{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Determinant</returns>
        public static double CalculateDeterminant(this IMatrix<double> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new InvalidOperationException(
                    "Determinant can be calculated only for square matrix");
            }
            if (matrix.ColumnsCount == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            double result = 0;
            for (var j = 0; j < matrix.ColumnsCount; j++)
            {
                result += (j % 2 == 1 ? 1 : -1) * matrix[1, j] *
                          ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<double>)?.CreateMatrixWithoutRow(1) as IMatrix<double>).CalculateDeterminant();
            }
            return result;
        }

        /// <summary>
        /// Creates inverted matrix from <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>New inverted <see cref="IMatrix{T}"/></returns>
        public static IMatrix<double> CreateInvertibleMatrix(this IMatrix<double> matrix)
        {
            if (!matrix.IsSquare)
                return null;
            var determinant = matrix.CalculateDeterminant();

            IMatrix<double> result = new Matrix<double>(matrix.RowsCount, matrix.ColumnsCount);
            matrix.ProcessFunctionOverData((i, j) =>
            {
                result[i, j] = Math.Round((i + j) % 2 == 1 ? -1 : 1 *
                    matrix.CalculateMinor(i, j) / determinant, 2);
            });

            result = result.Transpose();
            return result;
        }

        /// <summary>
        /// Calculates minor for <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Minor</returns>
        public static double CalculateMinor(this IMatrix<double> matrix, int i, int j) => ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<double>)?.CreateMatrixWithoutRow(i) as IMatrix<double>).CalculateDeterminant();

        #endregion

        #region Sum Operations

        /// <summary>
        /// Calculates sum of all numbers in main diagonal in <see cref="IMatrix{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static double DiagonalSum(this IMatrix<double> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            double sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        /// <summary>
        /// Calculates sum of all saddle points in matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>Sum of all saddle points in matrix</returns>
        public static double SumSaddlePoints(this IMatrix<double> matrix)
        {
            double sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (!matrix.IsMinInRow(i, j) || !matrix.IsMaxInColumn(i, j)) continue;
                    sum += matrix[i, j];
                }
            }

            return sum;
        }

        #endregion

        #region Internal Mul

        private static IMatrix<double> InternalMulAtoB(this IMatrix<double> matrix, IMatrix<double> second)
        {
            var result = new Matrix<double>(matrix.RowsCount, second.ColumnsCount);
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < second.ColumnsCount; j++)
                {
                    for (int k = 0; k < second.RowsCount; k++)
                    {
                        result[i, j] += matrix[i, k] * second[k, j];
                    }
                }
            }

            return result;
        }

        private static IMatrix<double> InternalMulBtoA(this IMatrix<double> matrix, IMatrix<double> second)
        {
            var result = new Matrix<double>(second.RowsCount, matrix.ColumnsCount);
            for (int i = 0; i < second.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    for (int k = 0; k < matrix.RowsCount; k++)
                    {
                        result[i, j] += second[i, k] * matrix[k, j];
                    }
                }
            }

            return matrix;
        }

        #endregion

        #region Fill

        /// <summary>
        /// Fills <paramref name="matrix"/> with randomly numbers
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IMatrix<double> matrix, double min = -50d, double max = 50d)
        {
            var rnd = new Random();
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    matrix[i, j] = rnd.NextDouble(min, max);
                }
            }
        }

        #endregion

        #endregion

        #region Decimal

        #region Math

        public static IMatrix<decimal> Multiplication(this IMatrix<decimal> first, IMatrix<decimal> second)
        {
            if (first.ColumnsCount == second.RowsCount) return first.InternalMulAtoB(second);
            if (second.ColumnsCount == first.RowsCount) return first.InternalMulBtoA(second);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static IMatrix<decimal> Add(this IMatrix<decimal> first, IMatrix<decimal> second)
        {
            if (first.ColumnsCount != second.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<decimal>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    matrix[i, j] = first[i, j] + second[i, j];
                }
            }

            return matrix;
        }

        public static IMatrix<decimal> Sub(this IMatrix<decimal> first, IMatrix<decimal> second)
        {
            if (first.ColumnsCount != second.ColumnsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of columns should be the same");
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<decimal>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    matrix[i, j] = first[i, j] - second[i, j];
                }
            }

            return matrix;
        }

        public static IMatrix<decimal> MulOnNumber(this IMatrix<decimal> first, decimal number)
        {
            var matrix = new Matrix<decimal>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    matrix[i, j] = first[i, j] * number;
                }
            }

            return matrix;
        }

        #endregion

        #region Operations

        /// <summary>
        /// Calculates determinant for <see cref="Int64"/> <see cref="IMatrix{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Determinant</returns>
        public static decimal CalculateDeterminant(this IMatrix<decimal> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new InvalidOperationException(
                    "Determinant can be calculated only for square matrix");
            }
            if (matrix.ColumnsCount == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            decimal result = 0;
            for (var j = 0; j < matrix.ColumnsCount; j++)
            {
                result += (j % 2 == 1 ? 1 : -1) * matrix[1, j] *
                          ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<decimal>)?.CreateMatrixWithoutRow(1) as IMatrix<decimal>).CalculateDeterminant();
            }
            return result;
        }

        /// <summary>
        /// Creates inverted matrix from <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>New inverted <see cref="IMatrix{T}"/></returns>
        public static IMatrix<decimal> CreateInvertibleMatrix(this IMatrix<decimal> matrix)
        {
            if (!matrix.IsSquare)
                return null;
            var determinant = matrix.CalculateDeterminant();

            IMatrix<decimal> result = new Matrix<decimal>(matrix.RowsCount, matrix.ColumnsCount);
            matrix.ProcessFunctionOverData((i, j) =>
            {
                result[i, j] = Math.Round((i + j) % 2 == 1 ? -1 : 1 *
                    matrix.CalculateMinor(i, j) / determinant, 4);
            });

            result = result.Transpose();
            return result;
        }

        /// <summary>
        /// Calculates minor for <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Minor</returns>
        public static decimal CalculateMinor(this IMatrix<decimal> matrix, int i, int j) => ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<decimal>)?.CreateMatrixWithoutRow(i) as IMatrix<decimal>).CalculateDeterminant();

        #endregion

        #region Sum Operations

        /// <summary>
        /// Calculates sum of all numbers in main diagonal in <see cref="IMatrix{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static decimal DiagonalSum(this IMatrix<decimal> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            decimal sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        /// <summary>
        /// Calculates sum of all saddle points in matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>Sum of all saddle points in matrix</returns>
        public static decimal SumSaddlePoints(this IMatrix<decimal> matrix)
        {
            decimal sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (!matrix.IsMinInRow(i, j) || !matrix.IsMaxInColumn(i, j)) continue;
                    sum += matrix[i, j];
                }
            }

            return sum;
        }

        #endregion

        #region Internal Mul

        private static IMatrix<decimal> InternalMulAtoB(this IMatrix<decimal> matrix, IMatrix<decimal> second)
        {
            var result = new Matrix<decimal>(matrix.RowsCount, second.ColumnsCount);
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < second.ColumnsCount; j++)
                {
                    for (int k = 0; k < second.RowsCount; k++)
                    {
                        result[i, j] += matrix[i, k] * second[k, j];
                    }
                }
            }

            return result;
        }

        private static IMatrix<decimal> InternalMulBtoA(this IMatrix<decimal> matrix, IMatrix<decimal> second)
        {
            var result = new Matrix<decimal>(second.RowsCount, matrix.ColumnsCount);
            for (int i = 0; i < second.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    for (int k = 0; k < matrix.RowsCount; k++)
                    {
                        result[i, j] += second[i, k] * matrix[k, j];
                    }
                }
            }

            return matrix;
        }

        #endregion

        #region Fill

        /// <summary>
        /// Fills <paramref name="matrix"/> with randomly numbers
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        public static void FillRandomly(this IMatrix<decimal> matrix, decimal min = -50, decimal max = 50)
        {
            var rnd = new Random();
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    matrix[i, j] = (decimal)rnd.NextDouble((double)min, (double)max);
                }
            }
        }

        #endregion

        #endregion

        #region string

        #region Math

        /// <summary>
        /// Returns the add <see cref="IMatrix{T}"/> <paramref name="first"/> and <see cref="IMatrix{T}"/> <paramref name="second"/>
        /// </summary>
        /// <param name="first">First matrix</param>
        /// <param name="second">Second matrix</param>
        /// <returns>New <see cref="IMatrix{T}"/> - the result sum of matrices <paramref name="first"/> and <paramref name="second"/></returns>
        public static IMatrix<string> Add(this IMatrix<string> first, IMatrix<string> second)
        {
            if (first.ColumnsCount != second.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<string>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    matrix[i, j] = first[i, j] + second[i, j];
                }
            }

            return matrix;
        }

        /// <summary>
        /// Returns the multiplication <see cref="IMatrix{T}"/> <paramref name="matrix"/> on <see cref="Int32"/> <paramref name="number"/>
        /// </summary>
        /// <param name="matrix">First</param>
        /// <param name="number"></param>
        /// <returns>New <see cref="IMatrix{T}"/> - the result multiplication of matrix <paramref name="matrix"/> on <paramref name="number"/></returns>
        public static IMatrix<string> MulOnNumber(this IMatrix<string> matrix, int number)
        {
            var newMatrix = new Matrix<string>(matrix.RowsCount, matrix.ColumnsCount);
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    newMatrix[i, j] = matrix[i, j].Concat(number);
                }
            }

            return newMatrix;
        }

        #endregion

        #region Sum Operations

        /// <summary>
        /// Calculates sum of all string in main diagonal in <see cref="IMatrix{T}"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>Sum in main diagonal</returns>
        public static string DiagonalSum(this IMatrix<string> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sb = new StringBuilder();
            for (int i = 0; i < matrix.RowsCount; i++)
                sb.Append(matrix[i, i]).Append(' ');
            return sb.ToString();
        }

        //TODO: resolve
        private static string Sum(this IMatrix<string> matrix)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    sb.Append(matrix[i, j]).Append(' ');
                }
            }

            return sb.ToString();
        }

        #endregion

        #region Fill

        /// <summary>
        /// Fills <paramref name="matrix"/> with randomly strings with <see cref="Guid"/>
        /// </summary>
        /// <param name="matrix">Initial matrix</param>
        /// <param name="guid">GUID object</param>
        /// <param name="length">Length of string</param>
        public static void FillRandomly(this IMatrix<string> matrix, Guid guid, int length = 10)
        {
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    matrix[i, j] = guid.ToString().Substring(0, length);
                }
            }
        }

        #endregion

        #endregion

        //TODO: summary
        #region Nullable

        #region Int32?

        #region Math

        public static IMatrix<int?> Multiplication(this IMatrix<int?> first, IMatrix<int?> second)
        {
            if (first.ColumnsCount == second.RowsCount) return first.InternalMulAtoB(second);
            if (second.ColumnsCount == first.RowsCount) return first.InternalMulBtoA(second);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static IMatrix<int?> Add(this IMatrix<int?> first, IMatrix<int?> second)
        {
            if (first.ColumnsCount != second.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<int?>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    if (first[i, j] != null && second[i, j] != null)
                        matrix[i, j] = first[i, j] + second[i, j];
                    else
                        matrix[i, j] = null;
                }
            }

            return matrix;
        }

        public static IMatrix<int?> Sub(this IMatrix<int?> first, IMatrix<int?> second)
        {
            if (first.ColumnsCount != second.ColumnsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of columns should be the same");
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<int?>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    if (first[i, j] != null && second[i, j] != null)
                        matrix[i, j] = first[i, j] - second[i, j];
                    else
                        matrix[i, j] = null;
                }
            }

            return matrix;
        }

        public static IMatrix<int?> MulOnNumber(this IMatrix<int?> first, int number)
        {
            var matrix = new Matrix<int?>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    if (matrix[i, j] != null)
                        matrix[i, j] = first[i, j] * number;
                    else
                        matrix[i, j] = null;
                }
            }

            return matrix;
        }

        #endregion

        #region Operations

        public static int CalculateDeterminant(this IMatrix<int?> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new InvalidOperationException(
                    "Determinant can be calculated only for square matrix");
            }
            if (matrix.ColumnsCount == 2)
            {
                if (matrix[0, 0] != null &&
                    matrix[1, 1] != null &&
                    matrix[0, 1] != null &&
                    matrix[1, 0] != null)
                    return matrix[0, 0].Value * matrix[1, 1].Value - matrix[0, 1].Value * matrix[1, 0].Value;
                else
                    return 0;
            }
            int result = 0;
            for (var j = 0; j < matrix.ColumnsCount; j++)
            {
                result += (j % 2 == 1 ? 1 : -1) * matrix[1, j] ?? 0 *
                                                            ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<int?>)?.CreateMatrixWithoutRow(1) as IMatrix<int?>).CalculateDeterminant();
            }
            return result;
        }

        public static IMatrix<double?> CreateInvertibleMatrix(this IMatrix<int?> matrix)
        {
            if (!matrix.IsSquare)
                return null;
            var determinant = matrix.CalculateDeterminant();

            IMatrix<double?> result = new Matrix<double?>(matrix.RowsCount, matrix.ColumnsCount);
            matrix.ProcessFunctionOverData((i, j) =>
            {
                result[i, j] = Math.Round((i + j) % 2 == 1 ? -1 : 1 * matrix.CalculateMinor(i, j) / determinant, 2);
            });

            result = result.Transpose();
            return result;
        }

        private static double CalculateMinor(this IMatrix<int?> matrix, int i, int j)
        {
            return ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<int?>)?.CreateMatrixWithoutRow(i) as IMatrix<int?>).CalculateDeterminant();
        }

        #endregion

        #region Sum Operations

        public static int DiagonalSum(this IMatrix<int?> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            int sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                if (matrix[i, i] != null)
                    sum += matrix[i, i].Value;
            }
            return sum;
        }

        public static int Sum(this IMatrix<int?> matrix)
        {
            int sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (matrix[i, j] != null)
                        sum += matrix[i, j].Value;
                }
            }

            return sum;
        }

        public static int SumSaddlePoints(this IMatrix<int?> matrix)
        {
            int sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (matrix[i, j] != null)
                    {
                        if (!matrix.IsMinInRow(i, j) || !matrix.IsMaxInColumn(i, j))
                            continue;
                        sum += matrix[i, j].Value;
                    }
                }
            }

            return sum;
        }

        #endregion

        #region Internal Mul

        private static IMatrix<int?> InternalMulAtoB(this IMatrix<int?> matrix, IMatrix<int?> second)
        {
            var result = new Matrix<int?>(matrix.RowsCount, second.ColumnsCount);
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < second.ColumnsCount; j++)
                {
                    for (int k = 0; k < second.RowsCount; k++)
                    {
                        if (matrix[i, k] != null && matrix[k, j] != null)
                            result[i, j] += matrix[i, k] * second[k, j];
                        else
                            break;
                    }
                }
            }

            return result;
        }

        private static IMatrix<int?> InternalMulBtoA(this IMatrix<int?> matrix, IMatrix<int?> second)
        {
            var result = new Matrix<int?>(second.RowsCount, matrix.ColumnsCount);
            for (int i = 0; i < second.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    for (int k = 0; k < matrix.RowsCount; k++)
                    {
                        if (matrix[i, k] != null && matrix[k, j] != null)
                            result[i, j] += second[i, k] * matrix[k, j];
                    }
                }
            }

            return matrix;
        }

        #endregion

        #region Find Ops

        public static bool IsMaxInColumn(this IMatrix<int?> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] != null && matrix[i, j] != null)
                {
                    if (matrix[k, j] > matrix[i, j])
                        return false;
                }
            }
            return true;
        }

        public static bool IsMinInRow(this IMatrix<int?> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.ColumnsCount; k++)
            {
                if (matrix[i, k] != null && matrix[i, j] != null)
                {
                    if (matrix[i, k] < matrix[i, j])
                        return false;
                }
            }
            return true;
        }

        #endregion

        #region Fill

        public static void FillRandomly(this IMatrix<int?> matrix, int min = -50, int max = 50, bool includeNull = false)
        {
            var rnd = new Random();
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
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

        #region long?

        #region Math

        public static IMatrix<long?> Multiplication(this IMatrix<long?> first, IMatrix<long?> second)
        {
            if (first.ColumnsCount == second.RowsCount) return first.InternalMulAtoB(second);
            if (second.ColumnsCount == first.RowsCount) return first.InternalMulBtoA(second);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static IMatrix<long?> Add(this IMatrix<long?> first, IMatrix<long?> second)
        {
            if (first.ColumnsCount != second.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<long?>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    if (first[i, j] != null && second[i, j] != null)
                        matrix[i, j] = first[i, j] + second[i, j];
                    else
                        matrix[i, j] = null;
                }
            }

            return matrix;
        }

        public static IMatrix<long?> Sub(this IMatrix<long?> first, IMatrix<long?> second)
        {
            if (first.ColumnsCount != second.ColumnsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of columns should be the same");
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<long?>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    if (first[i, j] != null && second[i, j] != null)
                        matrix[i, j] = first[i, j] - second[i, j];
                    else
                        matrix[i, j] = null;
                }
            }

            return matrix;
        }

        public static IMatrix<long?> MulOnNumber(this IMatrix<long?> first, long number)
        {
            var matrix = new Matrix<long?>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    if (first[i, j] != null)
                        matrix[i, j] = first[i, j] * number;
                }
            }

            return matrix;
        }

        #endregion

        #region Operations

        public static long CalculateDeterminant(this IMatrix<long?> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new InvalidOperationException(
                    "Determinant can be calculated only for square matrix");
            }
            if (matrix.ColumnsCount == 2)
            {
                if (matrix[0, 0] != null &&
                    matrix[1, 1] != null &&
                    matrix[0, 1] != null &&
                    matrix[1, 0] != null)
                    return matrix[0, 0].Value * matrix[1, 1].Value - matrix[0, 1].Value * matrix[1, 0].Value;
                else
                    return 0;
            }
            long result = 0;
            for (var j = 0; j < matrix.ColumnsCount; j++)
            {
                result += (j % 2 == 1 ? 1 : -1) * matrix[1, j] == null ? 0 : matrix[1, j].Value *
                                                                             ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<long?>)?.CreateMatrixWithoutRow(1) as IMatrix<long?>).CalculateDeterminant();
            }
            return result;
        }

        public static IMatrix<double> CreateInvertibleMatrix(this IMatrix<long?> matrix)
        {
            if (!matrix.IsSquare)
                throw new InvalidOperationException(
                    "Creating invertible matrix is possible only for square matrix");
            var determinant = matrix.CalculateDeterminant();

            IMatrix<double> result = new Matrix<double>(matrix.RowsCount, matrix.ColumnsCount);
            matrix.ProcessFunctionOverData((i, j) =>
            {
                result[i, j] = Math.Round((i + j) % 2 == 1 ? -1 : 1 *
                    matrix.CalculateMinor(i, j) / determinant, 2);
            });

            result = result.Transpose();
            return result;
        }

        private static double CalculateMinor(this IMatrix<long?> matrix, int i, int j)
        {
            return ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<long?>)?.CreateMatrixWithoutRow(i) as IMatrix<long?>).CalculateDeterminant();
        }

        #endregion

        #region Sum Operations

        public static long DiagonalSum(this IMatrix<long?> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            long sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                if (matrix[i, i] != null)
                {
                    sum += matrix[i, i].Value;
                }
            }
            return sum;
        }

        public static long Sum(this IMatrix<long?> matrix)
        {
            long sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (matrix[i, j] != null)
                    {
                        sum += matrix[i, j].Value;
                    }
                }
            }

            return sum;
        }

        public static long SumSaddlePoints(this IMatrix<long?> matrix)
        {
            long sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (matrix[i, j] != null)
                    {
                        if (!matrix.IsMinInRow(i, j) || !matrix.IsMaxInColumn(i, j))
                            continue;
                        sum += matrix[i, j].Value;
                    }
                }
            }
            return sum;
        }

        #endregion

        #region Internal Mul

        private static IMatrix<long?> InternalMulAtoB(this IMatrix<long?> matrix, IMatrix<long?> second)
        {
            var result = new Matrix<long?>(matrix.RowsCount, second.ColumnsCount);
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < second.ColumnsCount; j++)
                {
                    for (int k = 0; k < second.RowsCount; k++)
                    {
                        if (matrix[i, k] == null && second[k, j] == null)
                            matrix[i, k] = null;
                        else
                            result[i, j] += matrix[i, k] * second[k, j];
                    }
                }
            }

            return result;
        }

        private static IMatrix<long?> InternalMulBtoA(this IMatrix<long?> matrix, IMatrix<long?> second)
        {
            var result = new Matrix<long?>(second.RowsCount, matrix.ColumnsCount);
            for (int i = 0; i < second.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    for (int k = 0; k < matrix.RowsCount; k++)
                    {
                        if (second[i, k] != null && matrix[k, j] == null)
                            result[i, j] = null;
                        else
                            result[i, j] += second[i, k] * matrix[k, j];
                    }
                }
            }

            return matrix;
        }

        #endregion

        #region Find Ops

        public static bool IsMaxInColumn(this IMatrix<long?> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] != null && matrix[i, j] != null)
                    if (matrix[k, j] > matrix[i, j])
                        return false;
            }
            return true;
        }

        public static bool IsMinInRow(this IMatrix<long?> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.ColumnsCount; k++)
            {
                if (matrix[k, j] != null && matrix[i, j] != null)
                    if (matrix[k, j] < matrix[i, j])
                        return false;
            }
            return true;
        }

        #endregion

        #region Fill

        public static void FillRandomly(this IMatrix<long?> matrix, long min = -50, long max = 50, bool includeNull = false)
        {
            var rnd = new Random();
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
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

        #region double?

        #region Math

        public static IMatrix<double?> Multiplication(this IMatrix<double?> first, IMatrix<double?> second)
        {
            if (first.ColumnsCount == second.RowsCount) return first.InternalMulAtoB(second);
            if (second.ColumnsCount == first.RowsCount) return first.InternalMulBtoA(second);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static IMatrix<double?> Add(this IMatrix<double?> first, IMatrix<double?> second)
        {
            if (first.ColumnsCount != second.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<double?>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    if (first[i, j] != null && second[i, j] != null)
                        matrix[i, j] = first[i, j] + second[i, j];
                    else
                        matrix[i, j] = null;
                }
            }

            return matrix;
        }

        public static IMatrix<double?> Sub(this IMatrix<double?> first, IMatrix<double?> second)
        {
            if (first.ColumnsCount != second.ColumnsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of columns should be the same");
            if (first.RowsCount != second.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<double?>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    if (first[i, j] != null && second[i, j] != null)
                        matrix[i, j] = first[i, j] - second[i, j];
                    else
                        matrix[i, j] = null;
                }
            }

            return matrix;
        }

        public static IMatrix<double?> MulOnNumber(this IMatrix<double?> first, double number)
        {
            var matrix = new Matrix<double?>(first.RowsCount, first.ColumnsCount);
            for (int i = 0; i < first.RowsCount; i++)
            {
                for (int j = 0; j < first.ColumnsCount; j++)
                {
                    if (matrix[i, j] != null)
                        matrix[i, j] = first[i, j] * number;
                    else
                        matrix[i, j] = null;
                }
            }

            return matrix;
        }

        #endregion

        #region Operations

        public static double CalculateDeterminant(this IMatrix<double?> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new InvalidOperationException(
                    "Determinant can be calculated only for square matrix");
            }
            if (matrix.ColumnsCount == 2)
            {
                if (matrix[0, 0] != null &&
                    matrix[1, 1] != null &&
                    matrix[0, 1] != null &&
                    matrix[1, 0] != null)
                    return matrix[0, 0].Value * matrix[1, 1].Value - matrix[0, 1].Value * matrix[1, 0].Value;
                return 0;
            }
            double result = 0;
            for (var j = 0; j < matrix.ColumnsCount; j++)
            {
                result += (j % 2 == 1 ? 1 : -1) * matrix[1, j] ?? 0 *
                                                            ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<double?>)?.CreateMatrixWithoutRow(1) as IMatrix<double?>).CalculateDeterminant();
            }
            return result;
        }

        public static IMatrix<double?> CreateInvertibleMatrix(this IMatrix<double?> matrix)
        {
            if (!matrix.IsSquare)
                return null;
            var determinant = matrix.CalculateDeterminant();

            IMatrix<double?> result = new Matrix<double?>(matrix.RowsCount, matrix.ColumnsCount);
            matrix.ProcessFunctionOverData((i, j) =>
            {
                result[i, j] = Math.Round((i + j) % 2 == 1 ? -1 : 1 *
                    matrix.CalculateMinor(i, j) / determinant, 2);
            });

            result = result.Transpose();
            return result;
        }

        private static double CalculateMinor(this IMatrix<double?> matrix, int i, int j)
        {
            return ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<double?>)?.CreateMatrixWithoutRow(i) as IMatrix<double?>).CalculateDeterminant();
        }

        #endregion

        #region Sum Operations

        public static double DiagonalSum(this IMatrix<double?> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            double sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                if (matrix[i, i] != null)
                    sum += matrix[i, i].Value;
            }
            return sum;
        }

        public static double Sum(this IMatrix<double?> matrix)
        {
            double sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (matrix[i, j] != null)
                        sum += matrix[i, j].Value;
                }
            }

            return sum;
        }

        public static double SumSaddlePoints(this IMatrix<double?> matrix)
        {
            double sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (matrix[i, j] != null)
                    {
                        if (!matrix.IsMinInRow(i, j) || !matrix.IsMaxInColumn(i, j))
                            continue;
                        sum += matrix[i, j].Value;
                    }
                }
            }

            return sum;
        }

        #endregion

        #region Math

        private static IMatrix<double?> InternalMulAtoB(this IMatrix<double?> matrix, IMatrix<double?> second)
        {
            var result = new Matrix<double?>(matrix.RowsCount, second.ColumnsCount);
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < second.ColumnsCount; j++)
                {
                    for (int k = 0; k < second.RowsCount; k++)
                    {
                        if (matrix[i, k] != null && second[k, j] != null)
                            result[i, j] += matrix[i, k] * second[k, j];
                        else
                            break;
                    }
                }
            }

            return result;
        }

        private static IMatrix<double?> InternalMulBtoA(this IMatrix<double?> matrix, IMatrix<double?> second)
        {
            var result = new Matrix<double?>(second.RowsCount, matrix.ColumnsCount);
            for (int i = 0; i < second.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    for (int k = 0; k < matrix.RowsCount; k++)
                    {
                        if (matrix[i, k] != null && second[k, j] != null)
                            result[i, j] += matrix[i, k] * second[k, j];
                        else
                            break;
                    }
                }
            }

            return matrix;
        }

        #endregion

        #region Find Ops

        public static bool IsMaxInColumn(this IMatrix<double?> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] != null && matrix[i, j] != null)
                    if (matrix[k, j] > matrix[i, j])
                        return false;
            }
            return true;
        }

        public static bool IsMinInRow(this IMatrix<double?> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.ColumnsCount; k++)
            {
                if (matrix[i, k] != null && matrix[i, j] != null)
                    if (matrix[i, k] < matrix[i, j])
                        return false;
            }
            return true;
        }

        #endregion

        #region Fill

        public static void FillRandomly(this IMatrix<double?> matrix, double min = -50d, double max = 50d, bool includeNull = false)
        {
            var rnd = new Random();
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    double value = rnd.NextDouble(min, max);
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