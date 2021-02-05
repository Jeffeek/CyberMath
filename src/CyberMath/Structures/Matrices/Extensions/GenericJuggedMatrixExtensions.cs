using System;
using System.Linq;
using CyberMath.Structures.Matrices.Jagged_Matrix;

namespace CyberMath.Structures.Matrices.Extensions
{
    /// <summary>
    /// Extension methods only for <see cref="IJuggedMatrix{T}"/>
    /// </summary>
    public static class GenericJuggedMatrixExtensions
    {
        #region Removing Rows and Columns

        /// <summary>
        /// Creates a new <see cref="IJuggedMatrix{T}"/> matrix without row at <paramref name="rowIndex"/>
        /// </summary>
        /// <param name="juggedMatrix"></param>
        /// <param name="rowIndex">Column index to remove</param>
        /// <returns>A new <see cref="IJuggedMatrix{T}"/> matrix without row at <paramref name="rowIndex"/></returns>
        public static IJuggedMatrix<T> CreateMatrixWithoutRow<T>(this IJuggedMatrix<T> juggedMatrix, int rowIndex)
        {
	        if (ReferenceEquals(juggedMatrix, null)) throw new ArgumentNullException(nameof(juggedMatrix));
	        if (rowIndex < 0) throw new ArgumentException("Row index is < 0");
	        if (rowIndex >= juggedMatrix.RowsCount) throw new ArgumentException("Row index is out of range in matrix");
	        var tempMatrix = new T[juggedMatrix.RowsCount - 1][];
	        var currentRow = 0;
	        for (var i = 0; i < juggedMatrix.RowsCount; i++)
	        {
		        if (i == rowIndex) continue;
		        var elementsInRow = juggedMatrix.ElementsInRow(i);
		        tempMatrix[currentRow] = new T[elementsInRow];
		        for (var j = 0; j < elementsInRow; j++)
			        tempMatrix[currentRow][j] = juggedMatrix[i, j];
		        currentRow++;
	        }

	        var newMatrix = new JuggedMatrix<T>(tempMatrix);
	        return newMatrix;
        }

        /// <summary>
        /// Creates a new <see cref="IJuggedMatrix{T}"/> matrix without column at <paramref name="columnIndex"/>
        /// </summary>
        /// <param name="juggedMatrix"></param>
        /// <param name="columnIndex">Column index to remove</param>
        /// <returns>A new <see cref="IJuggedMatrix{T}"/> matrix without column at <paramref name="columnIndex"/></returns>
        public static IJuggedMatrix<T> CreateMatrixWithoutColumn<T>(this IJuggedMatrix<T> juggedMatrix, int columnIndex)
        {
	        if (ReferenceEquals(juggedMatrix, null)) throw new ArgumentNullException(nameof(juggedMatrix));
			var maxColumn = juggedMatrix.Max(x => x.Count());
	        if (columnIndex < 0) throw new ArgumentException("Column index is < 0");
	        if (columnIndex >= maxColumn) throw new ArgumentException("Column index is out of range in matrix");
			var tempMatrix = new T[juggedMatrix.RowsCount][];
	        for (var i = 0; i < juggedMatrix.RowsCount; i++)
	        {
		        var currentColumn = 0;
		        var elementsInRow = juggedMatrix.ElementsInRow(i);
		        if (columnIndex < elementsInRow)
			        tempMatrix[i] = new T[elementsInRow - 1];
		        else
			        tempMatrix[i] = new T[elementsInRow];
		        for (var j = 0; j < elementsInRow; j++)
		        {
			        if (j == columnIndex) continue;
			        tempMatrix[i][currentColumn] = juggedMatrix[i, j];
			        currentColumn++;
		        }
	        }

	        var newMatrix = new JuggedMatrix<T>(tempMatrix);
	        return newMatrix;
        }

        #endregion
    }
}
