#region Using namespaces

using BenchmarkDotNet.Running;

#endregion

namespace CyberMath.PerformanceTests
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