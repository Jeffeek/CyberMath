using CyberMath.Structures.JaggedMatrix;
using CyberMath.Structures.Matrix;
using System.Linq;

namespace CyberMath.Structures.MatrixExtensions
{
    public static class MatrixExtensions
    {
        public static int[] GetCountPerRow<T>(this IJuggedMatrix<T> matrix)
        {
            var elements = new int[matrix.RowsCount];
            for (int i = 0; i < elements.Length; i++)
                elements[i] = matrix.ElementsInRow(i);
            return elements;
        }

        public static IMatrix<T> ToMatrix<T>(this IJuggedMatrix<T> juggedMatrix)
        {
            var matrix = new Matrix<T>(juggedMatrix.RowsCount, juggedMatrix.GetCountPerRow().Max());
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    if (juggedMatrix.ElementsInRow(i) < matrix.ColumnsCount)
                        matrix[i, j] = juggedMatrix[i, j];
                    else
                        matrix[i, j] = default;
                }
            }

            return matrix;
        }

        public static IJuggedMatrix<T> ToJuggedMatrix<T>(this IMatrix<T> matrix)
        {
            var juggedMatrix = new JuggedMatrix<T>(matrix.RowsCount, Enumerable.Repeat(matrix.ColumnsCount, matrix.RowsCount).ToArray());
            for (int i = 0; i < juggedMatrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    juggedMatrix[i, j] = matrix[i, j];
                }
            }

            return juggedMatrix;
        }
    }
}
