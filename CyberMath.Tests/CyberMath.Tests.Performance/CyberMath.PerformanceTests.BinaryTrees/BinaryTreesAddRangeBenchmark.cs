using BenchmarkDotNet.Attributes;
using System;
using CyberMath.Structures.BinaryTrees.AVLBinaryTree;
using CyberMath.Structures.BinaryTrees.BinaryTree;
using CyberMath.Structures.BinaryTrees.RedBlackBinaryTree;

namespace CyberMath.PerformanceTests.BinaryTrees
{
    public sealed class BinaryTreesAddRangeBenchmark
    {
        [Params(1000, 100_000, 1_000_000)]
        public int Count;

        [Benchmark]
        public void AddRange_BinaryTree()
        {
            var tree = new BinaryTree<int>();
            tree.AddRange(GetRandomItems());
        }

        [Benchmark]
        public void AddRange_AVLTree()
        {
            var tree = new AVLBinaryTree<int>();
            tree.AddRange(GetRandomItems());
        }

        [Benchmark]
        public void AddRange_RedBlackTree()
        {
            var tree = new RedBlackBinaryTree<int>();
            tree.AddRange(GetRandomItems());
        }

        public int[] GetRandomItems()
        {
            var array = new int[Count];
            var rnd = new Random();
            for (var i = 0; i < Count; i++)
                array[i] = rnd.Next(-100, 100);
            return array;
        }
    }
}
