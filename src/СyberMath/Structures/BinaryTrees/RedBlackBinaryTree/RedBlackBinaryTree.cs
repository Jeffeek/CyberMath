using System;
using System.Collections.Generic;
using СyberMath.Structures.BinaryTrees.BinaryTreeBase;

namespace СyberMath.Structures.BinaryTrees.RedBlackBinaryTree
{
    /// <summary>
    /// Represents Red-Black Binary Tree. Implements <see cref="BinaryTreeBase{T}"/>
    /// </summary>
    /// <typeparam name="T"><see cref="IComparable{T}"/></typeparam>
    public class RedBlackBinaryTree<T> : BinaryTreeBase<T>
    where T : IComparable, IComparable<T>
    {
	    /// <inheritdoc />
	    public RedBlackBinaryTree() : base() { }

	    /// <inheritdoc />
	    public RedBlackBinaryTree(params T[] values) : base(values) { }

	    /// <inheritdoc />
	    public RedBlackBinaryTree(IEnumerable<T> values) : base(values) { }

	    /// <inheritdoc />
	    public override void Add(T item)
        {
            if (ReferenceEquals(Root, null))
            {
                Root = new RedBlackBinaryTreeNode<T>(item);
                Count = 1;
                return;
            }

            if (Contains(item)) return;
            Root = Root.Insert(item);
            Count++;
        }
    }
}
