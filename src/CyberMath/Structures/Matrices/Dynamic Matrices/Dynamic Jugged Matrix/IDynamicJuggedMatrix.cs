using System.Collections.Generic;
using CyberMath.Structures.Matrices.Jagged_Matrix;

namespace CyberMath.Structures.Matrices.Dynamic_Matrices.Dynamic_Jugged_Matrix
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IDynamicJuggedMatrix<T> : IJuggedMatrix<T>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="column"></param>
		void AddColumn(IEnumerable<T> column);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="column"></param>
		void InsertColumn(int index, IEnumerable<T> column);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="row"></param>
		void AddRow(IEnumerable<T> row);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="row"></param>
		void InsertRow(int index, IEnumerable<T> row);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		void RemoveColumn(int index);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		void RemoveRow(int index);
	}
}
