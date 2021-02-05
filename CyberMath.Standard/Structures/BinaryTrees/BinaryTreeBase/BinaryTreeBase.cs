using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CyberMath.Structures.BinaryTrees.BinaryTreeBase
{
    /// <summary>
    /// Implementing of native <see cref="IBinaryTree{T}"/>
    /// </summary>
    /// <typeparam name="T"><see cref="IComparable{T}"/></typeparam>
    public abstract class BinaryTreeBase<T> : IBinaryTree<T>
           where T : IComparable, IComparable<T>
    {
        private bool _disposed;

        /// <inheritdoc />
        public IBinaryTreeNode<T> Root { get; protected set; }

        /// <inheritdoc />
        public int Count { get; protected set; }

        /// <inheritdoc />
        public bool IsEmpty => Count == 0;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public TraversalOrderType TraversalOrderType { get; set; }

        /// <summary>
        /// Creates an instance and adds <paramref name="values"/> to the tree
        /// </summary>
        /// <param name="values">values to add</param>
        protected BinaryTreeBase(IEnumerable<T> values) => AddRange(values);

        /// <summary>
        /// Creates a new instance with 0 elements
        /// </summary>
        protected BinaryTreeBase() { }

        /// <inheritdoc />
        public int Depth()
        {
            if (IsEmpty) return -1;
            return Root.Depth();
        }

        #region ICollection

        /// <inheritdoc />
        public abstract void Add(T item);

        /// <inheritdoc />
        public bool Remove(T item)
        {
            if (ReferenceEquals(Root, null)) return false;
            if (!Contains(item)) return false;
            Root = Root.Remove(item);
            Count--;
            return true;
        }

        /// <inheritdoc />
        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= array.Length) return;
            var binaryTreeArrayInorder = Inorder().ToArray();
            var currentBinaryTreeIndex = 0;
            for (var i = arrayIndex; i < array.Length; i++)
            {
                array[i] = binaryTreeArrayInorder[currentBinaryTreeIndex];
                currentBinaryTreeIndex++;
            }
        }

        #endregion

        #region Add and Merge

        /// <inheritdoc />
        public void AddRange(IEnumerable<T> values)
        {
            if (ReferenceEquals(values, null)) throw new ArgumentNullException(nameof(values));
            foreach (var value in values)
                Add(value);
        }

        /// <inheritdoc />
        public void MergeWith(IBinaryTree<T> binaryTree)
        {
            if (binaryTree == null) throw new Exception($"{nameof(binaryTree)} was null");
            foreach (var elem in binaryTree)
                Add(elem);
        }

        #endregion

        #region Enumeration

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            switch (TraversalOrderType)
            {
                case TraversalOrderType.Preorder:
                    return Preorder().GetEnumerator();

                case TraversalOrderType.Inorder:
                    return Inorder().GetEnumerator();

                case TraversalOrderType.Postorder:
                    return Postorder().GetEnumerator();

                default:
                    throw new Exception("Something went wrong! Traversal strategy is not defined");
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Contains

        /// <inheritdoc />
        public bool Contains(T item) => InternalContains(Root, item);

        /// <summary>
        /// Internal method for searching a <paramref name="data"/> in subtree <paramref name="node"/>
        /// </summary>
        /// <param name="node">Subtree where to search</param>
        /// <param name="data">A value to search</param>
        /// <returns><see langword="true"/> if <paramref name="data"/> exists in subtree</returns>
        protected bool InternalContains(IBinaryTreeNode<T> node, T data)
        {
            while (true)
            {
                if (ReferenceEquals(node, null)) return false;
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

        /// <inheritdoc />
        public IEnumerable<T> Inorder() => ReferenceEquals(Root, null) ? Enumerable.Empty<T>() : InternalInorder(Root);

        private IEnumerable<T> InternalInorder(IBinaryTreeNode<T> node)
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

        /// <inheritdoc />
        public IEnumerable<T> Preorder() => ReferenceEquals(Root, null) ? Enumerable.Empty<T>() : InternalPreorder(Root);

        private IEnumerable<T> InternalPreorder(IBinaryTreeNode<T> node)
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

        /// <inheritdoc />
        public IEnumerable<T> Postorder() => ReferenceEquals(Root, null) ? Enumerable.Empty<T>() : InternalPostorder(Root);

        private IEnumerable<T> InternalPostorder(IBinaryTreeNode<T> node)
        {
            var list = new List<T>();
            var stack = new Stack<IBinaryTreeNode<T>>();
            stack.Push(node);
            IBinaryTreeNode<T> prev = null;
            while (stack.Count > 0)
            {
                var current = stack.Peek();

                if (prev == null || ReferenceEquals(prev.Left, current) ||
                    ReferenceEquals(prev.Right, current))
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

        /// <inheritdoc />
        public T Max()
        {
            if (Root == null) throw new NullReferenceException("Tree is empty");
            return Root.Max().Data;
        }

        /// <inheritdoc />
        public T Min()
        {
            if (Root == null) throw new NullReferenceException("Tree is empty");
            return Root.Min().Data;
        }

        #endregion

        #region Dispose

        /// <inheritdoc />
        public void Dispose()
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
