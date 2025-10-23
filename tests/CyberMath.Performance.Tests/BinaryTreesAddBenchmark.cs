#region Using namespaces

using BenchmarkDotNet.Attributes;
using CyberMath.Structures.BinaryTrees.AVLBinaryTree;
using CyberMath.Structures.BinaryTrees.BinaryTree;
using CyberMath.Structures.BinaryTrees.BinaryTreeBase;
using CyberMath.Structures.BinaryTrees.RedBlackBinaryTree;

#endregion

namespace CyberMath.Performance.Tests;

public class BinaryTreesAddBenchmark(IBinaryTree<int> tree)
{
    private readonly int[] _addItem =
    [
        1, 2
    ];

    public IBinaryTree<int> Tree = tree;

    [Benchmark]
    public void Add_BinaryTree() =>
        Tree = new BinaryTree<int>
        {
            _addItem[0],
            _addItem[1]
        };

    [Benchmark]
    public void Add_AVLTree() =>
        Tree = new AVLBinaryTree<int>
        {
            _addItem[0],
            _addItem[1]
        };

    [Benchmark]
    public void Add_RedBlackTree() =>
        Tree = new RedBlackBinaryTree<int>
        {
            _addItem[0],
            _addItem[1]
        };
}