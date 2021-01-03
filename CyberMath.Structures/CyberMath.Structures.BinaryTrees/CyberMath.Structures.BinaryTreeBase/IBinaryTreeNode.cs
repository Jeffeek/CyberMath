using System;

namespace CyberMath.Structures.BinaryTreeBase
{
    public interface IBinaryTreeNode<T> :
                                            IComparable<IBinaryTreeNode<T>>,
                                            IComparable,
                                            IEquatable<IBinaryTreeNode<T>> 
        where T : IComparable<T>, IComparable
    {
        IBinaryTreeNode<T> Left { get; set; }
        IBinaryTreeNode<T> Right { get; set; }
        T Data { get; }
        IBinaryTreeNode<T> Insert(T value);
    }
}
