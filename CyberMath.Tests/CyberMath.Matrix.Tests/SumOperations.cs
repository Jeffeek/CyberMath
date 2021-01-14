using CyberMath.Structures.Matrix;
using CyberMath.Structures.MatrixExtensions.Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CyberMath.Matrix.Tests
{
    [TestClass]
    public class SumOperations
    {
        [TestMethod]
        public void DiagonalSum_int()
        {
            var n = 3;
            var matrix = new Matrix<int>(n, n)
            {
                [0, 0] = 50,
                [0, 1] = 50,
                [0, 2] = 50,
                [1, 0] = 5,
                [1, 1] = 5,
                [1, 2] = 5,
                [2, 0] = 1,
                [2, 1] = 1,
                [2, 2] = 1
            };
            var expected = 56;
            var actual = matrix.DiagonalSum();
            Assert.IsTrue(actual == expected);
        }

        [TestMethod]
        public void Sum_int()
        {
            var n = 3;
            var matrix = new Matrix<int>(n, n)
            {
                [0, 0] = 50,
                [0, 1] = 50,
                [0, 2] = 50,
                [1, 0] = 5,
                [1, 1] = 5,
                [1, 2] = 5,
                [2, 0] = 1,
                [2, 1] = 1,
                [2, 2] = 1
            };
            var expected = 168;
            var actual = matrix.Sum();
            Assert.IsTrue(actual == expected);
        }

        [TestMethod]
        public void SumSaddlePoints_int()
        {
            var n = 3;
            var matrix = new Matrix<int>(n, n)
            {
                [0, 0] = 50,
                [0, 1] = 50,
                [0, 2] = 50,
                [1, 0] = 5,
                [1, 1] = 5,
                [1, 2] = 5,
                [2, 0] = 1,
                [2, 1] = 1,
                [2, 2] = 1
            };
            var expected = 150;
            var actual = matrix.SumSaddlePoints();
            Assert.IsTrue(actual == expected);
        }
    }
}
