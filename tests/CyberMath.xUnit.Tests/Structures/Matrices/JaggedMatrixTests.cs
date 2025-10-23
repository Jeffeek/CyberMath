using System.Linq;
using CyberMath.Structures.Matrices.Base;
using CyberMath.Structures.Matrices.Base.Exceptions;
using CyberMath.Structures.Matrices.Extensions;
using CyberMath.Structures.Matrices.Jagged_Matrix;
using Xunit;

namespace CyberMath.xUnit.Tests.Structures.Matrices;

public class JaggedMatrixTests
{
    [Fact]
    public void CreateJuggedMatrixWithoutColumn_0_Test()
    {
        var jugged = new JuggedMatrix<int>(3, 2, 1, 3)
        {
            [0, 0] = 1, [0, 1] = 10, [1, 0] = 5, [2, 0] = 90, [2, 1] = 55, [2, 2] = 12
        };
        var actual = jugged.CreateMatrixWithoutColumn(0);
        var expected = new JuggedMatrix<int>(3, 1, 0, 2)
        {
            [0, 0] = 10, [2, 0] = 55, [2, 1] = 12
        };
        for (var i = 0; i < expected.RowsCount; i++)
            for (var j = 0; j < expected.ElementsInRow(i); j++)
                Assert.Equal(expected[i, j], actual[i, j]);
    }

    [Fact]
    public void CreateJuggedMatrixWithoutColumn_1_Test()
    {
        var jugged = new JuggedMatrix<int>(3, 2, 1, 3)
        {
            [0, 0] = 1, [0, 1] = 10, [1, 0] = 5, [2, 0] = 90, [2, 1] = 55, [2, 2] = 12
        };
        var actual = jugged.CreateMatrixWithoutColumn(1);
        var expected = new JuggedMatrix<int>(3, 1, 1, 2)
        {
            [0, 0] = 1, [1, 0] = 5, [2, 0] = 90, [2, 1] = 12
        };
        for (var i = 0; i < expected.RowsCount; i++)
            for (var j = 0; j < expected.ElementsInRow(i); j++)
                Assert.Equal(expected[i, j], actual[i, j]);
    }

    [Fact]
    public void CreateJuggedMatrixWithoutRow_1_Test()
    {
        var jugged = new JuggedMatrix<int>(3, 2, 1, 3)
        {
            [0, 0] = 1, [0, 1] = 10, [1, 0] = 5, [2, 0] = 90, [2, 1] = 55, [2, 2] = 12
        };
        var actual = jugged.CreateMatrixWithoutRow(1);
        var expected = new JuggedMatrix<int>(2, 2, 3)
        {
            [0, 0] = 1, [0, 1] = 10, [1, 0] = 90, [1, 1] = 55, [1, 2] = 12
        };
        for (var i = 0; i < expected.RowsCount; i++)
            for (var j = 0; j < expected.ElementsInRow(i); j++)
                Assert.Equal(expected[i, j], actual[i, j]);
    }

