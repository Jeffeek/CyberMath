using System;
using System.Collections;
using System.Collections.Generic;

namespace CyberMath.Structures.Matrices.Base
{
    /// <summary>
    /// Represent the base interface for matrices
    /// </summary>
    /// <typeparam name="T">ANY</typeparam>
    public interface IMatrixBase<T> : IEnumerable<IEnumerable<T>>
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
        /// Returns enumerable, which is 'walks' on the column neither default <see cref="IEnumerator"/>
        /// </summary>
        /// <returns><see cref="IEnumerable"/> of <see cref="IEnumerable{T}"/> - columns of matrix</returns>
        IEnumerable<IEnumerable<T>> GetColumnsEnumerable();
    }
}
