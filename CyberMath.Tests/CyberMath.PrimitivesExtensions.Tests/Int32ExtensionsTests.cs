#region Using namespaces

using System;
using System.IO;
using System.Linq;
using CyberMath.Extensions.Int32;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.PrimitivesExtensions.Tests
{
    [TestClass]
    public class Int32ExtensionsTests
    {
        [TestMethod]
        public void GetLength_test()
        {
            for (var i = Int32.MinValue; i < 0; i /= 10)
            {
                long expect = i.ToString()
                               .Length
                              - 1;

                long actual = i.GetLength();
                Assert.IsTrue(expect == actual);
            }

            for (var i = 1; i <= 1000000000; i *= 10)
            {
                long expect = i.ToString()
                               .Length;

                long actual = i.GetLength();
                Assert.IsTrue(expect == actual);
            }

            var expectMaxValue = Int32.MaxValue.ToString()
                                      .Length;

            var actualMaxValue = Int32.MaxValue.GetLength();

            var expectZero = 0.ToString()
                              .Length;

            var actualZero = 0.GetLength();

            Assert.IsTrue(expectZero == actualZero);
            Assert.IsTrue(expectMaxValue == actualMaxValue);
        }

        [TestMethod]
        public void Palindrome_test()
        {
            var palindromes = File.ReadAllLines($"{Directory.GetCurrentDirectory()}\\Int32_palindromes.txt")
                                  .Select(Int32.Parse);

            Assert.IsTrue(palindromes.All(x => x.IsPalindrome()));
        }

        [TestMethod]
        public void GetDigits_test()
        {
            var testNumber = 123983;

            var expected = new byte[]
                           {
                               1,
                               2,
                               3,
                               9,
                               8,
                               3
                           };

            var actual = testNumber.GetDigits()
                                   .ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsOdd_test()
        {
            for (var i = 0; i < 10_000; i += 2)
                Assert.IsTrue(i.IsOdd());
        }

        [TestMethod]
        public void IsEven_test()
        {
            for (var i = 1; i < 10_000; i += 2)
                Assert.IsTrue(i.IsEven());
        }

        [TestMethod]
        public void GCD_test()
        {
            var firstNumbers = new[]
                               {
                                   2, 5, 10, 13, 55
                               };

            var secondNumbers = new[]
                                {
                                    7, 13, 11, 3, 10
                                };

            var gcds = new[]
                       {
                           1, 1, 1, 1, 5
                       };

            for (var i = 0; i < firstNumbers.Length; i++)
                Assert.AreEqual(gcds[i],
                                firstNumbers[i]
                                    .GCD(secondNumbers[i]));
        }

        [TestMethod]
        public void LCM_test()
        {
            var firstNumbers = new[]
                               {
                                   2, 5, 10, 13, 55
                               };

            var secondNumbers = new[]
                                {
                                    7, 13, 11, 3, 10
                                };

            var lcms = new[]
                       {
                           14, 65, 110, 39, 110
                       };

            for (var i = 0; i < firstNumbers.Length; i++)
                Assert.AreEqual(lcms[i],
                                firstNumbers[i]
                                    .LCM(secondNumbers[i]));
        }

        [TestMethod]
        public void Swap_test()
        {
            var firstNumber = 5;
            var secondNumber = 10;
            firstNumber.Swap(ref secondNumber);
            Assert.AreEqual(firstNumber, 10);
            Assert.AreEqual(secondNumber, 5);
        }

        [TestMethod]
        public void ToBinary_test()
        {
            var numbers = new[]
                          {
                              1,
                              2,
                              3,
                              4,
                              5,
                              6,
                              1100,
                              12234
                          };

            var binaries = new[]
                           {
                               "1",
                               "10",
                               "11",
                               "100",
                               "101",
                               "110",
                               "10001001100",
                               "10111111001010"
                           };

            CollectionAssert.AreEqual(numbers.Select(num => num.ToBinary())
                                             .ToArray(),
                                      binaries);
        }

        [TestMethod]
        public void ToHEX_test()
        {
            var numbers = new[]
                          {
                              10,
                              20,
                              37,
                              41,
                              58,
                              655,
                              110550,
                              112234
                          };

            var hexes = new[]
                        {
                            "A",
                            "14",
                            "25",
                            "29",
                            "3A",
                            "28F",
                            "1AFD6",
                            "1B66A"
                        };

            CollectionAssert.AreEqual(numbers.Select(num => num.ToHex())
                                             .ToArray(),
                                      hexes.Select(x => x.ToLower())
                                           .ToArray());
        }
    }
}