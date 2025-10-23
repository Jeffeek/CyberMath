#region Using namespaces

using CyberMath.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

#endregion

namespace CyberMath.xUnit.Tests.Extensions;

public class Int64ExtensionTests
{
    #region IsOdd/IsEven Tests

    [Theory]
    [InlineData(0, true)]   // 0 is even
    [InlineData(1, false)]  // 1 is not even
    [InlineData(2, true)]   // 2 is even
    [InlineData(-2, true)]  // -2 is even
    [InlineData(-1, false)] // -1 is not even
    [InlineData(int.MaxValue, false)] // MaxValue is odd
    [InlineData(int.MinValue, true)]  // MinValue is even
    public void IsEven_ReturnsExpectedResult(long number, bool expected)
    {
        Assert.Equal(expected, number.IsEven());
    }

    [Theory]
    [InlineData(0, false)]  // 0 is not odd
    [InlineData(1, true)]   // 1 is odd
    [InlineData(2, false)]  // 2 is not odd
    [InlineData(-1, true)]  // -1 is odd
    [InlineData(-2, false)] // -2 is not odd
    [InlineData(int.MaxValue, true)]  // MaxValue is odd
    [InlineData(int.MinValue, false)] // MinValue is not odd
    public void IsOdd_ReturnsExpectedResult(long number, bool expected)
    {
        Assert.Equal(expected, number.IsOdd());
    }

    #endregion

    #region GCD Tests

    [Theory]
    [InlineData(12, 8, 4)]
    [InlineData(24, 36, 12)]
    [InlineData(17, 13, 1)]  // Coprime numbers
    [InlineData(100, 50, 50)]
    [InlineData(0, 5, 5)]
    [InlineData(5, 0, 5)]
    [InlineData(1, 1, 1)]
    [InlineData(-12, 8, 4)]  // Negative numbers
    [InlineData(12, -8, 4)]
    public void GCD_ReturnsCorrectResult(long a, long b, long expected)
    {
        Assert.Equal(expected, a.GCD(b));
    }

    #endregion

    #region LCM Tests

    [Theory]
    [InlineData(4, 6, 12)]
    [InlineData(12, 18, 36)]
    [InlineData(7, 13, 91)]  // Coprime numbers
    [InlineData(0, 5, 0)]
    [InlineData(5, 0, 0)]
    [InlineData(1, 10, 10)]
    [InlineData(-4, 6, 12)]  // Negative numbers
    public void LCM_ReturnsCorrectResult(long a, long b, long expected)
    {
        Assert.Equal(expected, a.LCM(b));
    }

    [Fact]
    public void LCM_ThrowsOnOverflow()
    {
        // These values would cause overflow
        Assert.Throws<OverflowException>(() => long.MaxValue.LCM(2));
    }

    #endregion

    #region Swap Tests

    [Theory]
    [InlineData(5, 10)]
    [InlineData(-5, 10)]
    [InlineData(0, 100)]
    [InlineData(int.MaxValue, int.MinValue)]
    public void Swap_SwapsValues(long initialA, long initialB)
    {
        var a = initialA;
        var b = initialB;

        a.Swap(ref b);

        Assert.Equal(initialB, a);
        Assert.Equal(initialA, b);
    }

    #endregion

    #region IsPalindrome Tests

    [Theory]
    [InlineData(0, true)]
    [InlineData(1, true)]
    [InlineData(11, true)]
    [InlineData(121, true)]
    [InlineData(1221, true)]
    [InlineData(12321, true)]
    [InlineData(123321, true)]
    [InlineData(12, false)]
    [InlineData(123, false)]
    [InlineData(1231, false)]
    [InlineData(-121, false)]  // Negative numbers are not palindromes
    [InlineData(1000000001, true)]  // Large palindrome
    public void IsPalindrome_ReturnsExpectedResult(long number, bool expected)
    {
        Assert.Equal(expected, number.IsPalindrome());
    }

    #endregion

    #region GetLength Tests

