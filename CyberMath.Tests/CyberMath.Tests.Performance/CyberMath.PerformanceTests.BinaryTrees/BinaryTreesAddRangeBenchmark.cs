using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using CyberMath.Structures.AVLBinaryTree;
using CyberMath.Structures.BinaryTree;
using CyberMath.Structures.RedBlackBinaryTree;
using Microsoft.Diagnostics.Tracing.Parsers.IIS_Trace;

namespace CyberMath.PerformanceTests.BinaryTrees
{
    public class BinaryTreesAddRangeBenchmark
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
            for (int i = 0; i < Count; i++)
                array[i] = rnd.Next(-100, 100);
            return array;
        }
    }
}
