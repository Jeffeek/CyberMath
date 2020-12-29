using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CyberMath.Structures.BinaryTreeBase;

namespace CyberMath.Structures.BinaryTree
{
    public class BinaryTree<T> : BinaryTreeBase<T>
        where T : IComparable<T>, IComparable
    {
        public BinaryTree(params T[] values)
        {
            AddRange(values);
        }

        public BinaryTree(IEnumerable<T> values)
        {
            AddRange(values.ToArray());
        }

        public BinaryTree() { }

        public override IBinaryTreeNode<T> Root { get; protected set; }
        
        public override void Add(T item)
        {
            if (Root == null)
            {
                Root = new BinaryTreeNode<T>(item);
                Count = 1;
                return;
            }

            Root.Add(item);
            Count++;
        }

        public override bool Remove(T item)
        {
            IBinaryTreeNode<T> current = Root;
            IBinaryTreeNode<T> parent = Root;
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
            IBinaryTreeNode<T> parentOfSuccessor = node;
            IBinaryTreeNode<T> successor = node;
            IBinaryTreeNode<T> current = node.Right;

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
    }
}