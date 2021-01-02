using System;
using CyberMath.Structures.BinaryTreeBase;

namespace CyberMath.Structures.AVLBinaryTree
{
    public class AVLBinaryTreeNode<T> : BinaryTreeNodeBase<T> 
            where T : IComparable<T>, IComparable
    {
        private int _height = 0;
        
        public AVLBinaryTreeNode(T data) : base(data) { }

        public override void Add(T value) => InternalInsert(this, value);

        internal AVLBinaryTreeNode<T> InternalInsert(AVLBinaryTreeNode<T> node, T data)
        {
            if (ReferenceEquals(node, null)) return new AVLBinaryTreeNode<T>(data);
            if (data.CompareTo(node.Data) == -1)
                node.Left = InternalInsert(node.Left as AVLBinaryTreeNode<T>, data);
            else
                node.Right = InternalInsert(node.Right as AVLBinaryTreeNode<T>, data);
            return Balance(node);
        }

        private int GetHeight(AVLBinaryTreeNode<T> node) => node?._height ?? 0;

        private int BFactor() => GetHeight(Right as AVLBinaryTreeNode<T>) - GetHeight(Left as AVLBinaryTreeNode<T>);
        
        private void FixHeight(AVLBinaryTreeNode<T> node)
        {
            int hl = GetHeight(node.Left as AVLBinaryTreeNode<T>);
            int hr = GetHeight(node.Right as AVLBinaryTreeNode<T>);
            node._height = (hl > hr ? hl : hr) + 1;
        }

        private AVLBinaryTreeNode<T> RotateRight(AVLBinaryTreeNode<T> node)
        {
            var q = node.Left as AVLBinaryTreeNode<T>;
            node.Left = q.Right;
            q.Right = node;
            FixHeight(node);
            FixHeight(q);
            return q;
        }

        private AVLBinaryTreeNode<T> RotateLeft(AVLBinaryTreeNode<T> node)
        {
            var rightNode = node.Right as AVLBinaryTreeNode<T>;
            node.Right = rightNode.Left;
            rightNode.Left = node;
            FixHeight(node);
            FixHeight(rightNode);
            return rightNode;
        }

        private AVLBinaryTreeNode<T> Balance(AVLBinaryTreeNode<T> node)
        {
            FixHeight(node);
            if (node.BFactor() == 2)
            {
                if ((node.Right as AVLBinaryTreeNode<T>)?.BFactor() < 0)
                    node.Right = RotateRight(node.Right as AVLBinaryTreeNode<T>);
                return RotateLeft(node);
            }
            if (node.BFactor() != -2) return node;
            if ((node.Left as AVLBinaryTreeNode<T>)?.BFactor() > 0)
                node.Left = RotateLeft(node.Left as AVLBinaryTreeNode<T>);
            return RotateRight(node);
        }
    }
}