    [Theory]
    [InlineData(0, 1)]
    [InlineData(9, 1)]
    [InlineData(10, 2)]
    [InlineData(99, 2)]
    [InlineData(100, 3)]
    [InlineData(999, 3)]
    [InlineData(1000, 4)]
    [InlineData(9999, 4)]
    [InlineData(10000, 5)]
    [InlineData(99999, 5)]
    [InlineData(100000, 6)]
    [InlineData(999999, 6)]
    [InlineData(1000000, 7)]
    [InlineData(9999999, 7)]
    [InlineData(10000000, 8)]
    [InlineData(99999999, 8)]
    [InlineData(100000000, 9)]
    [InlineData(999999999, 9)]
    [InlineData(1000000000, 10)]
    [InlineData(long.MaxValue, 10)]
    [InlineData(-1, 1)]
    [InlineData(-123, 3)]
    [InlineData(int.MinValue, 10)]
    public void GetLength_ReturnsCorrectLength(long number, int expectedLength)
    {
        Assert.Equal(expectedLength, number.GetLength());
    }

    #endregion

    #region GetDigits Tests

    [Theory]
    [InlineData(0, new byte[] { 0 })]
    [InlineData(123, new byte[] { 1, 2, 3 })]
    [InlineData(4567, new byte[] { 4, 5, 6, 7 })]
    [InlineData(-123, new byte[] { 1, 2, 3 })]  // Negative sign is ignored
    [InlineData(1000, new byte[] { 1, 0, 0, 0 })]
    public void GetDigits_ReturnsCorrectDigits(long number, byte[] expected)
    {
        var result = number.GetDigits();
        Assert.Equal(expected, result);
    }

    #endregion

    #region ToBinary/ToHex Tests

    [Theory]
    [InlineData(0, "0")]
    [InlineData(1, "1")]
    [InlineData(2, "10")]
    [InlineData(8, "1000")]
    [InlineData(15, "1111")]
    [InlineData(255, "11111111")]
    [InlineData(-1, "11111111111111111111111111111111")]  // Two's complement
    public void ToBinary_ReturnsCorrectBinaryString(long number, string expected)
    {
        Assert.Equal(expected, number.ToBinary());
    }

    [Theory]
    [InlineData(0, "0")]
    [InlineData(10, "a")]
    [InlineData(15, "f")]
    [InlineData(16, "10")]
    [InlineData(255, "ff")]
    [InlineData(4096, "1000")]
    [InlineData(-1, "ffffffff")]  // Two's complement
    public void ToHex_ReturnsCorrectHexString(long number, string expected)
    {
        Assert.Equal(expected, number.ToHex());
    }

    #endregion
}

public class Int64PrimeNumbersTests
{
    [Theory]
    [InlineData(2, true)]
    [InlineData(3, true)]
    [InlineData(4, false)]
    [InlineData(5, true)]
    [InlineData(6, false)]
    [InlineData(7, true)]
    [InlineData(11, true)]
    [InlineData(13, true)]
    [InlineData(15, false)]
    [InlineData(17, true)]
    [InlineData(100, false)]
    [InlineData(101, true)]
    [InlineData(997, true)]  // Largest 3-digit prime
    [InlineData(1000, false)]
    [InlineData(0, false)]
    [InlineData(1, false)]
    [InlineData(-5, false)]  // Negative numbers are not prime
    public void IsPrime_ReturnsExpectedResult(long number, bool expected)
    {
        Assert.Equal(expected, number.IsPrime());
    }

    [Theory]
    [InlineData(10, new long[] { 2, 3, 5, 7 })]
    [InlineData(20, new long[] { 2, 3, 5, 7, 11, 13, 17, 19 })]
    [InlineData(2, new long[] { })]  // No primes less than 2
    public void GeneratePrimeNumbers_WithMax_ReturnsCorrectPrimes(int max, long[] expected)
    {
        var result = Int64PrimeNumbers.GeneratePrimeNumbers(max);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GeneratePrimeNumbers_WithMaxLessThanOrEqualTo2_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            Int64PrimeNumbers.GeneratePrimeNumbers(2).ToList());
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            Int64PrimeNumbers.GeneratePrimeNumbers(1).ToList());
    }

    [Fact]
    public void GeneratePrimeNumbers_Infinite_GeneratesCorrectSequence()
    {
        var primes = Int64PrimeNumbers.GeneratePrimeNumbers().Take(10).ToList();
        var expected = new List<long> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };

        Assert.Equal(expected, primes);
    }
}