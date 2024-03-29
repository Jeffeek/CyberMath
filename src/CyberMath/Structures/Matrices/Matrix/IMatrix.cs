﻿#region Using namespaces

using System;
using CyberMath.Structures.Matrices.Base;

#endregion

namespace CyberMath.Structures.Matrices.Matrix
{
    /// <summary>
    ///     Interface for vanilla Matrix. Implements <see cref="T:CyberMath.Structures.Matrices.Base.IMatrixBase`1"/>
    /// </summary>
    /// <typeparam name="T">ANY</typeparam>
    public interface IMatrix<T> : IMatrixBase<T>, IEquatable<IMatrix<T>>
    {
        /// <summary>
        ///     Count of columns in <see cref="IMatrix{T}"/>
        /// </summary>
        int ColumnsCount { get; }

        /// <summary>
        ///     Creates a new <see cref="IMatrix{T}"/> transposed
        /// </summary>
        /// <returns>New <see cref="IMatrix{T}"/> instance</returns>
        IMatrix<T> Transpose();
    }
}