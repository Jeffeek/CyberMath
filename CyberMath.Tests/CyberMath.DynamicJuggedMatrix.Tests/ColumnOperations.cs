#region Using derectives

using CyberMath.Structures.Matrices.Dynamic_Matrices.Dynamic_Jugged_Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.DynamicJuggedMatrix.Tests
{
	[TestClass]
	public class ColumnOperations
	{
		#region Remove

		[TestMethod]
		public void RemoveColumnAtStart_test()
		{
			var actualMatrix = new DynamicJuggedMatrix<int>(2, 5, 5)
			                   {
				                   [0, 0] = 1,
				                   [0, 1] = 2,
				                   [0, 2] = 3,
				                   [0, 3] = 4,
				                   [0, 4] = 5,
				                   [1, 0] = 1,
				                   [1, 1] = 2,
				                   [1, 2] = 3,
				                   [1, 3] = 4,
				                   [1, 4] = 5
			                   };

			actualMatrix.RemoveColumn(0);

			var expectedmatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
			                     {
				                     [0, 0] = 2,
				                     [0, 1] = 3,
				                     [0, 2] = 4,
				                     [0, 3] = 5,
				                     [1, 0] = 2,
				                     [1, 1] = 3,
				                     [1, 2] = 4,
				                     [1, 3] = 5
			                     };

			Assert.IsTrue(actualMatrix.Equals(expectedmatrix));
		}

		[TestMethod]
		public void RemoveColumnAtMiddle_test()
		{
			var actualMatrix = new DynamicJuggedMatrix<int>(2, 5, 5)
			                   {
				                   [0, 0] = 1,
				                   [0, 1] = 2,
				                   [0, 2] = 3,
				                   [0, 3] = 4,
				                   [0, 4] = 5,
				                   [1, 0] = 1,
				                   [1, 1] = 2,
				                   [1, 2] = 3,
				                   [1, 3] = 4,
				                   [1, 4] = 5
			                   };

			actualMatrix.RemoveColumn(2);

			var expectedmatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
			                     {
				                     [0, 0] = 1,
				                     [0, 1] = 2,
				                     [0, 2] = 4,
				                     [0, 3] = 5,
				                     [1, 0] = 1,
				                     [1, 1] = 2,
				                     [1, 2] = 4,
				                     [1, 3] = 5
			                     };

			Assert.IsTrue(actualMatrix.Equals(expectedmatrix));
		}

		[TestMethod]
		public void RemoveColumnAtFinish_test()
		{
			var actualMatrix = new DynamicJuggedMatrix<int>(2, 5, 5)
			                   {
				                   [0, 0] = 3, [0, 1] = 4, [0, 2] = 5, [0, 3] = 6, [0, 4] = 7,
				                   [1, 0] = 3, [1, 1] = 4, [1, 2] = 5, [1, 3] = 6, [1, 4] = 7
			                   };

			actualMatrix.RemoveColumn(4);

			var expectedmatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
			                     {
				                     [0, 0] = 3, [0, 1] = 4, [0, 2] = 5, [0, 3] = 6,
				                     [1, 0] = 3, [1, 1] = 4, [1, 2] = 5, [1, 3] = 6
			                     };

			Assert.IsTrue(actualMatrix.Equals(expectedmatrix));
		}

		#endregion

		#region Insert

		[TestMethod]
		public void InsertColumnAtStart_test()
		{
			var actualMatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
			                   {
				                   [0, 0] = 2,
				                   [0, 1] = 3,
				                   [0, 2] = 4,
				                   [0, 3] = 5,
				                   [1, 0] = 2,
				                   [1, 1] = 3,
				                   [1, 2] = 4,
				                   [1, 3] = 5
			                   };

			var elements = new[] { 1, 1 };

			actualMatrix.InsertColumn(0, elements);

			var expectedmatrix = new DynamicJuggedMatrix<int>(2, 5, 5)
			                     {
				                     [0, 0] = 1,
				                     [0, 1] = 2,
				                     [0, 2] = 3,
				                     [0, 3] = 4,
				                     [0, 4] = 5,
				                     [1, 0] = 1,
				                     [1, 1] = 2,
				                     [1, 2] = 3,
				                     [1, 3] = 4,
				                     [1, 4] = 5
			                     };

			Assert.IsTrue(actualMatrix.Equals(expectedmatrix));
		}

		[TestMethod]
		public void InsertColumnAtMiddle_test()
		{
			var actualMatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
			                   {
				                   [0, 0] = 1,
				                   [0, 1] = 2,
				                   [0, 2] = 4,
				                   [0, 3] = 5,
				                   [1, 0] = 1,
				                   [1, 1] = 2,
				                   [1, 2] = 4,
				                   [1, 3] = 5
			                   };

			var elements = new[] { 3, 3 };

			actualMatrix.InsertColumn(2, elements);

			var expectedmatrix = new DynamicJuggedMatrix<int>(2, 5, 5)
			                     {
				                     [0, 0] = 1,
				                     [0, 1] = 2,
				                     [0, 2] = 3,
				                     [0, 3] = 4,
				                     [0, 4] = 5,
				                     [1, 0] = 1,
				                     [1, 1] = 2,
				                     [1, 2] = 3,
				                     [1, 3] = 4,
				                     [1, 4] = 5
			                     };

			Assert.IsTrue(actualMatrix.Equals(expectedmatrix));
		}

		[TestMethod]
		public void AddColumnAtFinish_test()
		{
			var actualMatrix = new DynamicJuggedMatrix<int>(2, 4, 4)
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

			var elements = new[] { 7, 7 };

			actualMatrix.AddColumn(elements);

			var expectedmatrix = new DynamicJuggedMatrix<int>(2, 5, 5)
			                     {
				                     [0, 0] = 3,
				                     [0, 1] = 4,
				                     [0, 2] = 5,
				                     [0, 3] = 6,
				                     [0, 4] = 7,
				                     [1, 0] = 3,
				                     [1, 1] = 4,
				                     [1, 2] = 5,
				                     [1, 3] = 6,
				                     [1, 4] = 7
			                     };

			Assert.IsTrue(actualMatrix.Equals(expectedmatrix));
		}

		#endregion
	}
}