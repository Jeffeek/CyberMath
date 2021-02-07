#region Using derectives

using BenchmarkDotNet.Running;

#endregion

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