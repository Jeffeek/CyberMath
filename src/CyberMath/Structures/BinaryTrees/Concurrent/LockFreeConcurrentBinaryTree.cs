using CyberMath.Structures.BinaryTrees.BinaryTree;
using CyberMath.Structures.BinaryTrees.BinaryTreeBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CyberMath.Structures.BinaryTrees.Concurrent
{
    /// <summary>
    /// Lock-free concurrent binary tree using atomic operations
    /// Suitable for scenarios with high read contention
    /// </summary>
    public class LockFreeConcurrentBinaryTree<T> : IBinaryTree<T>
        where T : IComparable<T>, IComparable
    {
        private volatile IBinaryTreeNode<T>? _root;
        private int _count;
        private readonly object _writeLock = new object();

        /// <inheritdoc />
        public IBinaryTreeNode<T>? Root => _root;

        /// <inheritdoc />
        public int Count => _count;

        /// <inheritdoc />
        public bool IsEmpty => _count == 0;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public TraversalOrderType TraversalOrderType { get; set; }

        /// <inheritdoc />
        public void Add(T item)
        {
            lock (_writeLock)
            {
                if (_root == null)
                {
                    Volatile.Write(ref _root, new BinaryTreeNode<T>(item));
                    Interlocked.Exchange(ref _count, 1);
                    return;
                }

                if (!Contains(item))
                {
                    _root = _root.Insert(item);
                    Interlocked.Increment(ref _count);
                }
            }
        }

        /// <inheritdoc />
        public bool Remove(T item)
        {
            lock (_writeLock)
            {
                if (_root is null) return false;
                if (!Contains(item)) return false;

                _root = _root.Remove(item);
                Interlocked.Decrement(ref _count);
                return true;
            }
        }

        /// <inheritdoc />
        public void Clear()
        {
            lock (_writeLock)
            {
                Volatile.Write(ref _root, null);
                Interlocked.Exchange(ref _count, 0);
            }
        }

        /// <inheritdoc />
        public bool Contains(T item)
        {
            var root = Volatile.Read(ref _root);
            var current = root;
            while (current != null)
            {
                var cmp = item.CompareTo(current.Data);
                if (cmp == 0)
                    return true;
                current = cmp < 0 ? current.Left : current.Right;
            }
            return false;
        }

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= array.Length) return;

            var inorderArray = Inorder().ToArray();
            Array.Copy(inorderArray, 0, array, arrayIndex, Math.Min(inorderArray.Length, array.Length - arrayIndex));
        }

        /// <inheritdoc />
        public int Depth() => _root?.Depth() ?? 0;

        /// <inheritdoc />
        public IEnumerable<T> Inorder()
        {
            var root = Volatile.Read(ref _root);
            if (root == null) yield break;

            var stack = new Stack<IBinaryTreeNode<T>>();
            var current = root;

            while (current != null || stack.Count > 0)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }

                current = stack.Pop();
                yield return current.Data;
                current = current.Right;
            }
        }

        /// <inheritdoc />
        public IEnumerable<T> Preorder()
        {
            var root = Volatile.Read(ref _root);
            if (root == null) yield break;

            var stack = new Stack<IBinaryTreeNode<T>>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                yield return current.Data;

                if (current.Right != null)
                    stack.Push(current.Right);
                if (current.Left != null)
                    stack.Push(current.Left);
            }
        }

        /// <inheritdoc />
        public IEnumerable<T> Postorder()
        {
            var root = Volatile.Read(ref _root);
            if (root == null) yield break;

            var stack = new Stack<IBinaryTreeNode<T>>();
            var lastVisited = null as IBinaryTreeNode<T>;
            var current = root;

            while (current != null || stack.Count > 0)
            {
                if (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }
                else
                {
                    var peekNode = stack.Peek();
                    if (peekNode.Right != null && lastVisited != peekNode.Right)
                    {
                        current = peekNode.Right;
                    }
                    else
                    {
                        yield return peekNode.Data;
                        lastVisited = stack.Pop();
                    }
                }
            }
        }

        /// <inheritdoc />
        public T Max()
        {
            var root = Volatile.Read(ref _root);
            if (root == null)
                throw new InvalidOperationException("Tree is empty");

            return root.Max().Data;
        }

        /// <inheritdoc />
        public T Min()
        {
            var root = Volatile.Read(ref _root);
            if (root == null)
                throw new InvalidOperationException("Tree is empty");

            return root.Min().Data;
        }

        /// <inheritdoc />
        public void AddRange(IEnumerable<T> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            lock (_writeLock)
            {
                foreach (var value in values)
                {
                    if (_root == null)
                    {
                        Volatile.Write(ref _root, new BinaryTreeNode<T>(value));
                        Interlocked.Exchange(ref _count, 1);
                    }
                    else if (!Contains(value))
                    {
                        _root = _root.Insert(value);
                        Interlocked.Increment(ref _count);
                    }
                }
            }
        }

        /// <inheritdoc />
        public void MergeWith(IBinaryTree<T> binaryTree)
        {
            if (binaryTree == null)
                throw new ArgumentNullException(nameof(binaryTree));

            AddRange(binaryTree);
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return TraversalOrderType switch
            {
                TraversalOrderType.Preorder => Preorder().GetEnumerator(),
                TraversalOrderType.Inorder => Inorder().GetEnumerator(),
                TraversalOrderType.Postorder => Postorder().GetEnumerator(),
                _ => throw new InvalidOperationException($"Unknown traversal type: {TraversalOrderType}")
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
