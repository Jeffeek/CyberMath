using System;
using System.Collections.Generic;
using CyberMath.Structures.BinaryTreeBase;

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
            Root = (Root as AVLBinaryTreeNode<T>)?.InternalInsert(Root as AVLBinaryTreeNode<T>, item);
            Count++;
        }
    }
}
