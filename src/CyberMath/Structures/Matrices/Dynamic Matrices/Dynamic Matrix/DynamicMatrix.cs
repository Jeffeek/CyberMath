#region Using namespaces

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyberMath.Helpers;
using CyberMath.Structures.Matrices.Matrix;

#endregion

namespace CyberMath.Structures.Matrices.Dynamic_Matrices.Dynamic_Matrix
{
    /// <summary>
    ///     Represents am implementation of <see cref="IDynamicMatrix{T}"/> -> <see cref="IMatrix{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class DynamicMatrix<T> : IDynamicMatrix<T>, ICloneable
    {
        private readonly List<List<T>> _innerMatrix;

        /// <inheritdoc/>
        public void ProcessFunctionOverData(Action<int, int> func)
        {
            if (ReferenceEquals(func, null)) return;

            for (var i = 0; i < RowsCount; i++)
            {
                for (var j = 0; j < ColumnsCount; j++)
                    func.Invoke(i, j);
            }
        }

        /// <inheritdoc/>
        public IMatrix<T> Transpose()
        {
            var result = new DynamicMatrix<T>(ColumnsCount, RowsCount);

            for (var i = 0; i < RowsCount; i++)
            {
                for (var j = 0; j < ColumnsCount; j++)
                    result[j, i] = this[i, j];
            }

            return result;
        }

        #region Overrides of Object

        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var i = 0; i < RowsCount; i++)
            {
                for (var j = 0; j < ColumnsCount; j++) sb.Append($"{this[i, j]} | ");

                sb.AppendLine();
            }

            return sb.ToString();
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Init matrix with <paramref name="rowsCount"/> and <paramref name="columnsCount"/> and sets a default values
        /// </summary>
        /// <param name="rowsCount">Count of rows in initial matrix</param>
        /// <param name="columnsCount">Count of columns in initial matrix</param>
        /// <exception cref="ArgumentException">If <paramref name="rowsCount"/> or <paramref name="columnsCount"/> less than 0</exception>
        public DynamicMatrix(int rowsCount, int columnsCount)
        {
            if (rowsCount < 0) throw new ArgumentException(nameof(rowsCount) + $" was {rowsCount}");
            if (columnsCount < 0) throw new ArgumentException(nameof(columnsCount) + $" was {columnsCount}");

            _innerMatrix = new List<List<T>>(rowsCount);

            for (var i = 0; i < rowsCount; i++)
            {
                _innerMatrix.Add(new List<T>(columnsCount));

                for (var j = 0; j < columnsCount; j++)
                    _innerMatrix[i]
                        .Add(default);
            }
        }

        /// <summary>
        ///     Init matrix with 0 rows and 0 columns
        /// </summary>
        public DynamicMatrix() : this(0, 0) { }

        #endregion

        #region Properties

        /// <inheritdoc/>
        public T this[int row, int column]
        {
            get => _innerMatrix[row][column];
            set => _innerMatrix[row][column] = value;
        }

        /// <inheritdoc/>
        public int ElementsInRow(int rowIndex) => ColumnsCount;

        /// <inheritdoc/>
        public int ColumnsCount =>
            _innerMatrix[0]
                ?.Count
            ?? 0;

        /// <inheritdoc/>
        public int RowsCount => _innerMatrix.Count;

        /// <inheritdoc/>
        public bool IsSquare => _innerMatrix.TrueForAll(row => row.Count == RowsCount);

        #endregion

        #region Implementation of ICloneable

        /// <summary>
        ///     Returns a new cloned object of initial matrix.
        ///     <remarks>
        ///         Works only with primitives and [Serializable] types
        ///     </remarks>
        /// </summary>
        /// <returns>New matrix&lt;T&gt;</returns>
        public object Clone()
        {
            var type = typeof(T);

            if (type.IsPrimitive) return PrimitiveClone();
            if (type.IsSerializable) return SerializableClone();

            throw new Exception("Internal type of matrix can't be cloned");
        }

        private IDynamicMatrix<T> PrimitiveClone()
        {
            var clone = new DynamicMatrix<T>(RowsCount, ColumnsCount);

            for (var i = 0; i < RowsCount; i++)
            {
                for (var j = 0;
                     j
                     < _innerMatrix[i]
                         .Count;
                     j++) clone[i, j] = this[i, j];
            }

            return clone;
        }

