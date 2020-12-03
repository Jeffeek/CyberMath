using System;
using System.Collections.Generic;
using System.Text;
using CyberMath.Structures.Matrix.Matrix.Extensions;
using CyberMath.Structures.Matrix.Matrix.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var matrix = new Matrix<Animal>(3,3)
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

            int expected = 45;
            int actual = matrix.Sum(x => x.Age);
            Assert.AreEqual(expected, actual);
        }
    }
}
