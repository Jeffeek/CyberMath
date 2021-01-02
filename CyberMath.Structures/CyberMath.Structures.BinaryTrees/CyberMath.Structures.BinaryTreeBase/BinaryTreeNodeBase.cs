using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CyberMath.Structures.BinaryTreeBase
{
    public abstract class BinaryTreeNodeBase<T> : IBinaryTreeNode<T> 
        where T : IComparable<T>, IComparable
    {
        public IBinaryTreeNode<T> Left { get; set; }
        public IBinaryTreeNode<T> Right { get; set; }
        
        public abstract void Add(T value);
        public T Data { get; }

        protected BinaryTreeNodeBase(T data)
        {
            Data = data;
        }
        
        public int CompareTo([AllowNull] IBinaryTreeNode<T> other) => ReferenceEquals(this, other) ? 0 : ReferenceEquals(null, other) ? 1 : Data.CompareTo(other.Data);

        public int CompareTo(object obj)
        {
            return obj switch
            {
                null => 1,
                IBinaryTreeNode<T> other => CompareTo(other),
                _ => throw new ArgumentException("obj is not tree node")
            };
        }

        public bool Equals([AllowNull] IBinaryTreeNode<T> other)
        {
            return !ReferenceEquals(other, null) && Comparer<T>.Default.Compare(Data, other.Data) == 0;
        }

        public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is IBinaryTreeNode<T> other && Equals(other);

        public static bool operator ==(BinaryTreeNodeBase<T> first, BinaryTreeNodeBase<T> second) =>
            first?.Equals(second) ?? ReferenceEquals(second, null); 

        public static bool operator !=(BinaryTreeNodeBase<T> first, BinaryTreeNodeBase<T> second) => !(first == second);

        public override string ToString() => Data.ToString();
    }
}
