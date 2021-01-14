using CyberMath.Structures.MatrixBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyberMath.Structures.JaggedMatrix
{
    /// <summary>
    /// Describes a Jugged Matrix. Implements <see cref="IJuggedMatrix{T}"/>
    /// </summary>
    /// <typeparam name="T">ANY</typeparam>
    public class JuggedMatrix<T> : IJuggedMatrix<T>
    {
        private T[][] _innerMatrix;
        public int RowsCount { get; }
        public bool IsSquare { get; }

        #region Constructors

        public JuggedMatrix(int rowsCount, params int[] elementsAtRow)
        {
            if (elementsAtRow.Length != rowsCount) throw new ArgumentException("Count of Elements in row should be the same length as RowsCount");
            RowsCount = rowsCount;
            _innerMatrix = new T[rowsCount][];
            InitMatrix(elementsAtRow);
            if (elementsAtRow.All(x => x == RowsCount))
                IsSquare = true;
        }

        private JuggedMatrix(int rowsCount)
        {
            RowsCount = rowsCount;
            _innerMatrix = new T[RowsCount][];
        }

        #endregion

        #region Matrix Init

        private void InitMatrix(params int[] elementsAtRow)
        {
            for (var i = 0; i < RowsCount; i++)
                _innerMatrix[i] = new T[elementsAtRow[i]];
        }

        #endregion

        public T this[int row, int column]
        {
            get => _innerMatrix[row][column];
            set => _innerMatrix[row][column] = value;
        }

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

        public int ElementsInRow(int rowIndex) => _innerMatrix[rowIndex].Length;

        #region Matrix Creation 

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

        #endregion

        #region Row Sorting

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

        #region Enumeration

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < RowsCount; i++)
                for (var j = 0; j < ElementsInRow(i); j++)
                    yield return this[i, j];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

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
    }
}
