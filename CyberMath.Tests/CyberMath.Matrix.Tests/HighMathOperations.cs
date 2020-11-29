using System;
using CyberMath.Matrix.Extensions;
using CyberMath.Matrix.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.Matrix.Tests
{
    [TestClass]
    public class HighMathOperations
    {
        [TestMethod]
        public void DeterminantCalculateTest__int_3_positive()
        {
            int n = 3;
            var matrix = new Matrix<int>(n, n)
            {
                [0, 0] = 234,
                [0, 1] = 2,
                [0, 2] = 1,
                [1, 0] = 3,
                [1, 1] = 42,
                [1, 2] = -90,
                [2, 0] = 4,
                [2, 1] = 2,
                [2, 2] = 2
            };
            int actual = matrix.CalculateDeterminant();
            int expected = 60882;
            Assert.IsTrue(actual == expected);
        }

        [TestMethod]
        public void DeterminantCalculateTest__int_exception()
        {
            int n = 3;
            var matrix = new Matrix<int>(n, 4)
            {
                [0, 0] = 234,
                [0, 1] = 2,
                [0, 2] = 1,
                [0, 3] = 1,
                [1, 0] = 3,
                [1, 1] = 42,
                [1, 2] = -90,
                [1, 3] = -90,
                [2, 0] = 4,
                [2, 1] = 2,
                [2, 2] = 2,
                [2, 3] = 2
            };
            Assert.ThrowsException<InvalidOperationException>(() => matrix.CalculateDeterminant());
        }

        [TestMethod]
        public void InvertibleMatrixTest_int_positive()
        {
            int n = 3;
            var matrix = new Matrix<int>(n, n)
            {
                [0, 0] = 234,
                [0, 1] = 2,
                [0, 2] = 1,
                [1, 0] = 3,
                [1, 1] = 42,
                [1, 2] = -90,
                [2, 0] = 4,
                [2, 1] = 2,
                [2, 2] = 2
            };

            var expected = new Matrix<double>(n,n)
            {
                [0, 0] = 0.00d,
                [0, 1] = -1.00d,
                [0, 2] = 0.00d,
                [1, 0] = -1.00d,
                [1, 1] = 0.01d,
                [1, 2] = -1.00d,
                [2, 0] = 0.00d,
                [2, 1] = -1.00,
                [2, 2] = 0.16d
            };

            var actual = matrix.CreateInvertibleMatrix() as Matrix<double>;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Assert.IsTrue(expected[i,j] == actual[i,j]);
                }
            }
        }

        [TestMethod]
        public void InvertibleMatrixTest_int_null()
        {
            int n = 3;
            var matrix = new Matrix<int>(4, n)
            {
                [0, 0] = 234,
                [0, 1] = 2,
                [0, 2] = 1,
                [1, 0] = 3,
                [1, 1] = 42,
                [1, 2] = -90,
                [2, 0] = 4,
                [2, 1] = 2,
                [2, 2] = 2
            };

            var actual = matrix.CreateInvertibleMatrix();
            Assert.IsNull(actual);
        }
    }
}
