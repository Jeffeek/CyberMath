#region Using namespaces

using BenchmarkDotNet.Attributes;
using CyberMath.Structures.BinaryTrees.AVLBinaryTree;
using CyberMath.Structures.BinaryTrees.BinaryTree;
using CyberMath.Structures.BinaryTrees.BinaryTreeBase;
using CyberMath.Structures.BinaryTrees.RedBlackBinaryTree;

#endregion

namespace CyberMath.Performance.Tests;

public class BinaryTreesAddBenchmark
{
    private readonly int[] _addItem =
    [
        1, 2
    ];

    [Benchmark]
    public IBinaryTree<int> Add_BinaryTree()
    {
        var tree = new BinaryTree<int>
        {
            _addItem[0],
            _addItem[1]
        };
        return tree;
    }

    [Benchmark]
    public IBinaryTree<int> Add_AVLTree()
    {
        var tree = new AVLBinaryTree<int>
        {
            _addItem[0],
            _addItem[1]
        };
        return tree;
    }

    [Benchmark]
    public IBinaryTree<int> Add_RedBlackTree()
    {
        var tree = new RedBlackBinaryTree<int>
        {
            _addItem[0],
            _addItem[1]
        };
        return tree;
    }
}