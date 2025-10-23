using CyberMath.Structures.Matrices.Base.Exceptions;
using CyberMath.Structures.Matrices.Dynamic_Matrices.Dynamic_Jugged_Matrix;
using CyberMath.Structures.Matrices.Dynamic_Matrices.Dynamic_Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using CyberMath.Structures.Matrices.Base;
using CyberMath.Structures.Matrices.Extensions;
using Xunit;

namespace CyberMath.xUnit.Tests.Structures.Matrices;

public class DynamicJuggedMatrixTests
{
    [Fact]
    public void SumAgeTest()
    {
        var matrix = new DynamicJuggedMatrix<Animal>(3, 3, 3, 3)
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

        const int expected = 45;
        var actual = matrix.Sum(x => x.Sum(y => y.Age));
        Assert.Equal(expected, actual);
    }

    private sealed class Animal(int age)
    {
        public int Age { get; } = age;
    }

    #region Remove

    [Fact]
    public void RemoveColumnAtStart_test()
    {
        var actualMatrix = new DynamicJuggedMatrix<int>(2, 5, 5)
        {
            [0, 0] = 1,
            [0, 1] = 2,
            [0, 2] = 3,
            [0, 3] = 4,
            [0, 4] = 5,
            [1, 0] = 1,
            [1, 1] = 2,
            [1, 2] = 3,
            [1, 3] = 4,
            [1, 4] = 5
        };

        actualMatrix.RemoveColumn(0);

        var expectedmatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
        {
            [0, 0] = 2,
            [0, 1] = 3,
            [0, 2] = 4,
            [0, 3] = 5,
            [1, 0] = 2,
            [1, 1] = 3,
            [1, 2] = 4,
            [1, 3] = 5
        };

        Assert.True(actualMatrix.Equals(expectedmatrix));
    }

    [Fact]
    public void RemoveColumnAtMiddle_test()
    {
        var actualMatrix = new DynamicJuggedMatrix<int>(2, 5, 5)
        {
            [0, 0] = 1,
            [0, 1] = 2,
            [0, 2] = 3,
            [0, 3] = 4,
            [0, 4] = 5,
            [1, 0] = 1,
            [1, 1] = 2,
            [1, 2] = 3,
            [1, 3] = 4,
            [1, 4] = 5
        };

        actualMatrix.RemoveColumn(2);

        var expectedmatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
        {
            [0, 0] = 1,
            [0, 1] = 2,
            [0, 2] = 4,
            [0, 3] = 5,
            [1, 0] = 1,
            [1, 1] = 2,
            [1, 2] = 4,
            [1, 3] = 5
        };

        Assert.True(actualMatrix.Equals(expectedmatrix));
    }

    [Fact]
    public void RemoveColumnAtFinish_test()
    {
        var actualMatrix = new DynamicJuggedMatrix<int>(2, 5, 5)
        {
            [0, 0] = 3,
            [0, 1] = 4,
            [0, 2] = 5,
            [0, 3] = 6,
            [0, 4] = 7,
            [1, 0] = 3,
            [1, 1] = 4,
            [1, 2] = 5,
            [1, 3] = 6,
            [1, 4] = 7
        };

        actualMatrix.RemoveColumn(4);

        var expectedmatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
        {
            [0, 0] = 3,
            [0, 1] = 4,
            [0, 2] = 5,
            [0, 3] = 6,
            [1, 0] = 3,
            [1, 1] = 4,
            [1, 2] = 5,
            [1, 3] = 6
        };

        Assert.True(actualMatrix.Equals(expectedmatrix));
    }

    #endregion

    #region Insert

    [Fact]
    public void InsertColumnAtStart_test()
    {
        var actualMatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
        {
            [0, 0] = 2,
            [0, 1] = 3,
            [0, 2] = 4,
            [0, 3] = 5,
            [1, 0] = 2,
            [1, 1] = 3,
            [1, 2] = 4,
            [1, 3] = 5
        };

        var elements = new[] { 1, 1 };

        actualMatrix.InsertColumn(0, elements);

        var expectedmatrix = new DynamicJuggedMatrix<int>(2, 5, 5)
        {
            [0, 0] = 1,
            [0, 1] = 2,
            [0, 2] = 3,
            [0, 3] = 4,
            [0, 4] = 5,
            [1, 0] = 1,
            [1, 1] = 2,
            [1, 2] = 3,
            [1, 3] = 4,
            [1, 4] = 5
        };

        Assert.True(actualMatrix.Equals(expectedmatrix));
    }

    [Fact]
    public void InsertColumnAtMiddle_test()
    {
        var actualMatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
        {
            [0, 0] = 1,
            [0, 1] = 2,
            [0, 2] = 4,
            [0, 3] = 5,
            [1, 0] = 1,
            [1, 1] = 2,
            [1, 2] = 4,
            [1, 3] = 5
        };

        var elements = new[] { 3, 3 };

        actualMatrix.InsertColumn(2, elements);

        var expectedmatrix = new DynamicJuggedMatrix<int>(2, 5, 5)
        {
            [0, 0] = 1,
            [0, 1] = 2,
            [0, 2] = 3,
            [0, 3] = 4,
            [0, 4] = 5,
            [1, 0] = 1,
            [1, 1] = 2,
            [1, 2] = 3,
            [1, 3] = 4,
            [1, 4] = 5
        };

        Assert.True(actualMatrix.Equals(expectedmatrix));
    }

