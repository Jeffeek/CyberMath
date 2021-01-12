using CyberMath.Structures.MatrixBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CyberMath.Structures.Matrix
{
    //TODO: summary
    public class Matrix<T> : IMatrix<T>
    {
        private readonly T[,] _innerMatrix;
        public int ColumnsCount { get; }
        public int RowsCount { get; }
        public bool IsSquare => RowsCount == ColumnsCount;

        public T this[int row, int column]
        {
            get => _innerMatrix[row, column];
            set => _innerMatrix[row, column] = value;
        }

        public Matrix(int rowsCount, int columnsCount)
        {
            ColumnsCount = columnsCount;
            RowsCount = rowsCount;
            _innerMatrix = new T[rowsCount, columnsCount];
        }

        #region Operations

        public IMatrix<T> Transpose()
        {
            Matrix<T> result = new Matrix<T>(ColumnsCount, RowsCount);

            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    result[j, i] = _innerMatrix[i, j];
                }
            }

            return result;
        }

        public void ProcessFunctionOverData(Action<int, int> func)
        {
            if (ReferenceEquals(func, null)) return;
            for (var i = 0; i < RowsCount; i++)
            {
                for (var j = 0; j < ColumnsCount; j++)
                {
                    func.Invoke(i, j);
                }
            }
        }

        #endregion

        #region Extra Operations

        public bool Equals(Matrix<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (RowsCount != other.RowsCount) return false;
            if (ColumnsCount != other.ColumnsCount) return false;
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    if (!this[i, j].Equals(other[i, j]))
                        return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Matrix<T>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_innerMatrix != null ? _innerMatrix.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ColumnsCount;
                hashCode = (hashCode * 397) ^ RowsCount;
                return hashCode;
            }
        }

        #endregion

        #region Presentation

        public int ElementsInRow(int rowIndex) => ColumnsCount;

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    sb.Append($"{this[i, j]} | ");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        #endregion

        #region Creation

        public IMatrixBase<T> CreateMatrixWithoutColumn(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= ColumnsCount)
            {
                throw new ArgumentException("invalid column index");
            }
            var result = new Matrix<T>(RowsCount, ColumnsCount - 1);
            result.ProcessFunctionOverData((i, j) =>
                result[i, j] = j < columnIndex ? this[i, j] : this[i, j + 1]);
            return result;
        }

        public IMatrixBase<T> CreateMatrixWithoutRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= RowsCount)
            {
                throw new ArgumentException("invalid row index");
            }
            var result = new Matrix<T>(RowsCount - 1, ColumnsCount);
            result.ProcessFunctionOverData((i, j) =>
                result[i, j] = i < rowIndex ? this[i, j] : this[i + 1, j]);
            return result;
        }

        #endregion

        #region Enumeration

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < RowsCount; i++)
                for (int j = 0; j < ColumnsCount; j++)
                    yield return this[i, j];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
