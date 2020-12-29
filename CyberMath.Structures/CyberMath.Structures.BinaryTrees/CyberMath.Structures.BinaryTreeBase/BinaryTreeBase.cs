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

        public abstract IBinaryTreeNode<T> Root { get; protected set; }
        public abstract void Add(T item);
        public abstract bool Remove(T item);

        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            if (ReferenceEquals(Root, null)) return false;
            return InternalContains(Root, item);
        }

        private bool InternalContains(IBinaryTreeNode<T> node, T data)
        {
            if (ReferenceEquals(node, null)) return false;
            if (node.Data.CompareTo(data) == 0) return true;
            if (node.Data.CompareTo(data) == 1) return InternalContains(node.Left, data);
            if (node.Data.CompareTo(data) == -1) return InternalContains(node.Right, data);
            return false;
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

        public int Count { get; protected set; }
        
        public bool IsReadOnly => false;
        
        public IEnumerator<T> GetEnumerator()
        {
            return Inorder().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

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

        public IEnumerable<T> Inorder() => Root == null ? Enumerable.Empty<T>() : InternalInorder(Root);

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

        public IEnumerable<T> Preorder() => Root == null ? Enumerable.Empty<T>() : InternalPreorder(Root);

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

        public IEnumerable<T> Postorder() => Root == null ? Enumerable.Empty<T>() : InternalPostorder(Root);

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

        public T Max()
        {
            if (Root == null) throw new NullReferenceException("Tree is empty");
            var current = Root;
            while (current.Right != null)
            {
                current = current.Right;
            }

            return current.Data;
        }

        protected BinaryTreeBase(params T[] values)
        {
            AddRange(values);
        }

        protected BinaryTreeBase(IEnumerable<T> values)
        {
            AddRange(values.ToArray());
        }

        protected BinaryTreeBase()
        {
            
        }

        public T Min()
        {
            if (Root == null) throw new NullReferenceException("Tree is empty");
            var current = Root;
            while (current.Left != null)
            {
                current = current.Right;
            }

            return current.Data;
        }

        public void AddRange(params T[] values)
        {
            foreach (var element in values)
                Add(element);
        }

        public void MergeWith(IBinaryTree<T> binaryTree)
        {
            var elements = binaryTree.Inorder();
            foreach (var element in elements)
                Add(element);
        }
    }
}
