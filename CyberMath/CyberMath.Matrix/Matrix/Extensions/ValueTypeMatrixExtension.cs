using System;
using System.Collections.Generic;
using System.Text;
using CyberMath.Matrix.Exceptions;
using CyberMath.Matrix.Models;
using MatrixBase;

namespace CyberMath.Matrix.Extensions
{
    public static class ValueTypeMatrixExtension
    {
        #region Int32

        #region Math

        public static Matrix<int> Multiplication(this Matrix<int> a, Matrix<int> b)
        {
            if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
            if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
            throw new IncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static Matrix<int> Add(this Matrix<int> a, Matrix<int> b)
        {
            if (a.ColumnsCount != b.ColumnsCount) throw new IncomparableOperationException("Can't add second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new IncomparableOperationException("Can't add second matrix to first. Count of rows should be the same");
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
            if (a.ColumnsCount != b.ColumnsCount) throw new IncomparableOperationException("Can't sub second matrix to first. Count of columns should be the same");
            if (a.RowsCount != b.RowsCount) throw new IncomparableOperationException("Can't sub second matrix to first. Count of rows should be the same");
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

        public static IMatrixBase<double> CreateInvertibleMatrix(this Matrix<int> matrix)
        {
            if (!matrix.IsSquare)
                return null;
            var determinant = matrix.CalculateDeterminant();

            IMatrixBase<double> result = new Matrix<double>(matrix.RowsCount, matrix.ColumnsCount);
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
            var rnd = new Random();
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    double coefficient = rnd.NextDouble();
                    coefficient = coefficient > 0.5 ? coefficient : 0.5;
                    matrix[i, j] = rnd.Next(min, max);
                }
            }
        }

        #endregion

        #endregion

        //TODO: add long, short, double, decimal, string

    }
}
