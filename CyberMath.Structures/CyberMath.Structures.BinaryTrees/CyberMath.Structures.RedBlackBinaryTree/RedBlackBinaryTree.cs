using CyberMath.Structures.BinaryTreeBase;
using System;
using System.Collections.Generic;

namespace CyberMath.Structures.RedBlackBinaryTree
{
    /// <summary>
    /// Represents Red-Black Binary Tree. Implements <see cref="BinaryTreeBase{T}"/>
    /// </summary>
    /// <typeparam name="T"><see cref="IComparable{T}"/></typeparam>
    public class RedBlackBinaryTree<T> : BinaryTreeBase<T>
    where T : IComparable, IComparable<T>
    {
        public RedBlackBinaryTree() { }

        public RedBlackBinaryTree(params T[] values) : base(values) { }

        public RedBlackBinaryTree(IEnumerable<T> values) : base(values) { }

        public override void Add(T item)
        {
            if (ReferenceEquals(Root, null))
            {
                Root = new RedBlackBinaryTreeNode<T>(item);
                Count = 1;
                return;
            }

            if (Contains(item)) return;
            Root = Root.Insert(item);
            Count++;
        }
    }
}
