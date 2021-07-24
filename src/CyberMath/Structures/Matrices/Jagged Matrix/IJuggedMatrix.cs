#region Using namespaces

using System;
using CyberMath.Structures.Matrices.Base;

#endregion

namespace CyberMath.Structures.Matrices.Jagged_Matrix
{
    /// <summary>
    ///     Interface for Jugged Matrix. It implements <see cref="IMatrixBase{T}"/>
    /// </summary>
    /// <typeparam name="T">ANY</typeparam>
    public interface IJuggedMatrix<T> : IMatrixBase<T>, IEquatable<IJuggedMatrix<T>>
    {
        /// <summary>
        ///     Sorts rows in <see cref="IJuggedMatrix{T}"/> by count of elements
        /// </summary>
        /// <returns>New <see cref="IJuggedMatrix{T}"/> with sorted rows by count of elements</returns>
        IJuggedMatrix<T> SortRows();

        /// <returns>New <see cref="IJuggedMatrix{T}"/> with sorted rows by descending by count of elements</returns>
        IJuggedMatrix<T> SortRowsByDescending();
    }
}