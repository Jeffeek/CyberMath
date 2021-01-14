using CyberMath.Structures.Matrix;
using CyberMath.Structures.MatrixExtensions.Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CyberMath.Matrix.Tests
{
    [TestClass]
    public class AnimalSumOperations
    {
        internal class Animal
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public Animal(int age)
            {
                Age = age;
            }
        }

        [TestMethod]
        public void SumAgeTest()
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
                [2, 2] = new Animal(5),
            };

            var expected = 45;
            var actual = matrix.Sum(x => x.Age);
            Assert.AreEqual(expected, actual);
        }
    }
}
