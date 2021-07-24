#region Using namespaces

using CyberMath.Structures.Matrices.Extensions;
using CyberMath.Structures.Matrices.Jagged_Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.JaggedMatrix.Tests
{
    [TestClass]
    public class CreateOperations
    {
        [TestMethod]
        public void CreateJuggedMatrixWithoutColumn_0_Test()
        {
            var jugged = new JuggedMatrix<int>(3, 2, 1, 3)
                         {
                             [0, 0] = 1,
                             [0, 1] = 10,
                             [1, 0] = 5,
                             [2, 0] = 90,
                             [2, 1] = 55,
                             [2, 2] = 12
                         };

            var actual = jugged.CreateMatrixWithoutColumn(0);

            var expected = new JuggedMatrix<int>(3, 1, 0, 2)
                           {
                               [0, 0] = 10,
                               [2, 0] = 55,
                               [2, 1] = 12
                           };

            for (var i = 0; i < expected.RowsCount; i++)
            {
                for (var j = 0; j < expected.ElementsInRow(i); j++) Assert.IsTrue(actual[i, j] == expected[i, j]);
            }
        }

        [TestMethod]
        public void CreateJuggedMatrixWithoutColumn_1_Test()
        {
            var jugged = new JuggedMatrix<int>(3, 2, 1, 3)
                         {
                             [0, 0] = 1,
                             [0, 1] = 10,
                             [1, 0] = 5,
                             [2, 0] = 90,
                             [2, 1] = 55,
                             [2, 2] = 12
                         };

            var actual = jugged.CreateMatrixWithoutColumn(1);

            var expected = new JuggedMatrix<int>(3, 1, 1, 2)
                           {
                               [0, 0] = 1,
                               [1, 0] = 5,
                               [2, 0] = 90,
                               [2, 1] = 12
                           };

            for (var i = 0; i < expected.RowsCount; i++)
            {
                for (var j = 0; j < expected.ElementsInRow(i); j++) Assert.IsTrue(actual[i, j] == expected[i, j]);
            }
        }

        [TestMethod]
        public void CreateJuggedMatrixWithoutRow_1_Test()
        {
            var jugged = new JuggedMatrix<int>(3, 2, 1, 3)
                         {
                             [0, 0] = 1,
                             [0, 1] = 10,
                             [1, 0] = 5,
                             [2, 0] = 90,
                             [2, 1] = 55,
                             [2, 2] = 12
                         };

            var actual = jugged.CreateMatrixWithoutRow(1);

            var expected = new JuggedMatrix<int>(2, 2, 3)
                           {
                               [0, 0] = 1,
                               [0, 1] = 10,
                               [1, 0] = 90,
                               [1, 1] = 55,
                               [1, 2] = 12
                           };

            for (var i = 0; i < expected.RowsCount; i++)
            {
                for (var j = 0; j < expected.ElementsInRow(i); j++) Assert.IsTrue(actual[i, j] == expected[i, j]);
            }
        }
    }
}