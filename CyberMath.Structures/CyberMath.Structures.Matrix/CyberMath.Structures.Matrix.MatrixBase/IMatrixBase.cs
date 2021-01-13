using System;
using System.Collections.Generic;

namespace CyberMath.Structures.MatrixBase
{
    /// <summary>
    /// Represent the base interface for matrixes
    /// </summary>
    /// <typeparam name="T">ANY</typeparam>
    public interface IMatrixBase<T> : IEnumerable<T>
    {
        /// <summary>
        /// Count of rows in matrix
        /// </summary>
        int RowsCount { get; }
        /// <summary>
        /// Represent <see cref="bool"/> value if the rows count equals elements count on each row
        /// </summary>
        bool IsSquare { get; }
        /// <summary>
        /// Indexer for every element in <see cref="IMatrixBase{T}"/> 
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        /// <returns>Element at [<paramref name="row"/>, <paramref name="column"/>]</returns>
        T this[int row, int column] { get; set; }
        /// <summary>
        /// Do action over every element in matrix
        /// </summary>
        /// <param name="func">Function to do</param>
        void ProcessFunctionOverData(Action<int, int> func);
        /// <summary>
        /// Returns count of elements in row
        /// </summary>
        /// <param name="rowIndex">Row index</param>
        /// <returns>Count of elements in row</returns>
        int ElementsInRow(int rowIndex);
        /// <summary>
        /// Creates a new <see cref="IMatrixBase{T}"/> matrix without column at <paramref name="columnIndex"/>
        /// </summary>
        /// <param name="columnIndex">Column index to remove</param>
        /// <returns>A new <see cref="IMatrixBase{T}"/> matrix without column at <paramref name="columnIndex"/></returns>
        IMatrixBase<T> CreateMatrixWithoutColumn(int columnIndex);
        /// <summary>
        /// Creates a new <see cref="IMatrixBase{T}"/> matrix without row at <paramref name="rowIndex"/>
        /// </summary>
        /// <param name="columnIndex">Column index to remove</param>
        /// <returns>A new <see cref="IMatrixBase{T}"/> matrix without row at <paramref name="rowIndex"/></returns>
        IMatrixBase<T> CreateMatrixWithoutRow(int rowIndex);
    }
}
