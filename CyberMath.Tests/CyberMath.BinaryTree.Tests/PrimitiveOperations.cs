using CyberMath.Structures.BinaryTree;
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
            CollectionAssert.AreEqual(tree.Inorder(), new[] { 0, 1, 2, 3, 4, 6, 7, 8, 9 });
        }
    }
}
