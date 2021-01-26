#region Using derectives

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.Helpers.Tests
{
	[TestClass]
	public class DeepCopyTests
	{
		[TestMethod]
		public void IntDeepCopy_test()
		{
			var test = 5;
			var actual = test.SerializableDeepCopy();
			Assert.AreEqual(actual, test);
		}

		[TestMethod]
		public void StringDeepCopy_test()
		{
			var test = "test";
			var actual = test.SerializableDeepCopy();
			test = "haha";
			Assert.AreNotEqual(actual, test);
		}

		[TestMethod]
		public void AnimalDeepCopy_test()
		{
			var animal = new Animal()
			             {
				             Age = 50, Name = "AnimalTest",
				             Tail = new Tail()
				                    {
					                    Color = "5354AD",
					                    Length = 6
				                    }
			             };

			var copied = animal.SerializableDeepCopy();
			animal.Age = 12;
			animal.Tail.Length = 100;
			Assert.IsFalse(ReferenceEquals(copied, animal));
			Assert.AreNotEqual(animal.Age, copied.Age);
			Assert.AreNotEqual(animal.Tail.Length, copied.Tail.Length);
		}


		[Serializable]
		private class Animal
		{
			public int Age { get; set; }
			public string Name { get; set; }
			public Tail Tail { get; set; }
		}

		[Serializable]
		private class Tail
		{
			public int Length { get; set; }
			public string Color { get; set; }
		}
	}
}