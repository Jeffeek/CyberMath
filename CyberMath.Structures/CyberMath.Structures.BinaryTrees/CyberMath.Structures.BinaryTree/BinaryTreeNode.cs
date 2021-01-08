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
                Left = Left == null ? node : Left.Insert(value);
            else
                Right = Right == null ? node : Right.Insert(value);
            return this;
        }

        public override IBinaryTreeNode<T> Remove(T value) => InternalRemove(this, value);

        private IBinaryTreeNode<T> InternalRemove(BinaryTreeNode<T> node, T value)
        {
            if (ReferenceEquals(node, null)) return null;
            switch (value.CompareTo(node.Data))
            {
                case -1:
                    node.Left = InternalRemove(node.Left as BinaryTreeNode<T>, value);
                    break;
                case 1:
                    node.Right = InternalRemove(node.Right as BinaryTreeNode<T>, value);
                    break;
                default:
                {
                    if (node.Left != null && node.Right != null)
                    {
                        node.Data = (node.Right as BinaryTreeBase<T>).Min();
                        node.Right = InternalRemove(node.Right as BinaryTreeNode<T>, node.Data);
                    }
                    else 
                    {
                        if (node.Left != null)
                            node = node.Left as BinaryTreeNode<T>;
                        else if (node.Right != null)
                            node = node.Right as BinaryTreeNode<T>;
                        else
                            node = null;
                    }

                    break;
                }
            }
            return node;
        }
    }
}
