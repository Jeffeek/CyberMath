using System;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace CyberMath.Structures.BinaryTreeBase
{
    public abstract class BinaryTreeNodeBase<T> : IBinaryTreeNode<T> 
        where T : IComparable<T>, IComparable
    {
        public IBinaryTreeNode<T> Left { get; set; }
        public IBinaryTreeNode<T> Right { get; set; }
        public T Data { get; protected set; }
        public abstract IBinaryTreeNode<T> Insert(T value);

        #region Constructors

        protected BinaryTreeNodeBase() { }
        protected BinaryTreeNodeBase(T data) => Data = data;

        #endregion

        #region CompareTo

        public int CompareTo([AllowNull] IBinaryTreeNode<T> other) => ReferenceEquals(this, other) ? 0 : Data.CompareTo(other.Data);

        public int CompareTo(object obj)
        {
            return obj switch
            {
                IBinaryTreeNode<T> other => CompareTo(other),
                _ => throw new ArgumentException("obj is not tree node")
            };
        }

        #endregion

        #region Equals

        public bool Equals([AllowNull] IBinaryTreeNode<T> other) => !ReferenceEquals(other, null) && CompareTo(other) == 0;

        public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is IBinaryTreeNode<T> other && Equals(other);

        #endregion

        #region Operators

        public static bool operator >(BinaryTreeNodeBase<T> first, BinaryTreeNodeBase<T> second) => first.CompareTo(second) == 1;

        public static bool operator <(BinaryTreeNodeBase<T> first, BinaryTreeNodeBase<T> second) => first.CompareTo(second) == -1;

        public static bool operator ==(BinaryTreeNodeBase<T> first, BinaryTreeNodeBase<T> second) =>
            first?.Equals(second) ?? ReferenceEquals(second, null); 

        public static bool operator !=(BinaryTreeNodeBase<T> first, BinaryTreeNodeBase<T> second) => !(first == second);

        #endregion

        public override string ToString() => Data.ToString();
    }
}