using System;
using CyberMath.Structures.BinaryTrees.BinaryTreeBase;

namespace CyberMath.Structures.BinaryTrees.Exceptions
{
    public class UnknownTraversalTypeException<TTree, T> : TreeException<TTree, T>
        where TTree : IBinaryTree<T>
        where T : IComparable<T>, IComparable
    {
        public TraversalOrderType TraversalOrderType { get; }

        public UnknownTraversalTypeException(TTree tree, TraversalOrderType traversalOrderType,  string message = "Tree traversal type was unknown") : base(tree, message) =>
            TraversalOrderType = traversalOrderType;

        public UnknownTraversalTypeException(TTree tree, TraversalOrderType traversalOrderType, string message, Exception innerException) : base(tree, message, innerException) =>
            TraversalOrderType = traversalOrderType;
    }
}