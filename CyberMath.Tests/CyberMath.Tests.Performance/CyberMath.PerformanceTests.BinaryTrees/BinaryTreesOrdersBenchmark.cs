#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using CyberMath.Structures.BinaryTrees.AVLBinaryTree;
using CyberMath.Structures.BinaryTrees.BinaryTree;
using CyberMath.Structures.BinaryTrees.RedBlackBinaryTree;

#endregion

namespace CyberMath.PerformanceTests
{
    public sealed class BinaryTreesOrdersBenchmark
    {
        private static readonly Random Rnd = new();

        private static readonly IEnumerable<int> Argument =
            Enumerable.Range(0, 1_000_000)
                      .Select(_ => Rnd.Next())
                      .ToArray();

        private static readonly BinaryTree<int> Tree = new(Argument);
        private static readonly AVLBinaryTree<int> Avl = new(Argument);
        private static readonly RedBlackBinaryTree<int> RedBlack = new(Argument);

        [Benchmark]
        public void BinaryTreeInorder() => Tree.Inorder();

        [Benchmark]
        // ReSharper disable once InconsistentNaming
        public void AVLBinaryTreeInorder() => Avl.Inorder();

        [Benchmark]
        public void RedBlackBinaryTreeInorder() => RedBlack.Inorder();

        [Benchmark]
        public void BinaryTreePreorder() => Tree.Preorder();

        [Benchmark]
        // ReSharper disable once InconsistentNaming
        public void AVLBinaryTreePreorder() => Avl.Preorder();

        [Benchmark]
        public void RedBlackBinaryTreePreorder() => RedBlack.Preorder();

        [Benchmark]
        public void BinaryTreePostorder() => Tree.Postorder();

        [Benchmark]
        // ReSharper disable once InconsistentNaming
        public void AVLBinaryTreePostorder() => Avl.Postorder();

        [Benchmark]
        public void RedBlackBinaryTreePostorder() => RedBlack.Postorder();
    }
}