using CyberMath.Structures.Matrices.Dynamic_Matrices.Dynamic_Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.DynamicMatrix.Tests
{
    [TestClass]
    public class CreationTests
    {
        [TestMethod]
        public void RowsAndColumns_test()
        {
	        var matrix = new DynamicMatrix<int>(5, 3);
	        Assert.AreEqual(5, matrix.RowsCount);
	        Assert.AreEqual(3, matrix.ColumnsCount);
	        Assert.IsFalse(matrix.IsSquare);
        }
    }
}
