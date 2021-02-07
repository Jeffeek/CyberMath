#region Using derectives

using System.Linq;
using CyberMath.Structures.Matrices.Dynamic_Matrices.Dynamic_Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.DynamicMatrix.Tests
{
	[TestClass]
	public class RowOperations
	{
		[TestMethod]
		public void AvgInRowTest_int()
		{
			var matrix = new DynamicMatrix<int>(2, 2)
			             {
				             [0, 0] = 5,
				             [0, 1] = 10,
				             [1, 0] = 20,
				             [1, 1] = 50
			             };

			var actual = matrix.Select(x => x.Average()).ToArray();
			var expected = new[] { 7.5, 35.0 };
			CollectionAssert.AreEqual(actual, expected);
		}

		[TestMethod]
		public void MinInRowTest_int()
		{
			var matrix = new DynamicMatrix<int>(2, 2)
			             {
				             [0, 0] = 5,
				             [0, 1] = 10,
				             [1, 0] = 20,
				             [1, 1] = 50
			             };

			var actual = matrix.Select(x => x.Min()).ToArray();
			var expected = new[] { 5, 20 };
			CollectionAssert.AreEqual(actual, expected);
		}

		[TestMethod]
		public void MaxInRowTest_int()
		{
			var matrix = new DynamicMatrix<int>(2, 2)
			             {
				             [0, 0] = 5,
				             [0, 1] = 10,
				             [1, 0] = 20,
				             [1, 1] = 50
			             };

			var actual = matrix.Select(x => x.Max()).ToArray();
			var expected = new[] { 10, 50 };
			CollectionAssert.AreEqual(actual, expected);
		}

		[TestMethod]
		public void MinInRowTest_string()
		{
			var matrix = new DynamicMatrix<string>(2, 2)
			             {
				             [0, 0] = "ab",
				             [0, 1] = "bc",
				             [1, 0] = "cd",
				             [1, 1] = "de"
			             };

			var actual = matrix.Select(x => x.Min()).ToArray();
			var expected = new[] { "ab", "cd" };
			CollectionAssert.AreEqual(actual, expected);
		}

		[TestMethod]
		public void MaxInRowTest_string()
		{
			var matrix = new DynamicMatrix<string>(2, 2)
			             {
				             [0, 0] = "ab",
				             [0, 1] = "bc",
				             [1, 0] = "cd",
				             [1, 1] = "de"
			             };

			var actual = matrix.Select(x => x.Max()).ToArray();
			var expected = new[] { "bc", "de" };
			CollectionAssert.AreEqual(actual, expected);
		}

		#region Remove

		[TestMethod]
		public void RemoveRowAtStart_test()
		{
			var actualMatrix = new DynamicMatrix<int>(3, 4)
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

			var expectedmatrix = new DynamicMatrix<int>(2, 4)
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

			Assert.IsTrue(actualMatrix.Equals(expectedmatrix));
		}

		[TestMethod]
		public void RemoveRowAtMiddle_test()
		{
			var actualMatrix = new DynamicMatrix<int>(3, 4)
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

			var expectedmatrix = new DynamicMatrix<int>(2, 4)
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

			Assert.IsTrue(actualMatrix.Equals(expectedmatrix));
		}

		[TestMethod]
		public void RemoveRowAtEnd_test()
		{
			var actualMatrix = new DynamicMatrix<int>(3, 4)
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

			var expectedmatrix = new DynamicMatrix<int>(2, 4)
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

			Assert.IsTrue(actualMatrix.Equals(expectedmatrix));
		}

		#endregion

		#region Insert

		[TestMethod]
		public void InsertRowAtStart_test()
		{
			var actualMatrix = new DynamicMatrix<int>(2, 4)
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

			var expectedmatrix = new DynamicMatrix<int>(3, 4)
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

			Assert.IsTrue(actualMatrix.Equals(expectedmatrix));
		}

		[TestMethod]
		public void InsertRowAtMiddle_test()
		{
			var actualMatrix = new DynamicMatrix<int>(2, 4)
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

			var expectedmatrix = new DynamicMatrix<int>(3, 4)
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

			Assert.IsTrue(actualMatrix.Equals(expectedmatrix));
		}

		[TestMethod]
		public void AddRow_test()
		{
			var actualMatrix = new DynamicMatrix<int>(2, 4)
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

			var expectedmatrix = new DynamicMatrix<int>(3, 4)
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

			Assert.IsTrue(actualMatrix.Equals(expectedmatrix));
		}

		#endregion
	}
}