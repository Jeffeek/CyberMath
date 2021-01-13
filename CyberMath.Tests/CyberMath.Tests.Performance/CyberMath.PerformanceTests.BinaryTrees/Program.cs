using System;
using BenchmarkDotNet.Running;
using CyberMath.Extensions;

namespace CyberMath.PerformanceTests.BinaryTrees
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<BinaryTreesAddRangeBenchmark>();
            BenchmarkRunner.Run<BinaryTreesAddBenchmark>();
            BenchmarkRunner.Run<BinaryTreesRemoveBenchmark>();
            BenchmarkRunner.Run<BinaryTreesMaxMinBenchmark>();
            BenchmarkRunner.Run<BinaryTreesOrdersBenchmark>();
        }
    }
}
