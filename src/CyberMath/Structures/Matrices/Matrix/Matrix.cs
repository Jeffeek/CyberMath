using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CyberMath.Structures.Matrices.Base;

namespace CyberMath.Structures.Matrices.Matrix
{
	/// <summary>
    /// Implementation of <see cref="T:CyberMath.Structures.Matrices.Matrix.IMatrix`1" /> with Math-functional methods
    /// </summary>
    /// <typeparam name="T" />
    public sealed class Matrix<T> : IMatrix<T>, IEquatable<Matrix<T>>
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
                var hashCode = _innerMatrix != null ? _innerMatrix.GetHashCode() : 0;
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
        /// <inheritdoc/>
        /// </summary>
        public IMatrixBase<T> CreateMatrixWithoutColumn(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= ColumnsCount) throw new ArgumentException("invalid column index");
	        var result = new Matrix<T>(RowsCount, ColumnsCount - 1);
            result.ProcessFunctionOverData((i, j) =>
                result[i, j] = j < columnIndex ? this[i, j] : this[i, j + 1]);
            return result;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IMatrixBase<T> CreateMatrixWithoutRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= RowsCount) throw new ArgumentException("invalid row index");
            var result = new Matrix<T>(RowsCount - 1, ColumnsCount);
            result.ProcessFunctionOverData((i, j) =>
                result[i, j] = i < rowIndex ? this[i, j] : this[i + 1, j]);
            return result;
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

        /// <summary>
        /// Creates a vanilla matrix <see>
        ///     <cref>T</cref>
        /// </see>
        /// [,]
        /// </summary>
        /// <returns>Vanilla matrix which represents initial matrix</returns>
        public T[,] CreateVanilla()
        {
	        var matrix = new T[RowsCount, ColumnsCount];
            for (var i = 0; i < RowsCount; i++)
            {
	            for (var j = 0; j < ColumnsCount; j++)
		            matrix[i, j] = this[i, j];
            }

            return matrix;
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
