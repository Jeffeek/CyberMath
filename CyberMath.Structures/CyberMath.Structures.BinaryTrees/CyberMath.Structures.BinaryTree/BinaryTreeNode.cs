using System;
using CyberMath.Structures.BinaryTreeBase;

namespace CyberMath.Structures.BinaryTree
{
    public class BinaryTreeNode<T> : BinaryTreeNodeBase<T>
        where T : IComparable<T>, IComparable
    {
        public BinaryTreeNode(T data) : base(data) { }
        
        public override IBinaryTreeNode<T> Insert(T value)
        {
            var node = new BinaryTreeNode<T>(value);

            if (node.Data.CompareTo(Data) == -1)
            {
                Left = Left == null ? node : Left.Insert(value);
            }
            else
            {
                Right = Right == null ? node : Right.Insert(value);
            }

            return this;
        }
    }
}
