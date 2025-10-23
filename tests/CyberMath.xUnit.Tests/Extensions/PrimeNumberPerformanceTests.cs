#region Using namespaces

using System;
using System.IO;
using System.Linq;
using CyberMath.Extensions;
using Xunit;

#endregion

namespace CyberMath.xUnit.Tests.Extensions;

/// <summary>
/// Performance and validation tests for prime number generation
/// </summary>
public class PrimeNumberPerformanceTests
{
    [Fact]
    public void PrimeNumsCheck_10_000_FromFile()
    {
        var filePath = "Assets/10_000_primes.txt";

        if (!File.Exists(filePath))
        {
            // Skip test if file doesn't exist
            return;
        }

        var text = File.ReadAllText(filePath);
        var nums = text.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                       .Select(s => int.Parse(s.Trim()))
                       .ToArray();

        foreach (var num in nums)
        {
            Assert.True(num.IsPrime(), $"Expected {num} to be prime");
        }
    }

    [Fact]
    public void GeneratePrimeNums_100_Fast()
    {
        var primes = Int32PrimeNumbers.GeneratePrimeNumbers()
                                      .Take(100)
                                      .ToArray();

        Assert.Equal(100, primes.Length);
        Assert.Equal(2, primes[0]);
        Assert.Equal(3, primes[1]);
        Assert.Equal(5, primes[2]);
        Assert.Equal(541, primes[99]); // 100th prime number
    }
}
