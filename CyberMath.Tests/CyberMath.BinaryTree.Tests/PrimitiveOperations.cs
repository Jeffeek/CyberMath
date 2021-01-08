using System.Linq;
using CyberMath.Structures.BinaryTree;
using CyberMath.Structures.Extensions.NumberGenerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.BinaryTree.Tests
{
    [TestClass]
    public class PrimitiveOperations
    {
        [TestMethod]
        public void BinaryTree_AddTest()
        {
            var tree = new BinaryTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Add(i);
            Assert.IsTrue(tree.Count == 10);
            var array = new int[10];
            tree.CopyTo(array, 0);
            CollectionAssert.AreEqual(array, new[] {0,1,2,3,4,5,6,7,8,9});
        }

        [TestMethod]
        public void BinaryTree_RemoveTest()
        {
            var tree = new BinaryTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Add(i);
            tree.Remove(5);
            tree.Remove(1);
            tree.Remove(8);
            CollectionAssert.AreEqual(tree.Inorder().ToArray(), new[] { 0, 2, 3, 4, 6, 7, 9 });
        }

        [TestMethod]
        public void BinaryTreeMax_test()
        {
            var listOfNums = new IntRandomGenerator().GenerateMany(-10000, 10000).Take(1000).ToArray();
            var tree = new BinaryTree<int>();
            tree.AddRange(listOfNums.ToArray());

            int expected = listOfNums.Max();
            int actual = tree.Max();
            Assert.IsTrue(expected == actual);
        }

        [TestMethod]
        public void BinaryTreeMin_test()
        {
            var listOfNums = new IntRandomGenerator().GenerateMany(-10000, 10000).Take(1000).ToArray();
            var tree = new BinaryTree<int>();
            tree.AddRange(listOfNums.ToArray());

            int expected = listOfNums.Min();
            int actual = tree.Min();
            Assert.IsTrue(expected == actual);
        }
    }
}
