using CyberMath.Primitives.Int32;
using CyberMath.Primitives.Int64;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.PrimitivesExtensions.Tests
{
    [TestClass]
    public class Int64ExtensionsTests
    {
        [TestMethod]
        public void Int64_GetLength_test()
        {
            for (long i = long.MinValue; i < 0; i /= 10)
            {
                long expect = i < 0 ? i.ToString().Length - 1 : i.ToString().Length;
                long actual = i.GetLength();
                Assert.IsTrue(expect == actual);
            }

            for (long i = 1; i <= 1000000000000000000; i *= 10)
            {
                long expect = i < 0 ? i.ToString().Length - 1 : i.ToString().Length;
                long actual = i.GetLength();
                Assert.IsTrue(expect == actual);
            }


            int expectMaxValue = long.MaxValue.ToString().Length;
            int actualMaxValue = long.MaxValue.GetLength();
            int expectZero = 0.ToString().Length;
            int actualZero = 0.GetLength();

            Assert.IsTrue(expectZero == actualZero);
            Assert.IsTrue(expectMaxValue == actualMaxValue);
        }
    }
}
