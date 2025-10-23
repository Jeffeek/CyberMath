using CyberMath.Extensions;
using Xunit;

namespace CyberMath.xUnit.Tests.Extensions;

public class DeepCopyTests
{
    [Fact]
    public void IntDeepCopy()
    {
        var test = 5;
        var actual = test.ReflectionDeepCopy();
        Assert.Equal(actual, test);
    }

    [Fact]
    public void StringDeepCopy()
    {
        var test = "test";
        var actual = test.ReflectionDeepCopy();
        test = "haha";
        Assert.NotEqual(actual, test);
    }
}