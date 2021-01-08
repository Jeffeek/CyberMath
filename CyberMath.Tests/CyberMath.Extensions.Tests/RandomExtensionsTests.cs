using System;
using System.Linq;
using CyberMath.Extensions.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.Extensions.Tests
{
    [TestClass]
    public class RandomExtensionsTests
    {
        [TestMethod]
        public void TakeLong_test()
        {
            var rnd = new Random();
            long min = -5000000000000000;
            long max = 5000000000000000;
            var values = Enumerable.Range(0, 100).Select(x => rnd.NextLong(min, max));
            var expected = true;
            var actual = values.Any(x => x < int.MinValue || x > int.MaxValue);
            Assert.AreEqual(expected, actual);
        }
    }
}
