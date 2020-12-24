using System;
using System.Collections;
using System.Collections.Generic;

namespace CyberMath.Structures.BinaryTree
{
    public class BinaryTree<T> : ICollection<T>, IDisposable, IReadOnlyCollection<T>
        where T : IComparable<T>, IComparable
    {
        private bool _disposed;

        private TreeNode<T> _root;

        public bool Remove(T item)
        {
            var current = FindWithParent(item, out var parent);
            if (current == null)
                return false;
            Count--;
            if (current.Right == null)
            {
                if (parent == null)
                {
                    _root = current.Left;
                }
                else
                {
                    var result = parent.CompareTo(current);
                    if (result > 0)
                        parent.Left = current.Left;
                    else if (result < 0) parent.Right = current.Left;
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (parent == null)
                {
                    _root = current.Right;
                }
                else
                {
                    var result = parent.CompareTo(current);
                    if (result > 0)
                        parent.Left = current.Right;
                    else if (result < 0) parent.Right = current.Right;
                }
            }
            else
            {
                var leftmost = current.Right.Left;
                var leftMostParent = current.Right;
                while (leftmost.Left != null)
                {
                    leftMostParent = leftmost;
                    leftmost = leftmost.Left;
                }

                leftMostParent.Left = leftmost.Right;
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;
                if (parent == null)
                {
                    _root = leftmost;
                }
                else
                {
                    var result = parent.CompareTo(current);
                    if (result > 0)
                        parent.Left = leftmost;
                    else if (result < 0)
                        parent.Right = leftmost;
                }
            }

            return true;
        }

        private TreeNode<T> FindWithParent(T value, out TreeNode<T> parent)
        {
            var current = _root;
            parent = null;

            while (current != null)
            {
                var result = current.Data.CompareTo(value);
                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(T data)
        {
            if (_root == null)
            {
                _root = new TreeNode<T>(data);
                Count = 1;
                return;
            }

            _root.Add(data);
            Count++;
        }

        public void Clear()
        {
            _root = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            return FindWithParent(item, out _) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= array.Length) return;
            var binaryTreeArrayInorder = Inorder();
            var currentBinaryTreeIndex = 0;
            for (var i = arrayIndex; i < array.Length; i++)
            {
                array[i] = binaryTreeArrayInorder[currentBinaryTreeIndex];
                currentBinaryTreeIndex++;
            }
        }

        public List<T> Preorder()
        {
            return _root == null ? new List<T>() : Preorder(_root);
        }

        public List<T> Postorder()
        {
            return _root == null ? new List<T>() : Postorder(_root);
        }

        public List<T> Inorder()
        {
            return _root == null ? new List<T>() : Inorder(_root);
        }

        public void MergeWith(BinaryTree<T> secondBinaryTree)
        {
            var elements = secondBinaryTree.Inorder();
            for (var i = 0; i < secondBinaryTree.Count; i++)
                Add(elements[i]);
        }

        private List<T> Preorder(TreeNode<T> node)
        {
            var list = new List<T>();
            if (node == null) return list;
            list.Add(node.Data);
            if (node.Left != null)
                list.AddRange(Preorder(node.Left));
            if (node.Right != null)
                list.AddRange(Preorder(node.Right));
            return list;
        }

        private List<T> Postorder(TreeNode<T> node)
        {
            var list = new List<T>();
            if (node == null) return list;
            if (node.Left != null)
                list.AddRange(Postorder(node.Left));
            if (node.Right != null)
                list.AddRange(Postorder(node.Right));
            list.Add(node.Data);
            return list;
        }

        private List<T> Inorder(TreeNode<T> node)
        {
            var list = new List<T>();
            if (node == null) return list;
            if (node.Left != null)
                list.AddRange(Inorder(node.Left));
            list.Add(node.Data);
            if (node.Right != null)
                list.AddRange(Inorder(node.Right));
            return list;
        }

        public T Max()
        {
            if (_root == null) throw new NullReferenceException("Tree is empty");
            var current = _root;
            while (current.Right != null)
            {
                current = current.Right;
            }

            return current.Data;
        }

        public T Min()
        {
            if (_root == null) throw new NullReferenceException("Tree is empty");
            var current = _root;
            while (current.Left != null)
            {
                current = current.Left;
            }

            return current.Data;
        }

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

        public void AddRange(params T[] elements)
        {
            foreach (var element in elements)
                Add(element);
        }
    }
}