using System.Linq;
using CyberMath.Extensions.Int32;
using CyberMath.Extensions.Int64;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.PrimitivesExtensions.Tests
{
    [TestClass]
    public class Int64ExtensionsTests
    {
        [TestMethod]
        public void Int64_GetLength_test()
        {
            for (var i = long.MinValue; i < 0; i /= 10)
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


            var expectMaxValue = long.MaxValue.ToString().Length;
            var actualMaxValue = long.MaxValue.GetLength();
            var expectZero = 0.ToString().Length;
            var actualZero = 0.GetLength();

            Assert.IsTrue(expectZero == actualZero);
            Assert.IsTrue(expectMaxValue == actualMaxValue);
        }

        [TestMethod]
        public void Int64_GetDigits_test()
        {
            var testNumber = 12398564564353;
            var expected = new byte[] { 1,2,3,9,8,5,6,4,5,6,4,3,5,3 };
            var actual = testNumber.GetDigits().ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Int64_GetDigits_zero_test()
        {
            long testNumber = 0;
            var expected = new byte[] { 0 };
            var actual = testNumber.GetDigits().ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Int64_GetDigits_minus_test()
        {
            var testNumber = -94564651554563813;
            var expected = new byte[] { 9,4,5,6,4,6,5,1,5,5,4,5,6,3,8,1,3 };
            var actual = testNumber.GetDigits().ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
