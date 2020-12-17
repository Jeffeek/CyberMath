using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CyberMath.Structures.BinaryTree
{
    public class BinaryTree<T> : ICollection<T> 
        where T : IComparable<T>, IComparable
    {
        public TreeNode<T> Root { get; private set; }

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
                    Root = current.Left;
                }
                else
                {
                    int result = parent.CompareTo(current.Data);
                    if (result > 0)
                        parent.Left = current.Left;
                    else if (result < 0)
                    {
                        parent.Right = current.Left;
                    }
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (parent == null)
                {
                    Root = current.Right;
                } 
                else 
                { 
                    int result = parent.CompareTo(current.Data);
                    if (result > 0)
                    {
                        parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        parent.Right = current.Right;
                    }
                }
            }
            else 
            { 
                TreeNode<T> leftmost = current.Right.Left;
                TreeNode<T> leftmostParent = current.Right;
                while (leftmost.Left != null)
                {
                    leftmostParent = leftmost; leftmost = leftmost.Left;
                } 
                leftmostParent.Left = leftmost.Right;
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;
                if (parent == null)
                {
                    Root = leftmost;
                } 
                else 
                {
                    int result = parent.CompareTo(current.Data);
                    if (result > 0)
                    {
                        parent.Left = leftmost;
                    }
                    else if (result < 0)
                    {
                        parent.Right = leftmost;
                    }
                }
            }
            return true;
        }

        private TreeNode<T> FindWithParent(T value, out TreeNode<T> parent)
        {
            TreeNode<T> current = Root;
            parent = null;

            while (current != null)
            {
                int result = current.CompareTo(value);
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
                    break;
            }

            return current;
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(T data)
        {
            if (Root == null)
            {
                Root = new TreeNode<T>(data);
                Count = 1;
                return;
            }

            Root.Add(data);
            Count++;
        }

        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        public bool Contains(T item) => FindWithParent(item, out _) != null;

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= array.Length) return;
            var binaryTreeArrayInorder = Inorder();
            int currentBinaryTreeIndex = 0;
            for (int i = arrayIndex; i < array.Length; i++)
            {
                array[i] = binaryTreeArrayInorder[currentBinaryTreeIndex];
                currentBinaryTreeIndex++;
            }
        }

        public List<T> Preorder() => Root == null ? new List<T>() : Preorder(Root);
        public List<T> Postorder() => Root == null ? new List<T>() : Postorder(Root);
        public List<T> Inorder() => Root == null ? new List<T>() : Inorder(Root);

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

        public IEnumerator<T> GetEnumerator()
        {
            return Inorder().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
