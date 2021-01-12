using CyberMath.Structures.Matrix;
using CyberMath.Structures.MatrixExtensions.Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.Matrix.Tests
{
    [TestClass]
    public class SumOperations
    {
        [TestMethod]
        public void DiagonalSum_int()
        {
            int n = 3;
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
            int expected = 56;
            int actual = matrix.DiagonalSum();
            Assert.IsTrue(actual == expected);
        }

        [TestMethod]
        public void Sum_int()
        {
            int n = 3;
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
            int expected = 168;
            int actual = matrix.Sum();
            Assert.IsTrue(actual == expected);
        }

        [TestMethod]
        public void SumSaddlePoints_int()
        {
            int n = 3;
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
            int expected = 150;
            int actual = matrix.SumSaddlePoints();
            Assert.IsTrue(actual == expected);
        }
    }
}
