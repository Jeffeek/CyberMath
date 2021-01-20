using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using СyberMath.Structures.Matrices.Matrix;

namespace CyberMath.Matrix.Tests
{
    [TestClass]
    public class CreationOperations
    {
        [TestMethod]
        public void CreateIdentityMatrixTest_3()
        {
            var n = 3;
            var actual = Matrix<int>.CreateIdentityMatrix(n);
            var expected = new Matrix<int>(n, n)
            {
                [0, 0] = 1,
                [0, 1] = 0,
                [0, 2] = 0,
                [1, 0] = 0,
                [1, 1] = 1,
                [1, 2] = 0,
                [2, 0] = 0,
                [2, 1] = 0,
                [2, 2] = 1
            };
            Assert.IsTrue(actual.Equals(expected));
        }

        [TestMethod]
        public void TransposeTest_int_success()
        {
            var matrix = new Matrix<int>(3, 4)
            {
                [0, 0] = 50,
                [0, 1] = 11,
                [0, 2] = -50,
                [0, 3] = 77,
                [1, 0] = 50,
                [1, 1] = 11,
                [1, 2] = -50,
                [1, 3] = 77,
                [2, 0] = 50,
                [2, 1] = 11,
                [2, 2] = -50,
                [2, 3] = 77
            };

            var expected = new Matrix<int>(4, 3)
            {
                [0, 0] = 50,
                [0, 1] = 50,
                [0, 2] = 50,
                [1, 0] = 11,
                [1, 1] = 11,
                [1, 2] = 11,
                [2, 0] = -50,
                [2, 1] = -50,
                [2, 2] = -50,
                [3, 0] = 77,
                [3, 1] = 77,
                [3, 2] = 77
            };

            var actual = matrix.Transpose();
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void CreateMatrixWithoutColumn_test()
        {
            var matrix = new Matrix<int>(3, 4)
            {
                [0, 0] = 50,
                [0, 1] = 50,
                [0, 2] = 50,
                [0, 3] = 50,
                [1, 0] = 11,
                [1, 1] = 11,
                [1, 2] = 11,
                [1, 3] = 11,
                [2, 0] = -50,
                [2, 1] = -50,
                [2, 2] = -50,
                [2, 3] = -50
            };

            var expected = new Matrix<int>(3, 3)
            {
                [0, 0] = 50,
                [0, 1] = 50,
                [0, 2] = 50,
                [1, 0] = 11,
                [1, 1] = 11,
                [1, 2] = 11,
                [2, 0] = -50,
                [2, 1] = -50,
                [2, 2] = -50
            };

            var actual = matrix.CreateMatrixWithoutColumn(3) as Matrix<int>;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void CreateMatrixWithoutColumn_test_exception()
        {
            var matrix = new Matrix<int>(3, 4)
            {
                [0, 0] = 50,
                [0, 1] = 50,
                [0, 2] = 50,
                [0, 3] = 50,
                [1, 0] = 11,
                [1, 1] = 11,
                [1, 2] = 11,
                [1, 3] = 11,
                [2, 0] = -50,
                [2, 1] = -50,
                [2, 2] = -50,
                [2, 3] = -50
            };

            Assert.ThrowsException<ArgumentException>(() => matrix.CreateMatrixWithoutColumn(4));
        }

        [TestMethod]
        public void CreateMatrixWithoutRow_test()
        {
            var matrix = new Matrix<int>(4, 3)
            {
                [0, 0] = 50,
                [0, 1] = 50,
                [0, 2] = 50,
                [1, 0] = 5,
                [1, 1] = 5,
                [1, 2] = 5,
                [2, 0] = 1,
                [2, 1] = 1,
                [2, 2] = 1,
                [3, 0] = 2,
                [3, 1] = 2,
                [3, 2] = 2
            };

            var expected = new Matrix<int>(3, 3)
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

            var actual = matrix.CreateMatrixWithoutRow(3) as Matrix<int>;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void CreateMatrixWithoutRow_test_exception()
        {
            var matrix = new Matrix<int>(3, 4)
            {
                [0, 0] = 50,
                [0, 1] = 50,
                [0, 2] = 50,
                [0, 3] = 50,
                [1, 0] = 11,
                [1, 1] = 11,
                [1, 2] = 11,
                [1, 3] = 11,
                [2, 0] = -50,
                [2, 1] = -50,
                [2, 2] = -50,
                [2, 3] = -50
            };

            Assert.ThrowsException<ArgumentException>(() => matrix.CreateMatrixWithoutRow(3));
        }
    }
}
