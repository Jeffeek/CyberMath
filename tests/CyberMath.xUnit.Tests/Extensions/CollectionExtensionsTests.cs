#region Using namespaces

using System.Linq;
using CyberMath.Extensions;
using Xunit;

#endregion

namespace CyberMath.xUnit.Tests.Extensions;

public class CollectionExtensionsTests
{
    [Fact]
    public void Swap_test()
    {
        var collection = new[] { 1, 2, 3, 4 };
        collection.Swap(0, 3);
        var expected = new[] { 4, 2, 3, 1 };
        Assert.Equal(expected, collection);
    }

    [Fact]
    public void Shuffle_test()
    {
        var collection = Enumerable.Range(0, 10000).ToArray();
        var copied = new int[10000];
        collection.CopyTo(copied, 0);
        collection.Shuffle();
        Assert.NotEqual(copied, collection);
    }
}