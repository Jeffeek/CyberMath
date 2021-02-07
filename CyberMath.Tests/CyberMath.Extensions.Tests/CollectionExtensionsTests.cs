#region Using derectives

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
	}
}