#region Using derectives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CyberMath.Helpers;
using CyberMath.Structures.Matrices.Extensions;
using CyberMath.Structures.Matrices.Jagged_Matrix;

#endregion

namespace CyberMath.Structures.Matrices.Dynamic_Matrices.Dynamic_Jugged_Matrix
{
	/// <summary>
	///     Represents an implementation of <see cref="IDynamicJuggedMatrix{T}" /> -> <see cref="IJuggedMatrix{T}" />
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DynamicJuggedMatrix<T> : IDynamicJuggedMatrix<T>, ICloneable
	{
		private readonly List<List<T>> _innerMatrix;

		#region Overrides of Object

		/// <inheritdoc />
		public override string ToString()
		{
			var sb = new StringBuilder();
			for (var i = 0; i < RowsCount; i++)
			{
				for (var j = 0; j < _innerMatrix[i].Count; j++) sb.Append($"{this[i, j]} | ");

				sb.AppendLine();
			}

			return sb.ToString();
		}

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new matrix object with count of rows = <paramref name="rowsCount" />
		///     and count of columns on each row => <paramref name="elementsAtRow" />
		/// </summary>
		/// <param name="rowsCount"></param>
		/// <param name="elementsAtRow"></param>
		/// <exception cref="ArgumentException"></exception>
		public DynamicJuggedMatrix(int rowsCount, params int[] elementsAtRow) :
			this(rowsCount, elementsAtRow.AsEnumerable()) { }

		/// <summary>
		///     Initializes a new matrix object with count of rows = <paramref name="rowsCount" />
		///     and count of columns on each row => <paramref name="elementsAtRow" />
		/// </summary>
		/// <param name="rowsCount"></param>
		/// <param name="elementsAtRow"></param>
		/// <exception cref="ArgumentException"></exception>
		public DynamicJuggedMatrix(int rowsCount, IEnumerable<int> elementsAtRow)
		{
			if (rowsCount < 0) throw new ArgumentException(nameof(rowsCount));
			if (ReferenceEquals(elementsAtRow, null)) throw new ArgumentNullException(nameof(elementsAtRow));
			_innerMatrix = new List<List<T>>(rowsCount);
			var iterator = 0;
			foreach (var itemsInRow in elementsAtRow)
			{
				_innerMatrix.Add(new List<T>());
				for (var i = 0; i < itemsInRow; i++)
					_innerMatrix[iterator].Add(default);

				iterator++;
			}
		}

		/// <summary>
		///     Creates an instance of <see cref="DynamicJuggedMatrix{T}" /> with 0 rows and 0 columns
		/// </summary>
		public DynamicJuggedMatrix() : this(0, 0) { }

		#endregion

		#region Implementation of IEnumerable

		/// <summary>
		///     Enumerates all elements in a row
		/// </summary>
		/// <param name="rowIndex">Row to enumerate</param>
		/// <returns>
		///     <see cref="IEnumerable{T}" /> elements in a <see cref="DynamicJuggedMatrix{T}" /> in
		///     <paramref name="rowIndex" />
		/// </returns>
		private IEnumerable<T> RowEnumerator(int rowIndex)
		{
			for (var i = 0; i < _innerMatrix[rowIndex].Count; i++)
				yield return this[rowIndex, i];
		}

		/// <inheritdoc />
		public IEnumerable<IEnumerable<T>> GetColumnsEnumerable()
		{
			for (var i = 0; i < _innerMatrix[i].Count; i++)
				yield return ColumnEnumerator(i);
		}

		/// <inheritdoc />
		public IEnumerator<IEnumerable<T>> GetEnumerator()
		{
			for (var i = 0; i < RowsCount; i++)
				yield return RowEnumerator(i);
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		private IEnumerable<T> ColumnEnumerator(int columnIndex)
		{
			for (var i = 0; i < RowsCount; i++)
				yield return this[i, columnIndex];
		}

		#endregion

		#region Implementation of IMatrixBase<T>

		/// <inheritdoc />
		public int RowsCount => _innerMatrix.Count;

		/// <inheritdoc />
		public bool IsSquare => _innerMatrix.TrueForAll(row => row.Count == RowsCount);

		/// <inheritdoc />
		public T this[int row, int column]
		{
			get => _innerMatrix[row][column];
			set => _innerMatrix[row][column] = value;
		}

		/// <inheritdoc />
		public void ProcessFunctionOverData(Action<int, int> func)
		{
			if (ReferenceEquals(func, null)) return;
			for (var i = 0; i < RowsCount; i++)
			{
				for (var j = 0; j < _innerMatrix[i].Count; j++)
					func.Invoke(i, j);
			}
		}

		/// <inheritdoc />
		public int ElementsInRow(int rowIndex) => _innerMatrix[rowIndex].Count;

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

		private IJuggedMatrix<T> PrimitiveClone()
		{
			var clone = new DynamicJuggedMatrix<T>(RowsCount, this.CountOnEachRow());
			for (var i = 0; i < RowsCount; i++)
			{
				for (var j = 0; j < _innerMatrix[i].Count; j++) clone[i, j] = this[i, j];
			}

			return clone;
		}

		private IJuggedMatrix<T> SerializableClone()
		{
			var clone = new JuggedMatrix<T>(RowsCount, this.CountOnEachRow());
			for (var i = 0; i < RowsCount; i++)
			{
				for (var j = 0; j < ElementsInRow(i); j++) clone[i, j] = this[i, j].SerializableDeepCopy();
			}

			return clone;
		}

		#endregion

		#region Implementation of IJuggedMatrix<T>

		/// <inheritdoc />
		public IJuggedMatrix<T> SortRows() =>
			_innerMatrix.OrderBy(row => row.Count).AsEnumerable().CreateJuggedMatrix();

		/// <inheritdoc />
		public IJuggedMatrix<T> SortRowsByDescending() =>
			_innerMatrix.OrderByDescending(row => row.Count).AsEnumerable().CreateJuggedMatrix();

		#endregion

		#region Implementation of IDynamicJuggedMatrix<T>

		/// <inheritdoc />
		public void AddColumn(IEnumerable<T> column)
		{
			if (ReferenceEquals(column, null)) throw new ArgumentNullException(nameof(column));
			var firstRowCount = _innerMatrix[0]?.Count ?? 0;
			if (!_innerMatrix.TrueForAll(row => row.Count == firstRowCount))
				throw new
					Exception("To add rows in Jugged Matrix - all rows should be filled with the same count of elements");

			var enumerable = column as T[] ?? column.ToArray();
			if (enumerable.Length != RowsCount)
				throw new
					ArgumentException("Number of elements in input column should be the same as Rows count in matrix");

			for (var i = 0; i < enumerable.Length; i++)
				_innerMatrix[i].Add(enumerable[i]);
		}

		/// <inheritdoc />
		public void InsertColumn(int index, IEnumerable<T> column)
		{
			if (ReferenceEquals(column, null)) throw new ArgumentNullException(nameof(column));
			if (index < 0 ||
			    _innerMatrix.Any(row => row.Count <= index)) throw new ArgumentException(nameof(index));

			var enumerable = column as T[] ?? column.ToArray();
			if (enumerable.Length != RowsCount)
				throw new
					ArgumentException("Number of elements in input column should be the same as Rows count in matrix");

			for (var i = 0; i < enumerable.Length; i++)
				_innerMatrix[i].Insert(index, enumerable[i]);
		}

		/// <inheritdoc />
		public void AddRow(IEnumerable<T> row)
		{
			if (ReferenceEquals(row, null)) throw new ArgumentNullException(nameof(row));
			_innerMatrix.Add(row.ToList());
		}

		/// <inheritdoc />
		public void InsertRow(int index, IEnumerable<T> row)
		{
			if (ReferenceEquals(row, null)) throw new ArgumentNullException(nameof(row));
			if (index < 0 ||
			    index > RowsCount) throw new ArgumentException(nameof(index));

			_innerMatrix.Insert(index, row.ToList());
		}

		/// <inheritdoc />
		public void RemoveColumn(int index)
		{
			if (index < 0) throw new ArgumentException(nameof(index));
			for (var i = 0; i < RowsCount; i++)
			{
				if (_innerMatrix[i].Count <= index) continue;
				_innerMatrix[i].RemoveAt(index);
			}
		}

		/// <inheritdoc />
		public void RemoveRow(int index)
		{
			if (index < 0 ||
			    index > RowsCount) throw new ArgumentException(nameof(index));

			_innerMatrix.RemoveAt(index);
		}

		#endregion

		#region Equality members

		/// <inheritdoc />
		public bool Equals(IJuggedMatrix<T> other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			if (RowsCount != other.RowsCount) return false;
			for (var i = 0; i < RowsCount; i++)
			{
				if (_innerMatrix[i].Count != other.ElementsInRow(i)) return false;
				for (var j = 0; j < _innerMatrix[i].Count; j++)
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
			if (obj is IJuggedMatrix<T> matrix) return Equals(matrix);
			return false;
		}

		/// <inheritdoc />
		public override int GetHashCode() => _innerMatrix != null ? _innerMatrix.GetHashCode() : 0;

		#endregion
	}
}