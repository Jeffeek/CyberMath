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
    /// Thread-safe implementation of Binary Tree using ReaderWriterLockSlim
    /// </summary>
    /// <typeparam name="T">Type that implements IComparable{T}</typeparam>
    public class ConcurrentBinaryTree<T> : IBinaryTree<T>
        where T : IComparable<T>, IComparable
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private IBinaryTreeNode<T>? _root;
        private int _count;
        private bool _disposed;

        public ConcurrentBinaryTree()
        {
        }

        public ConcurrentBinaryTree(params T[] values) : this(values.AsEnumerable())
        {
        }

        public ConcurrentBinaryTree(IEnumerable<T> values)
        {
            AddRange(values);
        }

        /// <inheritdoc />
        public IBinaryTreeNode<T>? Root
        {
            get
            {
                _lock.EnterReadLock();
                try
                {
                    return _root;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
            protected set
            {
                _lock.EnterWriteLock();
                try
                {
                    _root = value;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
        }

        /// <inheritdoc />
        public int Count
        {
            get
            {
                _lock.EnterReadLock();
                try
                {
                    return _count;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
            protected set
            {
                _lock.EnterWriteLock();
                try
                {
                    _count = value;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
        }

        /// <inheritdoc />
        public bool IsEmpty
        {
            get
            {
                _lock.EnterReadLock();
                try
                {
                    return _count == 0;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
        }

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public TraversalOrderType TraversalOrderType { get; set; }

        /// <inheritdoc />
        public void Add(T item)
        {
            _lock.EnterWriteLock();
            try
            {
                if (_root == null)
                {
                    _root = new BinaryTreeNode<T>(item);
                    _count = 1;
                    return;
                }

                if (InternalContains(item)) return;

                _root = _root.Insert(item);
                _count++;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <inheritdoc />
        public bool Remove(T item)
        {
            _lock.EnterWriteLock();
            try
            {
                if (_root is null) return false;
                if (!InternalContains(item)) return false;

                _root = _root.Remove(item);
                _count--;
                return true;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <inheritdoc />
        public void Clear()
        {
            _lock.EnterWriteLock();
            try
            {
                _root = null;
                _count = 0;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <inheritdoc />
        public bool Contains(T item)
        {
            _lock.EnterReadLock();
            try
            {
                return InternalContains(item);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        private bool InternalContains(T item)
        {
            var current = _root;
            while (current != null)
            {
                int cmp = item.CompareTo(current.Data);
                if (cmp == 0)
                    return true;
                else if (cmp < 0)
                    current = current.Left;
                else
                    current = current.Right;
            }
            return false;
        }

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex)
        {
            _lock.EnterReadLock();
            try
            {
                if (arrayIndex < 0 || arrayIndex >= array.Length) return;

                var inorderArray = Inorder().ToArray();
                var currentIndex = 0;

                for (var i = arrayIndex; i < array.Length && currentIndex < inorderArray.Length; i++)
                {
                    array[i] = inorderArray[currentIndex++];
                }
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <inheritdoc />
        public int Depth()
        {
            _lock.EnterReadLock();
            try
            {
                return _root?.Depth() ?? 0;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <inheritdoc />
        public IEnumerable<T> Inorder()
        {
            _lock.EnterReadLock();
            try
            {
                if (_root == null) yield break;

                var stack = new Stack<IBinaryTreeNode<T>>();
                var current = _root;

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
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <inheritdoc />
        public IEnumerable<T> Preorder()
        {
            _lock.EnterReadLock();
            try
            {
                if (_root == null) yield break;

                var stack = new Stack<IBinaryTreeNode<T>>();
                stack.Push(_root);

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
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <inheritdoc />
        public IEnumerable<T> Postorder()
        {
            _lock.EnterReadLock();
            try
            {
                if (_root == null) yield break;

                var stack = new Stack<IBinaryTreeNode<T>>();
                var lastVisited = null as IBinaryTreeNode<T>;
                var current = _root;

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
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <inheritdoc />
        public T Max()
        {
            _lock.EnterReadLock();
            try
            {
                if (_root == null)
                    throw new InvalidOperationException("Tree is empty");

                return _root.Max().Data;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <inheritdoc />
        public T Min()
        {
            _lock.EnterReadLock();
            try
            {
                if (_root == null)
                    throw new InvalidOperationException("Tree is empty");

                return _root.Min().Data;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <inheritdoc />
        public void AddRange(IEnumerable<T> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            // Acquire lock once for the entire operation
            _lock.EnterWriteLock();
            try
            {
                foreach (var value in values)
                {
                    if (_root == null)
                    {
                        _root = new BinaryTreeNode<T>(value);
                        _count = 1;
                    }
                    else
                    {
                        if (!InternalContains(value))
                        {
                            _root = _root.Insert(value);
                            _count++;
                        }
                    }
                }
            }
            finally
            {
                _lock.ExitWriteLock();
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _lock?.Dispose();
            }

            _disposed = true;
        }
    }
}
