using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyberMath.Structures.Matrices.Base;

namespace CyberMath.Structures.Matrices.JaggedMatrix
{
    /// <summary>
    /// Describes a Jugged Matrix. Implements <see cref="IJuggedMatrix{T}"/>
    /// </summary>
    /// <typeparam name="T">ANY</typeparam>
    public class JuggedMatrix<T> : IJuggedMatrix<T>, IEquatable<JuggedMatrix<T>>
    {
        private T[][] _innerMatrix;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int RowsCount { get; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsSquare { get; }

        #region Constructors

        /// <summary>
        /// Initializes a new matrix object with count of rows = <paramref name="rowsCount"/>
        /// and count of columns on each row => <paramref name="elementsAtRow"/>
        /// </summary>
        /// <param name="rowsCount"></param>
        /// <param name="elementsAtRow"></param>
        /// <exception cref="ArgumentException"></exception>
        public JuggedMatrix(int rowsCount, params int[] elementsAtRow)
        {
            if (elementsAtRow.Length != rowsCount) throw new ArgumentException("Count of Elements in row should be the same length as RowsCount");
            RowsCount = rowsCount;
            _innerMatrix = new T[rowsCount][];
            InitMatrix(elementsAtRow);
            if (elementsAtRow.All(x => x == RowsCount))
                IsSquare = true;
        }


        /// <summary>
        /// Initializes a new matrix object with count of rows = <paramref name="rowsCount"/>
        /// and count of columns on each row => <paramref name="elementsAtRow"/>
        /// </summary>
        /// <param name="rowsCount"></param>
        /// <param name="elementsAtRow"></param>
        /// <exception cref="ArgumentException"></exception>
        public JuggedMatrix(int rowsCount, IEnumerable<int> elementsAtRow)
        {
	        var elementsCount = elementsAtRow as int[] ?? elementsAtRow.ToArray();
	        if (elementsCount.Length != rowsCount) throw new ArgumentException("Count of Elements in row should be the same length as RowsCount");
            RowsCount = rowsCount;
            _innerMatrix = new T[rowsCount][];
            InitMatrix(elementsCount.ToArray());
            if (elementsCount.All(x => x == RowsCount))
                IsSquare = true;
        }

        private JuggedMatrix(int rowsCount)
        {
            RowsCount = rowsCount;
            _innerMatrix = new T[RowsCount][];
        }

        #endregion

        #region Matrix Init

        private void InitMatrix(IReadOnlyList<int> elementsAtRow)
        {
            for (var i = 0; i < RowsCount; i++)
                _innerMatrix[i] = new T[elementsAtRow[i]];
        }

        #endregion

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public T this[int row, int column]
        {
            get => _innerMatrix[row][column];
            set => _innerMatrix[row][column] = value;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void ProcessFunctionOverData(Action<int, int> func)
        {
            if (ReferenceEquals(func, null)) return;
            for (var i = 0; i < RowsCount; i++)
            {
                for (var j = 0; j < ElementsInRow(i); j++)
                {
                    func.Invoke(i, j);
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int ElementsInRow(int rowIndex) => _innerMatrix[rowIndex].Length;

        #region Matrix Creation 

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IMatrixBase<T> CreateMatrixWithoutColumn(int columnIndex)
        {
            var maxColumn = _innerMatrix.Max(x => x.Length);
            if (columnIndex < 0) throw new ArgumentException("Column index is < 0");
            if (columnIndex >= maxColumn) throw new ArgumentException("Column index is out of range in matrix");
            var newMatrix = new JuggedMatrix<T>(RowsCount);
            for (var i = 0; i < RowsCount; i++)
            {
                var currentColumn = 0;
                var elementsInRow = ElementsInRow(i);
                if (columnIndex < elementsInRow)
                    newMatrix._innerMatrix[i] = new T[elementsInRow - 1];
                else
                    newMatrix._innerMatrix[i] = new T[elementsInRow];
                for (var j = 0; j < elementsInRow; j++)
                {
                    if (j == columnIndex)
                        continue;
                    newMatrix[i, currentColumn] = this[i, j];
                    currentColumn++;
                }
            }

            return newMatrix;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IMatrixBase<T> CreateMatrixWithoutRow(int rowIndex)
        {
            if (rowIndex < 0) throw new ArgumentException("Row index is < 0");
            if (rowIndex >= RowsCount) throw new ArgumentException("Row index is out of range in matrix");
            var newMatrix = new JuggedMatrix<T>(RowsCount - 1);
            var currentRow = 0;
            for (var i = 0; i < RowsCount; i++)
            {
                if (i != rowIndex)
                {
                    var elementsInRow = ElementsInRow(i);
                    newMatrix._innerMatrix[currentRow] = new T[elementsInRow];
                    for (var j = 0; j < elementsInRow; j++)
                        newMatrix[currentRow, j] = this[i, j];
                    currentRow++;
                }
            }

            return newMatrix;
        }

        /// <summary>
        /// Creates a vanilla array of arrays <see>
        ///     <cref>T</cref>
        /// </see>
        /// [][]
        /// </summary>
        /// <returns>Vanilla array of arrays which represents initial matrix</returns>
        public T[][] CreateVanilla()
        {
	        var matrix = new T[RowsCount][];
	        for (var i = 0; i < RowsCount; i++)
	        {
		        matrix[i] = new T[ElementsInRow(i)];
		        for (var j = 0; j < ElementsInRow(i); j++)
			        matrix[i][j] = this[i, j];
	        }

	        return matrix;
        }

        #endregion

        #region Row Sorting

        /// <inheritdoc />
        public IJuggedMatrix<T> SortRows()
        {
            var orderedMatrix = _innerMatrix.OrderBy(x => x.Length).ToArray();
            var matrix = new JuggedMatrix<T>(orderedMatrix.GetLength(0),
                orderedMatrix.Select(x => x.Length).ToArray())
            {
                _innerMatrix = orderedMatrix
            };
            return matrix;
        }

        /// <inheritdoc />
        public IJuggedMatrix<T> SortRowsByDescending()
        {
            var orderedMatrix = _innerMatrix.OrderByDescending(x => x.Length).ToArray();
            var matrix = new JuggedMatrix<T>(orderedMatrix.GetLength(0),
                orderedMatrix.Select(x => x.Length).ToArray())
            {
                _innerMatrix = orderedMatrix
            };
            return matrix;
        }

        #endregion

        /// <summary>
        /// Creates new <see cref="IJuggedMatrix{T}"/> identity matrix.
        /// <para></para>
        /// <example>
        /// n = 3
        /// <para/>
        /// matrix = <para/>
        /// {<para/>
        ///     {1,0,0},<para/>
        ///     {0,1,0},<para/>
        ///     {0,0,1}<para/>
        /// }
        /// </example>
        /// </summary>
        /// <param name="rowsAndColumnsCount">Count of rows and columns</param>
        /// <returns>Identity <see cref="IJuggedMatrix{T}"/> matrix</returns>
        public static IJuggedMatrix<int> CreateIdentityMatrix(int rowsAndColumnsCount)
        {
            var result = new JuggedMatrix<int>(rowsAndColumnsCount, Enumerable.Repeat(rowsAndColumnsCount, rowsAndColumnsCount));
            for (var i = 0; i < rowsAndColumnsCount; i++)
                result[i, i] = 1;
            return result;
        }

        #region Enumeration

        /// <inheritdoc />
        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            for (var i = 0; i < RowsCount; i++)
                yield return RowEnumerator(i);
        }

        private IEnumerable<T> RowEnumerator(int rowIndex)
        {
            for (var i = 0; i < ElementsInRow(rowIndex); i++)
                yield return this[rowIndex, i];
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < RowsCount; i++)
            {
                for (var j = 0; j < ElementsInRow(i); j++)
                {
                    sb.Append($"{this[i, j]} | ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        #region Equality members

        /// <inheritdoc />
        public bool Equals(JuggedMatrix<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (RowsCount != other.RowsCount) return false;
            for (var i = 0; i < RowsCount; i++)
            {
                for (var j = 0; j < ElementsInRow(i); j++)
                {
                    if (!this[i, j].Equals(other[i, j]))
                        return false;
                }
            }

            return true;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((JuggedMatrix<T>)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(_innerMatrix, RowsCount, IsSquare);

        #endregion
    }
}
