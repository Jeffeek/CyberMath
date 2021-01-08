using BenchmarkDotNet.Running;

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
        }
    }
}
