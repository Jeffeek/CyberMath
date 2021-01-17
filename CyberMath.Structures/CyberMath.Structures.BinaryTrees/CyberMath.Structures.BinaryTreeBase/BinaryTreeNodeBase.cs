using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CyberMath.Structures.BinaryTreeBase
{
    /// <summary>
    /// Implementing of native <see cref="IBinaryTreeNode{T}"/>
    /// </summary>
    /// <typeparam name="T"><see cref="IComparable{T}"/></typeparam>
    public abstract class BinaryTreeNodeBase<T> : IBinaryTreeNode<T>
        where T : IComparable<T>, IComparable
    {
        #region Equality members

        protected bool Equals(BinaryTreeNodeBase<T> other) => this == other;

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var start = 228 ^ 315 * 25;
            if (Left == null) start = start + 100 ^ 25;
            if (Right == null) start = start + 200 ^ 55;
            if (Data == null) start = start + 1000 * 5 ^ 300;
            return start;
        }

        #endregion

        public IBinaryTreeNode<T> Left { get; protected set; }
        public IBinaryTreeNode<T> Right { get; protected set; }
        public T Data { get; protected set; }

        public abstract IBinaryTreeNode<T> Insert(T value);
        public abstract IBinaryTreeNode<T> Remove(T value);

        public int Depth() => InternalDepth(this);

        protected int InternalDepth(BinaryTreeNodeBase<T> node)
        {
            var levels = 0;
            var queue = new Queue<IBinaryTreeNode<T>>();
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                var count = queue.Count;
                for (var i = 0; i < count; i++)
                {
                    var tempNode = queue.Dequeue();
                    if (tempNode.Right != null)
                        queue.Enqueue(tempNode.Right);
                    if (tempNode.Left != null)
                        queue.Enqueue(tempNode.Left);
                }
                levels++;
            }
            return levels;
        }

        public IBinaryTreeNode<T> Min()
        {
            var current = this as IBinaryTreeNode<T>;
            while (current.Left != null)
                current = current.Left;
            return current;
        }

        public IBinaryTreeNode<T> Max()
        {
            var current = this as IBinaryTreeNode<T>;
            while (current.Right != null)
                current = current.Right;
            return current;
        }

        /// <summary>
        /// Finds node by <paramref name="value"/>
        /// </summary>
        /// <param name="subTree">A <see cref="IBinaryTreeNode{T}"/> subTree where to find</param>
        /// <param name="value">Value to find</param>
        /// <returns>Reference to the node with <paramref name="value"/></returns>
        protected IBinaryTreeNode<T> FindNode(IBinaryTreeNode<T> subTree, T value)
        {
            var current = subTree;
            while (!ReferenceEquals(current, null))
            {
                switch (current.Data.CompareTo(value))
                {
                    case 0:
                        return current;
                    case -1:
                        current = current.Right;
                        break;
                    default:
                        current = current.Left;
                        break;
                }
            }

            return default;
        }

        #region Constructors

        protected BinaryTreeNodeBase() { }
        protected BinaryTreeNodeBase(T data) => Data = data;

        #endregion

        /// <summary>
        /// Finds the successor of <paramref name="node"/>
        /// </summary>
        /// <param name="node"><see cref="BinaryTreeNodeBase{T}"/> node from to find</param>
        /// <returns>Reference to the successor</returns>
        protected IBinaryTreeNode<T> GetSuccessor(BinaryTreeNodeBase<T> node)
        {
            var parentOfSuccessor = node;
            var successor = node;
            var current = node.Right as BinaryTreeNodeBase<T>;

            while (current != null)
            {
                parentOfSuccessor = successor;
                successor = current;
                current = current.Left as BinaryTreeNodeBase<T>;
            }
            if (successor.CompareTo(node.Right) != 0)
            {
                parentOfSuccessor.Left = successor.Right;
                successor.Right = node.Right;
            }
            successor.Left = node.Left;

            return successor;
        }

        #region CompareTo

        public int CompareTo([AllowNull] IBinaryTreeNode<T> other) => ReferenceEquals(this, other) ? 0 : Data.CompareTo(other.Data);

        public int CompareTo(object obj)
        {
            return obj switch
            {
                IBinaryTreeNode<T> other => CompareTo(other),
                _ => throw new ArgumentException("obj is not tree node")
            };
        }

        #endregion

        #region Equals

        public bool Equals([AllowNull] IBinaryTreeNode<T> other) => !ReferenceEquals(other, null) && CompareTo(other) == 0;

        public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is IBinaryTreeNode<T> other && Equals(other);

        #endregion

        #region Operators

        public static bool operator >(BinaryTreeNodeBase<T> first, BinaryTreeNodeBase<T> second) => first.CompareTo(second) == 1;

        public static bool operator <(BinaryTreeNodeBase<T> first, BinaryTreeNodeBase<T> second) => first.CompareTo(second) == -1;

        public static bool operator ==(BinaryTreeNodeBase<T> first, BinaryTreeNodeBase<T> second)
        {
            if (ReferenceEquals(first, second)) return true;

            if(ReferenceEquals(first, null)) return false;

            if (ReferenceEquals(second, null)) return false;

            if (ReferenceEquals(first.Data, null) && ReferenceEquals(second.Data, null)) return true;

            if(ReferenceEquals(first.Data, null)) return false;

            if (ReferenceEquals(second.Data, null)) return false;

            return first.Data.CompareTo(second.Data) == 0;
        }

        public static bool operator !=(BinaryTreeNodeBase<T> first, BinaryTreeNodeBase<T> second) => !(first == second);

        #endregion

        public override string ToString() => Data.ToString();
    }
}