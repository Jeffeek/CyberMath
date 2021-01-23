using BenchmarkDotNet.Running;

namespace CyberMath.PerformanceTests_Extensions
{
	internal static class Program
    {
	    private static void Main(string[] args)
        {
	        BenchmarkRunner.Run<StringExtensions>();
        }
    }
}
