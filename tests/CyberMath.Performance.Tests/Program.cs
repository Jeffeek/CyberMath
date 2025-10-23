#region Using namespaces

using BenchmarkDotNet.Running;

#endregion

namespace CyberMath.Performance.Tests;

public class Program
{
    public static void Main(string[] args)
    {
        // Use BenchmarkSwitcher to allow selecting benchmarks via command line
        // Examples:
        //   dotnet run -c Release -- --filter *BinaryTrees*
        //   dotnet run -c Release -- --list flat
        //   dotnet run -c Release -- --filter *String*
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}