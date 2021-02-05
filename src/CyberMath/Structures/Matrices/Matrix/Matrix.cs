using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CyberMath.Helpers;

namespace CyberMath.Structures.Matrices.Matrix
{
	/// <summary>
    /// Implementation of <see cref="T:CyberMath.Structures.Matrices.Matrix.IMatrix`1" /> with Math-functional methods
    /// </summary>
    /// <typeparam name="T" />
    public class Matrix<T> : IMatrix<T>, ICloneable
    {
        /// <summary>
        /// Internal matrix needed to implement the class <see cref="Matrix{T}"/>
        /// </summary>
        private readonly T[,] _innerMatrix;
        /// <inheritdoc/>
        public int ColumnsCount { get; }
        /// <inheritdoc/>
        public int RowsCount { get; }
        /// <inheritdoc/>
        public bool IsSquare => RowsCount == ColumnsCount;

        /// <inheritdoc/>
        public T this[int row, int column]
        {
            get => _innerMatrix[row, column];
            set => _innerMatrix[row, column] = value;
        }

        /// <summary>
        /// Initializes a new matrix object with count of rows = <paramref name="rowsCount"/>
        /// and count of columns = <paramref name="columnsCount"/>
        /// </summary>
        /// <param name="rowsCount">Count of rows in matrix</param>
        /// <param name="columnsCount">Count of columns in matrix</param>
        public Matrix(int rowsCount, int columnsCount)
        {
	        if (rowsCount <= 0) throw new ArgumentException(nameof(rowsCount));
	        if (columnsCount <= 0) throw new ArgumentException(nameof(columnsCount));
	        ColumnsCount = columnsCount;
            RowsCount = rowsCount;
            _innerMatrix = new T[rowsCount, columnsCount];
        }

        /// <summary>
        /// Creates an empty matrix with 0 rows and 0 columns
        /// </summary>
        protected Matrix()
        {
            ColumnsCount = 0;
            RowsCount = 0;
            _innerMatrix = new T[0, 0];
        }

        /// <summary>
        /// Initializes a new matrix object with the help of <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix">Matrix for init initial <see cref="Matrix{T}"/></param>
        public Matrix(T[,] matrix)
        {
	        if (matrix == null) throw new ArgumentNullException(nameof(matrix));
	        ColumnsCount = matrix.GetLength(1);
	        RowsCount = matrix.GetLength(0);
	        _innerMatrix = matrix;
        }

        #region Operations

        /// <summary>
        /// Returns a new transposed matrix of initial one
        /// </summary>
        /// <returns><see cref="IMatrix{T}"/> matrix</returns>
        public IMatrix<T> Transpose()
        {
            var result = new Matrix<T>(ColumnsCount, RowsCount);

            for (var i = 0; i < RowsCount; i++)
            {
                for (var j = 0; j < ColumnsCount; j++)
                {
                    result[j, i] = _innerMatrix[i, j];
                }
            }

            return result;
        }

        ///<inheritdoc/>
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

        ///<inheritdoc/>
        public bool Equals(IMatrix<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (RowsCount != other.RowsCount) return false;
            if (ColumnsCount != other.ColumnsCount) return false;
            for (var i = 0; i < RowsCount; i++)
            {
                for (var j = 0; j < ColumnsCount; j++)
                {
                    if (!this[i, j].Equals(other[i, j]))
                        return false;
                }
            }

            return true;
        }

        ///<inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is IMatrix<T> matrix && Equals(matrix);
        }

        ///<inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _innerMatrix != null ? _innerMatrix.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ ColumnsCount;
                hashCode = (hashCode * 397) ^ RowsCount;
                return hashCode;
            }
        }

        #endregion

        #region Presentation

        ///<inheritdoc/>
        public int ElementsInRow(int rowIndex) => ColumnsCount;

        ///<inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < RowsCount; i++)
            {
                for (var j = 0; j < ColumnsCount; j++)
                {
                    sb.Append($"{this[i, j]} | ");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        #endregion

        #region Creation

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

        private IMatrix<T> PrimitiveClone()
        {
	        var clone = new Matrix<T>(RowsCount, ColumnsCount);
            for (var i = 0; i < RowsCount; i++)
            {
	            for (var j = 0; j < ColumnsCount; j++)
	            {
		            clone[i, j] = this[i, j];
	            }
            }

            return clone;
        }

        private IMatrix<T> SerializableClone()
        {
	        var clone = new Matrix<T>(RowsCount, ColumnsCount);
	        for (var i = 0; i < RowsCount; i++)
	        {
		        for (var j = 0; j < ColumnsCount; j++)
		        {
			        clone[i, j] = this[i, j].SerializableDeepCopy();
		        }
	        }

	        return clone;
        }

        /// <summary>
        /// Creates new <see cref="IMatrix{T}"/> identity matrix.
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
        /// <returns>Identity <see cref="IMatrix{T}"/> matrix</returns>
        public static IMatrix<int> CreateIdentityMatrix(int rowsAndColumnsCount)
        {
            var result = new Matrix<int>(rowsAndColumnsCount, rowsAndColumnsCount);
            for (var i = 0; i < rowsAndColumnsCount; i++)
                result[i, i] = 1;
            return result;
        }

        #endregion

        #region Enumeration

        /// <inheritdoc />
        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            for (var i = 0; i < RowsCount; i++)
                yield return RowEnumerator(i);
        }

        /// <summary>
        /// Enumerates all elements in a row
        /// </summary>
        /// <param name="rowIndex">Row to enumerate</param>
        /// <returns><see cref="IEnumerable{T}"/> elements in a <see cref="Matrix{T}"/> in <paramref name="rowIndex"/></returns>
        private IEnumerable<T> RowEnumerator(int rowIndex)
        {
            for (var i = 0; i < ColumnsCount; i++)
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
	        for (var i = 0; i < ColumnsCount; i++)
		        yield return ColumnEnumerator(i);
        }
        #endregion
    }
}
