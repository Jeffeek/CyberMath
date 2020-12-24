using System;
using System.Collections.Generic;

namespace CyberMath.Structures.BinaryTree
{
    internal sealed class TreeNode<T> : 
                                        IComparable<TreeNode<T>>,
                                        IComparable,
                                        IEquatable<TreeNode<T>>
        where T : IComparable<T>, IComparable
    {
        public T Data { get; private set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }

        public TreeNode(T data) => Data = data;

        [Obsolete]
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
                    Left = node;
                else
                    Left.Add(data);
            }
            else
            {
                if (Right == null)
                    Right = node;
                else
                    Right.Add(data);
            }
        }

        public int CompareTo(object obj)
        {
            if (obj is TreeNode<T> item)
                return Data.CompareTo(item.Data);

            throw new ArgumentException("obj is not tree node");
        }

        

        public int CompareTo(TreeNode<T> other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Data.CompareTo(other.Data);
        }
        
        public override string ToString() => Data.ToString();

        public bool Equals(TreeNode<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Data, other.Data) && Equals(Left, other.Left) && Equals(Right, other.Right);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is TreeNode<T> other && Equals(other);
        }
    }
}
