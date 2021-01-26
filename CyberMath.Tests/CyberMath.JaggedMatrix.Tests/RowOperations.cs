#region Using derectives

using System.Linq;
using CyberMath.Structures.Matrices.Extensions;
using CyberMath.Structures.Matrices.JaggedMatrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.JaggedMatrix.Tests
{
	[TestClass]
	public class RowOperations
	{
		[TestMethod]
		public void AvgInRowTest_int()
		{
			var matrix = new JuggedMatrix<int>(2, 2, 2)
			             {
				             [0, 0] = 5, [0, 1] = 10,
				             [1, 0] = 20, [1, 1] = 50
			             };

			var actual = matrix.Select(x => x.Average()).ToArray();
			var expected = new[] { 7.5, 35.0 };
			CollectionAssert.AreEqual(actual, expected);
		}

		[TestMethod]
		public void MinInRowTest_int()
		{
			var matrix = new JuggedMatrix<int>(2, 2, 2)
			             {
				             [0, 0] = 5, [0, 1] = 10,
				             [1, 0] = 20, [1, 1] = 50
			             };

			var actual = matrix.Select(x => x.Min()).ToArray();
			var expected = new[] { 5, 20 };
			CollectionAssert.AreEqual(actual, expected);
		}

		[TestMethod]
		public void MaxInRowTest_int()
		{
			var matrix = new JuggedMatrix<int>(2, 2, 2)
			             {
				             [0, 0] = 5, [0, 1] = 10,
				             [1, 0] = 20, [1, 1] = 50
			             };

			var actual = matrix.Select(x => x.Max()).ToArray();
			var expected = new[] { 10, 50 };
			CollectionAssert.AreEqual(actual, expected);
		}

		[TestMethod]
		public void MinInRowTest_string()
		{
			var matrix = new JuggedMatrix<string>(2, 2, 2)
			             {
				             [0, 0] = "ab", [0, 1] = "bc",
				             [1, 0] = "cd", [1, 1] = "de"
			             };

			var actual = matrix.Select(x => x.Min()).ToArray();
			var expected = new[] { "ab", "cd" };
			CollectionAssert.AreEqual(actual, expected);
		}

		[TestMethod]
		public void MaxInRowTest_string()
		{
			var matrix = new JuggedMatrix<string>(2, 2, 2)
			             {
				             [0, 0] = "ab", [0, 1] = "bc",
				             [1, 0] = "cd", [1, 1] = "de"
			             };

			var actual = matrix.Select(x => x.Max()).ToArray();
			var expected = new[] { "bc", "de" };
			CollectionAssert.AreEqual(actual, expected);
		}

		[TestMethod]
		public void CountOnEachRow_test()
		{
			var matrix = new JuggedMatrix<int>(3, 5, 2, 1)
			             {
				             [0, 0] = 5, [0, 1] = 10, [0, 2] = 10, [0, 3] = 10, [0, 4] = 10,
							 [1, 0] = 20, [1, 1] = 50,
							 [2, 0] = 20
			             };

			var actual = matrix.CountOnEachRow().ToArray();
			var expected = matrix.Select(x => x.Count()).ToArray();
			CollectionAssert.AreEqual(actual, expected);
		}
	}
}