using CyberMath.Structures.Matrices.Dynamic_Matrices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.DynamicMatrix.Tests
{
    [TestClass]
    public class RowOperations
    {
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
				[2, 3] = 6,
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
				[1, 3] = 6,
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
				[2, 3] = 6,
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
				[1, 3] = 6,
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
				[2, 3] = 6,
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
				[1, 3] = 6,
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
				[2, 3] = 6,
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
				[1, 3] = 6,
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
				[2, 3] = 6,
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
				[2, 3] = 6,
			};

			Assert.IsTrue(actualMatrix.Equals(expectedmatrix));
		}

		#endregion
	}
}
