using System;
using System.Collections;
using System.Text;
using MatrixBase;

namespace CyberMath.Matrix.Models
{
    public class Matrix<T> : IMatrix<T>
    {
        private bool _isDisposed = false;
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

        #region Public Methods

        #region Operations

        public IMatrixBase<T> Transpose()
        {
            int w = _innerMatrix.GetLength(0);
            int h = _innerMatrix.GetLength(1);

            Matrix<T> result = new Matrix<T>(h, w);

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = _innerMatrix[i, j];
                }
            }

            return result;
        }

        public void ProcessFunctionOverData(Action<int, int> func)
        {
            for (var i = 0; i < RowsCount; i++)
            {
                for (var j = 0; j < ColumnsCount; j++)
                {
                    func?.Invoke(i, j);
                }
            }
        }

        #endregion

        #region Extra Operations

        public bool Equals(Matrix<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(_innerMatrix, other._innerMatrix) &&
                   ColumnsCount == other.ColumnsCount &&
                   RowsCount == other.RowsCount;
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

        #region Dispose

        public void Dispose()
        {
            if (!_isDisposed)
            {
                GC.Collect();
                _isDisposed = true;
            }
        }

        #endregion

        #endregion

        #region Presentation

        public string GetAsString()
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

        public override string ToString() => GetAsString();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return _innerMatrix.GetEnumerator();
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

        #endregion
    }
}
