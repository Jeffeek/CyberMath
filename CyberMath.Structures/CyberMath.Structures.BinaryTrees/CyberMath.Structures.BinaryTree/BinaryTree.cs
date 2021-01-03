using System;
using System.Collections.Generic;
using CyberMath.Structures.BinaryTreeBase;

namespace CyberMath.Structures.BinaryTree
{
    public class BinaryTree<T> : BinaryTreeBase<T>
        where T : IComparable<T>, IComparable
    {
        public BinaryTree(params T[] values) : base(values) { }

        public BinaryTree(IEnumerable<T> values) : base(values) { }
        
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