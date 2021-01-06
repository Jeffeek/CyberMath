using System.IO;
using System.Linq;
using CyberMath.Primitives.Int32;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.PrimitivesExtensions.Tests
{
    [TestClass]
    public class Int32ExtensionsTests
    {
        [TestMethod]
        public void Int32_GetLength_test()
        {
            for (int i = int.MinValue; i < 0; i /= 10)
            {
                long expect = i.ToString().Length - 1;
                long actual = i.GetLength();
                Assert.IsTrue(expect == actual);
            }

            for (int i = 1; i <= 1000000000; i *= 10)
            {
                long expect = i.ToString().Length;
                long actual = i.GetLength();
                Assert.IsTrue(expect == actual);
            }


            int expectMaxValue = int.MaxValue.ToString().Length;
            int actualMaxValue = int.MaxValue.GetLength();
            int expectZero = 0.ToString().Length;
            int actualZero = 0.GetLength();

            Assert.IsTrue(expectZero == actualZero);
            Assert.IsTrue(expectMaxValue == actualMaxValue);
        }

        [TestMethod]
        public void Int32_Palindrome_test()
        {
            var palindromes = File.ReadAllLines($"{Directory.GetCurrentDirectory()}\\Int32_palindromes.txt").Select(int.Parse);
            Assert.IsTrue(palindromes.All(x => x.IsPalindrome()));
        }
    }
}
