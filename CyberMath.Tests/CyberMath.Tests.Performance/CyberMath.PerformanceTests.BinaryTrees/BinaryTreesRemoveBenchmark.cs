#region Using namespaces

using BenchmarkDotNet.Attributes;
using CyberMath.Structures.BinaryTrees.AVLBinaryTree;
using CyberMath.Structures.BinaryTrees.BinaryTree;
using CyberMath.Structures.BinaryTrees.RedBlackBinaryTree;

#endregion

namespace CyberMath.PerformanceTests.BinaryTrees
{
    public sealed class BinaryTreesRemoveBenchmark
    {
        [Benchmark]
        public void Remove_BinaryTree()
        {
            using var tree = new BinaryTree<int>
                             {
                                 1,
                                 2
                             };

            tree.Remove(1);
        }

        [Benchmark]
        public void Remove_AVLTree()
        {
            using var tree = new AVLBinaryTree<int>
                             {
                                 1,
                                 2
                             };

            tree.Remove(1);
        }

        [Benchmark]
        public void Remove_RedBlackTree()
        {
            using var tree = new RedBlackBinaryTree<int>
                             {
                                 1,
                                 2
                             };

            tree.Remove(1);
        }
    }
}