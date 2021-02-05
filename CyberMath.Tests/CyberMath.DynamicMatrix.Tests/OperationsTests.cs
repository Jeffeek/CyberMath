using CyberMath.Structures.Matrices.Dynamic_Matrices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.DynamicMatrix.Tests
{
    [TestClass]
    public class OperationsTests
    {
	    [TestMethod]
	    public void Transpose_test()
	    {
		    var matrix = new DynamicMatrix<int>(3, 4)
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

		    var expected = new DynamicMatrix<int>(4, 3)
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
    }
}
