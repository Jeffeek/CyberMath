using CyberMath.Structures.Matrices.Base.Exceptions;
using CyberMath.Structures.Matrices.Extensions;
using CyberMath.Structures.Matrices.Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.Matrix.Tests
{
    [TestClass]
    public class MathPrimitiveOperations
    {
        [TestMethod]
        public void Add_int_positive()
        {
            var matrix1 = new Matrix<int>(3, 3)
            {
                [0, 0] = 6,
                [0, 1] = 3,
                [0, 2] = 5,
                [1, 0] = 4,
                [1, 1] = 34,
                [1, 2] = 6,
                [2, 0] = 34,
                [2, 1] = 0,
                [2, 2] = 2
            };

            var matrix2 = new Matrix<int>(3, 3)
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

            var expected = new Matrix<int>(3, 3)
            {
                [0, 0] = 240,
                [0, 1] = 5,
                [0, 2] = 6,
                [1, 0] = 7,
                [1, 1] = 76,
                [1, 2] = -84,
                [2, 0] = 38,
                [2, 1] = 2,
                [2, 2] = 4
            };

            var actual = matrix1.Add(matrix2);
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void Add_int_column_exception()
        {
            var matrix1 = new Matrix<int>(3, 4)
            {
                [0, 0] = 6,
                [0, 1] = 3,
                [0, 2] = 5,
                [0, 3] = -1,
                [1, 0] = 4,
                [1, 1] = 34,
                [1, 2] = 6,
                [1, 3] = -1,
                [2, 0] = 34,
                [2, 1] = 0,
                [2, 2] = 2,
                [2, 3] = -1
            };

            var matrix2 = new Matrix<int>(3, 3)
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

            Assert.ThrowsException<MatrixInvalidOperationException>(() => matrix1.Add(matrix2));
        }

        [TestMethod]
        public void Add_int_row_exception()
        {
            var matrix1 = new Matrix<int>(4, 3)
            {
                [0, 0] = 6,
                [0, 1] = 3,
                [0, 2] = 5,
                [1, 0] = 4,
                [1, 1] = 34,
                [1, 2] = 6,
                [2, 0] = 34,
                [2, 1] = 0,
                [2, 2] = 2,
                [3, 0] = 11,
                [3, 1] = 11,
                [3, 2] = 11
            };

            var matrix2 = new Matrix<int>(3, 3)
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

            Assert.ThrowsException<MatrixInvalidOperationException>(() => matrix1.Add(matrix2));
        }

        [TestMethod]
        public void Add_int_number_positive()
        {
            var number = 5;
            var matrix = new Matrix<int>(3, 3)
            {
                [0, 0] = 6,
                [0, 1] = 3,
                [0, 2] = 5,
                [1, 0] = 4,
                [1, 1] = 34,
                [1, 2] = 6,
                [2, 0] = 34,
                [2, 1] = 0,
                [2, 2] = 2
            };

            var actual = matrix.MulOnNumber(number);
            var expected = new Matrix<int>(3, 3)
            {
                [0, 0] = 30,
                [0, 1] = 15,
                [0, 2] = 25,
                [1, 0] = 20,
                [1, 1] = 170,
                [1, 2] = 30,
                [2, 0] = 170,
                [2, 1] = 0,
                [2, 2] = 10
            };

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void Sub_int_positive()
        {
            var matrix1 = new Matrix<int>(3, 3)
            {
                [0, 0] = 6,
                [0, 1] = 3,
                [0, 2] = 5,
                [1, 0] = 4,
                [1, 1] = 34,
                [1, 2] = 6,
                [2, 0] = 34,
                [2, 1] = 0,
                [2, 2] = 2
            };

            var matrix2 = new Matrix<int>(3, 3)
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

            var expected = new Matrix<int>(3, 3)
            {
                [0, 0] = -228,
                [0, 1] = 1,
                [0, 2] = 4,
                [1, 0] = 1,
                [1, 1] = -8,
                [1, 2] = 96,
                [2, 0] = 30,
                [2, 1] = -2,
                [2, 2] = 0
            };

            var actual = matrix1.Sub(matrix2);
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void Sub_int_column_exception()
        {
            var matrix1 = new Matrix<int>(3, 4)
            {
                [0, 0] = 6,
                [0, 1] = 3,
                [0, 2] = 5,
                [0, 3] = -1,
                [1, 0] = 4,
                [1, 1] = 34,
                [1, 2] = 6,
                [1, 3] = -1,
                [2, 0] = 34,
                [2, 1] = 0,
                [2, 2] = 2,
                [2, 3] = -1
            };

            var matrix2 = new Matrix<int>(3, 3)
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

            Assert.ThrowsException<MatrixInvalidOperationException>(() => matrix1.Sub(matrix2));
        }

        [TestMethod]
        public void Sub_int_row_exception()
        {
            var matrix1 = new Matrix<int>(4, 3)
            {
                [0, 0] = 6,
                [0, 1] = 3,
                [0, 2] = 5,
                [1, 0] = 4,
                [1, 1] = 34,
                [1, 2] = 6,
                [2, 0] = 34,
                [2, 1] = 0,
                [2, 2] = 2,
                [3, 0] = 11,
                [3, 1] = 11,
                [3, 2] = 11
            };

            var matrix2 = new Matrix<int>(3, 3)
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

            Assert.ThrowsException<MatrixInvalidOperationException>(() => matrix1.Sub(matrix2));
        }

        [TestMethod]
        public void Mul_int_positive()
        {
            var matrix = new Matrix<int>(3, 3)
            {
                [0, 0] = 6,
                [0, 1] = 3,
                [0, 2] = 5,
                [1, 0] = 4,
                [1, 1] = 34,
                [1, 2] = 6,
                [2, 0] = 34,
                [2, 1] = 0,
                [2, 2] = 2
            };

            var matrix2 = new Matrix<int>(3, 3)
            {
                [0, 0] = 6,
                [0, 1] = 3,
                [0, 2] = 5,
                [1, 0] = 4,
                [1, 1] = 34,
                [1, 2] = 6,
                [2, 0] = 34,
                [2, 1] = 0,
                [2, 2] = 2
            };

            var actual = matrix.Multiplication(matrix2);
            var expected = new Matrix<int>(3, 3)
            {
                [0, 0] = 218,
                [0, 1] = 120,
                [0, 2] = 58,
                [1, 0] = 364,
                [1, 1] = 1168,
                [1, 2] = 236,
                [2, 0] = 272,
                [2, 1] = 102,
                [2, 2] = 174
            };

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void Mul_int_exception()
        {
            var matrix = new Matrix<int>(3, 4)
            {
                [0, 0] = 6,
                [0, 1] = 3,
                [0, 2] = 5,
                [0, 3] = 5,
                [1, 0] = 4,
                [1, 1] = 34,
                [1, 2] = 6,
                [1, 3] = 6,
                [2, 0] = 34,
                [2, 1] = 0,
                [2, 2] = 2,
                [2, 3] = 2
            };

            var matrix2 = new Matrix<int>(3, 4)
            {
                [0, 0] = 6,
                [0, 1] = 3,
                [0, 2] = 5,
                [0, 3] = 5,
                [1, 0] = 4,
                [1, 1] = 34,
                [1, 2] = 6,
                [1, 3] = 6,
                [2, 0] = 34,
                [2, 1] = 0,
                [2, 2] = 2,
                [2, 3] = 2
            };

            Assert.ThrowsException<MatrixInvalidOperationException>(() => matrix.Multiplication(matrix2));
        }
    }
}
