using System;

namespace CyberMath.Structures.BinaryTree
{
    public sealed class TreeNode<T> where T : IComparable<T>, IComparable
    {
        public T Data { get; private set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }

        public TreeNode(T data)
        {
            Data = data;
        }

        public TreeNode(T data, TreeNode<T> left, TreeNode<T> right)
        {
            Data = data;
            Left = left;
            Right = right;
        }

        public void Add(T data)
        {
            var node = new TreeNode<T>(data);

            if (node.Data.CompareTo(Data) == -1)
            {
                if (Left == null)
                {
                    Left = node;
                }
                else
                {
                    Left.Add(data);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = node;
                }
                else
                {
                    Right.Add(data);
                }
            }
        }

        public int CompareTo(object obj)
        {
            if (obj is TreeNode<T> item)
            {
                return Data.CompareTo(item);
            }
            else
            {
                throw new ArgumentException("Не совпадение типов");
            }
        }

        public int CompareTo(T other)
        {
            return Data.CompareTo(other);
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
