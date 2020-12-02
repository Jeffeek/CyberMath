using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            GC.SuppressFinalize(this);
            GC.Collect();
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

        public IMatrixBase<T> Transpose()
        {
            var newmat = Enumerable
                                    .Range(0, _innerMatrix[0].Length - 1)
                                    .Select(i => _innerMatrix.Select(r => r[i]).ToArray()).ToArray();
            IJuggedMatrix<T> transposed = new JuggedMatrix<T>(newmat.GetLength(0), newmat.Select(x => x.Length).ToArray());
            return transposed;
        }

        public void ProcessFunctionOverData(Action<int, int> func)
        {
            if (ReferenceEquals(func, null)) return;
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ElementsInRow(i); j++)
                {
                    func?.Invoke(i, j);
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

        public IMatrixBase<T> CreateMatrixWithoutColumn(int columnIndex) => throw new NotImplementedException();

        public IMatrixBase<T> CreateMatrixWithoutRow(int rowIndex) => throw new NotImplementedException();

        public int ElementsInRow(int index) => _innerMatrix[index].Length;

        public IJuggedMatrix<T> SortRows()
        {
            var orderedmMatrix = _innerMatrix.OrderBy(x => x.Length).ToArray();
            var matrix = new JuggedMatrix<T>(orderedmMatrix.GetLength(0), orderedmMatrix.Select(x => x.Length).ToArray());
            return matrix;
        }

        public IJuggedMatrix<T> SortRowsByDescending()
        {
            var orderedmMatrix = _innerMatrix.OrderByDescending(x => x.Length).ToArray();
            var matrix = new JuggedMatrix<T>(orderedmMatrix.GetLength(0), orderedmMatrix.Select(x => x.Length).ToArray());
            return matrix;
        }
    }
}
