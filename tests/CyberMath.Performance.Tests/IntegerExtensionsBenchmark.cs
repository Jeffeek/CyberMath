#region Using namespaces

using BenchmarkDotNet.Attributes;
using CyberMath.Extensions;

#endregion

namespace CyberMath.Performance.Tests;

/// <summary>
/// Benchmarks for integer extension methods (IsPalindrome, GetLength, ToBinary, ToHex, GCD, LCM)
/// </summary>
[MemoryDiagnoser]
public class IntegerExtensionsBenchmark
{
    private const int TestValue = 123454321;
    private const long TestValueLong = 123454321123454321L;

    #region IsPalindrome Tests

    [Benchmark]
    public bool IsPalindrome_Int32_True() => 12321.IsPalindrome();

    [Benchmark]
    public bool IsPalindrome_Int32_False() => 12345.IsPalindrome();

    [Benchmark]
    public bool IsPalindrome_Int64_True() => 12321L.IsPalindrome();

    [Benchmark]
    public bool IsPalindrome_Int64_False() => 12345L.IsPalindrome();

    #endregion

    #region GetLength Tests

    [Benchmark]
    public int GetLength_Int32() => TestValue.GetLength();

    [Benchmark]
    public int GetLength_Int64() => TestValueLong.GetLength();

    [Benchmark]
    public int GetLength_MaxValue_Int32() => int.MaxValue.GetLength();

    [Benchmark]
    public int GetLength_MaxValue_Int64() => long.MaxValue.GetLength();

    #endregion

    #region Binary/Hex Conversion Tests

    [Benchmark]
    public string ToBinary_Int32() => TestValue.ToBinary();

    [Benchmark]
    public string ToBinary_Int64() => TestValueLong.ToBinary();

    [Benchmark]
    public string ToHex_Int32() => TestValue.ToHex();

    [Benchmark]
    public string ToHex_Int64() => TestValueLong.ToHex();

    #endregion

    #region GCD/LCM Tests

    [Benchmark]
    public int GCD_Int32() => 48.GCD(18);

    [Benchmark]
    public long GCD_Int64() => 48L.GCD(18L);

    [Benchmark]
    public int LCM_Int32() => 48.LCM(18);

    [Benchmark]
    public long LCM_Int64() => 48L.LCM(18L);

    [Benchmark]
    public int GCD_Large_Int32() => 123456.GCD(789012);

    [Benchmark]
    public long GCD_Large_Int64() => 123456789L.GCD(987654321L);

    #endregion
}
