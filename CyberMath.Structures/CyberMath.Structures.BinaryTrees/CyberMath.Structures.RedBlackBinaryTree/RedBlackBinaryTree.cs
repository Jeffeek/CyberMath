using System;
using System.Collections.Generic;
using CyberMath.Structures.BinaryTreeBase;

namespace CyberMath.Structures.RedBlackBinaryTree
{
    public class RedBlackBinaryTree<T> : BinaryTreeBase<T>
    where T : IComparable, IComparable<T>
    {
        public RedBlackBinaryTree(){ }

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
