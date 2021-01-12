using BenchmarkDotNet.Attributes;
using CyberMath.Structures.AVLBinaryTree;
using CyberMath.Structures.BinaryTree;
using CyberMath.Structures.RedBlackBinaryTree;

namespace CyberMath.PerformanceTests.BinaryTrees
{
    public class BinaryTreesRemoveBenchmark
    {
        [Benchmark]
        public void Remove_BinaryTree()
        {
            var tree = new BinaryTree<int> { 1, 2 };
            tree.Remove(1);
        }

        [Benchmark]
        public void Remove_AVLTree()
        {
            var tree = new AVLBinaryTree<int> { 1, 2 };
            tree.Remove(1);
        }

        [Benchmark]
        public void Remove_RedBlackTree()
        {
            var tree = new RedBlackBinaryTree<int> { 1, 2 };
            tree.Remove(1);
        }
    }
}
