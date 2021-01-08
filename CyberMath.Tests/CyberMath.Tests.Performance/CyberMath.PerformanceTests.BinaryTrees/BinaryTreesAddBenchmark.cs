using BenchmarkDotNet.Attributes;
using CyberMath.Structures.AVLBinaryTree;
using CyberMath.Structures.BinaryTree;
using CyberMath.Structures.RedBlackBinaryTree;
using CyberMath.Structures.BinaryTreeBase;

namespace CyberMath.PerformanceTests.BinaryTrees
{
    public class BinaryTreesAddBenchmark
    {
        public int[] AddItem = {1, 2};
        public IBinaryTree<int> Tree;

        [Benchmark]
        public void Add_BinaryTree() => Tree = new BinaryTree<int> {AddItem[0], AddItem[1]};

        [Benchmark]
        public void Add_AVLTree() => Tree = new AVLBinaryTree<int> {AddItem[0], AddItem[1]};

        [Benchmark]
        public void Add_RedBlackTree() => Tree = new RedBlackBinaryTree<int> {AddItem[0], AddItem[1]};
    }
}
