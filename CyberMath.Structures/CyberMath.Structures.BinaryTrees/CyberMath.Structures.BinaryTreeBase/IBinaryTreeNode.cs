using System;

namespace CyberMath.Structures.BinaryTreeBase
{
    public interface IBinaryTreeNode<T> :
                                            IComparable<IBinaryTreeNode<T>>,
                                            IComparable,
                                            IEquatable<IBinaryTreeNode<T>> 
        where T : IComparable<T>, IComparable
    {
        IBinaryTreeNode<T> Left { get; }
        IBinaryTreeNode<T> Right { get; }
        T Data { get; }
        int Depth();
        IBinaryTreeNode<T> Insert(T value);
        IBinaryTreeNode<T> Remove(T value);
        IBinaryTreeNode<T> Min();
        IBinaryTreeNode<T> Max();
    }
}
