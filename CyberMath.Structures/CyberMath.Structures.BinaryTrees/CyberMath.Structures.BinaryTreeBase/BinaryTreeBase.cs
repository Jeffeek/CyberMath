using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CyberMath.Structures.BinaryTreeBase
{
    public abstract class BinaryTreeBase<T> : IBinaryTree<T> 
           where T : IComparable, IComparable<T>
    {
        private bool _disposed = false;
        
        public IBinaryTreeNode<T> Root { get; protected set; }
        public int Count { get; protected set; }
        public bool IsReadOnly => false;
        public TraversalOrderType TraversalOrderType { get; set; }
        
        protected BinaryTreeBase(params T[] values) => AddRange(values);
        protected BinaryTreeBase(IEnumerable<T> values) => AddRange(values.ToArray());
        protected BinaryTreeBase() { }

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

        #region Add and Merge

        public void AddRange(params T[] values)
        {
            foreach (var element in values)
                Add(element);
        }

        public void MergeWith(IBinaryTree<T> binaryTree)
        {
            if (binaryTree == null) throw new Exception("binaryTree was null");
            var elements = binaryTree.Inorder();
            AddRange(elements.ToArray());
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

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
            if (node == null) return list;
            if (node.Left != null)
                list.AddRange(InternalInorder(node.Left));
            list.Add(node.Data);
            if (node.Right != null)
                list.AddRange(InternalInorder(node.Right));
            return list;
        }

        public IEnumerable<T> Preorder() => ReferenceEquals(Root, null) ? Enumerable.Empty<T>() : InternalPreorder(Root);

        private IEnumerable<T> InternalPreorder(IBinaryTreeNode<T> node)
        {
            var list = new List<T>();
            if (node == null) return list;
            list.Add(node.Data);
            if (node.Left != null)
                list.AddRange(InternalPreorder(node.Left));
            if (node.Right != null)
                list.AddRange(InternalPreorder(node.Right));
            return list.AsEnumerable();
        }

        public IEnumerable<T> Postorder() => ReferenceEquals(Root, null) ? Enumerable.Empty<T>() : InternalPostorder(Root);

        private IEnumerable<T> InternalPostorder(IBinaryTreeNode<T> node)
        {
            var list = new List<T>();
            if (node == null) return list;
            if (node.Left != null)
                list.AddRange(InternalPostorder(node.Left));
            if (node.Right != null)
                list.AddRange(InternalPostorder(node.Right));
            list.Add(node.Data);
            return list.AsEnumerable();
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
