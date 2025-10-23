using CyberMath.Structures.BinaryTrees.AVLBinaryTree;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CyberMath.xUnit.Tests.Structures.BinaryTrees;

// ReSharper disable once InconsistentNaming
public class AVLBinaryTreeTests
{
    [Fact]
    public void BinaryTree_AddTest()
    {
        var tree = new AVLBinaryTree<int>();

        for (var i = 0; i < 10; i++)
            tree.Add(i);

        Assert.Equal(10, tree.Count);
        var array = new int[10];
        tree.CopyTo(array, 0);

        Assert.Equal(new[]
        {
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9
        }, array);
    }

    [Fact]
    public void BinaryTree_RemoveTest()
    {
        var tree = new AVLBinaryTree<int>();

        for (var i = 0; i < 10; i++)
            tree.Add(i);

        tree.Remove(5);

        Assert.Equal(new[]
        {
            0, 1, 2, 3, 4, 6, 7, 8, 9
        }, tree.Inorder().ToArray());
    }

    [Fact]
    public void BinaryTreeMax_test()
    {
        var rnd = new Random();

        var listOfNums = Enumerable.Range(0, 1000)
                                   .Select(_ => rnd.Next(-10_000, 10_000))
                                   .ToArray();

        var tree = new AVLBinaryTree<int>();
        tree.AddRange(listOfNums.ToArray());
        var expected = listOfNums.Max();
        var actual = tree.Max();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void BinaryTreeMin_test()
    {
        var rnd = new Random();

        var listOfNums = Enumerable.Range(0, 1000)
                                   .Select(_ => rnd.Next(-10_000, 10_000))
                                   .ToArray();

        var tree = new AVLBinaryTree<int>();
        tree.AddRange(listOfNums.ToArray());

        var expected = listOfNums.Min();
        var actual = tree.Min();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void BinaryTree_Inorder_100_test()
    {
        var rnd = new Random();

        var numbers = Enumerable.Range(0, 100)
                                .Select(_ => rnd.Next(-30, 30))
                                .Distinct()
                                .ToArray();

        var tree = new AVLBinaryTree<int>(numbers);
        var sortedNumbers = numbers.ToList();
        sortedNumbers.Sort();

        var inorder = tree.Inorder()
                          .ToArray();

        Assert.Equal(sortedNumbers, inorder);
    }

    [Fact]
    public void BinaryTree_Inorder_1000_test()
    {
        var rnd = new Random();

        var numbers = Enumerable.Range(0, 1000)
                                .Select(_ => rnd.Next(-30, 30))
                                .Distinct()
                                .ToArray();

        var tree = new AVLBinaryTree<int>(numbers);
        var sortedNumbers = numbers.ToList();
        sortedNumbers.Sort();

        var inorder = tree.Inorder()
                          .ToArray();

        Assert.Equal(sortedNumbers, inorder);
    }

    [Fact]
    public void BinaryTree_Inorder_10000_test()
    {
        var rnd = new Random();

        var numbers = Enumerable.Range(0, 10000)
                                .Select(_ => rnd.Next(-30, 30))
                                .Distinct()
                                .ToArray();

        var tree = new AVLBinaryTree<int>(numbers);
        var sortedNumbers = numbers.ToList();
        sortedNumbers.Sort();

        var inorder = tree.Inorder()
                          .ToArray();

        Assert.Equal(sortedNumbers, inorder);
    }

    [Fact]
    public void BinaryTree_Postorder_test()
    {
        var tree = new AVLBinaryTree<int>(6,
                                          55,
                                          -55,
                                          -40,
                                          8,
                                          66,
                                          554,
                                          74,
                                          12,
                                          7);

        var ordered = new[]
                      {
                              -40,
                              -55,
                              7,
                              12,
                              8,
                              6,
                              74,
                              554,
                              66,
                              55
                          };

        var postorder = tree.Postorder()
                            .ToArray();

        Assert.Equal(ordered, postorder);
    }

    [Fact]
    public void BinaryTree_Preorder_test()
    {
        var tree = new AVLBinaryTree<int>(6,
                                          55,
                                          -55,
                                          -40,
                                          8,
                                          66,
                                          554,
                                          74,
                                          12,
                                          7);

        var ordered = new[]
                      {
                              55,
                              6,
                              -55,
                              -40,
                              8,
                              7,
                              12,
                              66,
                              554,
                              74
                          };

        var preorder = tree.Preorder()
                           .ToArray();

        Assert.Equal(ordered, preorder);
    }

    [Fact]
    public void BinaryTreeMergeTest()
    {
        var firstTree = new AVLBinaryTree<int>();

        for (var i = 0; i < 10; i++)
            firstTree.Add(i);

        var secondTree = new AVLBinaryTree<int>();

        for (var i = 10; i < 20; i++)
            secondTree.Add(i);

        firstTree.MergeWith(secondTree);

        var checkArray = new[]
                         {
                                 0,
                                 1,
                                 2,
                                 3,
                                 4,
                                 5,
                                 6,
                                 7,
                                 8,
                                 9,
                                 10,
                                 11,
                                 12,
                                 13,
                                 14,
                                 15,
                                 16,
                                 17,
                                 18,
                                 19
                             };

        Assert.Equal(checkArray,
                      firstTree.Inorder()
                               .ToArray());
    }

    [Fact]
    public void BinaryTreeMergeTime_100_Test()
    {
        var list = new List<int>();
        var firstTree = new AVLBinaryTree<int>();

        for (var i = 0; i < 50; i++)
        {
            firstTree.Add(i);
            list.Add(i);
        }

        var secondTree = new AVLBinaryTree<int>();

        for (var i = 50; i < 100; i++)
        {
            secondTree.Add(i);
            list.Add(i);
        }

        firstTree.MergeWith(secondTree);

        Assert.Equal(list,
                      firstTree.Inorder()
                               .ToArray());
    }

    [Fact]
    public void BinaryTreeMergeTime_1000_Test()
    {
        var list = new List<int>();
        var firstTree = new AVLBinaryTree<int>();

        for (var i = 0; i < 500; i++)
        {
            firstTree.Add(i);
            list.Add(i);
        }

        var secondTree = new AVLBinaryTree<int>();

        for (var i = 500; i < 1000; i++)
        {
            secondTree.Add(i);
            list.Add(i);
        }

        firstTree.MergeWith(secondTree);

        Assert.Equal(list,
                      firstTree.Inorder()
                               .ToArray());
    }

    [Fact]
    public void BinaryTreeMerge_random_Test()
    {
        var list = new List<int>();
        var rnd = new Random();
        var firstTree = new AVLBinaryTree<int>();

        for (var i = 0; i < 500; i++)
        {
            var num = rnd.Next(-50, 50);
            firstTree.Add(num);
            list.Add(num);
        }

        var secondTree = new AVLBinaryTree<int>();

        for (var i = 500; i < 1000; i++)
        {
            var num = rnd.Next(-50, 50);
            secondTree.Add(num);
            list.Add(num);
        }

        firstTree.MergeWith(secondTree);

        list = list.Distinct()
                   .ToList();

        list.Sort();

        Assert.Equal(list,
                      firstTree.Inorder()
                               .ToArray());
    }

    [Fact]
    public void BinaryTree_Contains_negative()
    {
        var tree = new AVLBinaryTree<int>();

        tree.AddRange(new[]
                      {
                              1,
                              2,
                              3,
                              4,
                              5,
                              6,
                              -50,
                              -99
                          });

        var actual = tree.Contains(-999);
        Assert.False(actual);
    }

    [Fact]
    public void BinaryTree_Contains_positive()
    {
        var tree = new AVLBinaryTree<int>();

        tree.AddRange(new[]
                      {
                              1,
                              2,
                              3,
                              4,
                              5,
                              6,
                              -50,
                              -99
                          });

        var actual = tree.Contains(6);
        Assert.True(actual);
    }
}