using System;
using System.Linq;
using System.Text;
using CyberMath.Structures.Extensions;
using CyberMath.Structures.Extensions.Extensions;
using CyberMath.Structures.Matrix.JaggedMatrix.Models;
using CyberMath.Structures.Matrix.MatrixBase;
using CyberMath.Structures.Matrix.MatrixBase.Exceptions;

namespace CyberMath.Structures.Matrix.MatrixExtensions.JaggedMatrix
{
    public static class ValueTypeJuggedMatrixExtensions
    {
        #region NOT nullable

        #region Int32

        #region Math

        public static IJuggedMatrix<int> Add(this IJuggedMatrix<int> a, IJuggedMatrix<int> b)
        {
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");
            for (int i = 0; i < a.RowsCount; i++)
            {
                if (a.ElementsInRow(i) != b.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<int>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = a[i, j] + b[i, j];
                }
            }

            return juggedMatrix;
        }

        public static IJuggedMatrix<int> Sub(this IJuggedMatrix<int> a, IJuggedMatrix<int> b)
        {
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");
            for (int i = 0; i < a.RowsCount; i++)
            {
                if (a.ElementsInRow(i) != b.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<int>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = a[i, j] - b[i, j];
                }
            }

            return juggedMatrix;
        }

        public static IJuggedMatrix<int> MulOnNumber(this IJuggedMatrix<int> a, int number)
        {
            var juggedMatrix = new JuggedMatrix<int>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = a[i, j] * number;
                }
            }

