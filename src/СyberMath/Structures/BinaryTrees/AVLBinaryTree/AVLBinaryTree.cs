using System;
using System.Collections.Generic;
using СyberMath.Structures.BinaryTrees.BinaryTreeBase;

namespace СyberMath.Structures.BinaryTrees.AVLBinaryTree
{
    /// <summary>
    /// Represents AVL Binary Tree. Implements <see cref="BinaryTreeBase{T}"/>
    /// </summary>
    /// <typeparam name="T"><see cref="IComparable{T}"/></typeparam>
    public class AVLBinaryTree<T> : BinaryTreeBase<T>
    where T : IComparable, IComparable<T>
    {
        public AVLBinaryTree() { }

        public AVLBinaryTree(params T[] values) : base(values) { }

        public AVLBinaryTree(IEnumerable<T> values) : base(values) { }

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
