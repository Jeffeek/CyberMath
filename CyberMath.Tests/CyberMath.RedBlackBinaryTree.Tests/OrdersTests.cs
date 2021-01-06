using System;
using System.Linq;
using CyberMath.Structures.RedBlackBinaryTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.RedBlackBinaryTree.Tests
{
    [TestClass]
    public class OrdersTests
    {
        [TestMethod]
        public void BinaryTree_Inorder_100_test()
        {
            var rnd = new Random();
            var numbers = Enumerable.Range(0, 100).Select(x => rnd.Next(-30, 30)).Distinct().ToArray();
            var tree = new RedBlackBinaryTree<int>(numbers);
            var sortedNumbers = numbers.ToList();
            sortedNumbers.Sort();
            var inorder = tree.Inorder().ToArray();
            CollectionAssert.AreEqual(inorder, sortedNumbers);
        }

        [TestMethod]
        public void BinaryTree_Inorder_1000_test()
        {
            var rnd = new Random();
            var numbers = Enumerable.Range(0, 1000).Select(x => rnd.Next(-30, 30)).Distinct().ToArray();
            var tree = new RedBlackBinaryTree<int>(numbers);
            var sortedNumbers = numbers.ToList();
            sortedNumbers.Sort();
            var inorder = tree.Inorder().ToArray();
            CollectionAssert.AreEqual(inorder, sortedNumbers);
        }

        [TestMethod]
        public void BinaryTree_Inorder_10000_test()
        {
            var rnd = new Random();
            var numbers = Enumerable.Range(0, 10000).Select(x => rnd.Next(-30, 30)).Distinct().ToArray();
            var tree = new RedBlackBinaryTree<int>(numbers);
            var sortedNumbers = numbers.ToList();
            sortedNumbers.Sort();
            var inorder = tree.Inorder().ToArray();
            CollectionAssert.AreEqual(inorder, sortedNumbers);
        }

        [TestMethod]
        public void BinaryTree_Postorder_test()
        {
            var tree = new RedBlackBinaryTree<int>(6,55,-55,-40,8,66,554,74,12,7);
            var ordered = new [] { -40, -55, 7, 12, 8, 66, 554, 74, 55, 6 };
            var postorder = tree.Postorder().ToArray();
            CollectionAssert.AreEqual(ordered, postorder);
        }

        [TestMethod]
        public void BinaryTree_Preorder_test()
        {
            var tree = new RedBlackBinaryTree<int>(6, 55, -55, -40, 8, 66, 554, 74, 12, 7);
            var ordered = new [] { 6, -55, -40, 55, 8, 7, 12, 74, 66, 554};
            var preorder = tree.Preorder().ToArray();
            CollectionAssert.AreEqual(ordered, preorder);
        }
    }
}
