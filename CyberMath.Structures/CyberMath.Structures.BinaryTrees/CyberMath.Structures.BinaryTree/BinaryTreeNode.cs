using System;
using CyberMath.Structures.BinaryTreeBase;

namespace CyberMath.Structures.BinaryTree
{
    public class BinaryTreeNode<T> : BinaryTreeNodeBase<T>
        where T : IComparable<T>, IComparable
    {
        public override IBinaryTreeNode<T> Left { get; set; }
        public override IBinaryTreeNode<T> Right { get; set; }

        public BinaryTreeNode(T data) : base(data) { }
        
        public override void Add(T value)
        {
            var node = new BinaryTreeNode<T>(value);

            if (node.Data.CompareTo(Data) == -1)
            {
                if (Left == null)
                    Left = node;
                else
                    Left.Add(value);
            }
            else
            {
                if (Right == null)
                    Right = node;
                else
                    Right.Add(value);
            }
        }
    }
}
