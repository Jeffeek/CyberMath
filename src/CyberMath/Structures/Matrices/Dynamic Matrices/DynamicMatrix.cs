using CyberMath.Structures.Matrices.Matrix;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyberMath.Structures.Matrices.Dynamic_Matrices
{
	//TODO: summary
	//TODO: unit-tests
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
    public sealed class DynamicMatrix<T> : IDynamicMatrix<T>, IEquatable<DynamicMatrix<T>>
	{
	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="rowsCount"></param>
	    /// <param name="columnsCount"></param>
	    /// <exception cref="ArgumentException"></exception>
	    public DynamicMatrix(int rowsCount, int columnsCount)
	    {
		    if (rowsCount < 0) throw new ArgumentException(nameof(rowsCount) + $" was {rowsCount}");
			if (columnsCount < 0) throw new ArgumentException(nameof(columnsCount) + $" was {columnsCount}");
			_innerMatrix = new List<List<T>>(rowsCount);
		    for (var i = 0; i < rowsCount; i++)
		    {
				_innerMatrix.Add(new List<T>(columnsCount));
				for (var j = 0; j < columnsCount; j++)
				{
					_innerMatrix[i].Add(default);
				}
		    }
	    }

	    public DynamicMatrix() : this(0, 0) { }
	    
	    private List<List<T>> _innerMatrix;

	    /// <inheritdoc />
	    public T this[int row, int column]
	    {
		    get => _innerMatrix[row][column];
		    set => _innerMatrix[row][column] = value;
	    }

	    /// <inheritdoc />
	    public int ColumnsCount => _innerMatrix[0]?.Count ?? 0;

	    /// <inheritdoc />
	    public int RowsCount => _innerMatrix.Count;

	    /// <inheritdoc />
	    public bool IsSquare => _innerMatrix.TrueForAll(row => row.Count == RowsCount);

	    /// <inheritdoc />
	    public void AddColumn(IEnumerable<T> column)
        {
	        if (ReferenceEquals(column, null)) throw new ArgumentNullException(nameof(column));
	        InsertColumn(ColumnsCount, column);
        }

	    /// <inheritdoc />
	    public void AddRow(IEnumerable<T> row)
        {
	        if (ReferenceEquals(row, null)) throw new NullReferenceException(nameof(row));
	        var rowAsList = row.ToList();
	        if (rowAsList.Count != ColumnsCount) throw new ArgumentException("Count of elements in a row should be the same");
            _innerMatrix.Add(rowAsList);
        }

	    /// <inheritdoc />
	    public int ElementsInRow(int rowIndex) => ColumnsCount;

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
		public void InsertColumn(int index, IEnumerable<T> column)
		{
			if (ReferenceEquals(column, null)) throw new ArgumentNullException(nameof(column));
			var columnAsList = column.ToList();
			if (columnAsList.Count != RowsCount) throw new ArgumentException(nameof(index));
			for (var i = 0; i < RowsCount; i++)
				_innerMatrix[i].Insert(index, columnAsList[i]);
		}

	    /// <inheritdoc />
	    public void InsertRow(int index, IEnumerable<T> row)
        {
			if (ReferenceEquals(row, null)) throw new ArgumentNullException(nameof(row));
			var rowAsList = row.ToList();
			if (rowAsList.Count != ColumnsCount) throw new ArgumentException(nameof(index));
			_innerMatrix.Insert(index, rowAsList);
		}

	    /// <inheritdoc />
	    public void ProcessFunctionOverData(Action<int, int> func)
        {
	        if (ReferenceEquals(func, null)) return;
	        for (var i = 0; i < RowsCount; i++)
	        {
		        for (var j = 0; j < ColumnsCount; j++)
			        func.Invoke(i, j);
	        }
		}

		/// <inheritdoc />
		public void RemoveColumn(int index)
		{
			if (index < 0 &&
			    index >= ColumnsCount) throw new ArgumentException(nameof(index));
			for (var i = 0; i < RowsCount; i++)
				_innerMatrix[i].RemoveAt(index);
		}

	    /// <inheritdoc />
	    public void RemoveRow(int index)
        {
			if (index < 0 &&
			    index >= RowsCount) throw new ArgumentException(nameof(index));
			_innerMatrix.RemoveAt(index);
		}

	    /// <inheritdoc />
	    public IMatrix<T> Transpose()
        {
			var result = new DynamicMatrix<T>(ColumnsCount, RowsCount);
			for (var i = 0; i < RowsCount; i++)
			{
				for (var j = 0; j < ColumnsCount; j++)
				{
					result[j, i] = this[i, j];
				}
			}

			return result;
		}

	    #region Equality members

	    /// <inheritdoc />
	    public bool Equals(DynamicMatrix<T> other)
	    {
		    if (ReferenceEquals(null, other)) return false;
		    if (ReferenceEquals(this, other)) return true;
		    if (ColumnsCount != other.ColumnsCount) return false;
		    if (RowsCount != other.RowsCount) return false;
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

	    /// <inheritdoc />
	    public override bool Equals(object obj)
	    {
		    if (ReferenceEquals(null, obj)) return false;
		    if (ReferenceEquals(this, obj)) return true;
		    if (obj.GetType() != this.GetType()) return false;
		    return Equals((DynamicMatrix<T>) obj);
	    }

	    /// <inheritdoc />
	    public override int GetHashCode() => (_innerMatrix != null ? _innerMatrix.GetHashCode() : 0);

	    #endregion
	}
}
