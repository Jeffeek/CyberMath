using System;
using CyberMath.Structures.BinaryTrees.BinaryTreeBase;

namespace CyberMath.Structures.BinaryTrees.Exceptions
{
    public class EmptyTreeException<TTree, T> : TreeException<TTree, T>
        where TTree : IBinaryTree<T>
        where T : IComparable<T>, IComparable
    {
        public EmptyTreeException(TTree tree, string message = "Empty tree exception") : base(tree, message) { }

        public EmptyTreeException(TTree tree, string message, Exception innerException) : base(tree, message, innerException) { }
    }
}