﻿using CyberMath.Structures.Extensions;
using CyberMath.Structures.Matrix.Matrix.Models;
using CyberMath.Structures.Matrix.MatrixBase;
using CyberMath.Structures.MatrixBase.Exceptions;
using System;
using System.Text;

namespace CyberMath.Structures.Matrix.Matrix.Extensions
{
    public static class ValueTypeMatrixExtension
    {
        #region Int32

        #region Math

        public static Matrix<int> Multiplication(this Matrix<int> a, Matrix<int> b)
        {
            if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
            if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static Matrix<int> Add(this Matrix<int> a, Matrix<int> b)
        {
            if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<int>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    matrix[i, j] = a[i, j] + b[i, j];
                }
            }

            return matrix;
        }

        public static Matrix<int> Sub(this Matrix<int> a, Matrix<int> b)
        {
            if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<int>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    matrix[i, j] = a[i, j] - b[i, j];
                }
            }

            return matrix;
        }

        public static Matrix<int> MulOnNumber(this Matrix<int> a, int number)
        {
            var matrix = new Matrix<int>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    matrix[i, j] = a[i, j] * number;
                }
            }

            return matrix;
        }

        #endregion

        #region Creation

        public static Matrix<int> CreateIdentityMatrix(int n)
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

        public static int CalculateDeterminant(this Matrix<int> matrix)
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
                                                            ((matrix.CreateMatrixWithoutColumn(j) as Matrix<int>)?.CreateMatrixWithoutRow(1) as Matrix<int>).CalculateDeterminant();
            }
            return result;
        }

        public static IMatrix<double> CreateInvertibleMatrix(this Matrix<int> matrix)
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

        private static double CalculateMinor(this Matrix<int> matrix, int i, int j)
        {
            return ((matrix.CreateMatrixWithoutColumn(j) as Matrix<int>)?.CreateMatrixWithoutRow(i) as Matrix<int>).CalculateDeterminant();
        }

        #endregion

        #region Sum Operations

        public static int DiagonalSum(this Matrix<int> matrix)
        {
            int sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        public static int Sum(this Matrix<int> matrix)
        {
            int sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    sum += matrix[i, j];
                }
            }

            return sum;
        }

        public static int SumSaddlePoints(this Matrix<int> matrix)
        {
            int sum = 0;
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

        #region Math

        private static Matrix<int> InternalMulAtoB(this Matrix<int> matrix, Matrix<int> b)
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

        private static Matrix<int> InternalMulBtoA(this Matrix<int> matrix, Matrix<int> b)
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

        #region Find Ops

        public static bool IsMaxInColumn(this Matrix<int> matrix,int i, int j)
        {
            for (int k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] > matrix[i, j])
                    return false;
            }
            return true;
        }

        public static bool IsMinInRow(this Matrix<int> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.ColumnsCount; k++)
            {
                if (matrix[i, k] < matrix[i, j])
                    return false;
            }
            return true;
        }

        #endregion

        #region Fill

        public static void FillRandomly(this Matrix<int> matrix, int min = -50, int max = 50)
        {
            var rnd = new Random(DateTime.Now.Millisecond + DateTime.Now.Second * DateTime.Now.Day);
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

        public static Matrix<long> Multiplication(this Matrix<long> a, Matrix<long> b)
        {
            if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
            if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static Matrix<long> Add(this Matrix<long> a, Matrix<long> b)
        {
            if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<long>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    matrix[i, j] = a[i, j] + b[i, j];
                }
            }

            return matrix;
        }

        public static Matrix<long> Sub(this Matrix<long> a, Matrix<long> b)
        {
            if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<long>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    matrix[i, j] = a[i, j] - b[i, j];
                }
            }

            return matrix;
        }

        public static Matrix<long> MulOnNumber(this Matrix<long> a, long number)
        {
            var matrix = new Matrix<long>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    matrix[i, j] = a[i, j] * number;
                }
            }

            return matrix;
        }

        #endregion

        #region Operations

        public static long CalculateDeterminant(this Matrix<long> matrix)
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
                                                            ((matrix.CreateMatrixWithoutColumn(j) as Matrix<long>)?.CreateMatrixWithoutRow(1) as Matrix<long>).CalculateDeterminant();
            }
            return result;
        }

        public static IMatrix<double> CreateInvertibleMatrix(this Matrix<long> matrix)
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

        private static double CalculateMinor(this Matrix<long> matrix, int i, int j)
        {
            return ((matrix.CreateMatrixWithoutColumn(j) as Matrix<long>)?.CreateMatrixWithoutRow(i) as Matrix<long>).CalculateDeterminant();
        }

        #endregion

        #region Sum Operations

        public static long DiagonalSum(this Matrix<long> matrix)
        {
            long sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        public static long Sum(this Matrix<long> matrix)
        {
            long sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    sum += matrix[i, j];
                }
            }

            return sum;
        }

        public static long SumSaddlePoints(this Matrix<long> matrix)
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

        #region Math

        private static Matrix<long> InternalMulAtoB(this Matrix<long> matrix, Matrix<long> b)
        {
            var result = new Matrix<long>(matrix.RowsCount, b.ColumnsCount);
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

        private static Matrix<long> InternalMulBtoA(this Matrix<long> matrix, Matrix<long> b)
        {
            var result = new Matrix<long>(b.RowsCount, matrix.ColumnsCount);
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

        #region Find Ops

        public static bool IsMaxInColumn(this Matrix<long> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] > matrix[i, j])
                    return false;
            }
            return true;
        }

        public static bool IsMinInRow(this Matrix<long> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.ColumnsCount; k++)
            {
                if (matrix[i, k] < matrix[i, j])
                    return false;
            }
            return true;
        }

        #endregion

        #region Fill

        public static void FillRandomly(this Matrix<long> matrix, long min = -50, long max = 50)
        {
            var rnd = new Random();
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    matrix[i, j] = rnd.Next((int)min, (int)max);
                }
            }
        }

        #endregion

        #endregion

        #region Double

        #region Math

        public static Matrix<double> Multiplication(this Matrix<double> a, Matrix<double> b)
        {
            if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
            if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static Matrix<double> Add(this Matrix<double> a, Matrix<double> b)
        {
            if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<double>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    matrix[i, j] = a[i, j] + b[i, j];
                }
            }

            return matrix;
        }

        public static Matrix<double> Sub(this Matrix<double> a, Matrix<double> b)
        {
            if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<double>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    matrix[i, j] = a[i, j] - b[i, j];
                }
            }

            return matrix;
        }

        public static Matrix<double> MulOnNumber(this Matrix<double> a, double number)
        {
            var matrix = new Matrix<double>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    matrix[i, j] = a[i, j] * number;
                }
            }

            return matrix;
        }

        #endregion

        #region Operations

        public static double CalculateDeterminant(this Matrix<double> matrix)
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
                                                            ((matrix.CreateMatrixWithoutColumn(j) as Matrix<double>)?.CreateMatrixWithoutRow(1) as Matrix<double>).CalculateDeterminant();
            }
            return result;
        }

        public static IMatrix<double> CreateInvertibleMatrix(this Matrix<double> matrix)
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

        private static double CalculateMinor(this Matrix<double> matrix, int i, int j)
        {
            return ((matrix.CreateMatrixWithoutColumn(j) as Matrix<double>)?.CreateMatrixWithoutRow(i) as Matrix<double>).CalculateDeterminant();
        }

        #endregion

        #region Sum Operations

        public static double DiagonalSum(this Matrix<double> matrix)
        {
            double sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        public static double Sum(this Matrix<double> matrix)
        {
            double sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    sum += matrix[i, j];
                }
            }

            return sum;
        }

        public static double SumSaddlePoints(this Matrix<double> matrix)
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

        #region Math

        private static Matrix<double> InternalMulAtoB(this Matrix<double> matrix, Matrix<double> b)
        {
            var result = new Matrix<double>(matrix.RowsCount, b.ColumnsCount);
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

        private static Matrix<double> InternalMulBtoA(this Matrix<double> matrix, Matrix<double> b)
        {
            var result = new Matrix<double>(b.RowsCount, matrix.ColumnsCount);
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

        #region Find Ops

        public static bool IsMaxInColumn(this Matrix<double> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] > matrix[i, j])
                    return false;
            }
            return true;
        }

        public static bool IsMinInRow(this Matrix<double> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.ColumnsCount; k++)
            {
                if (matrix[i, k] < matrix[i, j])
                    return false;
            }
            return true;
        }

        #endregion

        #region Fill

        public static void FillRandomly(this Matrix<double> matrix, double min = -50d, double max = 50d)
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

        public static Matrix<decimal> Multiplication(this Matrix<decimal> a, Matrix<decimal> b)
        {
            if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
            if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static Matrix<decimal> Add(this Matrix<decimal> a, Matrix<decimal> b)
        {
            if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<decimal>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    matrix[i, j] = a[i, j] + b[i, j];
                }
            }

            return matrix;
        }

        public static Matrix<decimal> Sub(this Matrix<decimal> a, Matrix<decimal> b)
        {
            if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<decimal>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    matrix[i, j] = a[i, j] - b[i, j];
                }
            }

            return matrix;
        }

        public static Matrix<decimal> MulOnNumber(this Matrix<decimal> a, decimal number)
        {
            var matrix = new Matrix<decimal>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    matrix[i, j] = a[i, j] * number;
                }
            }

            return matrix;
        }

        #endregion

        #region Operations

        public static decimal CalculateDeterminant(this Matrix<decimal> matrix)
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
                                                            ((matrix.CreateMatrixWithoutColumn(j) as Matrix<decimal>)?.CreateMatrixWithoutRow(1) as Matrix<decimal>).CalculateDeterminant();
            }
            return result;
        }

        public static IMatrix<decimal> CreateInvertibleMatrix(this Matrix<decimal> matrix)
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

        private static decimal CalculateMinor(this Matrix<decimal> matrix, int i, int j)
        {
            return ((matrix.CreateMatrixWithoutColumn(j) as Matrix<decimal>)?.CreateMatrixWithoutRow(i) as Matrix<decimal>).CalculateDeterminant();
        }

        #endregion

        #region Sum Operations

        public static decimal DiagonalSum(this Matrix<decimal> matrix)
        {
            decimal sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        public static decimal Sum(this Matrix<decimal> matrix)
        {
            decimal sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    sum += matrix[i, j];
                }
            }

            return sum;
        }

        public static decimal SumSaddlePoints(this Matrix<decimal> matrix)
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

        #region Math

        private static Matrix<decimal> InternalMulAtoB(this Matrix<decimal> matrix, Matrix<decimal> b)
        {
            var result = new Matrix<decimal>(matrix.RowsCount, b.ColumnsCount);
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

        private static Matrix<decimal> InternalMulBtoA(this Matrix<decimal> matrix, Matrix<decimal> b)
        {
            var result = new Matrix<decimal>(b.RowsCount, matrix.ColumnsCount);
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

        #region Find Ops

        public static bool IsMaxInColumn(this Matrix<decimal> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] > matrix[i, j])
                    return false;
            }
            return true;
        }

        public static bool IsMinInRow(this Matrix<decimal> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.ColumnsCount; k++)
            {
                if (matrix[i, k] < matrix[i, j])
                    return false;
            }
            return true;
        }

        #endregion

        #region Fill

        public static void FillRandomly(this Matrix<decimal> matrix, decimal min = -50, decimal max = 50)
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

        public static Matrix<string> Add(this Matrix<string> a, Matrix<string> b)
        {
            if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<string>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    matrix[i, j] = a[i, j] + b[i, j];
                }
            }

            return matrix;
        }

        public static Matrix<string> MulOnNumber(this Matrix<string> a, int number)
        {
            var matrix = new Matrix<string>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    matrix[i, j] = a[i, j].Concat(number);
                }
            }

            return matrix;
        }

        #endregion

        #region Sum Operations

        public static string DiagonalSum(this Matrix<string> matrix)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < matrix.RowsCount; i++)
                sb.Append(matrix[i, i]);
            return sb.ToString();
        }

        public static string Sum(this Matrix<string> matrix)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    sb.Append(matrix[i, j]);
                }
            }

            return sb.ToString();
        }

        #endregion

        #region Find Ops

        public static bool IsMaxInColumn(this Matrix<string> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j].CompareTo(matrix[i, j]) == 1)
                    return false;
            }
            return true;
        }

        public static bool IsMinInRow(this Matrix<string> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.ColumnsCount; k++)
            {
                if (matrix[i, k].CompareTo(matrix[i, j]) == -1)
                    return false;
            }
            return true;
        }

        #endregion

        #region Fill

        public static void FillRandomly(this Matrix<string> matrix, Guid guid, int length = 10)
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

        #region Nullable

        #region Int32?

        #region Math

        public static Matrix<int?> Multiplication(this Matrix<int?> a, Matrix<int?> b)
        {
            if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
            if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static Matrix<int?> Add(this Matrix<int?> a, Matrix<int?> b)
        {
            if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<int?>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    if (a[i, j] != null && b[i, j] != null)
                        matrix[i, j] = a[i, j] + b[i, j];
                    else
                        matrix[i, j] = null;
                }
            }

            return matrix;
        }

        public static Matrix<int?> Sub(this Matrix<int?> a, Matrix<int?> b)
        {
            if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<int?>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    if (a[i, j] != null && b[i, j] != null)
                        matrix[i, j] = a[i, j] - b[i, j];
                    else
                        matrix[i, j] = null;
                }
            }

            return matrix;
        }

        public static Matrix<int?> MulOnNumber(this Matrix<int?> a, int number)
        {
            var matrix = new Matrix<int?>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    if (matrix[i, j] != null)
                        matrix[i, j] = a[i, j] * number;
                    else
                        matrix[i, j] = null;
                }
            }

            return matrix;
        }

        #endregion

        #region Operations

        public static int CalculateDeterminant(this Matrix<int?> matrix)
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
                                                            ((matrix.CreateMatrixWithoutColumn(j) as Matrix<int?>)?.CreateMatrixWithoutRow(1) as Matrix<int?>).CalculateDeterminant();
            }
            return result;
        }

        public static IMatrix<double?> CreateInvertibleMatrix(this Matrix<int?> matrix)
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

        private static double CalculateMinor(this Matrix<int?> matrix, int i, int j)
        {
            return ((matrix.CreateMatrixWithoutColumn(j) as Matrix<int?>)?.CreateMatrixWithoutRow(i) as Matrix<int?>).CalculateDeterminant();
        }

        #endregion

        #region Sum Operations

        public static int DiagonalSum(this Matrix<int?> matrix)
        {
            int sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                if (matrix[i, i] != null)
                    sum += matrix[i, i].Value;
            }
            return sum;
        }

        public static int Sum(this Matrix<int?> matrix)
        {
            int sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (matrix[i,j] != null)
                        sum += matrix[i, j].Value;
                }
            }

            return sum;
        }

        public static int SumSaddlePoints(this Matrix<int?> matrix)
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

        #region Math

        private static Matrix<int?> InternalMulAtoB(this Matrix<int?> matrix, Matrix<int?> b)
        {
            var result = new Matrix<int?>(matrix.RowsCount, b.ColumnsCount);
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < b.ColumnsCount; j++)
                {
                    for (int k = 0; k < b.RowsCount; k++)
                    {
                        if (matrix[i, k] != null && matrix[k, j] != null)
                            result[i, j] += matrix[i, k] * b[k, j];
                        else
                            break;
                    }
                }
            }

            return result;
        }

        private static Matrix<int?> InternalMulBtoA(this Matrix<int?> matrix, Matrix<int?> b)
        {
            var result = new Matrix<int?>(b.RowsCount, matrix.ColumnsCount);
            for (int i = 0; i < b.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    for (int k = 0; k < matrix.RowsCount; k++)
                    {
                        if (matrix[i, k] != null && matrix[k, j] != null)
                            result[i, j] += b[i, k] * matrix[k, j];
                    }
                }
            }

            return matrix;
        }

        #endregion

        #region Find Ops

        public static bool IsMaxInColumn(this Matrix<int?> matrix, int i, int j)
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

        public static bool IsMinInRow(this Matrix<int?> matrix, int i, int j)
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

        public static void FillRandomly(this Matrix<int?> matrix, int min = -50, int max = 50, bool includeNull = false)
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

        public static Matrix<long?> Multiplication(this Matrix<long?> a, Matrix<long?> b)
        {
            if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
            if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static Matrix<long?> Add(this Matrix<long?> a, Matrix<long?> b)
        {
            if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<long?>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    if (a[i, j] != null && b[i, j] != null)
                        matrix[i, j] = a[i, j] + b[i, j];
                    else
                        matrix[i, j] = null;
                }
            }

            return matrix;
        }

        public static Matrix<long?> Sub(this Matrix<long?> a, Matrix<long?> b)
        {
            if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<long?>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    if (a[i, j] != null && b[i, j] != null)
                        matrix[i, j] = a[i, j] - b[i, j];
                    else
                        matrix[i, j] = null;
                }
            }

            return matrix;
        }

        public static Matrix<long?> MulOnNumber(this Matrix<long?> a, long number)
        {
            var matrix = new Matrix<long?>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    if (a[i, j] != null)
                        matrix[i, j] = a[i, j] * number;
                }
            }

            return matrix;
        }

        #endregion

        #region Operations

        public static long CalculateDeterminant(this Matrix<long?> matrix)
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
                                                            ((matrix.CreateMatrixWithoutColumn(j) as Matrix<long?>)?.CreateMatrixWithoutRow(1) as Matrix<long?>).CalculateDeterminant();
            }
            return result;
        }

        public static IMatrix<double> CreateInvertibleMatrix(this Matrix<long?> matrix)
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

        private static double CalculateMinor(this Matrix<long?> matrix, int i, int j)
        {
            return ((matrix.CreateMatrixWithoutColumn(j) as Matrix<long?>)?.CreateMatrixWithoutRow(i) as Matrix<long?>).CalculateDeterminant();
        }

        #endregion

        #region Sum Operations

        public static long DiagonalSum(this Matrix<long?> matrix)
        {
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

        public static long Sum(this Matrix<long?> matrix)
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

        public static long SumSaddlePoints(this Matrix<long?> matrix)
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

        #region Math

        private static Matrix<long?> InternalMulAtoB(this Matrix<long?> matrix, Matrix<long?> b)
        {
            var result = new Matrix<long?>(matrix.RowsCount, b.ColumnsCount);
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < b.ColumnsCount; j++)
                {
                    for (int k = 0; k < b.RowsCount; k++)
                    {
                        if (matrix[i, k] == null && b[k, j] == null)
                            matrix[i, k] = null;
                        else
                            result[i, j] += matrix[i, k] * b[k, j];
                    }
                }
            }

            return result;
        }

        private static Matrix<long?> InternalMulBtoA(this Matrix<long?> matrix, Matrix<long?> b)
        {
            var result = new Matrix<long?>(b.RowsCount, matrix.ColumnsCount);
            for (int i = 0; i < b.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    for (int k = 0; k < matrix.RowsCount; k++)
                    {
                        if (b[i, k] != null && matrix[k, j] == null)
                            result[i, j] = null;
                        else
                            result[i, j] += b[i, k] * matrix[k, j];
                    }
                }
            }

            return matrix;
        }

        #endregion

        #region Find Ops

        public static bool IsMaxInColumn(this Matrix<long?> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] != null && matrix[i, j] != null)
                    if (matrix[k, j] > matrix[i, j])
                        return false;
            }
            return true;
        }

        public static bool IsMinInRow(this Matrix<long?> matrix, int i, int j)
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

        public static void FillRandomly(this Matrix<long?> matrix, long min = -50, long max = 50, bool includeNull = false)
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
                    matrix[i, j] = rnd.Next((int) min, (int) max);
                }
            }
        }

        #endregion

        #endregion

        #region double?

        #region Math

        public static Matrix<double?> Multiplication(this Matrix<double?> a, Matrix<double?> b)
        {
            if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
            if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static Matrix<double?> Add(this Matrix<double?> a, Matrix<double?> b)
        {
            if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<double?>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    if (a[i, j] != null && b[i, j] != null)
                        matrix[i, j] = a[i, j] + b[i, j];
                    else
                        matrix[i, j] = null;
                }
            }

            return matrix;
        }

        public static Matrix<double?> Sub(this Matrix<double?> a, Matrix<double?> b)
        {
            if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second matrix to first. Count of rows should be the same");
            var matrix = new Matrix<double?>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    if (a[i, j] != null && b[i, j] != null)
                        matrix[i, j] = a[i, j] - b[i, j];
                    else
                        matrix[i, j] = null;
                }
            }

            return matrix;
        }

        public static Matrix<double?> MulOnNumber(this Matrix<double?> a, double number)
        {
            var matrix = new Matrix<double?>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ColumnsCount; j++)
                {
                    if (matrix[i, j] != null)
                        matrix[i, j] = a[i, j] * number;
                    else
                        matrix[i, j] = null;
                }
            }

            return matrix;
        }

        #endregion

        #region Operations

        public static double CalculateDeterminant(this Matrix<double?> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new InvalidOperationException(
                    "Determinant can be calculated only for square matrix");
            }
            if (matrix.ColumnsCount == 2)
            {
                if (matrix[0, 0] != null &&
                    matrix[1,1] != null &&
                    matrix[0, 1] != null &&
                    matrix[1, 0] != null)
                return matrix[0, 0].Value * matrix[1, 1].Value - matrix[0, 1].Value * matrix[1, 0].Value;
                return 0;
            }
            double result = 0;
            for (var j = 0; j < matrix.ColumnsCount; j++)
            {
                result += (j % 2 == 1 ? 1 : -1) * matrix[1, j] ?? 0 *
                                                            ((matrix.CreateMatrixWithoutColumn(j) as Matrix<double?>)?.CreateMatrixWithoutRow(1) as Matrix<double?>).CalculateDeterminant();
            }
            return result;
        }

        public static IMatrix<double?> CreateInvertibleMatrix(this Matrix<double?> matrix)
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

        private static double CalculateMinor(this Matrix<double?> matrix, int i, int j)
        {
            return ((matrix.CreateMatrixWithoutColumn(j) as Matrix<double?>)?.CreateMatrixWithoutRow(i) as Matrix<double?>).CalculateDeterminant();
        }

        #endregion

        #region Sum Operations

        public static double DiagonalSum(this Matrix<double?> matrix)
        {
            double sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                if (matrix[i, i] != null)
                    sum += matrix[i, i].Value;
            }
            return sum;
        }

        public static double Sum(this Matrix<double?> matrix)
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

        public static double SumSaddlePoints(this Matrix<double?> matrix)
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

        private static Matrix<double?> InternalMulAtoB(this Matrix<double?> matrix, Matrix<double?> b)
        {
            var result = new Matrix<double?>(matrix.RowsCount, b.ColumnsCount);
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < b.ColumnsCount; j++)
                {
                    for (int k = 0; k < b.RowsCount; k++)
                    {
                        if (matrix[i, k] != null && b[k, j] != null)
                            result[i, j] += matrix[i, k] * b[k, j];
                        else
                            break;
                    }
                }
            }

            return result;
        }

        private static Matrix<double?> InternalMulBtoA(this Matrix<double?> matrix, Matrix<double?> b)
        {
            var result = new Matrix<double?>(b.RowsCount, matrix.ColumnsCount);
            for (int i = 0; i < b.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    for (int k = 0; k < matrix.RowsCount; k++)
                    {
                        if (matrix[i, k] != null && b[k, j] != null)
                            result[i, j] += matrix[i, k] * b[k, j];
                        else
                            break;
                    }
                }
            }

            return matrix;
        }

        #endregion

        #region Find Ops

        public static bool IsMaxInColumn(this Matrix<double?> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] != null && matrix[i, j] != null)
                    if (matrix[k, j] > matrix[i, j])
                        return false;
            }
            return true;
        }

        public static bool IsMinInRow(this Matrix<double?> matrix, int i, int j)
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

        public static void FillRandomly(this Matrix<double?> matrix, double min = -50d, double max = 50d, bool includeNull = false)
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