    [Fact]
    public void AvgInRowTest_int()
    {
        var matrix = new JuggedMatrix<int>(2, 2, 2)
        {
            [0, 0] = 5, [0, 1] = 10, [1, 0] = 20, [1, 1] = 50
        };
        var actual = matrix.Select(x => x.Average()).ToArray();
        var expected = new[] { 7.5, 35.0 };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MinInRowTest_int()
    {
        var matrix = new JuggedMatrix<int>(2, 2, 2)
        {
            [0, 0] = 5, [0, 1] = 10, [1, 0] = 20, [1, 1] = 50
        };
        var actual = matrix.Select(x => x.Min()).ToArray();
        var expected = new[] { 5, 20 };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MaxInRowTest_int()
    {
        var matrix = new JuggedMatrix<int>(2, 2, 2)
        {
            [0, 0] = 5, [0, 1] = 10, [1, 0] = 20, [1, 1] = 50
        };
        var actual = matrix.Select(x => x.Max()).ToArray();
        var expected = new[] { 10, 50 };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MinInRowTest_string()
    {
        var matrix = new JuggedMatrix<string>(2, 2, 2)
        {
            [0, 0] = "ab", [0, 1] = "bc", [1, 0] = "cd", [1, 1] = "de"
        };
        var actual = matrix.Select(x => x.Min()).ToArray();
        var expected = new[] { "ab", "cd" };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MaxInRowTest_string()
    {
        var matrix = new JuggedMatrix<string>(2, 2, 2)
        {
            [0, 0] = "ab", [0, 1] = "bc", [1, 0] = "cd", [1, 1] = "de"
        };
        var actual = matrix.Select(x => x.Max()).ToArray();
        var expected = new[] { "bc", "de" };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Add_int_positive()
    {
        var matrix1 = new JuggedMatrix<int>(3, 3, 3, 3)
        {
            [0, 0] = 6, [0, 1] = 3, [0, 2] = 5, [1, 0] = 4, [1, 1] = 34, [1, 2] = 6, [2, 0] = 34, [2, 1] = 0, [2, 2] = 2
        };
        var matrix2 = new JuggedMatrix<int>(3, 3, 3, 3)
        {
            [0, 0] = 234, [0, 1] = 2, [0, 2] = 1, [1, 0] = 3, [1, 1] = 42, [1, 2] = -90, [2, 0] = 4, [2, 1] = 2, [2, 2] = 2
        };
        var expected = new JuggedMatrix<int>(3, 3, 3, 3)
        {
            [0, 0] = 240, [0, 1] = 5, [0, 2] = 6, [1, 0] = 7, [1, 1] = 76, [1, 2] = -84, [2, 0] = 38, [2, 1] = 2, [2, 2] = 4
        };
        var actual = matrix1.Add(matrix2);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Add_int_column_exception()
    {
        var matrix1 = new JuggedMatrix<int>(3, 4, 4, 4)
        {
            [0, 0] = 6, [0, 1] = 3, [0, 2] = 5, [0, 3] = -1, [1, 0] = 4, [1, 1] = 34, [1, 2] = 6, [1, 3] = -1, [2, 0] = 34, [2, 1] = 0, [2, 2] = 2, [2, 3] = -1
        };
        var matrix2 = new JuggedMatrix<int>(3, 3, 3, 3)
        {
            [0, 0] = 234, [0, 1] = 2, [0, 2] = 1, [1, 0] = 3, [1, 1] = 42, [1, 2] = -90, [2, 0] = 4, [2, 1] = 2, [2, 2] = 2
        };
        Assert.Throws<MatrixInvalidOperationException>(() => matrix1.Add(matrix2));
    }

    [Fact]
    public void Add_int_row_exception()
    {
        var matrix1 = new JuggedMatrix<int>(4, 3, 3, 3, 3)
        {
            [0, 0] = 6, [0, 1] = 3, [0, 2] = 5, [1, 0] = 4, [1, 1] = 34, [1, 2] = 6, [2, 0] = 34, [2, 1] = 0, [2, 2] = 2, [3, 0] = 11, [3, 1] = 11, [3, 2] = 11
        };
        var matrix2 = new JuggedMatrix<int>(3, 3, 3, 3)
        {
            [0, 0] = 234, [0, 1] = 2, [0, 2] = 1, [1, 0] = 3, [1, 1] = 42, [1, 2] = -90, [2, 0] = 4, [2, 1] = 2, [2, 2] = 2
        };
        Assert.Throws<MatrixInvalidOperationException>(() => matrix1.Add(matrix2));
    }

    [Fact]
    public void Add_int_number_positive()
    {
        var number = 5;
        var matrix = new JuggedMatrix<int>(3, 3, 3, 3)
        {
            [0, 0] = 6, [0, 1] = 3, [0, 2] = 5, [1, 0] = 4, [1, 1] = 34, [1, 2] = 6, [2, 0] = 34, [2, 1] = 0, [2, 2] = 2
        };
        var actual = matrix.MulOnNumber(number);
        var expected = new JuggedMatrix<int>(3, 3, 3, 3)
        {
            [0, 0] = 30, [0, 1] = 15, [0, 2] = 25, [1, 0] = 20, [1, 1] = 170, [1, 2] = 30, [2, 0] = 170, [2, 1] = 0, [2, 2] = 10
        };
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Sub_int_positive()
    {
        var matrix1 = new JuggedMatrix<int>(3, 3, 3, 3)
        {
            [0, 0] = 6, [0, 1] = 3, [0, 2] = 5, [1, 0] = 4, [1, 1] = 34, [1, 2] = 6, [2, 0] = 34, [2, 1] = 0, [2, 2] = 2
        };
        var matrix2 = new JuggedMatrix<int>(3, 3, 3, 3)
        {
            [0, 0] = 234, [0, 1] = 2, [0, 2] = 1, [1, 0] = 3, [1, 1] = 42, [1, 2] = -90, [2, 0] = 4, [2, 1] = 2, [2, 2] = 2
        };
        var expected = new JuggedMatrix<int>(3, 3, 3, 3)
        {
            [0, 0] = -228, [0, 1] = 1, [0, 2] = 4, [1, 0] = 1, [1, 1] = -8, [1, 2] = 96, [2, 0] = 30, [2, 1] = -2, [2, 2] = 0
        };
        var actual = matrix1.Sub(matrix2);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Sub_int_column_exception()
    {
        var matrix1 = new JuggedMatrix<int>(3, 4, 4, 4)
        {
            [0, 0] = 6, [0, 1] = 3, [0, 2] = 5, [0, 3] = -1, [1, 0] = 4, [1, 1] = 34, [1, 2] = 6, [1, 3] = -1, [2, 0] = 34, [2, 1] = 0, [2, 2] = 2, [2, 3] = -1
        };
        var matrix2 = new JuggedMatrix<int>(3, 3, 3, 3)
        {
            [0, 0] = 234, [0, 1] = 2, [0, 2] = 1, [1, 0] = 3, [1, 1] = 42, [1, 2] = -90, [2, 0] = 4, [2, 1] = 2, [2, 2] = 2
        };
        Assert.Throws<MatrixInvalidOperationException>(() => matrix1.Sub(matrix2));
    }

    [Fact]
    public void Sub_int_row_exception()
    {
        var matrix1 = new JuggedMatrix<int>(4, 3, 3, 3, 3)
        {
            [0, 0] = 6, [0, 1] = 3, [0, 2] = 5, [1, 0] = 4, [1, 1] = 34, [1, 2] = 6, [2, 0] = 34, [2, 1] = 0, [2, 2] = 2, [3, 0] = 11, [3, 1] = 11, [3, 2] = 11
        };
        var matrix2 = new JuggedMatrix<int>(3, 3, 3, 3)
        {
            [0, 0] = 234, [0, 1] = 2, [0, 2] = 1, [1, 0] = 3, [1, 1] = 42, [1, 2] = -90, [2, 0] = 4, [2, 1] = 2, [2, 2] = 2
        };
        Assert.Throws<MatrixInvalidOperationException>(() => matrix1.Sub(matrix2));
    }

    [Fact]
    public void DiagonalSum_int()
    {
        var n = 3;
        var matrix = new JuggedMatrix<int>(n, n, n, n)
        {
            [0, 0] = 50, [0, 1] = 0, [0, 2] = 0, [1, 0] = 5, [1, 1] = 10, [1, 2] = 5, [2, 0] = 1, [2, 1] = 1, [2, 2] = 5
        };
        var expected = 65;
        var actual = matrix.DiagonalSum();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SideDiagonalSum_int()
    {
        var n = 3;
        var matrix = new JuggedMatrix<int>(n, n, n, n)
        {
            [0, 0] = 1, [0, 1] = 2, [0, 2] = 50, [1, 0] = 1, [1, 1] = 5, [1, 2] = 5, [2, 0] = 3, [2, 1] = 0, [2, 2] = 0
        };
        var expected = 58;
        var actual = matrix.SideDiagonalSum();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Sum_int()
    {
        var n = 3;
        var matrix = new JuggedMatrix<int>(n, n, n, n)
        {
            [0, 0] = 50, [0, 1] = 50, [0, 2] = 50, [1, 0] = 5, [1, 1] = 5, [1, 2] = 5, [2, 0] = 1, [2, 1] = 1, [2, 2] = 1
        };
        var expected = 168;
        var actual = matrix.Sum(x => x.Sum());
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SumSaddlePoints_int()
    {
        var n = 3;
        var matrix = new JuggedMatrix<int>(n, n, n, n)
        {
            [0, 0] = 50, [0, 1] = 50, [0, 2] = 50, [1, 0] = 5, [1, 1] = 5, [1, 2] = 5, [2, 0] = 1, [2, 1] = 1, [2, 2] = 1
        };
        var expected = 150;
        var actual = matrix.SumSaddlePoints();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SortByRowsTest()
    {
        var rowsCount = 3;
        int[] columnArr = { 2, 3, 1 };
        var jugged = new JuggedMatrix<int>(rowsCount, columnArr)
        {
            [0, 0] = 3, [0, 1] = 1, [1, 0] = 10, [1, 1] = 11, [1, 2] = 3, [2, 0] = 5
        };
        var actual = jugged.SortRows();
        var expected = new JuggedMatrix<int>(rowsCount, 1, 2, 3)
        {
            [0, 0] = 5, [1, 0] = 3, [1, 1] = 1, [2, 0] = 10, [2, 1] = 11, [2, 2] = 3
        };
        for (var i = 0; i < actual.RowsCount; i++)
            for (var j = 0; j < actual.ElementsInRow(i); j++)
                Assert.Equal(expected[i, j], actual[i, j]);
    }

    [Fact]
    public void SortByDescendingRowsTest()
    {
        var rowsCount = 3;
        int[] columnArr = { 2, 3, 1 };
        var jugged = new JuggedMatrix<int>(rowsCount, columnArr)
        {
            [0, 0] = 3, [0, 1] = 1, [1, 0] = 10, [1, 1] = 11, [1, 2] = 3, [2, 0] = 5
        };
        var actual = jugged.SortRowsByDescending();
        var expected = new JuggedMatrix<int>(rowsCount, 3, 2, 1)
        {
            [0, 0] = 10, [0, 1] = 11, [0, 2] = 3, [1, 0] = 3, [1, 1] = 1, [2, 0] = 5
        };
        for (var i = 0; i < actual.RowsCount; i++)
            for (var j = 0; j < actual.ElementsInRow(i); j++)
                Assert.Equal(expected[i, j], actual[i, j]);
    }

    [Fact]
    public void IsSquareTest()
    {
        var rowsCount = 3;
        int[] columnArr = { 3, 3, 3 };
        var jugged = new JuggedMatrix<int>(rowsCount, columnArr);
        Assert.True(jugged.IsSquare);
    }

    [Fact]
    public void SumAgeTest()
    {
        var matrix = new JuggedMatrix<Animal>(3, 3, 3, 3)
        {
            [0, 0] = new(5), [0, 1] = new(5), [0, 2] = new(5), [1, 0] = new(5), [1, 1] = new(5), [1, 2] = new(5), [2, 0] = new(5), [2, 1] = new(5), [2, 2] = new(5)
        };
        const int expected = 45;
        var actual = matrix.Sum(x => x.Sum(y => y.Age));
        Assert.Equal(expected, actual);
    }

    private class Animal(int age)
    {
        public int Age { get; } = age;
    }
}