    [Fact]
    public void AddColumnAtFinish_test()
    {
        var actualMatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
        {
            [0, 0] = 3,
            [0, 1] = 4,
            [0, 2] = 5,
            [0, 3] = 6,
            [1, 0] = 3,
            [1, 1] = 4,
            [1, 2] = 5,
            [1, 3] = 6
        };

        var elements = new[] { 7, 7 };

        actualMatrix.AddColumn(elements);

        var expectedmatrix = new DynamicJuggedMatrix<int>(2, 5, 5)
        {
            [0, 0] = 3,
            [0, 1] = 4,
            [0, 2] = 5,
            [0, 3] = 6,
            [0, 4] = 7,
            [1, 0] = 3,
            [1, 1] = 4,
            [1, 2] = 5,
            [1, 3] = 6,
            [1, 4] = 7
        };

        Assert.True(actualMatrix.Equals(expectedmatrix));
    }

    #endregion

    [Fact]
    public void RowsAndColumns_test()
    {
        var matrix = new DynamicJuggedMatrix<int>(5, 5, 5, 5, 5, 5);

        Assert.Equal(5, matrix.RowsCount);
        Assert.True(matrix.IsSquare);
    }

    [Fact]
    public void Add_int_positive()
    {
        var matrix1 = new DynamicJuggedMatrix<int>(3, 3, 3, 3)
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

        var matrix2 = new DynamicJuggedMatrix<int>(3, 3, 3, 3)
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

        var expected = new DynamicJuggedMatrix<int>(3, 3, 3, 3)
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
        var matrix1 = new DynamicJuggedMatrix<int>(3, 4, 4, 4)
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

        var matrix2 = new DynamicJuggedMatrix<int>(3, 3, 3, 3)
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
        var matrix1 = new DynamicJuggedMatrix<int>(4, 3, 3, 3, 3)
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

        var matrix2 = new DynamicJuggedMatrix<int>(3, 3, 3, 3)
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

        var matrix = new DynamicJuggedMatrix<int>(3, 3, 3, 3)
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

        var expected = new DynamicJuggedMatrix<int>(3, 3, 3, 3)
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
        var matrix1 = new DynamicJuggedMatrix<int>(3, 3, 3, 3)
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

        var matrix2 = new DynamicJuggedMatrix<int>(3, 3, 3, 3)
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

        var expected = new DynamicJuggedMatrix<int>(3, 3, 3, 3)
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
        var matrix1 = new DynamicJuggedMatrix<int>(3, 4, 4, 4)
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

        var matrix2 = new DynamicJuggedMatrix<int>(3, 3, 3, 3)
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
        var matrix1 = new DynamicJuggedMatrix<int>(4, 3, 3, 3, 3)
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

        var matrix2 = new DynamicJuggedMatrix<int>(3, 3, 3, 3)
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
    public void AvgInRowTest_int()
    {
        var matrix = new DynamicJuggedMatrix<int>(2, 2, 2)
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
        var matrix = new DynamicJuggedMatrix<int>(2, 2, 2)
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
        var matrix = new DynamicJuggedMatrix<int>(2, 2, 2)
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
        var matrix = new DynamicJuggedMatrix<string>(2, 2, 2)
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
        var matrix = new DynamicJuggedMatrix<string>(2, 2, 2)
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

    #region Remove

    [Fact]
    public void RemoveRowAtStart_test()
    {
        var actualMatrix = new DynamicJuggedMatrix<int>(3, 4, 4, 4)
        {
            [0, 0] = 3,
            [0, 1] = 4,
            [0, 2] = 5,
            [0, 3] = 10,
            [1, 0] = 3,
            [1, 1] = 4,
            [1, 2] = 5,
            [1, 3] = 6,
            [2, 0] = 3,
            [2, 1] = 4,
            [2, 2] = 5,
            [2, 3] = 6
        };

        actualMatrix.RemoveRow(0);

        var expectedmatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
        {
            [0, 0] = 3,
            [0, 1] = 4,
            [0, 2] = 5,
            [0, 3] = 6,
            [1, 0] = 3,
            [1, 1] = 4,
            [1, 2] = 5,
            [1, 3] = 6
        };

        Assert.True(actualMatrix.Equals(expectedmatrix));
    }

    [Fact]
    public void RemoveRowAtMiddle_test()
    {
        var actualMatrix = new DynamicJuggedMatrix<int>(3, 4, 4, 4)
        {
            [0, 0] = 3,
            [0, 1] = 4,
            [0, 2] = 5,
            [0, 3] = 6,
            [1, 0] = 3,
            [1, 1] = 4,
            [1, 2] = 5,
            [1, 3] = 10,
            [2, 0] = 3,
            [2, 1] = 4,
            [2, 2] = 5,
            [2, 3] = 6
        };

        actualMatrix.RemoveRow(1);

        var expectedmatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
        {
            [0, 0] = 3,
            [0, 1] = 4,
            [0, 2] = 5,
            [0, 3] = 6,
            [1, 0] = 3,
            [1, 1] = 4,
            [1, 2] = 5,
            [1, 3] = 6
        };

        Assert.True(actualMatrix.Equals(expectedmatrix));
    }

    [Fact]
    public void RemoveRowAtEnd_test()
    {
        var actualMatrix = new DynamicJuggedMatrix<int>(3, 4, 4, 4)
        {
            [0, 0] = 3,
            [0, 1] = 4,
            [0, 2] = 5,
            [0, 3] = 6,
            [1, 0] = 3,
            [1, 1] = 4,
            [1, 2] = 5,
            [1, 3] = 6,
            [2, 0] = 3,
            [2, 1] = 4,
            [2, 2] = 10,
            [2, 3] = 6
        };

        actualMatrix.RemoveRow(2);

        var expectedmatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
        {
            [0, 0] = 3,
            [0, 1] = 4,
            [0, 2] = 5,
            [0, 3] = 6,
            [1, 0] = 3,
            [1, 1] = 4,
            [1, 2] = 5,
            [1, 3] = 6
        };

        Assert.True(actualMatrix.Equals(expectedmatrix));
    }

    #endregion

    #region Insert

    [Fact]
    public void InsertRowAtStart_test()
    {
        var actualMatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
        {
            [0, 0] = 3,
            [0, 1] = 4,
            [0, 2] = 5,
            [0, 3] = 6,
            [1, 0] = 3,
            [1, 1] = 4,
            [1, 2] = 5,
            [1, 3] = 6
        };

        var elements = new[] { 3, 4, 5, 10 };

        actualMatrix.InsertRow(0, elements);

        var expectedmatrix = new DynamicJuggedMatrix<int>(3, 4, 4, 4)
        {
            [0, 0] = 3,
            [0, 1] = 4,
            [0, 2] = 5,
            [0, 3] = 10,
            [1, 0] = 3,
            [1, 1] = 4,
            [1, 2] = 5,
            [1, 3] = 6,
            [2, 0] = 3,
            [2, 1] = 4,
            [2, 2] = 5,
            [2, 3] = 6
        };

        Assert.True(actualMatrix.Equals(expectedmatrix));
    }

    [Fact]
    public void InsertRowAtMiddle_test()
    {
        var actualMatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
        {
            [0, 0] = 3,
            [0, 1] = 4,
            [0, 2] = 5,
            [0, 3] = 6,
            [1, 0] = 3,
            [1, 1] = 4,
            [1, 2] = 5,
            [1, 3] = 6
        };

        var elements = new[] { 3, 4, 5, 10 };

        actualMatrix.InsertRow(1, elements);

        var expectedmatrix = new DynamicJuggedMatrix<int>(3, 4, 4, 4)
        {
            [0, 0] = 3,
            [0, 1] = 4,
            [0, 2] = 5,
            [0, 3] = 6,
            [1, 0] = 3,
            [1, 1] = 4,
            [1, 2] = 5,
            [1, 3] = 10,
            [2, 0] = 3,
            [2, 1] = 4,
            [2, 2] = 5,
            [2, 3] = 6
        };

        Assert.True(actualMatrix.Equals(expectedmatrix));
    }

    [Fact]
    public void AddRow_test()
    {
        var actualMatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
        {
            [0, 0] = 3,
            [0, 1] = 4,
            [0, 2] = 5,
            [0, 3] = 6,
            [1, 0] = 3,
            [1, 1] = 4,
            [1, 2] = 5,
            [1, 3] = 6
        };

        var elements = new[] { 3, 4, 10, 6 };

        actualMatrix.InsertRow(2, elements);

        var expectedmatrix = new DynamicJuggedMatrix<int>(3, 4, 4, 4)
        {
            [0, 0] = 3,
            [0, 1] = 4,
            [0, 2] = 5,
            [0, 3] = 6,
            [1, 0] = 3,
            [1, 1] = 4,
            [1, 2] = 5,
            [1, 3] = 6,
            [2, 0] = 3,
            [2, 1] = 4,
            [2, 2] = 10,
            [2, 3] = 6
        };

        Assert.True(actualMatrix.Equals(expectedmatrix));
    }

    #endregion

    [Fact]
    public void DiagonalSum_int()
    {
        var n = 3;

        var matrix = new DynamicMatrix<int>(n, n)
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

        var matrix = new DynamicMatrix<int>(n, n)
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

        var matrix = new DynamicMatrix<int>(n, n)
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

        var matrix = new DynamicMatrix<int>(n, n)
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