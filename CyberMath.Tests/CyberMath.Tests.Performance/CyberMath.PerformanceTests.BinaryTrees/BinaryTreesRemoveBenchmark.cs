using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using CyberMath.Structures.AVLBinaryTree;
using CyberMath.Structures.BinaryTree;
using CyberMath.Structures.RedBlackBinaryTree;

namespace CyberMath.PerformanceTests.BinaryTrees
{
    public class BinaryTreesRemoveBenchmark
    {
        [Benchmark]
        public void Add_BinaryTree()
        {
            var tree = new BinaryTree<int> {1, 2};
            tree.Remove(1);
        }

        [Benchmark]
        public void Add_AVLTree()
        {
            var tree = new AVLBinaryTree<int> {1, 2};
            tree.Remove(1);
        }

        [Benchmark]
        public void Add_RedBlackTree()
        {
            var tree = new RedBlackBinaryTree<int> {1, 2};
            tree.Remove(1);
        }
    }
}
