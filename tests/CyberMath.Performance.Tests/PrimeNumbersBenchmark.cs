#region Using namespaces

using System.Linq;
using BenchmarkDotNet.Attributes;
using CyberMath.Extensions;

#endregion

namespace CyberMath.Performance.Tests;

/// <summary>
/// Benchmarks for prime number operations (IsPrime, GeneratePrimeNumbers)
/// </summary>
[MemoryDiagnoser]
public class PrimeNumbersBenchmark
{
    [Params(100, 1000, 10000)]
    public int MaxNumber;

    #region IsPrime Tests

    [Benchmark]
    public int IsPrime_TestRange()
    {
        var count = 0;
        for (var i = 2; i <= MaxNumber; i++)
        {
            if (i.IsPrime())
                count++;
        }
        return count;
    }

    [Benchmark]
    public int IsPrime_TestRange_Int64()
    {
        var count = 0;
        for (long i = 2; i <= MaxNumber; i++)
        {
            if (i.IsPrime())
                count++;
        }
        return count;
    }

    #endregion

    #region GeneratePrimeNumbers Tests

    [Benchmark]
    public int GeneratePrimes_Int32()
    {
        var primes = Int32PrimeNumbers.GeneratePrimeNumbers(MaxNumber).ToArray();
        return primes.Length;
    }

    [Benchmark]
    public int GeneratePrimes_Int64()
    {
        var primes = Int64PrimeNumbers.GeneratePrimeNumbers(MaxNumber).ToArray();
        return primes.Length;
    }

    #endregion

    #region Specific Prime Tests

    [Benchmark]
    public bool IsPrime_LargePrime_Int32() => 104729.IsPrime(); // 10000th prime

    [Benchmark]
    public bool IsPrime_LargePrime_Int64() => 104729L.IsPrime(); // 10000th prime

    [Benchmark]
    public bool IsPrime_Composite() => 104728.IsPrime(); // Not prime

    #endregion
}
