#region Using derectives

using System.Collections.Generic;
using CyberMath.Structures.Matrices.Jagged_Matrix;

#endregion

namespace CyberMath.Structures.Matrices.Dynamic_Matrices.Dynamic_Jugged_Matrix
{
	/// <summary>
	///     Represents an interface for dynamic jugged matrix
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IDynamicJuggedMatrix<T> : IJuggedMatrix<T>
	{
		/// <summary>
		///     Inserting a new column to the end of matrix; only if count of elements on each row is equal
		/// </summary>
		/// <param name="column">Column to push</param>
		void AddColumn(IEnumerable<T> column);

		/// <summary>
		///     Inserting the column into <paramref name="index" /> place in matrix, if it's possible
		/// </summary>
		/// <param name="index">Index to insert into</param>
		/// <param name="column">Column to insert</param>
		void InsertColumn(int index, IEnumerable<T> column);

		/// <summary>
		///     Inserting a new row to the end of matrix
		/// </summary>
		/// <param name="row">Row to push</param>
		void AddRow(IEnumerable<T> row);

		/// <summary>
		///     Inserting the column into <paramref name="index" /> place in matrix
		/// </summary>
		/// <param name="index">Index to insert</param>
		/// <param name="row">Row to insert into</param>
		void InsertRow(int index, IEnumerable<T> row);

		/// <summary>
		///     Removes a column at <paramref name="index" /> in matrix
		/// </summary>
		/// <param name="index">Index of deleting column</param>
		void RemoveColumn(int index);

		/// <summary>
		///     Removes a row at <paramref name="index" /> in matrix
		/// </summary>
		/// <param name="index">Index of deleting row</param>
		void RemoveRow(int index);
	}
}