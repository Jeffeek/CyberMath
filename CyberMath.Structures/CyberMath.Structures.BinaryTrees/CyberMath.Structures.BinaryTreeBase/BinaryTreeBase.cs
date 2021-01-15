using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CyberMath.Structures.BinaryTreeBase
{
    /// <summary>
    /// Implementing of native <see cref="IBinaryTree{T}"/>
    /// </summary>
    /// <typeparam name="T"><see cref="IComparable{T}"/></typeparam>
    public abstract class BinaryTreeBase<T> : IBinaryTree<T>
           where T : IComparable, IComparable<T>
    {
        private bool _disposed;

        public IBinaryTreeNode<T> Root { get; protected set; }
        public int Count { get; protected set; }
        public bool IsEmpty => Count == 0;
        public bool IsReadOnly => false;
        public TraversalOrderType TraversalOrderType { get; set; }

        protected BinaryTreeBase(IEnumerable<T> values) => AddRange(values);

        /// <summary>
        /// Creates a new instance with 0 elements
        /// </summary>
        protected BinaryTreeBase() { }

        public int Depth()
        {
            if (IsEmpty) return -1;
            return Root.Depth();
        }

        #region ICollection

        public abstract void Add(T item);

        public bool Remove(T item)
        {
            if (ReferenceEquals(Root, null)) return false;
            if (!Contains(item)) return false;
            Root = Root.Remove(item);
            Count--;
            return true;
        }

        public void Clear()
        {
            Root = null;
            Count = 0;
        }

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

        public void AddRange(IEnumerable<T> values)
        {
            if (ReferenceEquals(values, null)) throw new ArgumentNullException(nameof(values));
            foreach (var value in values)
                Add(value);
        }

        public void MergeWith(IBinaryTree<T> binaryTree)
        {
            if (binaryTree == null) throw new Exception($"{nameof(binaryTree)} was null");
            foreach (var elem in binaryTree)
                Add(elem);
        }

        #endregion

        #region Enumeration

        public IEnumerator<T> GetEnumerator()
        {
            return TraversalOrderType switch
            {
                TraversalOrderType.Preorder => Preorder().GetEnumerator(),
                TraversalOrderType.Inorder => Inorder().GetEnumerator(),
                TraversalOrderType.Postorder => Postorder().GetEnumerator(),
                _ => throw new Exception("Something went wrong! Traversal strategy is not defined")
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Contains

        public bool Contains(T item) => InternalContains(Root, item);

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

        public T Max()
        {
            if (Root == null) throw new NullReferenceException("Tree is empty");
            return Root.Max().Data;
        }

        public T Min()
        {
            if (Root == null) throw new NullReferenceException("Tree is empty");
            return Root.Min().Data;
        }

        #endregion

        #region Dispose

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
