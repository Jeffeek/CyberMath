#region Using namespaces

using System;

#endregion

namespace CyberMath.Structures.BinaryTrees.BinaryTreeBase
{
    /// <summary>
    ///     Represents interface for Node for <see cref="IBinaryTree{T}"/>
    /// </summary>
    /// <typeparam name="T">
    ///     <see cref="IComparable{T}"/>
    /// </typeparam>
    public interface IBinaryTreeNode<T> :
        IComparable<IBinaryTreeNode<T>>,
        IComparable,
        IEquatable<IBinaryTreeNode<T>>
        where T : IComparable<T>, IComparable
    {
        /// <summary>
        ///     Reference to the left-child node
        /// </summary>
        IBinaryTreeNode<T> Left { get; }

        /// <summary>
        ///     Reference to the right-child node
        /// </summary>
        IBinaryTreeNode<T> Right { get; }

        /// <summary>
        ///     Data in node
        /// </summary>
        T Data { get; }

        /// <summary>
        ///     Finds depth of the DEEPEST child in initial <see cref="IBinaryTreeNode{T}"/>
        /// </summary>
        /// <returns>Depth of <see cref="IBinaryTreeNode{T}"/></returns>
        int Depth();

        /// <summary>
        ///     Inserting data into initial <see cref="IBinaryTreeNode{T}"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Reference to the root</returns>
        IBinaryTreeNode<T> Insert(T value);

        /// <summary>
        ///     Removing data from initial <see cref="IBinaryTreeNode{T}"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Reference to the root</returns>
        IBinaryTreeNode<T> Remove(T value);

        /// <summary>
        ///     Find the minimal <see cref="IBinaryTreeNode{T}"/> in initial <see cref="IBinaryTreeNode{T}"/>
        /// </summary>
        /// <returns>Reference to the minimal element</returns>
        IBinaryTreeNode<T> Min();

        /// <summary>
        ///     Find the maximal <see cref="IBinaryTreeNode{T}"/> in initial <see cref="IBinaryTreeNode{T}"/>
        /// </summary>
        /// <returns>Reference to the maximal element</returns>
        IBinaryTreeNode<T> Max();
    }
}