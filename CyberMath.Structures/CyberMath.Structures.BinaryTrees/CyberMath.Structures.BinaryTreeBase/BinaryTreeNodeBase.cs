using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CyberMath.Structures.BinaryTreeBase
{
    public abstract class BinaryTreeNodeBase<T> : IBinaryTreeNode<T> 
        where T : IComparable<T>, IComparable
    {
        public abstract IBinaryTreeNode<T> Left { get; set; }
        public abstract IBinaryTreeNode<T> Right { get; set; }
        public abstract void Add(T value);

        public T Data { get; }

        protected BinaryTreeNodeBase(T data)
        {
            Data = data;
        }
        
        public int CompareTo([AllowNull] IBinaryTreeNode<T> other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Data.CompareTo(other.Data);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null)) return 1;
            if (obj is IBinaryTreeNode<T> other)
                return CompareTo(other);
            throw new ArgumentException("obj is not tree node");
        }

        public bool Equals([AllowNull] IBinaryTreeNode<T> other) =>
            EqualityComparer<T>.Default.Equals(Data, other.Data) &&
            Equals(Left, other.Left) &&
            Equals(Right, other.Right);

        public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is IBinaryTreeNode<T> other && Equals(other);

        public override string ToString() => Data.ToString();
    }
}
