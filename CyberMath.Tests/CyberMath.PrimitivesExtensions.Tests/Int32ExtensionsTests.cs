using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using CyberMath.Extensions.Int32;

namespace CyberMath.PrimitivesExtensions.Tests
{
    [TestClass]
    public class Int32ExtensionsTests
    {
        [TestMethod]
        public void Int32_GetLength_test()
        {
            for (var i = int.MinValue; i < 0; i /= 10)
            {
                long expect = i.ToString().Length - 1;
                long actual = i.GetLength();
                Assert.IsTrue(expect == actual);
            }

            for (var i = 1; i <= 1000000000; i *= 10)
            {
                long expect = i.ToString().Length;
                long actual = i.GetLength();
                Assert.IsTrue(expect == actual);
            }


            var expectMaxValue = int.MaxValue.ToString().Length;
            var actualMaxValue = int.MaxValue.GetLength();
            var expectZero = 0.ToString().Length;
            var actualZero = 0.GetLength();

            Assert.IsTrue(expectZero == actualZero);
            Assert.IsTrue(expectMaxValue == actualMaxValue);
        }

		[TestMethod]
		public void Int32_Palindrome_test()
		{
			var palindromes = File.ReadAllLines($"{Directory.GetCurrentDirectory()}\\Int32_palindromes.txt")
			                      .Select(int.Parse);

			Assert.IsTrue(palindromes.All(x => x.IsPalindrome()));
		}

		[TestMethod]
		public void Int32_GetDigits_test()
		{
			var testNumber = 123983;
			var expected = new byte[] { 1, 2, 3, 9, 8, 3 };
			var actual = testNumber.GetDigits().ToArray();
			CollectionAssert.AreEqual(expected, actual);
		}
	}
}