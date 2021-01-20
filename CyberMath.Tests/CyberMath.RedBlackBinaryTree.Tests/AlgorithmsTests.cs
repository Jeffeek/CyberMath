using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using СyberMath.Structures.BinaryTrees.RedBlackBinaryTree;

namespace CyberMath.RedBlackBinaryTree.Tests
{
    [TestClass]
    public class AlgorithmsTests
    {
        [TestMethod]
        public void BinaryTreeMergeTest()
        {
            var firstTree = new RedBlackBinaryTree<int>();
            for (var i = 0; i < 10; i++)
                firstTree.Add(i);
            var secondTree = new RedBlackBinaryTree<int>();
            for (var i = 10; i < 20; i++)
                firstTree.Add(i);
            firstTree.MergeWith(secondTree);
            var checkArray = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
            CollectionAssert.AreEqual(checkArray, firstTree.Inorder().ToArray());
        }

        [TestMethod]
        public void BinaryTreeMergeTime_100_Test()
        {
            var list = new List<int>();
            var firstTree = new RedBlackBinaryTree<int>();
            for (var i = 0; i < 50; i++)
            {
                firstTree.Add(i);
                list.Add(i);
            }
            var secondTree = new RedBlackBinaryTree<int>();
            for (var i = 50; i < 100; i++)
            {
                firstTree.Add(i);
                list.Add(i);
            }
            firstTree.MergeWith(secondTree);
            CollectionAssert.AreEqual(list, firstTree.Inorder().ToArray());
        }

        [TestMethod]
        public void BinaryTreeMergeTime_1000_Test()
        {
            var list = new List<int>();
            var firstTree = new RedBlackBinaryTree<int>();
            for (var i = 0; i < 500; i++)
            {
                firstTree.Add(i);
                list.Add(i);
            }
            var secondTree = new RedBlackBinaryTree<int>();
            for (var i = 500; i < 1000; i++)
            {
                firstTree.Add(i);
                list.Add(i);
            }
            firstTree.MergeWith(secondTree);
            CollectionAssert.AreEqual(list, firstTree.Inorder().ToArray());
        }

        [TestMethod]
        public void BinaryTreeMerge_random_Test()
        {
            var list = new List<int>();
            var rnd = new Random();
            var firstTree = new RedBlackBinaryTree<int>();
            var secondTree = new RedBlackBinaryTree<int>();
            for (var i = 0; i < 500; i++)
            {
                var num = rnd.Next(-50, 50);
                firstTree.Add(num);
                list.Add(num);
            }
            for (var i = 500; i < 1000; i++)
            {
                var num = rnd.Next(-50, 50);
                firstTree.Add(num);
                list.Add(num);
            }
            firstTree.MergeWith(secondTree);
            list = list.Distinct().ToList();
            list.Sort();
            CollectionAssert.AreEqual(list, firstTree.Inorder().ToArray());
        }

        [TestMethod]
        public void BinaryTree_Contains_negative()
        {
            var tree = new RedBlackBinaryTree<int>();
            tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, -50, -99 });
            var actual = tree.Contains(-999);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void BinaryTree_Contains_positive()
        {
            var tree = new RedBlackBinaryTree<int>();
            tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, -50, -99 });
            var actual = tree.Contains(6);
            Assert.AreEqual(true, actual);
        }
    }
}
