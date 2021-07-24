#region Using namespaces

using System;
using System.Collections.Generic;
using CyberMath.Structures.BinaryTrees.BinaryTreeBase;

#endregion

namespace CyberMath.Structures.BinaryTrees.AVLBinaryTree
{
    /// <summary>
    ///     Represents AVL Binary Tree. Implements <see cref="BinaryTreeBase{T}"/>
    /// </summary>
    /// <typeparam name="T">
    ///     <see cref="IComparable{T}"/>
    /// </typeparam>
    public class AVLBinaryTree<T> : BinaryTreeBase<T>
        where T : IComparable, IComparable<T>
    {
        /// <inheritdoc/>
        public AVLBinaryTree() { }

        /// <summary>
        ///     Creates an instance of <see cref="AVLBinaryTree{T}"/> and adds <paramref name="values"/>
        /// </summary>
        /// <param name="values">Values to add</param>
        public AVLBinaryTree(params T[] values) : base(values) { }

        /// <summary>
        ///     Creates an instance of <see cref="AVLBinaryTree{T}"/> and adds <paramref name="values"/>
        /// </summary>
        /// <param name="values">Values to add</param>
        public AVLBinaryTree(IEnumerable<T> values) : base(values) { }

        /// <inheritdoc/>
        public override void Add(T item)
        {
            if (ReferenceEquals(Root, null))
            {
                Root = new AVLBinaryTreeNode<T>(item);
                Count = 1;

                return;
            }

            if (Contains(item)) return;

            Root = Root.Insert(item);
            Count++;
        }
    }
}