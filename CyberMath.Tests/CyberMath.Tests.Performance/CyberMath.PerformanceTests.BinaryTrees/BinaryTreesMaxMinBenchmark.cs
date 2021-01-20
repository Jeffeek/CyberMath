using BenchmarkDotNet.Attributes;
using System;
using System.Linq;
using СyberMath.Structures.BinaryTrees.BinaryTree;
using СyberMath.Structures.BinaryTrees.BinaryTreeBase;

namespace CyberMath.PerformanceTests.BinaryTrees
{
    public sealed class BinaryTreesMaxMinBenchmark
    {
        private static readonly Random rnd = new Random();
        public static int[] Elements = Enumerable.Range(0, 10_000).Select(x => rnd.Next(-10_000_000, 10_000_000)).ToArray();

        private readonly IBinaryTree<int> TREE = new BinaryTree<int>(Elements);
        private readonly IBinaryTree<int> AVL = new BinaryTree<int>(Elements);
        private readonly IBinaryTree<int> REDBLACK = new BinaryTree<int>(Elements);

        [Benchmark]
        public void BinaryTreeMin() => TREE.Min();
        [Benchmark]
        public void BinaryTreeMax() => TREE.Max();

        [Benchmark]
        public void AVLBinaryTreeMin() => AVL.Min();
        [Benchmark]
        public void AVLBinaryTreeMax() => AVL.Max();

        [Benchmark]
        public void RedBlackBinaryTreeMin() => REDBLACK.Min();
        [Benchmark]
        public void RedBlackBinaryTreeMax() => REDBLACK.Max();
    }
}
