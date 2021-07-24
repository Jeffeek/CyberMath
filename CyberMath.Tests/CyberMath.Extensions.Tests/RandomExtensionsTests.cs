#region Using namespaces

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.Extensions.Tests
{
    [TestClass]
    public class RandomExtensionsTests
    {
        [TestMethod]
        public void TakeLong_test()
        {
            var rnd = new Random();
            var min = -5000000000000000;
            var max = 5000000000000000;

            var values = Enumerable.Range(0, 100)
                                   .Select(x => rnd.NextLong(min, max));

            var actual = values.Any(x => x < System.Int32.MinValue || x > System.Int32.MaxValue);
            Assert.AreEqual(true, actual);
        }
    }
}