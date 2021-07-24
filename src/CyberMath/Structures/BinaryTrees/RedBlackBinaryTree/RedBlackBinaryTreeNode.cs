#region Using namespaces

using System;
using CyberMath.Structures.BinaryTrees.BinaryTreeBase;

#endregion

// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable PossibleNullReferenceException

namespace CyberMath.Structures.BinaryTrees.RedBlackBinaryTree
{
    /// <summary>
    ///     Represents Red-Black Binary Tree NODE. Implement <see cref="BinaryTreeNodeBase{T}"/>
    /// </summary>
    /// <typeparam name="T">
    ///     <see cref="IComparable{T}"/>
    /// </typeparam>
    public class RedBlackBinaryTreeNode<T> : BinaryTreeNodeBase<T>
        where T : IComparable, IComparable<T>
    {
        /// <summary>
        ///     Color of initial <see cref="RedBlackBinaryTreeNode{T}"/>
        /// </summary>
        private BinaryTreeColor _color = BinaryTreeColor.Black;

        /// <summary>
        ///     Reference to the parent of the initial node
        /// </summary>
        private RedBlackBinaryTreeNode<T> _parent;

        /// <inheritdoc/>
        public RedBlackBinaryTreeNode(T data) : base(data) { }

        #region Relatives

        /// <summary>
        ///     Finds uncle of <paramref name="node"/>
        /// </summary>
        /// <param name="node"></param>
        /// <returns>Uncle of <paramref name="node"/></returns>
        private RedBlackBinaryTreeNode<T> GetUncle(RedBlackBinaryTreeNode<T> node)
        {
            var parent = node._parent;

            return GetSibling(parent);
        }

        /// <summary>
        ///     Finds sibling of <paramref name="node"/>
        /// </summary>
        /// <param name="node"></param>
        /// <returns>Sibling of <paramref name="node"/></returns>
        private RedBlackBinaryTreeNode<T> GetSibling(RedBlackBinaryTreeNode<T> node)
        {
            var parent = node._parent;

            return parent == null
                       ? null
                       : node.Equals(parent.Left)
                           ? parent.Right as RedBlackBinaryTreeNode<T>
                           : parent.Left as RedBlackBinaryTreeNode<T>;
        }

        /// <summary>
        ///     Finds grandparent of <paramref name="node"/>
        /// </summary>
        /// <param name="node"></param>
        /// <returns>Grandparent of <paramref name="node"/></returns>
        private RedBlackBinaryTreeNode<T> GetGrandparent(RedBlackBinaryTreeNode<T> node) => node?._parent?._parent;

        #endregion

        #region FixUp Methods

        /// <summary>
        ///     Fix up after inserting
        /// </summary>
        /// <param name="root">Reference to root</param>
        /// <param name="nodeX">Reference to node which was inserted</param>
        /// <returns>Reference to root</returns>
        private RedBlackBinaryTreeNode<T> InsertFixUp(RedBlackBinaryTreeNode<T> root, RedBlackBinaryTreeNode<T> nodeX)
        {
            while (nodeX != root && nodeX._parent._color == BinaryTreeColor.Red)
            {
                if (nodeX._parent == nodeX._parent._parent.Left as RedBlackBinaryTreeNode<T>)
                {
                    var nodeY = nodeX._parent._parent.Right as RedBlackBinaryTreeNode<T>;

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
                    var x = nodeX._parent._parent.Left as RedBlackBinaryTreeNode<T>;

                    if (x != null && x._color == BinaryTreeColor.Red)
                    {
                        nodeX._parent._color = BinaryTreeColor.Black;
                        x._color = BinaryTreeColor.Black;
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

        /// <summary>
        ///     Fix up after removing
        /// </summary>
        /// <param name="root">Reference to root</param>
        /// <param name="nodeX">Reference to node which helps to fix up</param>
        /// <returns>Reference to root</returns>
        private RedBlackBinaryTreeNode<T> DeleteFixUp(RedBlackBinaryTreeNode<T> root, RedBlackBinaryTreeNode<T> nodeX)
        {
            while (nodeX != null && nodeX != root && nodeX._color == BinaryTreeColor.Black)
                if (nodeX == nodeX._parent.Left as RedBlackBinaryTreeNode<T>)
                {
                    var nodeW = nodeX._parent.Right as RedBlackBinaryTreeNode<T>;

                    if (nodeW._color == BinaryTreeColor.Red)
                    {
                        nodeW._color = BinaryTreeColor.Black;
                        nodeX._parent._color = BinaryTreeColor.Red;
                        root = RotateLeft(root, nodeX._parent);
                        nodeW = nodeX._parent.Right as RedBlackBinaryTreeNode<T>;
                    }

                    if ((nodeW?.Left as RedBlackBinaryTreeNode<T>)?._color == BinaryTreeColor.Black
                        && (nodeW.Right as RedBlackBinaryTreeNode<T>)?._color == BinaryTreeColor.Black)
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
                    var w = nodeX._parent.Left as RedBlackBinaryTreeNode<T>;

                    if (w._color == BinaryTreeColor.Red)
                    {
                        w._color = BinaryTreeColor.Black;
                        nodeX._parent._color = BinaryTreeColor.Red;
                        root = RotateRight(root, nodeX._parent);
                        w = nodeX._parent.Left as RedBlackBinaryTreeNode<T>;
                    }

                    if ((w.Right as RedBlackBinaryTreeNode<T>)._color == BinaryTreeColor.Black
                        && (w.Left as RedBlackBinaryTreeNode<T>)._color == BinaryTreeColor.Black)
                    {
                        w._color = BinaryTreeColor.Black;
                        nodeX = nodeX._parent;
                    }
                    else if ((w.Left as RedBlackBinaryTreeNode<T>)._color == BinaryTreeColor.Black)
                    {
                        (w.Right as RedBlackBinaryTreeNode<T>)._color = BinaryTreeColor.Black;
                        w._color = BinaryTreeColor.Red;
                        root = RotateLeft(root, w);
                        w = nodeX._parent.Left as RedBlackBinaryTreeNode<T>;
                    }

                    w._color = nodeX._parent._color;
                    nodeX._parent._color = BinaryTreeColor.Black;
                    (w.Left as RedBlackBinaryTreeNode<T>)._color = BinaryTreeColor.Black;
                    root = RotateRight(root, nodeX._parent);
                    nodeX = root;
                }

            if (nodeX != null)
                nodeX._color = BinaryTreeColor.Black;

            return root;
        }

        #endregion

        #region Insert

        /// <inheritdoc/>
        public override IBinaryTreeNode<T> Insert(T value) => InternalInsert(this, value);

        private RedBlackBinaryTreeNode<T> InternalInsert(RedBlackBinaryTreeNode<T> root, T value)
        {
            var newNode = new RedBlackBinaryTreeNode<T>(value);
            RedBlackBinaryTreeNode<T> nodeY = null;
            var nodeX = root;

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

        /// <inheritdoc/>
        public override IBinaryTreeNode<T> Remove(T value) => InternalRemove(this, value);

        private RedBlackBinaryTreeNode<T> InternalRemove(RedBlackBinaryTreeNode<T> root, T value)
        {
            var item = FindNode(root, value) as RedBlackBinaryTreeNode<T>;
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

        /// <summary>
        ///     Left rotate of <paramref name="node"/>
        /// </summary>
        /// <param name="root">Reference to root</param>
        /// <param name="node">Reference to node to rotate</param>
        /// <returns>Reference to <paramref name="root"/></returns>
        private RedBlackBinaryTreeNode<T> RotateLeft(RedBlackBinaryTreeNode<T> root, RedBlackBinaryTreeNode<T> node)
        {
            var nodeY = node.Right as RedBlackBinaryTreeNode<T>;
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

        /// <summary>
        ///     Right rotate of <paramref name="node"/>
        /// </summary>
        /// <param name="root">Reference to root</param>
        /// <param name="node">Reference to node to rotate</param>
        /// <returns>Reference to <paramref name="root"/></returns>
        private RedBlackBinaryTreeNode<T> RotateRight(RedBlackBinaryTreeNode<T> root, RedBlackBinaryTreeNode<T> node)
        {
            var X = node.Left as RedBlackBinaryTreeNode<T>;
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