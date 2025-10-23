#region Using namespaces

using BenchmarkDotNet.Running;

#endregion

namespace CyberMath.Performance.Tests;

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<BinaryTreesAddRangeBenchmark>();
        BenchmarkRunner.Run<BinaryTreesAddBenchmark>();
        BenchmarkRunner.Run<BinaryTreesRemoveBenchmark>();
        BenchmarkRunner.Run<BinaryTreesMaxMinBenchmark>();
        BenchmarkRunner.Run<BinaryTreesOrdersBenchmark>();
        BenchmarkRunner.Run<StringExtensionsBenchmark>();
    }
}