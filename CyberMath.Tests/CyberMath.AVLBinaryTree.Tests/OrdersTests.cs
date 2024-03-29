﻿#region Using namespaces

using System;
using System.Linq;
using CyberMath.Structures.BinaryTrees.AVLBinaryTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.AVLBinaryTree.Tests
{
    [TestClass]
    public class OrdersTests
    {
        [TestMethod]
        public void BinaryTree_Inorder_100_test()
        {
            var rnd = new Random();

            var numbers = Enumerable.Range(0, 100)
                                    .Select(x => rnd.Next(-30, 30))
                                    .Distinct()
                                    .ToArray();

            var tree = new AVLBinaryTree<int>(numbers);
            var sortedNumbers = numbers.ToList();
            sortedNumbers.Sort();

            var inorder = tree.Inorder()
                              .ToArray();

            CollectionAssert.AreEqual(inorder, sortedNumbers);
        }

        [TestMethod]
        public void BinaryTree_Inorder_1000_test()
        {
            var rnd = new Random();

            var numbers = Enumerable.Range(0, 1000)
                                    .Select(x => rnd.Next(-30, 30))
                                    .Distinct()
                                    .ToArray();

            var tree = new AVLBinaryTree<int>(numbers);
            var sortedNumbers = numbers.ToList();
            sortedNumbers.Sort();

            var inorder = tree.Inorder()
                              .ToArray();

            CollectionAssert.AreEqual(inorder, sortedNumbers);
        }

        [TestMethod]
        public void BinaryTree_Inorder_10000_test()
        {
            var rnd = new Random();

            var numbers = Enumerable.Range(0, 10000)
                                    .Select(x => rnd.Next(-30, 30))
                                    .Distinct()
                                    .ToArray();

            var tree = new AVLBinaryTree<int>(numbers);
            var sortedNumbers = numbers.ToList();
            sortedNumbers.Sort();

            var inorder = tree.Inorder()
                              .ToArray();

            CollectionAssert.AreEqual(inorder, sortedNumbers);
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(ordered, postorder);
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(ordered, preorder);
        }
    }
}