            return juggedMatrix;
        }

        #endregion

        #region Creation

        public static IJuggedMatrix<int> CreateIdentityMatrix(int n)
        {
            var result = new JuggedMatrix<int>(n, CollectionExtensions.
                GetRepeatedIntEnumerable(n).Take(n).ToArray());
            for (var i = 0; i < n; i++)
            {
                result[i, i] = 1;
            }
            return result;
        }

        #endregion

        #region Sum Operations

        public static int DiagonalSum(this IJuggedMatrix<int> juggedMatrix)
        {
            if (!juggedMatrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            int sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
                sum += juggedMatrix[i, i];
            return sum;
        }

        public static int Sum(this IJuggedMatrix<int> juggedMatrix)
        {
            int sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    sum += juggedMatrix[i, j];
                }
            }

            return sum;
        }

        public static int SumSaddlePoints(this IJuggedMatrix<int> juggedMatrix)
        {
            int sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
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

        public static void FillRandomly(this IJuggedMatrix<int> juggedMatrix, int min = -50, int max = 50)
        {
            var rnd = new Random();
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = rnd.Next(min, max + 1);
                }
            }
        }

        #endregion

        #endregion

        #region Long

        #region Math

        public static IJuggedMatrix<long> Add(this IJuggedMatrix<long> a, IJuggedMatrix<long> b)
        {
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");
            for (int i = 0; i < a.RowsCount; i++)
            {
                if (a.ElementsInRow(i) != b.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<long>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = a[i, j] + b[i, j];
                }
            }

            return juggedMatrix;
        }

        public static IJuggedMatrix<long> Sub(this IJuggedMatrix<long> a, IJuggedMatrix<long> b)
        {
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");
            for (int i = 0; i < a.RowsCount; i++)
            {
                if (a.ElementsInRow(i) != b.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<long>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = a[i, j] - b[i, j];
                }
            }

            return juggedMatrix;
        }

        public static IJuggedMatrix<long> MulOnNumber(this IJuggedMatrix<long> a, long number)
        {
            var juggedMatrix = new JuggedMatrix<long>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = a[i, j] * number;
                }
            }

            return juggedMatrix;
        }

        #endregion

        #region Sum Operations

        public static long DiagonalSum(this IJuggedMatrix<long> juggedMatrix)
        {
            if (!juggedMatrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            long sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
                sum += juggedMatrix[i, i];
            return sum;
        }

        public static long Sum(this IJuggedMatrix<long> juggedMatrix)
        {
            long sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    sum += juggedMatrix[i, j];
                }
            }

            return sum;
        }

        public static long SumSaddlePoints(this IJuggedMatrix<long> juggedMatrix)
        {
            long sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
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

        public static void FillRandomly(this IJuggedMatrix<long> juggedMatrix, int min = -50, int max = 50)
        {
            var rnd = new Random();
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = rnd.Next(min, max + 1);
                }
            }
        }

        #endregion

        #endregion

        #region Double

        #region Math

        public static IJuggedMatrix<double> Add(this IJuggedMatrix<double> a, IJuggedMatrix<double> b)
        {
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");
            for (int i = 0; i < a.RowsCount; i++)
            {
                if (a.ElementsInRow(i) != b.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<double>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = a[i, j] + b[i, j];
                }
            }

            return juggedMatrix;
        }

        public static IJuggedMatrix<double> Sub(this IJuggedMatrix<double> a, IJuggedMatrix<double> b)
        {
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");
            for (int i = 0; i < a.RowsCount; i++)
            {
                if (a.ElementsInRow(i) != b.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<double>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = a[i, j] - b[i, j];
                }
            }

            return juggedMatrix;
        }

        public static IJuggedMatrix<double> MulOnNumber(this IJuggedMatrix<double> a, double number)
        {
            var juggedMatrix = new JuggedMatrix<double>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = a[i, j] * number;
                }
            }

            return juggedMatrix;
        }

        #endregion

        #region Sum Operations

        public static double DiagonalSum(this IJuggedMatrix<double> juggedMatrix)
        {
            if (!juggedMatrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            double sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
                sum += juggedMatrix[i, i];
            return sum;
        }

        public static double Sum(this IJuggedMatrix<double> juggedMatrix)
        {
            double sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    sum += juggedMatrix[i, j];
                }
            }

            return sum;
        }

        public static double SumSaddlePoints(this IJuggedMatrix<double> juggedMatrix)
        {
            double sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
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

        public static void FillRandomly(this IJuggedMatrix<double> juggedMatrix, double min = -50, double max = 50)
        {
            var rnd = new Random();
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = rnd.NextDouble(min, max + 1);
                }
            }
        }

        #endregion

        #endregion

        #region Decimal

        #region Math

        public static IJuggedMatrix<decimal> Add(this IJuggedMatrix<decimal> a, IJuggedMatrix<decimal> b)
        {
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");
            for (int i = 0; i < a.RowsCount; i++)
            {
                if (a.ElementsInRow(i) != b.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<decimal>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = a[i, j] + b[i, j];
                }
            }

            return juggedMatrix;
        }

        public static IJuggedMatrix<decimal> Sub(this IJuggedMatrix<decimal> a, IJuggedMatrix<decimal> b)
        {
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");
            for (int i = 0; i < a.RowsCount; i++)
            {
                if (a.ElementsInRow(i) != b.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<decimal>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = a[i, j] - b[i, j];
                }
            }

            return juggedMatrix;
        }

        public static IJuggedMatrix<decimal> MulOnNumber(this IJuggedMatrix<decimal> a, decimal number)
        {
            var juggedMatrix = new JuggedMatrix<decimal>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = a[i, j] * number;
                }
            }

            return juggedMatrix;
        }

        #endregion

        #region Sum Operations

        public static decimal DiagonalSum(this IJuggedMatrix<decimal> juggedMatrix)
        {
            if (!juggedMatrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            decimal sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
                sum += juggedMatrix[i, i];
            return sum;
        }

        public static decimal Sum(this IJuggedMatrix<decimal> juggedMatrix)
        {
            decimal sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    sum += juggedMatrix[i, j];
                }
            }

            return sum;
        }

        public static decimal SumSaddlePoints(this IJuggedMatrix<decimal> juggedMatrix)
        {
            decimal sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
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

        public static void FillRandomly(this IJuggedMatrix<decimal> juggedMatrix, decimal min = -50, decimal max = 50)
        {
            var rnd = new Random();
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = (decimal)rnd.NextDouble((double)min, (double)max + 1);
                }
            }
        }

        #endregion

        #endregion

        #region string

        #region Math

        public static IJuggedMatrix<string> Add(this IJuggedMatrix<string> a, IJuggedMatrix<string> b)
        {
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");
            for (int i = 0; i < a.RowsCount; i++)
            {
                if (a.ElementsInRow(i) != b.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<string>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = a[i, j] + b[i, j];
                }
            }

            return juggedMatrix;
        }

        public static IJuggedMatrix<string> Sub(this IJuggedMatrix<string> a, IJuggedMatrix<string> b)
        {
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");
            for (int i = 0; i < a.RowsCount; i++)
            {
                if (a.ElementsInRow(i) != b.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<string>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = String.Concat(a[i, j].Except(b[i, j]));
                }
            }

            return juggedMatrix;
        }

        public static IJuggedMatrix<string> MulOnNumber(this IJuggedMatrix<string> a, int number)
        {
            var juggedMatrix = new JuggedMatrix<string>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = a[i, j].Concat(number);
                }
            }

            return juggedMatrix;
        }

        #endregion

        #region Sum Operations

        public static string DiagonalSum(this IJuggedMatrix<string> juggedMatrix)
        {
            if (!juggedMatrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            var sb = new StringBuilder();
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
                sb.Append(juggedMatrix[i, i]).Append(" ");
            return sb.ToString();
        }

        public static string Sum(this IJuggedMatrix<string> juggedMatrix)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    sb.Append(juggedMatrix[i, j]).Append(" ");
                }
            }

            return sb.ToString();
        }

        public static string SumSaddlePoints(this IJuggedMatrix<string> juggedMatrix)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
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

        public static IJuggedMatrix<int?> Add(this IJuggedMatrix<int?> a, IJuggedMatrix<int?> b)
        {
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");
            for (int i = 0; i < a.RowsCount; i++)
            {
                if (a.ElementsInRow(i) != b.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<int?>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    if (a[i, j] != null && b[i, j] != null)
                        juggedMatrix[i, j] = a[i, j].Value + b[i, j].Value;
                }
            }

            return juggedMatrix;
        }

        public static IJuggedMatrix<int?> Sub(this IJuggedMatrix<int?> a, IJuggedMatrix<int?> b)
        {
            if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");
            for (int i = 0; i < a.RowsCount; i++)
            {
                if (a.ElementsInRow(i) != b.ElementsInRow(i))
                    throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
            }
            var juggedMatrix = new JuggedMatrix<int?>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    if (a[i, j] != null && b[i, j] != null)
                        juggedMatrix[i, j] = a[i, j].Value - b[i, j].Value;
                }
            }

            return juggedMatrix;
        }

        public static IJuggedMatrix<int?> MulOnNumber(this IJuggedMatrix<int?> a, int number)
        {
            var juggedMatrix = new JuggedMatrix<int?>(a.RowsCount, a.GetCountPerRow());
            for (int i = 0; i < a.RowsCount; i++)
            {
                for (int j = 0; j < a.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = a[i, j].Value * number;
                }
            }

            return juggedMatrix;
        }

        #endregion

        #region Creation

        public static IJuggedMatrix<int?> CreateIdentityMatrix(int n, bool includeNull = false)
        {
            var result = new JuggedMatrix<int?>(n, Structures.Extensions.Extensions.CollectionExtensions.
                GetRepeatedIntEnumerable(n).Take(n).ToArray());
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

        public static int DiagonalSum(this IJuggedMatrix<int?> juggedMatrix)
        {
            if (!juggedMatrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            int sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
                if (juggedMatrix[i, i] != null)
                    sum += juggedMatrix[i, i].Value;
            return sum;
        }

        public static int Sum(this IJuggedMatrix<int?> juggedMatrix)
        {
            int sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    if (juggedMatrix[i, j] != null)
                        sum += juggedMatrix[i, j].Value;
                }
            }

            return sum;
        }

        #endregion

        #region Fill

        public static void FillRandomly(this IJuggedMatrix<int?> juggedMatrix, int min = -50, int max = 50)
        {
            var rnd = new Random();
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    juggedMatrix[i, j] = rnd.Next(min, max + 1);
                }
            }
        }

        #endregion

        #endregion

        #region long?

        #region Math

        //public static JuggedMatrix<long?> Multiplication(this JuggedMatrix<long?> a, JuggedMatrix<long?> b)
        //{
        //    if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
        //    if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
        //    throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        //}

        //public static JuggedMatrix<long?> Add(this JuggedMatrix<long?> a, JuggedMatrix<long?> b)
        //{
        //    if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
        //    if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");
        //    var JuggedMatrix = new JuggedMatrix<long?>(a.RowsCount, a.ColumnsCount);
        //    for (int i = 0; i < a.RowsCount; i++)
        //    {
        //        for (int j = 0; j < a.ColumnsCount; j++)
        //        {
        //            if (a[i, j] != null && b[i, j] != null)
        //                JuggedMatrix[i, j] = a[i, j] + b[i, j];
        //            else
        //                JuggedMatrix[i, j] = null;
        //        }
        //    }

        //    return JuggedMatrix;
        //}

        //public static JuggedMatrix<long?> Sub(this JuggedMatrix<long?> a, JuggedMatrix<long?> b)
        //{
        //    if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of columns should be the same");
        //    if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");
        //    var JuggedMatrix = new JuggedMatrix<long?>(a.RowsCount, a.ColumnsCount);
        //    for (int i = 0; i < a.RowsCount; i++)
        //    {
        //        for (int j = 0; j < a.ColumnsCount; j++)
        //        {
        //            if (a[i, j] != null && b[i, j] != null)
        //                JuggedMatrix[i, j] = a[i, j] - b[i, j];
        //            else
        //                JuggedMatrix[i, j] = null;
        //        }
        //    }

        //    return JuggedMatrix;
        //}

        //public static JuggedMatrix<long?> MulOnNumber(this JuggedMatrix<long?> a, long number)
        //{
        //    var JuggedMatrix = new JuggedMatrix<long?>(a.RowsCount, a.ColumnsCount);
        //    for (int i = 0; i < a.RowsCount; i++)
        //    {
        //        for (int j = 0; j < a.ColumnsCount; j++)
        //        {
        //            if (a[i, j] != null)
        //                JuggedMatrix[i, j] = a[i, j] * number;
        //        }
        //    }

        //    return JuggedMatrix;
        //}

        #endregion

        #region Sum Operations

        public static long DiagonalSum(this JuggedMatrix<long?> JuggedMatrix)
        {
            if (!JuggedMatrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            long sum = 0;
            for (int i = 0; i < JuggedMatrix.RowsCount; i++)
            {
                if (JuggedMatrix[i, i] != null)
                {
                    sum += JuggedMatrix[i, i].Value;
                }
            }
            return sum;
        }

        public static long Sum(this JuggedMatrix<long?> juggedMatrix)
        {
            long sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    if (juggedMatrix[i, j] != null)
                    {
                        sum += juggedMatrix[i, j].Value;
                    }
                }
            }

            return sum;
        }

        #endregion

        #region Fill

        //public static void FillRandomly(this JuggedMatrix<long?> JuggedMatrix, long min = -50, long max = 50, bool includeNull = false)
        //{
        //    var rnd = new Random();
        //    for (int i = 0; i < JuggedMatrix.RowsCount; i++)
        //    {
        //        for (int j = 0; j < JuggedMatrix.ColumnsCount; j++)
        //        {
        //            if (i + j * i / j - i + j % 2 == 0 && includeNull)
        //            {
        //                JuggedMatrix[i, j] = null;
        //                continue;
        //            }
        //            JuggedMatrix[i, j] = rnd.Next((int)min, (int)max);
        //        }
        //    }
        //}

        #endregion

        #endregion

        #region double?

        #region Math

        //public static JuggedMatrix<double?> Multiplication(this JuggedMatrix<double?> a, JuggedMatrix<double?> b)
        //{
        //    if (a.ColumnsCount == b.RowsCount) return a.InternalMulAtoB(b);
        //    if (b.ColumnsCount == a.RowsCount) return a.InternalMulBtoA(b);
        //    throw new MatrixIncomparableOperationException("Multiplication of this matrices is not possible");
        //}

        //public static JuggedMatrix<double?> Add(this JuggedMatrix<double?> a, JuggedMatrix<double?> b)
        //{
        //    if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of columns should be the same");
        //    if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't add second JuggedMatrix to first. Count of rows should be the same");
        //    var JuggedMatrix = new JuggedMatrix<double?>(a.RowsCount, a.ColumnsCount);
        //    for (int i = 0; i < a.RowsCount; i++)
        //    {
        //        for (int j = 0; j < a.ColumnsCount; j++)
        //        {
        //            if (a[i, j] != null && b[i, j] != null)
        //                JuggedMatrix[i, j] = a[i, j] + b[i, j];
        //            else
        //                JuggedMatrix[i, j] = null;
        //        }
        //    }

        //    return JuggedMatrix;
        //}

        //public static JuggedMatrix<double?> Sub(this JuggedMatrix<double?> a, JuggedMatrix<double?> b)
        //{
        //    if (a.ColumnsCount != b.ColumnsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of columns should be the same");
        //    if (a.RowsCount != b.RowsCount) throw new MatrixIncomparableOperationException("Can't sub second JuggedMatrix to first. Count of rows should be the same");
        //    var JuggedMatrix = new JuggedMatrix<double?>(a.RowsCount, a.ColumnsCount);
        //    for (int i = 0; i < a.RowsCount; i++)
        //    {
        //        for (int j = 0; j < a.ColumnsCount; j++)
        //        {
        //            if (a[i, j] != null && b[i, j] != null)
        //                JuggedMatrix[i, j] = a[i, j] - b[i, j];
        //            else
        //                JuggedMatrix[i, j] = null;
        //        }
        //    }

        //    return JuggedMatrix;
        //}

        //public static JuggedMatrix<double?> MulOnNumber(this JuggedMatrix<double?> a, double number)
        //{
        //    var JuggedMatrix = new JuggedMatrix<double?>(a.RowsCount, a.ColumnsCount);
        //    for (int i = 0; i < a.RowsCount; i++)
        //    {
        //        for (int j = 0; j < a.ColumnsCount; j++)
        //        {
        //            if (JuggedMatrix[i, j] != null)
        //                JuggedMatrix[i, j] = a[i, j] * number;
        //            else
        //                JuggedMatrix[i, j] = null;
        //        }
        //    }

        //    return JuggedMatrix;
        //}

        #endregion

        #region Sum Operations

        public static double DiagonalSum(this JuggedMatrix<double?> juggedMatrix)
        {
            if (!juggedMatrix.IsSquare) throw new MatrixIncomparableOperationException("Diagonal sum can be calculated only for square matrices");
            double sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                if (juggedMatrix[i, i] != null)
                    sum += juggedMatrix[i, i].Value;
            }
            return sum;
        }

        public static double Sum(this JuggedMatrix<double?> juggedMatrix)
        {
            double sum = 0;
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    if (juggedMatrix[i, j] != null)
                        sum += juggedMatrix[i, j].Value;
                }
            }

            return sum;
        }

        #endregion

        #region Fill

        public static void FillRandomly(this JuggedMatrix<double?> juggedMatrix, double min = -50d, double max = 50d, bool includeNull = false)
        {
            var rnd = new Random();
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                {
                    double value = rnd.NextDouble(min, max);
                    if (i + j * i / j - i + j % 2 == 0 && includeNull)
                    {
                        juggedMatrix[i, j] = null;
                        continue;
                    }

                    juggedMatrix[i, j] = value;
                }
            }
        }

        #endregion

        #endregion

        #endregion
    }
}