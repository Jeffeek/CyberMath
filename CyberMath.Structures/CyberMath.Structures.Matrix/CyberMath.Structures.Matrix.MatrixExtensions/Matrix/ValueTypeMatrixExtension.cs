using System;
using System.Text;
using CyberMath.Extensions.Extensions;
using CyberMath.Structures.Matrix.Matrix.Models;
using CyberMath.Structures.Matrix.MatrixBase.Exceptions;

namespace CyberMath.Structures.Matrix.MatrixExtensions.Matrix
{
    public static class ValueTypeMatrixExtension
    {
        #region Int32

        #region Math

        public static IMatrix<int> Multiplication(this IMatrix<int> a, IMatrix<int> b)
        {
            if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
            if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static IMatrix<int> Add(this IMatrix<int> a, IMatrix<int> b)
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

        public static IMatrix<int> Sub(this IMatrix<int> a, IMatrix<int> b)
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

        public static IMatrix<int> MulOnNumber(this Matrix<int> a, int number)
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

        private static double CalculateMinor(this IMatrix<int> matrix, int i, int j)
        {
            return ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<int>)?.CreateMatrixWithoutRow(i) as IMatrix<int>).CalculateDeterminant();
        }

        #endregion

        #region Sum Operations

        public static int DiagonalSum(this IMatrix<int> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            int sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        public static int Sum(this IMatrix<int> matrix)
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

        #region Math

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

        #region Find Ops

        public static bool IsMaxInColumn(this IMatrix<int> matrix,int i, int j)
        {
            for (int k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] > matrix[i, j])
                    return false;
            }
            return true;
        }

        public static bool IsMinInRow(this IMatrix<int> matrix, int i, int j)
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

        public static void FillRandomly(this IMatrix<int> matrix, int min = -50, int max = 50)
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

        public static IMatrix<long> Multiplication(this IMatrix<long> a, IMatrix<long> b)
        {
            if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
            if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static IMatrix<long> Add(this IMatrix<long> a, IMatrix<long> b)
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

        public static IMatrix<long> Sub(this IMatrix<long> a, IMatrix<long> b)
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

        public static IMatrix<long> MulOnNumber(this IMatrix<long> a, long number)
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

        private static double CalculateMinor(this IMatrix<long> matrix, int i, int j)
        {
            return ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<long>)?.CreateMatrixWithoutRow(i) as IMatrix<long>).CalculateDeterminant();
        }

        #endregion

        #region Sum Operations

        public static long DiagonalSum(this IMatrix<long> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            long sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        public static long Sum(this IMatrix<long> matrix)
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

        #region Math

        private static IMatrix<long> InternalMulAtoB(this IMatrix<long> matrix, IMatrix<long> b)
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

        private static IMatrix<long> InternalMulBtoA(this IMatrix<long> matrix, IMatrix<long> b)
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

        public static bool IsMaxInColumn(this IMatrix<long> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] > matrix[i, j])
                    return false;
            }
            return true;
        }

        public static bool IsMinInRow(this IMatrix<long> matrix, int i, int j)
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

        public static void FillRandomly(this IMatrix<long> matrix, long min = -50, long max = 50)
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

        public static IMatrix<double> Multiplication(this IMatrix<double> a, IMatrix<double> b)
        {
            if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
            if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static IMatrix<double> Add(this IMatrix<double> a, IMatrix<double> b)
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

        public static IMatrix<double> Sub(this IMatrix<double> a, IMatrix<double> b)
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

        public static IMatrix<double> MulOnNumber(this IMatrix<double> a, double number)
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

        private static double CalculateMinor(this IMatrix<double> matrix, int i, int j)
        {
            return ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<double>)?.CreateMatrixWithoutRow(i) as IMatrix<double>).CalculateDeterminant();
        }

        #endregion

        #region Sum Operations

        public static double DiagonalSum(this IMatrix<double> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            double sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        public static double Sum(this IMatrix<double> matrix)
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

        #region Math

        private static IMatrix<double> InternalMulAtoB(this IMatrix<double> matrix, IMatrix<double> b)
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

        private static IMatrix<double> InternalMulBtoA(this IMatrix<double> matrix, IMatrix<double> b)
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

        public static bool IsMaxInColumn(this IMatrix<double> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] > matrix[i, j])
                    return false;
            }
            return true;
        }

        public static bool IsMinInRow(this IMatrix<double> matrix, int i, int j)
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

        public static IMatrix<decimal> Multiplication(this IMatrix<decimal> a, IMatrix<decimal> b)
        {
            if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
            if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static IMatrix<decimal> Add(this IMatrix<decimal> a, IMatrix<decimal> b)
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

        public static IMatrix<decimal> Sub(this IMatrix<decimal> a, IMatrix<decimal> b)
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

        public static IMatrix<decimal> MulOnNumber(this IMatrix<decimal> a, decimal number)
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

        private static decimal CalculateMinor(this IMatrix<decimal> matrix, int i, int j)
        {
            return ((matrix.CreateMatrixWithoutColumn(j) as IMatrix<decimal>)?.CreateMatrixWithoutRow(i) as IMatrix<decimal>).CalculateDeterminant();
        }

        #endregion

        #region Sum Operations

        public static decimal DiagonalSum(this IMatrix<decimal> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            decimal sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
                sum += matrix[i, i];
            return sum;
        }

        public static decimal Sum(this IMatrix<decimal> matrix)
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

        #region Math

        private static IMatrix<decimal> InternalMulAtoB(this IMatrix<decimal> matrix, IMatrix<decimal> b)
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

        private static IMatrix<decimal> InternalMulBtoA(this IMatrix<decimal> matrix, IMatrix<decimal> b)
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

        public static bool IsMaxInColumn(this IMatrix<decimal> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j] > matrix[i, j])
                    return false;
            }
            return true;
        }

        public static bool IsMinInRow(this IMatrix<decimal> matrix, int i, int j)
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

        public static IMatrix<string> Add(this IMatrix<string> a, IMatrix<string> b)
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

        public static IMatrix<string> MulOnNumber(this IMatrix<string> a, int number)
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

        public static string DiagonalSum(this IMatrix<string> matrix)
        {
            if (!matrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sb = new StringBuilder();
            for (int i = 0; i < matrix.RowsCount; i++)
                sb.Append(matrix[i, i]).Append(' ');
            return sb.ToString();
        }

        public static string Sum(this IMatrix<string> matrix)
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

        #region Find Ops

        public static bool IsMaxInColumn(this IMatrix<string> matrix, int i, int j)
        {
            for (int k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j].CompareTo(matrix[i, j]) == 1)
                    return false;
            }
            return true;
        }

        public static bool IsMinInRow(this IMatrix<string> matrix, int i, int j)
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

        #region Nullable

        #region Int32?

        #region Math

        public static IMatrix<int?> Multiplication(this IMatrix<int?> a, IMatrix<int?> b)
        {
            if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
            if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static IMatrix<int?> Add(this IMatrix<int?> a, IMatrix<int?> b)
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

        public static IMatrix<int?> Sub(this IMatrix<int?> a, IMatrix<int?> b)
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

        public static IMatrix<int?> MulOnNumber(this IMatrix<int?> a, int number)
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
                    if (matrix[i,j] != null)
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

        #region Math

        private static IMatrix<int?> InternalMulAtoB(this IMatrix<int?> matrix, IMatrix<int?> b)
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

        private static IMatrix<int?> InternalMulBtoA(this IMatrix<int?> matrix, IMatrix<int?> b)
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

        public static IMatrix<long?> Multiplication(this IMatrix<long?> a, IMatrix<long?> b)
        {
            if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
            if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static IMatrix<long?> Add(this IMatrix<long?> a, IMatrix<long?> b)
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

        public static IMatrix<long?> Sub(this IMatrix<long?> a, IMatrix<long?> b)
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

        public static IMatrix<long?> MulOnNumber(this IMatrix<long?> a, long number)
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

        #region Math

        private static IMatrix<long?> InternalMulAtoB(this IMatrix<long?> matrix, IMatrix<long?> b)
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

        private static IMatrix<long?> InternalMulBtoA(this IMatrix<long?> matrix, IMatrix<long?> b)
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
                    matrix[i, j] = rnd.Next((int) min, (int) max);
                }
            }
        }

        #endregion

        #endregion

        #region double?

        #region Math

        public static IMatrix<double?> Multiplication(this IMatrix<double?> a, IMatrix<double?> b)
        {
            if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
            if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
            throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        }

        public static IMatrix<double?> Add(this IMatrix<double?> a, IMatrix<double?> b)
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

        public static IMatrix<double?> Sub(this IMatrix<double?> a, IMatrix<double?> b)
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

        public static IMatrix<double?> MulOnNumber(this IMatrix<double?> a, double number)
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

        private static IMatrix<double?> InternalMulAtoB(this IMatrix<double?> matrix, IMatrix<double?> b)
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

        private static IMatrix<double?> InternalMulBtoA(this IMatrix<double?> matrix, IMatrix<double?> b)
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