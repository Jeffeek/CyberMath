using System.Collections.Generic;
using System.Linq;
using CyberMath.Structures.Matrices.JaggedMatrix;
using CyberMath.Structures.Matrices.Matrix;

namespace CyberMath.Structures.Matrices.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IMatrix{T}"/> and <see cref="IJuggedMatrix{T}"/> for their transformation
    /// </summary>
    public static class MatrixExtensions
    {
        //TODO: unit-test
        /// <summary>
        /// Returns an array with count of elements on each row in <see cref="IJuggedMatrix{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static IEnumerable<int> CountOnEachRow<T>(this IJuggedMatrix<T> matrix)
        {
            for (var i = 0; i < matrix.RowsCount; i++)
                yield return matrix.ElementsInRow(i);
        }

        //TODO: unit-test
        /// <summary>
        /// Creates a new instance of <see cref="IMatrix{T}"/> from <see cref="IJuggedMatrix{T}"/>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="juggedMatrix">Initial matrix</param>
        /// <returns>A new instance of <see cref="IMatrix{T}"/></returns>
        public static IMatrix<T> ToMatrix<T>(this IJuggedMatrix<T> juggedMatrix)
        {
            var matrix = new Matrix<T>(juggedMatrix.RowsCount, juggedMatrix.CountOnEachRow().Max());
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (juggedMatrix.ElementsInRow(i) < matrix.ColumnsCount)
                        matrix[i, j] = juggedMatrix[i, j];
                    else
                        matrix[i, j] = default;
                }
            }

            return matrix;
        }

        //TODO: unit-test
        /// <summary>
        /// Creates a new instance of <see cref="IJuggedMatrix{T}"/> from <see cref="IMatrix{T}"/>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="matrix">Initial matrix</param>
        /// <returns>A new instance of <see cref="IJuggedMatrix{T}"/></returns>
        public static IJuggedMatrix<T> ToJuggedMatrix<T>(this IMatrix<T> matrix)
        {
            var juggedMatrix = new JuggedMatrix<T>(matrix.RowsCount, Enumerable.Repeat(matrix.ColumnsCount, matrix.RowsCount).ToArray());
            for (var i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (var j = 0; j < matrix.ColumnsCount; j++)
                {
                    juggedMatrix[i, j] = matrix[i, j];
                }
            }

            return juggedMatrix;
        }
    }
}
