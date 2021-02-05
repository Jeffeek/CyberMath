using System;
using System.Collections.Generic;

namespace CyberMath.Structures.BinaryTrees.BinaryTreeBase
{
    /// <summary>
    /// Interface for Binary Tree
    /// </summary>
    /// <typeparam name="T"><see cref="IComparable{T}"/></typeparam>
    public interface IBinaryTree<T> :
                                    ICollection<T>,
                                    IDisposable
        where T : IComparable<T>, IComparable
    {
        /// <summary>
        /// Reference to the main node, called Root
        /// </summary>
        IBinaryTreeNode<T> Root { get; }
        /// <summary>
        /// <see cref="bool"/> result which show the emptiness of <see cref="IBinaryTree{T}"/>
        /// </summary>
        bool IsEmpty { get; }
        /// <summary>
        /// Depth of <see cref="IBinaryTree{T}"/>
        /// </summary>
        /// <returns><see cref="int"/> result of deepness in <see cref="IBinaryTree{T}"/></returns>
        int Depth();
        /// <summary>
        /// Returns an inorder traversal <see cref="IEnumerable{T}"/> collection
        /// </summary>
        /// <returns>Inorder traversal <see cref="IEnumerable{T}"/> collection</returns>
        IEnumerable<T> Inorder();
        /// <summary>
        /// Returns an preorder traversal <see cref="IEnumerable{T}"/> collection
        /// </summary>
        /// <returns>Preorder traversal <see cref="IEnumerable{T}"/> collection</returns>
        IEnumerable<T> Preorder();
        /// <summary>
        /// Returns an postorder traversal <see cref="IEnumerable{T}"/> collection
        /// </summary>
        /// <returns>Postorder traversal <see cref="IEnumerable{T}"/> collection</returns>
        IEnumerable<T> Postorder();
        /// <summary>
        /// Returns maximal element in <see cref="IBinaryTree{T}"/>
        /// </summary>
        /// <returns></returns>
        T Max();
        /// <summary>
        /// Returns minimal element in <see cref="IBinaryTree{T}"/>
        /// </summary>
        /// <returns></returns>
        T Min();
        /// <summary>
        /// Adds <see cref="IEnumerable{T}"/> collection into <see cref="IBinaryTree{T}"/>
        /// </summary>
        /// <param name="values"></param>
        void AddRange(IEnumerable<T> values);
        /// <summary>
        /// Merges initial <see cref="IBinaryTree{T}"/> with another <see cref="IBinaryTree{T}"/>
        /// </summary>
        /// <param name="binaryTree"></param>
        void MergeWith(IBinaryTree<T> binaryTree);
        /// <summary>
        /// Traversal strategy for <see langword="foreach"/> statement
        /// </summary>
        TraversalOrderType TraversalOrderType { get; set; }
    }
}
