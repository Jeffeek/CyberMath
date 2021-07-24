#region Using namespaces

using CyberMath.Structures.Matrices.Dynamic_Matrices.Dynamic_Jugged_Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.DynamicJuggedMatrix.Tests
{
    [TestClass]
    public class CreationTests
    {
        [TestMethod]
        public void RowsAndColumns_test()
        {
            var matrix = new DynamicJuggedMatrix<int>(5,
                                                      5,
                                                      5,
                                                      5,
                                                      5,
                                                      5);

            Assert.AreEqual(5, matrix.RowsCount);
            Assert.IsTrue(matrix.IsSquare);
        }
    }
}