        private IDynamicMatrix<T> SerializableClone()
        {
            var clone = new DynamicMatrix<T>(RowsCount, ColumnsCount);

            for (var i = 0; i < RowsCount; i++)
            {
                for (var j = 0; j < ElementsInRow(i); j++)
                    clone[i, j] = this[i, j]
                        .SerializableDeepCopy();
            }

            return clone;
        }

        #endregion

        #region Enumeration

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private IEnumerable<T> ColumnEnumerator(int columnIndex)
        {
            for (var i = 0; i < RowsCount; i++)
                yield return this[i, columnIndex];
        }

        /// <inheritdoc/>
        public IEnumerable<IEnumerable<T>> GetColumnsEnumerable()
        {
            for (var i = 0; i < ColumnsCount; i++)
                yield return ColumnEnumerator(i);
        }

        /// <inheritdoc/>
        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            for (var i = 0; i < RowsCount; i++)
                yield return RowEnumerator(i);
        }

        /// <summary>
        ///     Enumerates all elements in a row
        /// </summary>
        /// <param name="rowIndex">Row to enumerate</param>
        /// <returns>
        ///     <see cref="IEnumerable{T}"/> elements in a <see cref="DynamicMatrix{T}"/> in <paramref name="rowIndex"/>
        /// </returns>
        private IEnumerable<T> RowEnumerator(int rowIndex)
        {
            for (var i = 0; i < ColumnsCount; i++)
                yield return this[rowIndex, i];
        }

        #endregion

        #region Adding

        /// <inheritdoc/>
        public void AddColumn(IEnumerable<T> column)
        {
            if (ReferenceEquals(column, null)) throw new ArgumentNullException(nameof(column));

            InsertColumn(ColumnsCount, column);
        }

        /// <inheritdoc/>
        public void AddRow(IEnumerable<T> row)
        {
            if (ReferenceEquals(row, null)) throw new NullReferenceException(nameof(row));

            var rowAsList = row.ToList();

            if (rowAsList.Count != ColumnsCount)
                throw new ArgumentException("Count of elements in a row should be the same");

            _innerMatrix.Add(rowAsList);
        }

        /// <inheritdoc/>
        public void InsertColumn(int index, IEnumerable<T> column)
        {
            if (ReferenceEquals(column, null)) throw new ArgumentNullException(nameof(column));

            var columnAsList = column.ToList();

            if (columnAsList.Count != RowsCount) throw new ArgumentException(nameof(index));

            for (var i = 0; i < RowsCount; i++)
                _innerMatrix[i]
                    .Insert(index, columnAsList[i]);
        }

        /// <inheritdoc/>
        public void InsertRow(int index, IEnumerable<T> row)
        {
            if (ReferenceEquals(row, null)) throw new ArgumentNullException(nameof(row));

            var rowAsList = row.ToList();

            if (rowAsList.Count != ColumnsCount) throw new ArgumentException(nameof(row));

            _innerMatrix.Insert(index, rowAsList);
        }

        #endregion

        #region Removing

        /// <inheritdoc/>
        public void RemoveColumn(int index)
        {
            if (index < 0 || index >= ColumnsCount) throw new ArgumentException(nameof(index));

            for (var i = 0; i < RowsCount; i++)
                _innerMatrix[i]
                    .RemoveAt(index);
        }

        /// <inheritdoc/>
        public void RemoveRow(int index)
        {
            if (index < 0 || index >= RowsCount) throw new ArgumentException(nameof(index));

            _innerMatrix.RemoveAt(index);
        }

        #endregion

        #region Equality members

        /// <inheritdoc/>
        public bool Equals(IMatrix<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (ColumnsCount != other.ColumnsCount) return false;
            if (RowsCount != other.RowsCount) return false;

            for (var i = 0; i < RowsCount; i++)
            {
                for (var j = 0; j < ColumnsCount; j++)
                    if (!this[i, j]
                            .Equals(other[i, j]))
                        return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj is IMatrix<T> matrix && Equals(matrix);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => ColumnsCount ^ (339 + RowsCount) ^ (339 + (IsSquare ? 1 : 0));

        #endregion
    }
}