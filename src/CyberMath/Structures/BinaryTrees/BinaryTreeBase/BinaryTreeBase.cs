#region Using namespaces

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CyberMath.Structures.BinaryTrees.Exceptions;

#endregion

namespace CyberMath.Structures.BinaryTrees.BinaryTreeBase
{
    /// <summary>
    ///     Implementing of native <see cref="IBinaryTree{T}"/>
    /// </summary>
    /// <typeparam name="T">
    ///     <see cref="IComparable{T}"/>
    /// </typeparam>
    public abstract class BinaryTreeBase<T> : IBinaryTree<T>
        where T : IComparable, IComparable<T>
    {
        private bool _disposed;

        /// <summary>
        ///     Creates an instance and adds <paramref name="values"/> to the tree
        /// </summary>
        /// <param name="values">values to add</param>
        // ReSharper disable once VirtualMemberCallInConstructor
        protected BinaryTreeBase(IEnumerable<T> values) => AddRange(values);

        /// <summary>
        ///     Creates a new instance with 0 elements
        /// </summary>
        protected BinaryTreeBase() { }

        /// <inheritdoc/>
        public IBinaryTreeNode<T>? Root { get; protected set; }

        /// <inheritdoc/>
        public int Count { get; protected set; }

        /// <inheritdoc/>
        public bool IsEmpty => Count == 0;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public TraversalOrderType TraversalOrderType { get; set; }

        /// <inheritdoc/>
        public virtual int Depth() => Root?.Depth() ?? 0;

        #region ICollection

        /// <inheritdoc/>
        public abstract void Add(T item);

        /// <inheritdoc/>
        public virtual bool Remove(T item)
        {
            if (Root is null) return false;
            if (!Contains(item)) return false;

            Root = Root.Remove(item);
            Count--;

            return true;
        }

        /// <inheritdoc/>
        public virtual void Clear()
        {
            Root = null;
            Count = 0;
        }

        /// <inheritdoc/>
        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= array.Length) return;

            var binaryTreeArrayInorder = Inorder()
                .ToArray();

            var currentBinaryTreeIndex = 0;

            for (var i = arrayIndex; i < array.Length; i++)
            {
                array[i] = binaryTreeArrayInorder[currentBinaryTreeIndex];
                currentBinaryTreeIndex++;
            }
        }

        #endregion

        #region Add and Merge

        /// <inheritdoc/>
        public virtual void AddRange(IEnumerable<T> values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));

            foreach (var value in values)
                Add(value);
        }

        /// <inheritdoc/>
        public virtual void MergeWith(IBinaryTree<T> binaryTree)
        {
            if (binaryTree == null) throw new ArgumentNullException($"{nameof(binaryTree)} was null");

            foreach (var elem in binaryTree)
                Add(elem);
        }

        #endregion

        #region Enumeration

        /// <inheritdoc/>
        public virtual IEnumerator<T> GetEnumerator() =>
            // ReSharper disable once NotDisposedResourceIsReturned
            TraversalOrderType switch
            {
                TraversalOrderType.Preorder => Preorder().GetEnumerator(),
                TraversalOrderType.Inorder => Inorder().GetEnumerator(),
                TraversalOrderType.Postorder => Postorder().GetEnumerator(),
                _ => throw new UnknownTraversalTypeException<BinaryTreeBase<T>, T>(this, TraversalOrderType,
                    "Something went wrong! Traversal strategy is not defined")
            };

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Contains

        /// <inheritdoc/>
        public virtual bool Contains(T item) => InternalContains(Root, item);

        /// <summary>
        ///     Internal method for searching a <paramref name="data"/> in subtree <paramref name="node"/>
        /// </summary>
        /// <param name="node">Subtree where to search</param>
        /// <param name="data">A value to search</param>
        /// <returns><see langword="true"/> if <paramref name="data"/> exists in subtree</returns>
        private bool InternalContains(IBinaryTreeNode<T> node, T data)
        {
            while (true)
            {
                if (node is null) return false;

                switch (node.Data.CompareTo(data))
                {
                    case 0:
                        return true;

                    case 1:
                        node = node.Left;

                        continue;

                    default:
                        node = node.Right;

                        break;
                }
            }
        }

        #endregion

        #region Orders

        /// <inheritdoc/>
        public virtual IEnumerable<T> Inorder() => Root is null ? Enumerable.Empty<T>() : InternalInorder(Root);

        private static IEnumerable<T> InternalInorder(IBinaryTreeNode<T>? node)
        {
            var list = new List<T>();
            var stack = new Stack<IBinaryTreeNode<T>>();

            while (node != null || stack.Count > 0)
            {
                while (node != null)
                {
                    stack.Push(node);
                    node = node.Left;
                }

                node = stack.Pop();
                list.Add(node.Data);
                node = node.Right;
            }

            return list;
        }

        /// <inheritdoc/>
        public virtual IEnumerable<T> Preorder() => Root is null ? Enumerable.Empty<T>() : InternalPreorder(Root);

        private static IEnumerable<T> InternalPreorder(IBinaryTreeNode<T> node)
        {
            var list = new List<T>();
            var stack = new Stack<IBinaryTreeNode<T>>();
            stack.Push(node);

            while (stack.Count > 0)
            {
                var cur = stack.Pop();
                list.Add(cur.Data);

                if (cur.Right != null)
                    stack.Push(cur.Right);

                if (cur.Left != null)
                    stack.Push(cur.Left);
            }

            return list;
        }

        /// <inheritdoc/>
        public virtual IEnumerable<T> Postorder() => Root is null ? Enumerable.Empty<T>() : InternalPostorder(Root);

        private static IEnumerable<T> InternalPostorder(IBinaryTreeNode<T> node)
        {
            var list = new List<T>();
            var stack = new Stack<IBinaryTreeNode<T>>();
            stack.Push(node);
            IBinaryTreeNode<T> prev = null;

            while (stack.Count > 0)
            {
                var current = stack.Peek();

                if (prev == null || ReferenceEquals(prev.Left, current) || ReferenceEquals(prev.Right, current))
                {
                    if (current.Left != null)
                    {
                        stack.Push(current.Left);
                    }
                    else if (current.Right != null)
                    {
                        stack.Push(current.Right);
                    }
                    else
                    {
                        stack.Pop();
                        list.Add(current.Data);
                    }
                }
                else if (ReferenceEquals(current.Left, prev))
                {
                    if (current.Right != null)
                    {
                        stack.Push(current.Right);
                    }
                    else
                    {
                        stack.Pop();
                        list.Add(current.Data);
                    }
                }
                else if (ReferenceEquals(current.Right, prev))
                {
                    stack.Pop();
                    list.Add(current.Data);
                }

                prev = current;
            }

            return list;
        }

        #endregion

        #region MinMax

        /// <inheritdoc/>
        public virtual T Max()
        {
            if (Root == null) throw new EmptyTreeException<BinaryTreeBase<T>, T>(this);

            return Root.Max()
                       .Data;
        }

        /// <inheritdoc/>
        public virtual T Min()
        {
            if (Root == null) throw new EmptyTreeException<BinaryTreeBase<T>, T>(this);

            return Root.Min()
                       .Data;
        }

        #endregion

        #region Dispose

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            if (_disposed) return;

            Dispose(true);
            _disposed = true;
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;

            Clear();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}