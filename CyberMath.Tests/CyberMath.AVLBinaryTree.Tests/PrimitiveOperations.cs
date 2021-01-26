#region Using derectives

using System;
using System.Linq;
using CyberMath.Structures.BinaryTrees.AVLBinaryTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.AVLBinaryTree.Tests
{
	[TestClass]
	public class PrimitiveOperations
	{
		[TestMethod]
		public void BinaryTree_AddTest()
		{
			var tree = new AVLBinaryTree<int>();
			for (var i = 0; i < 10; i++)
				tree.Add(i);

			Assert.IsTrue(tree.Count == 10);
			var array = new int[10];
			tree.CopyTo(array, 0);
			CollectionAssert.AreEqual(array, new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
		}

		[TestMethod]
		public void BinaryTree_RemoveTest()
		{
			var tree = new AVLBinaryTree<int>();
			for (var i = 0; i < 10; i++)
				tree.Add(i);

			tree.Remove(5);
			CollectionAssert.AreEqual(tree.Inorder().ToArray(), new[] { 0, 1, 2, 3, 4, 6, 7, 8, 9 });
		}

		[TestMethod]
		public void BinaryTreeMax_test()
		{
			var rnd = new Random();
			var listOfNums = Enumerable.Range(0, 1000).Select(x => rnd.Next(-10_000, 10_000)).ToArray();
			var tree = new AVLBinaryTree<int>();
			tree.AddRange(listOfNums.ToArray());
			var expected = listOfNums.Max();
			var actual = tree.Max();
			Assert.IsTrue(expected == actual);
		}

		[TestMethod]
		public void BinaryTreeMin_test()
		{
			var rnd = new Random();
			var listOfNums = Enumerable.Range(0, 1000).Select(x => rnd.Next(-10_000, 10_000)).ToArray();
			var tree = new AVLBinaryTree<int>();
			tree.AddRange(listOfNums.ToArray());

			var expected = listOfNums.Min();
			var actual = tree.Min();
			Assert.IsTrue(expected == actual);
		}
	}
}