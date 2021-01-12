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
        bool IsEmpty { get; }
        int Depth();
        IEnumerable<T> Inorder();
        IEnumerable<T> Preorder();
        IEnumerable<T> Postorder();
        T Max();
        T Min();
        void AddRange(IEnumerable<T> values);
        void MergeWith(IBinaryTree<T> binaryTree);
    }
}
