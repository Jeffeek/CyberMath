#region Using derectives

using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using CyberMath.Structures.BinaryTrees.AVLBinaryTree;
using CyberMath.Structures.BinaryTrees.BinaryTree;
using CyberMath.Structures.BinaryTrees.RedBlackBinaryTree;

#endregion

namespace CyberMath.PerformanceTests.BinaryTrees
{
	public sealed class BinaryTreesOrdersBenchmark
	{
		private static readonly Random rnd = new Random();

		private static readonly IEnumerable<int> _argument =
			Enumerable.Range(0, 1_000_000).Select(x => rnd.Next()).ToArray();

		private static readonly BinaryTree<int> _tree = new BinaryTree<int>(_argument);
		private static readonly AVLBinaryTree<int> _avl = new AVLBinaryTree<int>(_argument);
		private static readonly RedBlackBinaryTree<int> _redBlack = new RedBlackBinaryTree<int>(_argument);

		[Benchmark]
		public void BinaryTreeInorder() => _tree.Inorder();

		[Benchmark]
		public void AVLBinaryTreeInorder() => _avl.Inorder();

		[Benchmark]
		public void RedBlackBinaryTreeInorder() => _redBlack.Inorder();

		[Benchmark]
		public void BinaryTreePreorder() => _tree.Preorder();

		[Benchmark]
		public void AVLBinaryTreePreorder() => _avl.Preorder();

		[Benchmark]
		public void RedBlackBinaryTreePreorder() => _redBlack.Preorder();

		[Benchmark]
		public void BinaryTreePostorder() => _tree.Postorder();

		[Benchmark]
		public void AVLBinaryTreePostorder() => _avl.Postorder();

		[Benchmark]
		public void RedBlackBinaryTreePostorder() => _redBlack.Postorder();
	}
}