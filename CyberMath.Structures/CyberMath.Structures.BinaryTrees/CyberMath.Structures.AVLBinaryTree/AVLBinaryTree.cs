using CyberMath.Structures.BinaryTreeBase;
using System;
using System.Collections.Generic;

namespace CyberMath.Structures.AVLBinaryTree
{
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
