﻿using System;
using System.Collections.Generic;
using System.Text;
using CyberMath.Structures.Matrix.Matrix.Models;

namespace CyberMath.Structures.Matrix.Matrix.Extensions
{
    public static class ReferenceTypeMatrixExtensions
    {
        public static int Sum<TSource>(this Matrix<TSource> matrix, Func<TSource, int> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            int sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    sum += selector(matrix[i, j]);
                }
            }

            return sum;
        }

        public static int Sum<TSource>(this Matrix<TSource> matrix, Func<TSource, int?> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            int sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (matrix[i, j] != null)
                        sum += selector(matrix[i, j]).Value;
                }
            }

            return sum;
        }

        public static double Sum<TSource>(this Matrix<TSource> matrix, Func<TSource, double> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            double sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    sum += selector(matrix[i, j]);
                }
            }

            return sum;
        }

        public static double Sum<TSource>(this Matrix<TSource> matrix, Func<TSource, double?> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            double sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (matrix[i, j] != null)
                        sum += selector(matrix[i, j]).Value;
                }
            }

            return sum;
        }

        public static decimal Sum<TSource>(this Matrix<TSource> matrix, Func<TSource, decimal> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            decimal sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    sum += selector(matrix[i, j]);
                }
            }

            return sum;
        }

        public static decimal? Sum<TSource>(this Matrix<TSource> matrix, Func<TSource, decimal?> selector)
        {
            if (matrix == null) throw new ArgumentException("Matrix is null!");
            if (selector == null) throw new ArgumentException("selector is null");
            decimal sum = 0;
            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (matrix[i, j] != null)
                        sum += selector(matrix[i, j]).Value;
                }
            }

            return sum;
        }
    }
}
