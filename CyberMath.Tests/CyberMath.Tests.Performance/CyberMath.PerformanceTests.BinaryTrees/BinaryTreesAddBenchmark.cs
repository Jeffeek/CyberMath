#region Using derectives

using BenchmarkDotNet.Attributes;
using CyberMath.Structures.BinaryTrees.AVLBinaryTree;
using CyberMath.Structures.BinaryTrees.BinaryTree;
using CyberMath.Structures.BinaryTrees.BinaryTreeBase;
using CyberMath.Structures.BinaryTrees.RedBlackBinaryTree;

#endregion

namespace CyberMath.PerformanceTests.BinaryTrees
{
	public class BinaryTreesAddBenchmark
	{
		public readonly int[] AddItem = { 1, 2 };
		public IBinaryTree<int> Tree;

		[Benchmark]
		public void Add_BinaryTree() => Tree = new BinaryTree<int> { AddItem[0], AddItem[1] };

		[Benchmark]
		public void Add_AVLTree() => Tree = new AVLBinaryTree<int> { AddItem[0], AddItem[1] };

		[Benchmark]
		public void Add_RedBlackTree() => Tree = new RedBlackBinaryTree<int> { AddItem[0], AddItem[1] };
	}
}