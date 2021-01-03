using System;
using CyberMath.Structures.BinaryTreeBase;

namespace CyberMath.Structures.RedBlackBinaryTree
{
    public class RedBlackBinaryTreeNode<T> : BinaryTreeNodeBase<T>
    where T : IComparable, IComparable<T>
    {
        private BinaryTreeColor _color = BinaryTreeColor.Black;
        private RedBlackBinaryTreeNode<T> _parent = null;
        
        public RedBlackBinaryTreeNode(T data) : base(data) { }
        
        public override IBinaryTreeNode<T> Insert(T value) => InternalInsert(this, value);

        private RedBlackBinaryTreeNode<T> GetUncle(RedBlackBinaryTreeNode<T> node)
        {
            RedBlackBinaryTreeNode<T> parent = node._parent;
            return GetSibling(parent);
        }

        private RedBlackBinaryTreeNode<T> GetSibling(RedBlackBinaryTreeNode<T> node)
        {
            var parent = node._parent;
            return parent == null
                ? null
                : node.Equals(parent.Left)
                    ? parent.Right as RedBlackBinaryTreeNode<T>
                    : parent.Left as RedBlackBinaryTreeNode<T>;
        }

        private RedBlackBinaryTreeNode<T> GetGrandfather(RedBlackBinaryTreeNode<T> node) => node?._parent?._parent;

        private RedBlackBinaryTreeNode<T> InternalInsert(RedBlackBinaryTreeNode<T> root, T value)
        {
            var newNode = new RedBlackBinaryTreeNode<T>(value);
            InsertRecurse(root, newNode);
            InsertRepairTree(newNode);
            root = newNode;
            while (root._parent != null)
                root = root._parent;
            return root;
        }

        private void InsertRepairTree(RedBlackBinaryTreeNode<T> node)
        {
            if (node._parent== null)
                InsertCase1(node);
            else if (node._parent._color == BinaryTreeColor.Black) return;
            else if (GetUncle(node) != null && GetUncle(node)._color == BinaryTreeColor.Red)
                InsertCase3(node);
            else
                InsertCase4(node);
        }

        private void InsertRecurse(RedBlackBinaryTreeNode<T> root, RedBlackBinaryTreeNode<T> node)
        {
            if (root == null) return;
            if (node.Data.CompareTo(root.Data) == -1)
            {
                if (root.Left != null)
                {
                    InsertRecurse(root.Left as RedBlackBinaryTreeNode<T>, node);
                    return;
                }
                root.Left = node;
            }
            else
            {
                if (root.Right != null)
                {
                    InsertRecurse(root.Right as RedBlackBinaryTreeNode<T>, node);
                    return;
                }
                root.Right = node;
            }

            node._parent = root;
            node.Left = null;
            node.Right = null;
            node._color = BinaryTreeColor.Red;
        }

        private void InsertCase1(RedBlackBinaryTreeNode<T> node) => node._color = BinaryTreeColor.Black;

        private void InsertCase3(RedBlackBinaryTreeNode<T> node)
        {
            node._parent._color = BinaryTreeColor.Black;
            GetUncle(node)._color = BinaryTreeColor.Black;
            GetGrandfather(node)._color = BinaryTreeColor.Red;
            InsertRepairTree(GetGrandfather(node));
        }

        private void InsertCase4(RedBlackBinaryTreeNode<T> node)
        {
            RedBlackBinaryTreeNode<T> parent = node._parent;
            RedBlackBinaryTreeNode<T> grandfather = GetGrandfather(node);

            if (node.Equals(parent.Right) && parent.Equals(grandfather.Left))
            {
                RotateLeft(parent);
                node = node.Left as RedBlackBinaryTreeNode<T>;
            }
            else if (node.Equals(parent.Left) && parent.Equals(grandfather.Right))
            {
                RotateRight(parent);
                node = node.Right as RedBlackBinaryTreeNode<T>;
            }

            parent = node._parent;
            grandfather = GetGrandfather(node);

            if (node.Equals(parent.Left))
                RotateRight(grandfather);
            else
                RotateLeft(grandfather);
            parent._color = BinaryTreeColor.Black;
            grandfather._color = BinaryTreeColor.Red;
        }

        private void RotateLeft(RedBlackBinaryTreeNode<T> node)
        {
            RedBlackBinaryTreeNode<T> rightNode = node.Right as RedBlackBinaryTreeNode<T>;
            RedBlackBinaryTreeNode<T> parent = node._parent;
            if (rightNode == null) return;
            node.Right = rightNode.Left;
            rightNode.Left = node;
            node._parent = rightNode;
            if (node.Right != null)
                (node.Right as RedBlackBinaryTreeNode<T>)._parent = node;
            if (parent != null)
            {
                if (node.Equals(parent.Left))
                    parent.Left = rightNode;
                else if (node.Equals(parent.Right))
                    parent.Right = rightNode;
            }
            rightNode._parent = parent;
        }

        private void RotateRight(RedBlackBinaryTreeNode<T> node)
        {
            RedBlackBinaryTreeNode<T> leftNode = node.Left as RedBlackBinaryTreeNode<T>;
            RedBlackBinaryTreeNode<T> parent = node._parent;
            if (leftNode == null) return;
            node.Left = leftNode.Right;
            leftNode.Right = node;
            node._parent = leftNode;
            if (node.Left != null)
                (node.Left as RedBlackBinaryTreeNode<T>)._parent = node;
            if (parent != null)
            {
                if (node.Equals(parent.Left))
                    parent.Left = leftNode;
                else if (node.Equals(parent.Right))
                    parent.Right = leftNode;
            }
            leftNode._parent = parent;
        }
    }
}
