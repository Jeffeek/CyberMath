#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using CyberMath.Structures.Matrices.Dynamic_Matrices.Dynamic_Jugged_Matrix;
using CyberMath.Structures.Matrices.Dynamic_Matrices.Dynamic_Matrix;
using CyberMath.Structures.Matrices.Jagged_Matrix;
using CyberMath.Structures.Matrices.Matrix;

#endregion

namespace CyberMath.Structures.Matrices.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="IMatrix{T}"/> and <see cref="IJuggedMatrix{T}"/> for their transformation
    /// </summary>
    public static class MatrixExtensions
    {
        /// <summary>
        ///     Returns an array with count of elements on each row in <see cref="IJuggedMatrix{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static IEnumerable<int> CountOnEachRow<T>(this IJuggedMatrix<T> matrix)
        {
            for (var i = 0; i < matrix.RowsCount; i++)
                yield return matrix.ElementsInRow(i);
        }

        #region Converting

        #region JuggedMatrix -> Matrix

        //TODO: unit-test
        /// <summary>
        ///     Creates a new instance of <see cref="DynamicMatrix{T}"/> from <see cref="JuggedMatrix{T}"/>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <returns>A new instance of <see cref="IMatrix{T}"/></returns>
        public static DynamicMatrix<T> ToDynamicMatrix<T>(this JuggedMatrix<T> juggedMatrix)
        {
            var matrix = new DynamicMatrix<T>(juggedMatrix.RowsCount,
                                              juggedMatrix.CountOnEachRow()
                                                          .Max());

            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++)
                    if (juggedMatrix.ElementsInRow(i) < matrix.ColumnsCount)
                        matrix[i, j] = juggedMatrix[i, j];
                    else
                        matrix[i, j] = default;
            }

            return matrix;
        }

        //TODO: unit-test
        /// <summary>
        ///     Creates a new instance of <see cref="Matrix{T}"/> from <see cref="JuggedMatrix{T}"/>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <returns>A new instance of <see cref="IMatrix{T}"/></returns>
        public static Matrix<T> ToMatrix<T>(this JuggedMatrix<T> juggedMatrix)
        {
            var matrix = new Matrix<T>(juggedMatrix.RowsCount,
                                       juggedMatrix.CountOnEachRow()
                                                   .Max());

            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++)
                    if (juggedMatrix.ElementsInRow(i) < matrix.ColumnsCount)
                        matrix[i, j] = juggedMatrix[i, j];
                    else
                        matrix[i, j] = default;
            }

            return matrix;
        }

        #endregion

        #region Matrix -> JuggedMatrix

        //TODO: unit-test
        /// <summary>
        ///     Creates a new instance of <see cref="JuggedMatrix{T}"/> from <see cref="IMatrix{T}"/>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>A new instance of <see cref="JuggedMatrix{T}"/></returns>
        public static JuggedMatrix<T> ToJuggedMatrix<T>(this IMatrix<T> matrix)
        {
            var juggedMatrix = new JuggedMatrix<T>(matrix.RowsCount,
                                                   Enumerable.Repeat(matrix.ColumnsCount, matrix.RowsCount)
                                                             .ToArray());

            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++) juggedMatrix[i, j] = matrix[i, j];
            }

            return juggedMatrix;
        }

        //TODO: unit-test
        /// <summary>
        ///     Creates a new instance of <see cref="DynamicJuggedMatrix{T}"/> from <see cref="IMatrix{T}"/>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>A new instance of <see cref="DynamicJuggedMatrix{T}"/></returns>
        public static DynamicJuggedMatrix<T> ToDynamicJuggedMatrix<T>(this IMatrix<T> matrix)
        {
            var juggedMatrix = new DynamicJuggedMatrix<T>(matrix.RowsCount,
                                                          Enumerable.Repeat(matrix.ColumnsCount, matrix.RowsCount)
                                                                    .ToArray());

            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++) juggedMatrix[i, j] = matrix[i, j];
            }

            return juggedMatrix;
        }

        #endregion

        #region From IEnumerable

        //TODO: unit-test
        /// <summary>
        ///     Creates a new instance of <see cref="Matrix{T}"/> which contains elements from <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Matrix<T> CreateMatrix<T>(this IEnumerable<IEnumerable<T>> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            var enumerables = source as IEnumerable<T>[] ?? source.ToArray();

            if (enumerables.Any(item => item is null))
                throw new ArgumentNullException(nameof(source) + ": one of enumerables was null");

            var columnsCount = enumerables[0]
                .Count();

            if (enumerables.All(item => item.Count() == columnsCount))
                throw new ArgumentException("Number of elements in inner enumerables should be equal");

            var matrix = new Matrix<T>(enumerables.Length,
                                       enumerables[0]
                                           .Count());

            var rowIndex = 0;
            var columnIndex = 0;

            foreach (var row in enumerables)
            {
                foreach (var item in row)
                {
                    matrix[rowIndex, columnIndex] = item;
                    columnIndex++;
                }

                rowIndex++;
                columnIndex = 0;
            }

            return matrix;
        }

        //TODO: unit-test
        /// <summary>
        ///     Creates a new instance of <see cref="DynamicMatrix{T}"/> which contains elements from
        ///     <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DynamicMatrix<T> CreateDynamicMatrix<T>(this IEnumerable<IEnumerable<T>> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            var enumerables = source as IEnumerable<T>[] ?? source.ToArray();

            if (enumerables.Any(item => item is null))
                throw new ArgumentNullException(nameof(source) + ": one of enumerables was null");

            var columnsCount = enumerables[0]
                .Count();

            if (enumerables.All(item => item.Count() == columnsCount))
                throw new ArgumentException("Number of elements in inner enumerables should be equal");

            var matrix = new DynamicMatrix<T>(enumerables.Length,
                                              enumerables[0]
                                                  .Count());

            var rowIndex = 0;
            var columnIndex = 0;

            foreach (var row in enumerables)
            {
                foreach (var item in row)
                {
                    matrix[rowIndex, columnIndex] = item;
                    columnIndex++;
                }

                rowIndex++;
                columnIndex = 0;
            }

            return matrix;
        }

        //TODO: unit-test
        /// <summary>
        ///     Creates a new instance of <see cref="JuggedMatrix{T}"/> which contains elements from <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static JuggedMatrix<T> CreateJuggedMatrix<T>(this IEnumerable<IEnumerable<T>> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            var enumerables = source as IEnumerable<T>[] ?? source.ToArray();

            if (enumerables.Any(item => item is null))
                throw new ArgumentNullException(nameof(source) + ": one of enumerables was null");

            var columnsCounts = enumerables.Select(row => row.Count());
            var matrix = new JuggedMatrix<T>(enumerables.Length, columnsCounts);

            var rowIndex = 0;
            var columnIndex = 0;

            foreach (var row in enumerables)
            {
                foreach (var item in row)
                {
                    matrix[rowIndex, columnIndex] = item;
                    columnIndex++;
                }

                rowIndex++;
                columnIndex = 0;
            }

            return matrix;
        }

        //TODO: unit-test
        /// <summary>
        ///     Creates a new instance of <see cref="DynamicJuggedMatrix{T}"/> which contains elements from
        ///     <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DynamicJuggedMatrix<T> CreateDynamicJuggedMatrix<T>(this IEnumerable<IEnumerable<T>> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            var enumerables = source as IEnumerable<T>[] ?? source.ToArray();

            if (enumerables.Any(item => item is null))
                throw new ArgumentNullException(nameof(source) + ": one of enumerables was null");

            var columnsCounts = enumerables.Select(row => row.Count());
            var matrix = new DynamicJuggedMatrix<T>(enumerables.Length, columnsCounts);

            var rowIndex = 0;
            var columnIndex = 0;

            foreach (var row in enumerables)
            {
                foreach (var item in row)
                {
                    matrix[rowIndex, columnIndex] = item;
                    columnIndex++;
                }

                rowIndex++;
                columnIndex = 0;
            }

            return matrix;
        }

        #endregion

        #region To Vanilla

        //TODO: unit-test
        /// <summary>
        ///     Creates a vanilla array of arrays
        ///     <see>
        ///         <cref>T</cref>
        ///     </see>
        ///     [][]
        /// </summary>
        /// <returns>Vanilla array of arrays which represents initial matrix</returns>
        public static T[][] CreateVanilla<T>(this IJuggedMatrix<T> juggedMatrix)
        {
            if (juggedMatrix is null) throw new ArgumentNullException(nameof(juggedMatrix));

            var tempMatrix = new T[juggedMatrix.RowsCount][];

            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                tempMatrix[i] = new T[juggedMatrix.ElementsInRow(i)];

                for (var j = 0; j < juggedMatrix.ElementsInRow(i); j++)
                    tempMatrix[i][j] = juggedMatrix[i, j];
            }

            return tempMatrix;
        }

        //TODO: unit-test
        /// <summary>
        ///     Creates a vanilla matrix
        ///     <see>
        ///         <cref>T</cref>
        ///     </see>
        ///     [,]
        /// </summary>
        /// <returns>Vanilla matrix which represents initial matrix</returns>
        public static T[,] CreateVanilla<T>(this IMatrix<T> matrix)
        {
            if (matrix is null) throw new ArgumentNullException(nameof(matrix));

            var tempMatrix = new T[matrix.RowsCount, matrix.ColumnsCount];

            for (var i = 0; i < matrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++)
                    tempMatrix[i, j] = matrix[i, j];
            }

            return tempMatrix;
        }

        #endregion

        #region From Vanilla

        //TODO: unit-test
        /// <summary>
        ///     Converts a vanilla .NET matrix <see langword="T"/>[,] to <see cref="Matrix{T}"/>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="matrix">Initial matrix to convert</param>
        /// <returns><see cref="IMatrix{T}"/>, made on base <paramref name="matrix"/></returns>
        public static Matrix<T> ToMatrix<T>(this T[,] matrix)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));

            var newMatrix = new Matrix<T>(matrix.GetLength(0), matrix.GetLength(1));

            for (var i = 0; i < newMatrix.RowsCount; i++)
            {
                for (var j = 0; j < newMatrix.ColumnsCount; j++) newMatrix[i, j] = matrix[i, j];
            }

            return newMatrix;
        }

        //TODO: unit-test
        /// <summary>
        ///     Converts a vanilla .NET matrix <see langword="T"/>[,] to <see cref="DynamicMatrix{T}"/>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="matrix">Initial matrix to convert</param>
        /// <returns><see cref="DynamicMatrix{T}"/>, made on base <paramref name="matrix"/></returns>
        public static DynamicMatrix<T> ToDynamicMatrix<T>(this T[,] matrix)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));

            var newMatrix = new DynamicMatrix<T>(matrix.GetLength(0), matrix.GetLength(1));

            for (var i = 0; i < newMatrix.RowsCount; i++)
            {
                for (var j = 0; j < newMatrix.ColumnsCount; j++) newMatrix[i, j] = matrix[i, j];
            }

            return newMatrix;
        }

        //TODO: unit-test
        /// <summary>
        ///     Converts a vanilla .NET matrix <see langword="T"/>[][] to <see cref="JuggedMatrix{T}"/>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="matrix">Initial matrix to convert</param>
        /// <returns><see cref="JuggedMatrix{T}"/>, made on base <paramref name="matrix"/></returns>
        public static JuggedMatrix<T> ToJuggedMatrix<T>(this T[][] matrix)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));

            if (matrix.Any(x => x == null))
                throw new NullReferenceException(nameof(matrix) + " one or more rows are null");

            var newJuggedMatrix = new JuggedMatrix<T>(matrix.Length, matrix.Select(row => row.Length));

            for (var i = 0; i < matrix.Length; i++)
            {
                for (var j = 0; j < newJuggedMatrix.ElementsInRow(i); j++) newJuggedMatrix[i, j] = matrix[i][j];
            }

            return newJuggedMatrix;
        }

        //TODO: unit-test
        /// <summary>
        ///     Converts a vanilla .NET matrix <see langword="T"/>[][] to <see cref="DynamicJuggedMatrix{T}"/>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="matrix">Initial matrix to convert</param>
        /// <returns><see cref="JuggedMatrix{T}"/>, made on base <paramref name="matrix"/></returns>
        public static DynamicJuggedMatrix<T> ToDynamicJuggedMatrix<T>(this T[][] matrix)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));

            if (matrix.Any(x => x == null))
                throw new NullReferenceException(nameof(matrix) + " one or more rows are null");

            var newJuggedMatrix = new DynamicJuggedMatrix<T>(matrix.Length, matrix.Select(row => row.Length));

            for (var i = 0; i < matrix.Length; i++)
            {
                for (var j = 0; j < newJuggedMatrix.ElementsInRow(i); j++) newJuggedMatrix[i, j] = matrix[i][j];
            }

            return newJuggedMatrix;
        }

        #endregion

        #endregion
    }
}