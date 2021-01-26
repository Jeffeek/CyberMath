#region Using derectives

using System.Linq;
using CyberMath.Structures.Matrices.Base;
using CyberMath.Structures.Matrices.Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.Matrix.Tests
{
	[TestClass]
	public class AnimalSumOperations
	{
		[TestMethod]
		public void Sum_age_test()
		{
			var matrix = new Matrix<Animal>(3, 3)
			             {
				             [0, 0] = new Animal(5),
				             [0, 1] = new Animal(5),
				             [0, 2] = new Animal(5),
				             [1, 0] = new Animal(5),
				             [1, 1] = new Animal(5),
				             [1, 2] = new Animal(5),
				             [2, 0] = new Animal(5),
				             [2, 1] = new Animal(5),
				             [2, 2] = new Animal(5)
			             };

			var expected = 45;
			var actual = matrix.Sum(x => x.Sum(_ => _.Age));
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void DiagonalSum_age_test()
		{
			var matrix = new Matrix<Animal>(3, 3)
			             {
				             [0, 0] = new Animal(5), [0, 1] = new Animal(5), [0, 2] = new Animal(5),
				             [1, 0] = new Animal(5), [1, 1] = new Animal(50), [1, 2] = new Animal(5),
				             [2, 0] = new Animal(5), [2, 1] = new Animal(5), [2, 2] = new Animal(7)
			             };

			var expected = 62;
			var actual = matrix.DiagonalSum(animal => animal.Age);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void SideDiagonalSum_age_test()
		{
			var matrix = new Matrix<Animal>(3, 3)
			             {
				             [0, 0] = new Animal(5), [0, 1] = new Animal(5), [0, 2] = new Animal(5),
				             [1, 0] = new Animal(5), [1, 1] = new Animal(50), [1, 2] = new Animal(5),
				             [2, 0] = new Animal(5), [2, 1] = new Animal(5), [2, 2] = new Animal(7)
			             };

			var expected = 60;
			var actual = matrix.SideDiagonalSum(animal => animal.Age);
			Assert.AreEqual(expected, actual);
		}

		private class Animal
		{
			public Animal(int age) => Age = age;
			public string Name { get; set; }
			public int Age { get; }
		}
	}
}