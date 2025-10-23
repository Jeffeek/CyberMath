using CyberMath.Structures.Matrices.Base.Exceptions;
using CyberMath.Structures.Matrices.Matrix;
using System;
using System.Linq;
using CyberMath.Structures.Matrices.Base;
using CyberMath.Structures.Matrices.Extensions;
using Xunit;

namespace CyberMath.xUnit.Tests.Structures.Matrices;

public class MatrixTests
{
    [Fact]
    public void SumAgeTest()
    {
        var matrix = new Matrix<Animal>(3, 3)
        {
            [0, 0] = new(5),
            [0, 1] = new(5),
            [0, 2] = new(5),
            [1, 0] = new(5),
            [1, 1] = new(5),
            [1, 2] = new(5),
            [2, 0] = new(5),
            [2, 1] = new(5),
            [2, 2] = new(5)
        };

        var expected = 45;
        var actual = matrix.Sum(x => x.Sum(y => y.Age));
        Assert.Equal(expected, actual);
    }

    private class Animal(int age)
    {
        public int Age { get; } = age;
    }

    [Fact]
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

        Assert.True(actual.Equals(expected));
    }

    [Fact]
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
        Assert.Equal(expected, actual);
    }

    [Fact]
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
        Assert.Equal(expected, actual);
    }

    [Fact]
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

        Assert.Throws<ArgumentException>(() => matrix.CreateMatrixWithoutColumn(4));
    }

    [Fact]
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
        Assert.Equal(expected, actual);
    }

    [Fact]
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

        Assert.Throws<ArgumentException>(() => matrix.CreateMatrixWithoutRow(3));
    }

    [Fact]
    public void DeterminantCalculateTest__int_3_positive()
    {
        var n = 3;

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

        var actual = matrix.CalculateDeterminant();
        var expected = 60882;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeterminantCalculateTest__int_exception()
    {
        var n = 3;

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

        Assert.Throws<InvalidOperationException>(() => matrix.CalculateDeterminant());
    }

    [Fact]
    public void InvertibleMatrixTest_int_positive()
    {
        var n = 3;

        var matrix = new Matrix<int>(n, n)
        {
            [0, 0] = 5,
            [0, 1] = 2,
            [0, 2] = 17,
            [1, 0] = 3,
            [1, 1] = 42,
            [1, 2] = -90,
            [2, 0] = 1,
            [2, 1] = 2,
            [2, 2] = 2
        };

        var expected = new Matrix<double>(n, n)
        {
            [0, 0] = 0.51d,
            [0, 1] = -1.00d,
            [0, 2] = -1.73d,
            [1, 0] = -1.00d,
            [1, 1] = -0.01d,
            [1, 2] = -1.00d,
            [2, 0] = -0.07d,
            [2, 1] = -1.00,
            [2, 2] = 0.40d
        };

        var actual = matrix.CreateInvertibleMatrix() as Matrix<double>;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void InvertibleMatrixTest_int_null()
    {
        var n = 3;

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
        Assert.Null(actual);
    }

    [Fact]
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
        Assert.Equal(expected, actual);
    }

    [Fact]
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

        Assert.Throws<MatrixInvalidOperationException>(() => matrix1.Add(matrix2));
    }

    [Fact]
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

        Assert.Throws<MatrixInvalidOperationException>(() => matrix1.Add(matrix2));
    }

    [Fact]
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

        Assert.Equal(expected, actual);
    }

    [Fact]
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
        Assert.Equal(expected, actual);
    }

    [Fact]
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

        Assert.Throws<MatrixInvalidOperationException>(() => matrix1.Sub(matrix2));
    }

    [Fact]
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

        Assert.Throws<MatrixInvalidOperationException>(() => matrix1.Sub(matrix2));
    }

    [Fact]
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

        Assert.Equal(expected, actual);
    }

    [Fact]
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

        Assert.Throws<MatrixInvalidOperationException>(() => matrix.Multiplication(matrix2));
    }

    [Fact]
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

        Assert.Equal(expected, actual);
    }

    [Fact]
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

        Assert.Equal(expected, actual);
    }

    [Fact]
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

        Assert.Equal(expected, actual);
    }

    [Fact]
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

        Assert.Equal(expected, actual);
    }

    [Fact]
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

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DiagonalSum_int()
    {
        var n = 3;

        var matrix = new Matrix<int>(n, n)
        {
            [0, 0] = 50,
            [0, 1] = 0,
            [0, 2] = 0,
            [1, 0] = 5,
            [1, 1] = 10,
            [1, 2] = 5,
            [2, 0] = 1,
            [2, 1] = 1,
            [2, 2] = 5
        };

        var expected = 65;
        var actual = matrix.DiagonalSum();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SideDiagonalSum_int()
    {
        var n = 3;

        var matrix = new Matrix<int>(n, n)
        {
            [0, 0] = 1,
            [0, 1] = 2,
            [0, 2] = 50,
            [1, 0] = 1,
            [1, 1] = 5,
            [1, 2] = 5,
            [2, 0] = 3,
            [2, 1] = 0,
            [2, 2] = 0
        };

        var expected = 58;
        var actual = matrix.SideDiagonalSum();
        Assert.Equal(expected, actual);
    }

    [Fact]
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
        var actual = matrix.Sum(x => x.Sum());
        Assert.Equal(expected, actual);
    }

    [Fact]
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
        Assert.Equal(expected, actual);
    }
}