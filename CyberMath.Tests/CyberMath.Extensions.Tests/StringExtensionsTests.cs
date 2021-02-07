#region Using derectives

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.Extensions.Tests
{
	[TestClass]
	public class StringExtensionsTests
	{
		[TestMethod]
		public void ConcatCount_test()
		{
			var test = "test";
			var actual = test.Concat(10, "-");
			var expected = "test-test-test-test-test-test-test-test-test-test";
			Assert.AreEqual(actual, expected);
		}

		[TestMethod]
		public void ConcatSeparatorCountNoAppend_test()
		{
			var test = "test";
			var actual = test.Concat(10);
			var expected = "testtesttesttesttesttesttesttesttesttest";
			Assert.AreEqual(actual, expected);
		}

		[TestMethod]
		public void ConcatSeparatorCountAppend_test()
		{
			var test = "test";
			var actual = test.Concat(10, true);
			var expected = string.Join(Environment.NewLine, Enumerable.Repeat(test, 10));
			Assert.AreEqual(actual, expected);
		}

		[TestMethod]
		public void IsPalindrome_positive_test()
		{
			var test = "qwertyuiooiuytrewq";
			var expected = true;
			var actual = test.IsPalindrome();
			Assert.IsTrue(expected == actual);
		}

		[TestMethod]
		public void IsPalindrome_negative_test()
		{
			var test = "qwertyuiooiutrewq";
			var expected = false;
			var actual = test.IsPalindrome();
			Assert.IsTrue(expected == actual);
		}

		[TestMethod]
		public void WordsFrequencyTest()
		{
			var testString = "11fddqqwdvypppppppp";
			var expected = new Dictionary<char, int>
			               {
				               { 'f', 1 },
				               { '1', 2 },
				               { 'd', 3 },
				               { 'q', 2 },
				               { 'w', 1 },
				               { 'v', 1 },
				               { 'y', 1 },
				               { 'p', 8 }
			               };

			var actual = testString.WordsFrequency();
			CollectionAssert.AreEquivalent(expected, actual);
		}
	}
}