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
		public void WordsFrequency_test()
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

		[TestMethod]
		public void AnagramOf_test()
		{
			var anagram = "ANAGRAM";
			var testStrings = new[]
			                  {
				                  "RANGAMA",
				                  "GANMARA",
				                  "NAGMARA",
				                  "NAGMAAR",
				                  "GANMAAR",
				                  "MANRAGA",
				                  "NAMRAGA",
				                  "NAMAGAR",
				                  "MANAGAR",
				                  "RANAGMA",
				                  "GRANAMA",
				                  "ANGARAM",
				                  "ANGAMAR",
				                  "ANGAARM",
				                  "MANARAG",
				                  "MANAGAR",
				                  "RANGAMA",
				                  "GRANAMA",
				                  "GNARAMA",
				                  "ANAGRAM"
			                  };
			Assert.IsTrue(testStrings.All(x => x.IsAnagramOf(anagram)));
		}

		[TestMethod]
		public void ConvertToInt32_test()
		{
			var inputs = new[]
			             {
				             "234234",
				             "499234",
				             "234345234",
				             "2334",
				             "-34",
				             "-234234",
				             "1234",
				             "234",
			             };

			foreach (var input in inputs)
			{
				input.ToInt32();
			}
		}

		[TestMethod]
		public void ConvertToInt64_test()
		{
			var inputs = new[]
			             {
				             "234564234",
				             "5549456945645645234",
				             "130040000456456634",
				             "2334564564",
				             "-3445674857456684576",
				             "-234564564234",
				             "-1234",
				             "245645634",
			             };

			foreach (var input in inputs)
			{
				input.ToInt64();
			}
		}

		[TestMethod]
		public void ToAlternateCase()
		{
			var test = "h7TToppPPsd283";
			var expected = "H7ttOPPppSD283";
			var actual = test.ToAlternatingCase();
			Assert.AreEqual(expected, actual);
		}
	}
}