using System;
using BenchmarkDotNet.Running;
using CyberMath.Extensions;

namespace CyberMath.PerformanceTests.BinaryTrees
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var elems = new int[] {1, 2, 3};
            var erg = elems.PermutationsWithRepeat();
            //BenchmarkRunner.Run<BinaryTreesAddRangeBenchmark>();
            //BenchmarkRunner.Run<BinaryTreesAddBenchmark>();
            //BenchmarkRunner.Run<BinaryTreesRemoveBenchmark>();
            //BenchmarkRunner.Run<BinaryTreesMaxMinBenchmark>();
            //BenchmarkRunner.Run<BinaryTreesOrdersBenchmark>();
        }
    }
}
