using System;
using System.Collections.Generic;

namespace CyberMath.Structures.BinaryTreeBase
{
    public interface IBinaryTree<T> : 
                                    ICollection<T>, 
                                    IDisposable
        where T : IComparable<T>, IComparable
    {
        IBinaryTreeNode<T> Root { get; }
        IBinaryTreeNode<T> FindNode(T value);
        IEnumerable<T> Inorder();
        IEnumerable<T> Preorder();
        IEnumerable<T> Postorder();
        T Max();
        T Min();
        void AddRange(params T[] values);
        void MergeWith(IBinaryTree<T> binaryTree);
    }
}
