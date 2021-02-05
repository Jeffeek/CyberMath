using System.Collections.Generic;
using CyberMath.Structures.Matrices.Matrix;

namespace CyberMath.Structures.Matrices.Dynamic_Matrices
{
	//TODO: summary
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IDynamicMatrix<T> : IMatrix<T>
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
