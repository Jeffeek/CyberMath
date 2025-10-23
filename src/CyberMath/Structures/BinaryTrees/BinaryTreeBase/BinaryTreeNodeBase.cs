#region Using namespaces

using System;
using System.Collections.Generic;

#endregion

namespace CyberMath.Structures.BinaryTrees.BinaryTreeBase
{
    /// <summary>
    ///     Implementing of native <see cref="IBinaryTreeNode{T}"/>
    /// </summary>
    /// <typeparam name="T">
    ///     <see cref="IComparable{T}"/>
    /// </typeparam>
    public abstract class BinaryTreeNodeBase<T> : IBinaryTreeNode<T>
        where T : IComparable<T>, IComparable
    {
        /// <inheritdoc/>
        public IBinaryTreeNode<T>? Left { get; protected set; }

        /// <inheritdoc/>
        public IBinaryTreeNode<T>? Right { get; protected set; }

        /// <inheritdoc/>
        public T Data { get; protected set; }

        /// <inheritdoc/>
        public abstract IBinaryTreeNode<T> Insert(T value);

        /// <inheritdoc/>
        public abstract IBinaryTreeNode<T>? Remove(T value);

        /// <inheritdoc/>
        public virtual int Depth() => InternalDepth(this);

        /// <inheritdoc/>
        public virtual IBinaryTreeNode<T>? Min()
        {
            var current = this as IBinaryTreeNode<T>;

            while (current.Left != null)
                current = current.Left;

            return current;
        }

        /// <inheritdoc/>
        public virtual IBinaryTreeNode<T>? Max()
        {
            var current = this as IBinaryTreeNode<T>;

            while (current.Right != null)
                current = current.Right;

            return current;
        }

        /// <summary>
        ///     Internal method to find the deep of <paramref name="node"/>
        /// </summary>
        /// <param name="node">Node where to search</param>
        /// <returns>The deep of <paramref name="node"/></returns>
        private static int InternalDepth(BinaryTreeNodeBase<T> node)
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

        /// <summary>
        ///     Finds node by <paramref name="value"/>
        /// </summary>
        /// <param name="subTree">A <see cref="IBinaryTreeNode{T}"/> subTree where to find</param>
        /// <param name="value">Value to find</param>
        /// <returns>Reference to the node with <paramref name="value"/></returns>
        protected virtual IBinaryTreeNode<T>? FindNode(IBinaryTreeNode<T> subTree, T value)
        {
            var current = subTree;

            while (current is object)
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

            return default;
        }

        /// <summary>
        ///     Finds the successor of <paramref name="node"/>
        /// </summary>
        /// <param name="node"><see cref="BinaryTreeNodeBase{T}"/> node from to find</param>
        /// <returns>Reference to the successor</returns>
        protected virtual IBinaryTreeNode<T>? GetSuccessor(BinaryTreeNodeBase<T> node)
        {
            var parentOfSuccessor = node;
            var successor = node;
            var current = node.Right as BinaryTreeNodeBase<T>;

            while (current != null!)
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

        /// <inheritdoc/>
        public override string ToString() => Data.ToString();

        #region Equality members

        /// <summary>
        ///     Shows the equality of initial <see cref="IBinaryTreeNode{T}"/> and <paramref name="other"/>
        /// </summary>
        /// <param name="other"></param>
        /// <returns>
        ///     <see langword="true"/> if this node equals to <paramref name="other"/>; otherwise <see langword="false"/>
        /// </returns>
        protected virtual bool Equals(BinaryTreeNodeBase<T> other) => this == other;

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var start = 228 ^ (315 * 25);
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            if (Left == null) start = (start + 100) ^ 25;
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            if (Right == null) start = (start + 200) ^ 55;
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            if (Data == null!) start = (start + 1000 * 5) ^ 300;

            return start;
        }

        /// <inheritdoc/>
        public bool Equals(IBinaryTreeNode<T>? other) => other is object && CompareTo(other) == 0;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is IBinaryTreeNode<T> other && Equals(other);

        #endregion

        #region Constructors

        /// <summary>
        ///     Empty constructor to create an empty <see cref="IBinaryTreeNode{T}"/>
        /// </summary>
        protected BinaryTreeNodeBase() { }

        /// <summary>
        ///     Create an instance of <see cref="IBinaryTreeNode{T}"/> with <paramref name="data"/>
        /// </summary>
        /// <param name="data"></param>
        protected BinaryTreeNodeBase(T data) => Data = data;

        #endregion

        #region CompareTo

        /// <inheritdoc/>
        public virtual int CompareTo(IBinaryTreeNode<T>? other)
            => other == null
                ? -1
                : ReferenceEquals(this, other)
                    ? 0
                    : Data.CompareTo(other.Data);

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        {
            return obj switch
            {
                IBinaryTreeNode<T> other => CompareTo(other),
                _ => throw new ArgumentException("obj is not tree node")
            };
        }

        #endregion

        #region Operators

        /// <summary>
        ///     Returns <see langword="true"/> if <paramref name="first"/> is greater than <paramref name="second"/>; otherwise
        ///     <see langword="false"/>
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator >(BinaryTreeNodeBase<T> first, BinaryTreeNodeBase<T> second) => first.CompareTo(second) == 1;

        /// <summary>
        ///     Returns <see langword="true"/> if <paramref name="second"/> is greater than <paramref name="first"/>; otherwise
        ///     <see langword="false"/>
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator <(BinaryTreeNodeBase<T> first, BinaryTreeNodeBase<T> second) => first.CompareTo(second) == -1;

        /// <summary>
        ///     Returns <see langword="true"/> if <paramref name="first"/> is equal by <see cref="Data"/> than
        ///     <paramref name="second"/>; otherwise <see langword="false"/>
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator ==(BinaryTreeNodeBase<T> first, BinaryTreeNodeBase<T> second)
        {
            if (ReferenceEquals(first, second)) return true;

            if (first is null) return false;

            if (second is null) return false;

            if (first.Data is null && second.Data is null) return true;

            if (first.Data is null) return false;

            if (second.Data is null) return false;

            return first.Data.CompareTo(second.Data) == 0;
        }

        /// <summary>
        ///     Returns <see langword="true"/> if <paramref name="first"/> is not equal by <see cref="Data"/> than
        ///     <paramref name="second"/>; otherwise <see langword="false"/>
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator !=(BinaryTreeNodeBase<T> first, BinaryTreeNodeBase<T> second) => !(first == second);

        #endregion
    }
}