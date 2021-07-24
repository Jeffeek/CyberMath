using System;
using CyberMath.Structures.BinaryTrees.BinaryTreeBase;

namespace CyberMath.Structures.BinaryTrees.Exceptions
{
    public abstract class TreeException<TTree, T> : Exception
        where TTree : IBinaryTree<T>
        where T : IComparable<T>, IComparable
    {
        public IBinaryTree<T> Tree { get; }

        protected TreeException(TTree tree, string message = "Tree exception") : base(message) =>
            Tree = tree;

        protected TreeException(TTree tree, string message, Exception innerException) : base(message, innerException) =>
            Tree = tree;
    }
}
