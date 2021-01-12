using System;

namespace CyberMath.Structures.MatrixBase
{
    public static class MatrixBaseExtensions
    {
        public static bool IsMaxInColumn<T>(this IMatrixBase<T> matrix, int i, int j) where T : IComparable
        {
            for (int k = 0; k < matrix.RowsCount; k++)
            {
                if (matrix[k, j].CompareTo(matrix[i, j]) == 1)
                    return false;
            }
            return true;
        }

        public static bool IsMinInRow<T>(this IMatrixBase<T> matrix, int i, int j) where T : IComparable
        {
            for (int k = 0; k < matrix.ElementsInRow(i); k++)
            {
                if (matrix[i, k].CompareTo(matrix[i, j]) == -1)
                    return false;
            }
            return true;
        }
    }
}
