using System;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using CyberMath.Structures.Matrix.MatrixBase;

namespace CyberMath.Structures.Matrix.JaggedMatrix.Models
{
    public class JuggedMatrix<T> : IJuggedMatrix<T>
    {
        private T[][] _innerMatrix;
        public int RowsCount { get; }
        public bool IsSquare { get; }

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

        private void InitMatrix(params int[] elementsAtRow)
        {
            for (int i = 0; i < RowsCount; i++)
            {
                _innerMatrix[i] = new T[elementsAtRow[i]];
            }
        }

        public T this[int row, int column]
        {
            get => _innerMatrix[row][column];
            set => _innerMatrix[row][column] = value;
        }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ElementsInRow(i); j++)
                {
                    yield return this[i, j];
                }
                if (i == RowsCount - 1)
                    yield break;
            }
        }

        public void ProcessFunctionOverData(Action<int, int> func)
        {
            if (ReferenceEquals(func, null)) return;
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ElementsInRow(i); j++)
                {
                    func.Invoke(i, j);
                }
            }
        }

        public string GetAsString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ElementsInRow(i); j++)
                {
                    sb.Append($"{this[i, j]} | ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public IMatrixBase<T> CreateMatrixWithoutColumn(int columnIndex)
        {
            int maxColumn = _innerMatrix.Max(x => x.Length);
            if (columnIndex < 0) throw new ArgumentException("Column index is < 0");
            if (columnIndex >= maxColumn) throw new ArgumentException("Column index is out of range in matrix");
            JuggedMatrix<T> newMatrix = new JuggedMatrix<T>(RowsCount);
            for (int i = 0; i < RowsCount; i++)
            {
                int currentColumn = 0;
                int elementsInRow = ElementsInRow(i);
                if (columnIndex < elementsInRow)
                    newMatrix._innerMatrix[i] = new T[elementsInRow-1];
                else
                    newMatrix._innerMatrix[i] = new T[elementsInRow];
                for (int j = 0; j < elementsInRow; j++)
                {
                    if (j == columnIndex)
                        continue;
                    else
                    {
                        newMatrix[i, currentColumn] = this[i, j];
                        currentColumn++;
                    }
                }
            }

            return newMatrix;
        }

        public IMatrixBase<T> CreateMatrixWithoutRow(int rowIndex)
        {
            if (rowIndex < 0) throw new ArgumentException("Row index is < 0");
            if (rowIndex >= RowsCount) throw new ArgumentException("Row index is out of range in matrix");
            var newMatrix = new JuggedMatrix<T>(RowsCount - 1);
            int currentRow = 0;
            for (int i = 0; i < RowsCount; i++)
            {
                if (i != rowIndex)
                {
                    newMatrix._innerMatrix[currentRow] = _innerMatrix[i];
                    currentRow++;
                }
            }

            return newMatrix;
        }

        public int ElementsInRow(int index) => _innerMatrix[index].Length;

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
    }
}
