using CyberMath.Matrix.Extensions;
using CyberMath.Matrix.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.Matrix.Tests
{
    [TestClass]
    public class CreationOperations
    {
        [TestMethod]
        public void CreateIdentityMatrixTest_3()
        {
            int n = 3;
            var actual = ValueTypeMatrixExtension.CreateIdentityMatrix(n);
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
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Assert.IsTrue(actual[i,j] == expected[i,j]);
                }
            }
        }
    }
}
