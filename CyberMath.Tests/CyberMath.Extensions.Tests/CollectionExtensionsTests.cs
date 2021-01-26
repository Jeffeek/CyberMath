#region Using derectives

using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.Extensions.Tests
{
	[TestClass]
	public class CollectionExtensionsTests
	{
		[TestMethod]
		public void Swap_test()
		{
			var collection = new[] { 1, 2, 3, 4 };
			collection.Swap(0, 3);
			var expected = new[] { 4, 2, 3, 1 };
			CollectionAssert.AreEqual(collection, expected);
		}

		[TestMethod]
		public void Shuffle_test()
		{
			var collection = Enumerable.Range(0, 10000).ToArray();
			var copied = new int[10000];
			collection.CopyTo(copied, 0);
			collection.Shuffle();
			for (var i = 0; i < collection.Length; i++)
				Assert.AreNotEqual(copied[i], collection[i]);
		}

		[TestMethod]
		public void Permutations_test()
		{
			var testArray = new[] { 1, 2, 3 };
			var expected = new List<List<int>>
			               {
													new List<int> { 1, 2, 3 },
													new List<int> { 1, 3, 2 },
													new List<int> { 2, 1, 3 },
													new List<int> { 2, 3, 1 },
													new List<int> { 3, 1, 2 },
													new List<int> { 3, 2, 1 }
			                                     };

			var actual = testArray.Permutations().Select(x => x.ToList()).ToList();
			Assert.AreEqual(expected.Count, actual.Count);
			for (var i = 0; i < expected.Count; i++)
			{
				Assert.AreEqual(expected[i].Count, actual[i].Count);
				for (var j = 0; j < expected[i].Count; j++)
				{
					Assert.AreEqual(expected[i][j], actual[i][j]);
				}
			}
		}

		[TestMethod]
		public void PermutationsWithRepeat_test()
		{
			var testArray = new[] { 1, 2, 3 };
			var expected = new List<List<int>>
			               {
				               new List<int> { 1, 1, 1 },
				               new List<int> { 1, 1, 2 },
				               new List<int> { 1, 1, 3 },
				               new List<int> { 1, 2, 1 },
				               new List<int> { 1, 2, 2 },
				               new List<int> { 1, 2, 3 },
				               new List<int> { 1, 3, 1 },
				               new List<int> { 1, 3, 2 },
				               new List<int> { 1, 3, 3 },
				               new List<int> { 2, 1, 1 },
				               new List<int> { 2, 1, 2 },
				               new List<int> { 2, 1, 3 },
				               new List<int> { 2, 2, 1 },
				               new List<int> { 2, 2, 2 },
				               new List<int> { 2, 2, 3 },
				               new List<int> { 2, 3, 1 },
				               new List<int> { 2, 3, 2 },
				               new List<int> { 2, 3, 3 },
				               new List<int> { 3, 1, 1 },
				               new List<int> { 3, 1, 2 },
				               new List<int> { 3, 1, 3 },
				               new List<int> { 3, 2, 1 },
				               new List<int> { 3, 2, 2 },
				               new List<int> { 3, 2, 3 },
				               new List<int> { 3, 3, 1 },
				               new List<int> { 3, 3, 2 },
				               new List<int> { 3, 3, 3 }
						   };

			var actual = testArray.PermutationsWithRepeat().Select(x => x.ToList()).ToList();
			Assert.AreEqual(expected.Count, actual.Count);
			for (var i = 0; i < expected.Count; i++)
			{
				Assert.AreEqual(expected[i].Count, actual[i].Count);
				for (var j = 0; j < expected[i].Count; j++)
				{
					Assert.AreEqual(expected[i][j], actual[i][j]);
				}
			}
		}
	}
}