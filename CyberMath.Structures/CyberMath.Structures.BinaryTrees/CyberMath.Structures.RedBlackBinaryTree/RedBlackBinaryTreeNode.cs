using CyberMath.Structures.BinaryTreeBase;
using System;

namespace CyberMath.Structures.RedBlackBinaryTree
{
    public class RedBlackBinaryTreeNode<T> : BinaryTreeNodeBase<T>
    where T : IComparable, IComparable<T>
    {
        private BinaryTreeColor _color = BinaryTreeColor.Black;
        private RedBlackBinaryTreeNode<T> _parent = null;
        public RedBlackBinaryTreeNode(T data) : base(data) { }

        #region Relatives

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

        private RedBlackBinaryTreeNode<T> GetGrandparent(RedBlackBinaryTreeNode<T> node) => node?._parent?._parent;

        #endregion

        #region FixUp Methods

        private RedBlackBinaryTreeNode<T> InsertFixUp(RedBlackBinaryTreeNode<T> root, RedBlackBinaryTreeNode<T> nodeX)
        {
            while (nodeX != root && nodeX._parent._color == BinaryTreeColor.Red)
            {
                if (nodeX._parent == nodeX._parent._parent.Left as RedBlackBinaryTreeNode<T>)
                {
                    RedBlackBinaryTreeNode<T> nodeY = nodeX._parent._parent.Right as RedBlackBinaryTreeNode<T>;
                    if (nodeY != null && nodeY._color == BinaryTreeColor.Red)
                    {
                        nodeX._parent._color = BinaryTreeColor.Black;
                        nodeY._color = BinaryTreeColor.Black;
                        nodeX._parent._parent._color = BinaryTreeColor.Red;
                        nodeX = nodeX._parent._parent;
                    }
                    else
                    {
                        if (nodeX == nodeX._parent.Right as RedBlackBinaryTreeNode<T>)
                        {
                            nodeX = nodeX._parent;
                            root = RotateLeft(root, nodeX);
                        }
                        nodeX._parent._color = BinaryTreeColor.Black;
                        nodeX._parent._parent._color = BinaryTreeColor.Red;
                        root = RotateRight(root, nodeX._parent._parent);
                    }
                }
                else
                {
                    var X = nodeX._parent._parent.Left as RedBlackBinaryTreeNode<T>;
                    if (X != null && X._color == BinaryTreeColor.Red)
                    {
                        nodeX._parent._color = BinaryTreeColor.Black;
                        X._color = BinaryTreeColor.Black;
                        nodeX._parent._parent._color = BinaryTreeColor.Red;
                        nodeX = nodeX._parent._parent;
                    }
                    else
                    {
                        if (nodeX == nodeX._parent.Left as RedBlackBinaryTreeNode<T>)
                        {
                            nodeX = nodeX._parent;
                            root = RotateRight(root, nodeX);
                        }
                        nodeX._parent._color = BinaryTreeColor.Black;
                        nodeX._parent._parent._color = BinaryTreeColor.Red;
                        root = RotateLeft(root, nodeX._parent._parent);
                    }
                }
                root._color = BinaryTreeColor.Black;
            }

            return root;
        }

        private RedBlackBinaryTreeNode<T> DeleteFixUp(RedBlackBinaryTreeNode<T> root, RedBlackBinaryTreeNode<T> nodeX)
        {
            while (nodeX != null && nodeX != root && nodeX._color == BinaryTreeColor.Black)
            {
                if (nodeX == nodeX._parent.Left as RedBlackBinaryTreeNode<T>)
                {
                    RedBlackBinaryTreeNode<T> nodeW = nodeX._parent.Right as RedBlackBinaryTreeNode<T>;
                    if (nodeW._color == BinaryTreeColor.Red)
                    {
                        nodeW._color = BinaryTreeColor.Black;
                        nodeX._parent._color = BinaryTreeColor.Red;
                        root = RotateLeft(root, nodeX._parent);
                        nodeW = nodeX._parent.Right as RedBlackBinaryTreeNode<T>;
                    }
                    if ((nodeW.Left as RedBlackBinaryTreeNode<T>)._color == BinaryTreeColor.Black &&
                        (nodeW.Right as RedBlackBinaryTreeNode<T>)._color == BinaryTreeColor.Black)
                    {
                        nodeW._color = BinaryTreeColor.Red;
                        nodeX = nodeX._parent;
                    }
                    else if ((nodeW.Right as RedBlackBinaryTreeNode<T>)._color == BinaryTreeColor.Black)
                    {
                        (nodeW.Left as RedBlackBinaryTreeNode<T>)._color = BinaryTreeColor.Black;
                        nodeW._color = BinaryTreeColor.Red;
                        root = RotateRight(root, nodeW);
                        nodeW = nodeX._parent.Right as RedBlackBinaryTreeNode<T>;
                    }
                    nodeW._color = nodeX._parent._color;
                    nodeX._parent._color = BinaryTreeColor.Black;
                    (nodeW.Right as RedBlackBinaryTreeNode<T>)._color = BinaryTreeColor.Black;
                    root = RotateLeft(root, nodeX._parent);
                    nodeX = root;
                }
                else
                {
                    RedBlackBinaryTreeNode<T> W = nodeX._parent.Left as RedBlackBinaryTreeNode<T>;
                    if (W._color == BinaryTreeColor.Red)
                    {
                        W._color = BinaryTreeColor.Black;
                        nodeX._parent._color = BinaryTreeColor.Red;
                        root = RotateRight(root, nodeX._parent);
                        W = nodeX._parent.Left as RedBlackBinaryTreeNode<T>;
                    }
                    if ((W.Right as RedBlackBinaryTreeNode<T>)._color == BinaryTreeColor.Black &&
                        (W.Left as RedBlackBinaryTreeNode<T>)._color == BinaryTreeColor.Black)
                    {
                        W._color = BinaryTreeColor.Black;
                        nodeX = nodeX._parent;
                    }
                    else if ((W.Left as RedBlackBinaryTreeNode<T>)._color == BinaryTreeColor.Black)
                    {
                        (W.Right as RedBlackBinaryTreeNode<T>)._color = BinaryTreeColor.Black;
                        W._color = BinaryTreeColor.Red;
                        root = RotateLeft(root, W);
                        W = nodeX._parent.Left as RedBlackBinaryTreeNode<T>;
                    }
                    W._color = nodeX._parent._color;
                    nodeX._parent._color = BinaryTreeColor.Black;
                    (W.Left as RedBlackBinaryTreeNode<T>)._color = BinaryTreeColor.Black;
                    root = RotateRight(root, nodeX._parent);
                    nodeX = root;
                }
            }
            if (nodeX != null)
                nodeX._color = BinaryTreeColor.Black;
            return root;
        }

        #endregion

        #region Insert

        public override IBinaryTreeNode<T> Insert(T value) => InternalInsert(this, value);

        private RedBlackBinaryTreeNode<T> InternalInsert(RedBlackBinaryTreeNode<T> root, T value)
        {
            var newNode = new RedBlackBinaryTreeNode<T>(value);
            RedBlackBinaryTreeNode<T> nodeY = null;
            RedBlackBinaryTreeNode<T> nodeX = root;
            while (nodeX != null)
            {
                nodeY = nodeX;
                if (newNode < nodeX)
                    nodeX = nodeX.Left as RedBlackBinaryTreeNode<T>;
                else
                    nodeX = nodeX.Right as RedBlackBinaryTreeNode<T>;
            }
            newNode._parent = nodeY;
            if (nodeY == null)
                root = newNode;
            else if (newNode < nodeY)
                nodeY.Left = newNode;
            else
                nodeY.Right = newNode;
            newNode.Left = null;
            newNode.Right = null;
            newNode._color = BinaryTreeColor.Red;
            root = InsertFixUp(root, newNode);
            return root;
        }

        #endregion

        #region Remove

        public override IBinaryTreeNode<T> Remove(T value) => InternalRemove(this, value);

        private RedBlackBinaryTreeNode<T> InternalRemove(RedBlackBinaryTreeNode<T> root, T value)
        {
            RedBlackBinaryTreeNode<T> item = FindNode(root, value) as RedBlackBinaryTreeNode<T>;
            RedBlackBinaryTreeNode<T> nodeY;

            if (item.Left == null && item.Right == null)
            {
                if (item == root) return null;
                nodeY = item._parent;
                if (nodeY.Left as RedBlackBinaryTreeNode<T> == item)
                    nodeY.Left = null;
                else
                    nodeY.Right = null;
            }
            else if (item.Left == null)
            {
                nodeY = item._parent;
                if (nodeY == null) return item.Right as RedBlackBinaryTreeNode<T>;
                if (nodeY.Left as RedBlackBinaryTreeNode<T> == item)
                    nodeY.Left = item.Right;
                else
                    nodeY.Right = item.Right;
            }
            else if (item.Right == null)
            {
                nodeY = item._parent;
                if (nodeY == null) return item.Left as RedBlackBinaryTreeNode<T>;
                if (nodeY.Left as RedBlackBinaryTreeNode<T> == item)
                    nodeY.Left = item.Left;
                else
                    nodeY.Right = item.Left;
            }
            else
            {
                var nodeX = item.Left.Max() as RedBlackBinaryTreeNode<T>;
                item.Data = nodeX.Data;
                nodeY = nodeX._parent;
                if (nodeY.Left as RedBlackBinaryTreeNode<T> == nodeX)
                {
                    nodeY.Left = nodeX.Left;
                    if (nodeX._color == BinaryTreeColor.Black)
                        root = DeleteFixUp(root, nodeY.Left as RedBlackBinaryTreeNode<T>);
                }
                else
                {
                    nodeY.Right = nodeX.Left;
                    if (nodeX._color == BinaryTreeColor.Black)
                        root = DeleteFixUp(root, nodeY.Right as RedBlackBinaryTreeNode<T>);
                }
            }
            return root;
        }

        #endregion

        #region Rotating

        private RedBlackBinaryTreeNode<T> RotateLeft(RedBlackBinaryTreeNode<T> root, RedBlackBinaryTreeNode<T> node)
        {
            RedBlackBinaryTreeNode<T> nodeY = node.Right as RedBlackBinaryTreeNode<T>;
            node.Right = nodeY.Left;
            if (nodeY.Left != null)
                (nodeY.Left as RedBlackBinaryTreeNode<T>)._parent = node;
            if (nodeY != null)
                nodeY._parent = node._parent;
            if (node._parent == null)
                root = nodeY;
            if (nodeY._parent != null)
            {
                if (node == node._parent.Left as RedBlackBinaryTreeNode<T>)
                    node._parent.Left = nodeY;
                else
                    node._parent.Right = nodeY;
            }
            nodeY.Left = node;
            if (node != null)
                node._parent = nodeY;
            return root;
        }

        private RedBlackBinaryTreeNode<T> RotateRight(RedBlackBinaryTreeNode<T> root, RedBlackBinaryTreeNode<T> node)
        {
            RedBlackBinaryTreeNode<T> X = node.Left as RedBlackBinaryTreeNode<T>;
            node.Left = X.Right;
            if (X.Right != null)
                (X.Right as RedBlackBinaryTreeNode<T>)._parent = node;
            if (X != null)
                X._parent = node._parent;
            if (node._parent == null)
                root = X;
            if (node._parent != null)
            {
                if (node == node._parent.Right as RedBlackBinaryTreeNode<T>)
                    node._parent.Right = X;
                if (node == node._parent.Left as RedBlackBinaryTreeNode<T>)
                    node._parent.Left = X;
            }
            X.Right = node;
            if (node != null)
                node._parent = X;
            return root;
        }

        #endregion
    }
}