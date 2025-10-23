#region Using namespaces

using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using CyberMath.Structures.BinaryTrees.BinaryTree;
using CyberMath.Structures.BinaryTrees.BinaryTreeBase;

#endregion

namespace CyberMath.Performance.Tests;

public sealed class BinaryTreesMaxMinBenchmark
{
    private static readonly Random Rnd = new();

    // ReSharper disable once MemberCanBePrivate.Global
    public static readonly int[] Elements =
        Enumerable.Range(0, 10_000)
            .Select(_ => Rnd.Next(-10_000_000, 10_000_000))
            .ToArray();

    private readonly IBinaryTree<int> _avl = new BinaryTree<int>(Elements);
    private readonly IBinaryTree<int> _redBlack = new BinaryTree<int>(Elements);
    private readonly IBinaryTree<int> _tree = new BinaryTree<int>(Elements);

    [Benchmark]
    public void BinaryTreeMin() => _tree.Min();

    [Benchmark]
    public void BinaryTreeMax() => _tree.Max();

    [Benchmark]
    // ReSharper disable once InconsistentNaming
    public void AVLBinaryTreeMin() => _avl.Min();

    [Benchmark]
    // ReSharper disable once InconsistentNaming
    public void AVLBinaryTreeMax() => _avl.Max();

    [Benchmark]
    public void RedBlackBinaryTreeMin() => _redBlack.Min();

    [Benchmark]
    public void RedBlackBinaryTreeMax() => _redBlack.Max();
}