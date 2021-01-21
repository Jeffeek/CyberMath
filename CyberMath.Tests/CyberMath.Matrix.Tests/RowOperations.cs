using System.Linq;
using CyberMath.Structures.Matrices.Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.Matrix.Tests
{
    [TestClass]
    public class RowOperations
    {
        [TestMethod]
        public void AvgInRowTest_int()
        {
            var matrix = new Matrix<int>(2, 2)
            {
                [0, 0] = 5,
                [0, 1] = 10,
                [1, 0] = 20,
                [1, 1] = 50
            };

            var actual = matrix.Select(x => x.Average()).ToArray();
            var expected = new[] { 7.5, 35.0 };
            CollectionAssert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void MinInRowTest_int()
        {
            var matrix = new Matrix<int>(2, 2)
            {
                [0, 0] = 5,
                [0, 1] = 10,
                [1, 0] = 20,
                [1, 1] = 50
            };

            var actual = matrix.Select(x => x.Min()).ToArray();
            var expected = new[] { 5, 20 };
            CollectionAssert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void MaxInRowTest_int()
        {
            var matrix = new Matrix<int>(2, 2)
            {
                [0, 0] = 5,
                [0, 1] = 10,
                [1, 0] = 20,
                [1, 1] = 50
            };

            var actual = matrix.Select(x => x.Max()).ToArray();
            var expected = new[] { 10, 50 };
            CollectionAssert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void MinInRowTest_string()
        {
            var matrix = new Matrix<string>(2, 2)
            {
                [0, 0] = "ab",
                [0, 1] = "bc",
                [1, 0] = "cd",
                [1, 1] = "de"
            };

            var actual = matrix.Select(x => x.Min()).ToArray();
            var expected = new[] { "ab", "cd" };
            CollectionAssert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void MaxInRowTest_string()
        {
            var matrix = new Matrix<string>(2, 2)
            {
                [0, 0] = "ab",
                [0, 1] = "bc",
                [1, 0] = "cd",
                [1, 1] = "de"
            };

            var actual = matrix.Select(x => x.Max()).ToArray();
            var expected = new[] { "bc", "de" };
            CollectionAssert.AreEqual(actual, expected);
        }
    }
}
