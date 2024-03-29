﻿#region Using namespaces

using System;
using System.Collections.Generic;
using CyberMath.Structures.BinaryTrees.BinaryTreeBase;

#endregion

namespace CyberMath.Structures.BinaryTrees.BinaryTree
{
    /// <summary>
    ///     Represents vanilla Binary Tree. Implements <see cref="BinaryTreeBase{T}"/>
    /// </summary>
    /// <typeparam name="T">
    ///     <see cref="IComparable{T}"/>
    /// </typeparam>
    public class BinaryTree<T> : BinaryTreeBase<T>
        where T : IComparable<T>, IComparable
    {
        /// <inheritdoc/>
        public BinaryTree() { }

        /// <summary>
        ///     Creates an instance of <see cref="BinaryTree{T}"/> and adds <paramref name="values"/>
        /// </summary>
        /// <param name="values">Values to add</param>
        public BinaryTree(params T[] values) : base(values) { }

        /// <summary>
        ///     Creates an instance of <see cref="BinaryTree{T}"/> and adds <paramref name="values"/>
        /// </summary>
        /// <param name="values">Values to add</param>
        public BinaryTree(IEnumerable<T> values) : base(values) { }

        /// <inheritdoc/>
        public override void Add(T item)
        {
            if (Root == null)
            {
                Root = new BinaryTreeNode<T>(item);
                Count = 1;

                return;
            }

            if (Contains(item)) return;

            Root = Root.Insert(item);
            Count++;
        }
    }
}