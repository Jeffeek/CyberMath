#region Using derectives

using System.Linq;
using CyberMath.Structures.Matrices.Base;
using CyberMath.Structures.Matrices.Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.Matrix.Tests
{
	[TestClass]
	public class SumOperations
	{
		[TestMethod]
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
			Assert.IsTrue(actual == expected);
		}

		[TestMethod]
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
			Assert.IsTrue(actual == expected);
		}

		[TestMethod]
		public void Sum_int()
		{
			var n = 3;
			var matrix = new Matrix<int>(n, n)
			             {
				             [0, 0] = 50, [0, 1] = 50, [0, 2] = 50,
				             [1, 0] = 5, [1, 1] = 5, [1, 2] = 5,
				             [2, 0] = 1, [2, 1] = 1, [2, 2] = 1
			             };


			var expected = 168;
			var actual = matrix.Sum(x => x.Sum());
			Assert.IsTrue(actual == expected);
		}

		[TestMethod]
		public void SumSaddlePoints_int()
		{
			var n = 3;
			var matrix = new Matrix<int>(n, n)
			             {
				             [0, 0] = 50, [0, 1] = 50, [0, 2] = 50,
				             [1, 0] = 5, [1, 1] = 5, [1, 2] = 5,
				             [2, 0] = 1, [2, 1] = 1, [2, 2] = 1
			             };

			var expected = 150;
			var actual = matrix.SumSaddlePoints();
			Assert.IsTrue(actual == expected);
		}

		[TestMethod]
		public void DiagonalSum_nullable_int()
		{
			var n = 3;
			var matrix = new Matrix<int?>(n, n)
			             {
				             [0, 0] = 50, [0, 1] = null, [0, 2] = null,
				             [1, 0] = 5,  [1, 1] = null, [1, 2] = 5,
				             [2, 0] = 1,  [2, 1] = 1,    [2, 2] = 1
			             };

			var expected = 51;
			var actual = matrix.DiagonalSum();
			Assert.IsTrue(actual == expected);
		}
	}
}