using CyberMath.Structures.BinaryTreeBase;
using System;

namespace CyberMath.Structures.AVLBinaryTree
{
    /// <summary>
    /// Represents an AVL Binary Tree NODE. Implements <see cref="BinaryTreeNodeBase{T}"/>
    /// </summary>
    /// <typeparam name="T"><see cref="IComparable{T}"/></typeparam>
    public class AVLBinaryTreeNode<T> : BinaryTreeNodeBase<T>
            where T : IComparable<T>, IComparable
    {
        /// <summary>
        /// Height of initial <see cref="AVLBinaryTreeNode{T}"/>
        /// </summary>
        private int _height = 0;

        public AVLBinaryTreeNode(T data) : base(data) { }

        public override IBinaryTreeNode<T> Insert(T value) => InternalInsert(this, value);

        public override IBinaryTreeNode<T> Remove(T value) => InternalRemove(this, value);

        private AVLBinaryTreeNode<T> InternalRemove(AVLBinaryTreeNode<T> node, T value)
        {
            if (ReferenceEquals(node, null))
                return null;
            if (value.CompareTo(node.Data) == -1)
                node.Left = InternalRemove(node.Left as AVLBinaryTreeNode<T>, value);
            else if (value.CompareTo(node.Data) == 1)
                node.Right = InternalRemove(node.Right as AVLBinaryTreeNode<T>, value);
            else
            {
                var leftNode = node.Left as AVLBinaryTreeNode<T>;
                if (!(node.Right is AVLBinaryTreeNode<T> rightNode))
                    return leftNode;
                var min = rightNode.Min() as AVLBinaryTreeNode<T>;
                min.Right = RemoveMin(rightNode);
                min.Left = leftNode;
                return Balance(min);
            }
            return Balance(node);
        }

        /// <summary>
        /// Removes a minimal node from <paramref name="subTree"/>
        /// </summary>
        /// <param name="subTree"></param>
        /// <returns>If <see cref="IBinaryTreeNode{T}.Left"/> equals <see langword="null"/> returns reference to <see cref="IBinaryTreeNode{T}.Right"/>. Else balanced <paramref name="subTree"/> after removing</returns>
        private AVLBinaryTreeNode<T> RemoveMin(AVLBinaryTreeNode<T> subTree)
        {
            if (ReferenceEquals(subTree.Left, null))
                return subTree.Right as AVLBinaryTreeNode<T>;
            subTree.Left = RemoveMin(subTree.Left as AVLBinaryTreeNode<T>);
            return Balance(subTree);
        }

        private AVLBinaryTreeNode<T> InternalInsert(AVLBinaryTreeNode<T> node, T data)
        {
            if (ReferenceEquals(node, null)) return new AVLBinaryTreeNode<T>(data);
            if (data.CompareTo(node.Data) == -1)
                node.Left = InternalInsert(node.Left as AVLBinaryTreeNode<T>, data);
            else
                node.Right = InternalInsert(node.Right as AVLBinaryTreeNode<T>, data);
            return Balance(node);
        }

        private int GetHeight(AVLBinaryTreeNode<T> node) => node?._height ?? 0;

        /// <summary>
        /// Calculates BFactor in initial <see cref="AVLBinaryTreeNode{T}"/>
        /// </summary>
        /// <returns></returns>
        private int BFactor() => GetHeight(Right as AVLBinaryTreeNode<T>) - GetHeight(Left as AVLBinaryTreeNode<T>);

        /// <summary>
        /// Fixes <see cref="_height"/> after fixing <paramref name="node"/>
        /// </summary>
        /// <param name="node">Node to fix</param>
        private void FixHeight(AVLBinaryTreeNode<T> node)
        {
            int hl = GetHeight(node.Left as AVLBinaryTreeNode<T>);
            int hr = GetHeight(node.Right as AVLBinaryTreeNode<T>);
            node._height = (hl > hr ? hl : hr) + 1;
        }

        /// <summary>
        /// Right rotating of <see cref="AVLBinaryTreeNode{T}"/>
        /// </summary>
        /// <param name="node">Node to rotate</param>
        /// <returns>Reference to the rotated node</returns>
        private AVLBinaryTreeNode<T> RotateRight(AVLBinaryTreeNode<T> node)
        {
            var q = node.Left as AVLBinaryTreeNode<T>;
            node.Left = q.Right;
            q.Right = node;
            FixHeight(node);
            FixHeight(q);
            return q;
        }

        /// <summary>
        /// Left rotating of <see cref="AVLBinaryTreeNode{T}"/>
        /// </summary>
        /// <param name="node">Node to rotate</param>
        /// <returns>Reference to the rotated node</returns>
        private AVLBinaryTreeNode<T> RotateLeft(AVLBinaryTreeNode<T> node)
        {
            var rightNode = node.Right as AVLBinaryTreeNode<T>;
            node.Right = rightNode.Left;
            rightNode.Left = node;
            FixHeight(node);
            FixHeight(rightNode);
            return rightNode;
        }

        /// <summary>
        /// Balancing <paramref name="node"/>
        /// </summary>
        /// <param name="node"></param>
        /// <returns>Reference to the <paramref name="node"/> after balancing</returns>
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
