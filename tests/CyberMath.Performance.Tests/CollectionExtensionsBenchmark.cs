#region Using namespaces

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using CyberMath.Extensions;

#endregion

namespace CyberMath.Performance.Tests;

/// <summary>
/// Benchmarks for collection extension methods (Shuffle, Permutations, RandomItem)
/// </summary>
[MemoryDiagnoser]
public class CollectionExtensionsBenchmark
{
    private List<int>? _smallList;
    private List<int>? _mediumList;
    private List<int>? _largeList;

    [GlobalSetup]
    public void Setup()
    {
        _smallList = Enumerable.Range(1, 10).ToList();
        _mediumList = Enumerable.Range(1, 100).ToList();
        _largeList = Enumerable.Range(1, 1000).ToList();
    }

    #region Shuffle Operations

    [Benchmark]
    public void Shuffle_Small()
    {
        var list = new List<int>(_smallList!);
        list.Shuffle();
    }

    [Benchmark]
    public void Shuffle_Medium()
    {
        var list = new List<int>(_mediumList!);
        list.Shuffle();
    }

    [Benchmark]
    public void Shuffle_Large()
    {
        var list = new List<int>(_largeList!);
        list.Shuffle();
    }

    #endregion

    #region Permutations Operations

    [Benchmark]
    public int Permutations_5Elements()
    {
        var result = Enumerable.Range(1, 5).Permutations();
        return result.Count();
    }

    [Benchmark]
    public int Permutations_6Elements()
    {
        var result = Enumerable.Range(1, 6).Permutations();
        return result.Count();
    }

    [Benchmark]
    public int PermutationsWithRepeat_4Elements()
    {
        var result = Enumerable.Range(1, 4).PermutationsWithRepeat();
        return result.Count();
    }

    #endregion

    #region RandomItem Operations

    [Benchmark]
    public int RandomItem_Small() => _smallList!.RandomItem();

    [Benchmark]
    public int RandomItem_Medium() => _mediumList!.RandomItem();

    [Benchmark]
    public int RandomItem_Large() => _largeList!.RandomItem();

    #endregion

    #region Swap Operations

    [Benchmark]
    public void Swap_Multiple()
    {
        var list = new List<int>(_mediumList!);
        for (var i = 0; i < 50; i++)
        {
            list.Swap(i, 99 - i);
        }
    }

    #endregion
}
