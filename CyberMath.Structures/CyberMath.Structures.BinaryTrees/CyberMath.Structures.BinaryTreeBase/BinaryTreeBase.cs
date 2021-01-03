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

        public IBinaryTreeNode<T> FindNode(T value)
        {
            var current = Root;
            while (!ReferenceEquals(current, null))
            {
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
            }

            return default;
        }
        
        protected BinaryTreeBase(params T[] values) => AddRange(values);

        protected BinaryTreeBase(IEnumerable<T> values) => AddRange(values.ToArray());

        protected BinaryTreeBase() { }
        
        public IBinaryTreeNode<T> Root { get; protected set; }
        
        public abstract void Add(T item);
        
        public virtual bool Remove(T item)
        {
            var current = Root;
            var parent = Root;
            bool isLeftChild = false;
            if (current == null) return false;
            while (current != null && current.Data.CompareTo(item) != 0)
            {
                parent = current;
                if (item?.CompareTo(current.Data) == -1)
                {
                    current = current.Left;
                    isLeftChild = true;
                }
                else
                {
                    current = current.Right;
                    isLeftChild = false;
                }
            }

            if (current == null)
                return false;

            switch (current.Right)
            {
                case null when current.Left == null:
                    {
                        if (current.Data.CompareTo(Root.Data) == 0)
                            Root = null;
                        else
                        {
                            if (isLeftChild)
                                parent.Left = null;
                            else
                                parent.Right = null;
                        }

                        break;
                    }
                case null when current.Data.CompareTo(Root.Data) == 0:
                    Root = current.Left;
                    break;
                case null when isLeftChild:
                    parent.Left = current.Left;
                    break;
                case null:
                    parent.Right = current.Left;
                    break;
                default:
                    {
                        if (current.Left == null)
                        {
                            if (current.Data.CompareTo(Root.Data) == 0)
                                Root = current.Right;
                            else
                            {
                                if (isLeftChild)
                                    parent.Left = current.Right;
                                else
                                    parent.Right = current.Right;
                            }
                        }
                        else
                        {
                            IBinaryTreeNode<T> successor = GetSuccessor(current);
                            if (current.Data.CompareTo(Root.Data) == 0)
                                Root = successor;
                            else if (isLeftChild)
                                parent.Left = successor;
                            else
                                parent.Right = successor;
                        }

                        break;
                    }
            }

            return true;
        }

        private IBinaryTreeNode<T> GetSuccessor(IBinaryTreeNode<T> node)
        {
            var parentOfSuccessor = node;
            var successor = node;
            var current = node.Right;

            while (current != null)
            {
                parentOfSuccessor = successor;
                successor = current;
                current = current.Left;
            }
            if (successor.CompareTo(node.Right) != 0)
            {
                parentOfSuccessor.Left = successor.Right;
                successor.Right = node.Right;
            }
            successor.Left = node.Left;

            return successor;
        }

        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        public bool Contains(T item) => InternalContains(Root, item);

        private bool InternalContains(IBinaryTreeNode<T> node, T data)
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

        public T Max()
        {
            if (Root == null) throw new NullReferenceException("Tree is empty");
            var current = Root;
            while (current.Right != null)
                current = current.Right;
            return current.Data;
        }

        public T Min()
        {
            if (Root == null) throw new NullReferenceException("Tree is empty");
            var current = Root;
            while (current.Left != null)
                current = current.Left;
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
