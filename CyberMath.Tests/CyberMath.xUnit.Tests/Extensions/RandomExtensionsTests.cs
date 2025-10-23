#region Using namespaces

using System;
using System.Linq;
using CyberMath.Extensions;
using Xunit;

#endregion

namespace CyberMath.xUnit.Tests.Extensions;

public class RandomExtensionsTests
{
    [Fact]
    public void TakeLong_test()
    {
        var rnd = new Random();
        var min = -5000000000000000;
        var max = 5000000000000000;
        var values = Enumerable.Range(0, 100).Select(_ => rnd.NextLong(min, max));
        var actual = values.Any(x => x is < int.MinValue or > int.MaxValue);
        Assert.True(actual);
    }
}