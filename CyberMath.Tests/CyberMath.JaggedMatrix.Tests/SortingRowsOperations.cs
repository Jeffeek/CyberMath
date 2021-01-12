using CyberMath.Structures.JaggedMatrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.JaggedMatrix.Tests
{
    [TestClass]
    public class SortingRowsOperations
    {
        [TestMethod]
        public void SortByRowsTest()
        {
            int rowsCount = 3;
            int[] columnArr = { 2, 3, 1 };
            var jugged = new JuggedMatrix<int>(rowsCount, columnArr)
            {
                [0, 0] = 3,
                [0, 1] = 1,
                [1, 0] = 10,
                [1, 1] = 11,
                [1, 2] = 3,
                [2, 0] = 5
            };

            var actual = jugged.SortRows();
            var expected = new JuggedMatrix<int>(rowsCount, 1, 2, 3)
            {
                [0, 0] = 5,
                [1, 0] = 3,
                [1, 1] = 1,
                [2, 0] = 10,
                [2, 1] = 11,
                [2, 2] = 3
            };

            for (int i = 0; i < actual.RowsCount; i++)
            {
                for (int j = 0; j < actual.ElementsInRow(i); j++)
                {
                    Assert.IsTrue(actual[i, j] == expected[i, j]);
                }
            }
        }

        [TestMethod]
        public void SortByDescendingRowsTest()
        {
            int rowsCount = 3;
            int[] columnArr = { 2, 3, 1 };
            var jugged = new JuggedMatrix<int>(rowsCount, columnArr)
            {
                [0, 0] = 3,
                [0, 1] = 1,
                [1, 0] = 10,
                [1, 1] = 11,
                [1, 2] = 3,
                [2, 0] = 5
            };

            var actual = jugged.SortRowsByDescending();
            var expected = new JuggedMatrix<int>(rowsCount, 3, 2, 1)
            {
                [0, 0] = 10,
                [0, 1] = 11,
                [0, 2] = 3,
                [1, 0] = 3,
                [1, 1] = 1,
                [2, 0] = 5
            };

            for (int i = 0; i < actual.RowsCount; i++)
            {
                for (int j = 0; j < actual.ElementsInRow(i); j++)
                {
                    Assert.IsTrue(actual[i, j] == expected[i, j]);
                }
            }
        }

        [TestMethod]
        public void IsSquareTest()
        {
            int rowsCount = 3;
            int[] columnArr = { 3, 3, 3 };
            var jugged = new JuggedMatrix<int>(rowsCount, columnArr);
            Assert.IsTrue(jugged.IsSquare);
        }
    }
}
