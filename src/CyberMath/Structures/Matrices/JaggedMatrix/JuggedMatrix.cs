using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyberMath.Helpers;
using CyberMath.Structures.Matrices.Extensions;

namespace CyberMath.Structures.Matrices.JaggedMatrix
{
    /// <summary>
    /// Describes a Jugged Matrix. Implements <see cref="IJuggedMatrix{T}"/>
    /// </summary>
    /// <typeparam name="T">ANY</typeparam>
    public class JuggedMatrix<T> : IJuggedMatrix<T>, IEquatable<JuggedMatrix<T>>
    {
        private protected T[][] _innerMatrix;
        /// <inheritdoc/>
        public int RowsCount { get; protected set; }
        /// <inheritdoc/>
        public bool IsSquare { get; protected set; }

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

        public JuggedMatrix(T[][] matrix)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));
            RowsCount = matrix.Length;
            _innerMatrix = matrix;
        }

        protected JuggedMatrix(int rowsCount)
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

        /// <inheritdoc/>
        public T this[int row, int column]
        {
            get => _innerMatrix[row][column];
            set => _innerMatrix[row][column] = value;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public int ElementsInRow(int rowIndex) => _innerMatrix[rowIndex].Length;

        /// <summary>
        /// Returns a new cloned object of initial matrix.
        /// <remarks>
        /// Works only with primitives and [Serializable] types
        /// </remarks>
        /// </summary>
        /// <returns>New matrix&lt;T&gt;</returns>
        public object Clone()
        {
	        var type = typeof(T);
	        if (type.IsPrimitive) return PrimitiveClone();
	        if (type.IsSerializable) return SerializableClone();
	        throw new Exception("Internal type of matrix can't be cloned");
        }

        private IJuggedMatrix<T> PrimitiveClone()
        {
	        var clone = new JuggedMatrix<T>(RowsCount, this.CountOnEachRow());
	        for (var i = 0; i < RowsCount; i++)
	        {
		        for (var j = 0; j < ElementsInRow(i); j++)
		        {
			        clone[i, j] = this[i, j];
		        }
	        }

	        return clone;
        }

        private IJuggedMatrix<T> SerializableClone()
        {
	        var clone = new JuggedMatrix<T>(RowsCount, this.CountOnEachRow());
	        for (var i = 0; i < RowsCount; i++)
	        {
		        for (var j = 0; j < ElementsInRow(i); j++)
		        {
			        clone[i, j] = this[i, j].SerializableDeepCopy();
		        }
	        }

	        return clone;
        }

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

        private IEnumerable<T> ColumnEnumerator(int columnIndex)
        {
	        for (var i = 0; i < RowsCount; i++)
		        yield return this[i, columnIndex];
        }

        /// <inheritdoc />
        public IEnumerable<IEnumerable<T>> GetColumnsEnumerable()
        {
	        for (var i = 0; i < ElementsInRow(i); i++)
		        yield return ColumnEnumerator(i);
        }

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
