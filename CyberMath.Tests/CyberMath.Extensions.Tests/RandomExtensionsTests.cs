#region Using derectives

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.Extensions.Tests
{
	[TestClass]
	public class RandomExtensionsTests
	{
		[TestMethod]
		public void TakeLong_test()
		{
			var rnd = new Random();
			var min = -5000000000000000;
			var max = 5000000000000000;
			var values = Enumerable.Range(0, 100).Select(x => rnd.NextLong(min, max));
			var actual = values.Any(x => x < int.MinValue || x > int.MaxValue);
			Assert.AreEqual(true, actual);
		}
	}
}