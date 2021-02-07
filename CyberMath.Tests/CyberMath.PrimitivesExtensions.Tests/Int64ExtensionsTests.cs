#region Using derectives

using System.Linq;
using CyberMath.Extensions.Int32;
using CyberMath.Extensions.Int64;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.PrimitivesExtensions.Tests
{
	[TestClass]
	public class Int64ExtensionsTests
	{
		[TestMethod]
		public void Int64_GetLength_test()
		{
			for (var i = long.MinValue; i < 0; i /= 10)
			{
				long expect = i < 0 ? i.ToString().Length - 1 : i.ToString().Length;
				long actual = i.GetLength();
				Assert.IsTrue(expect == actual);
			}

			for (long i = 1; i <= 1000000000000000000; i *= 10)
			{
				long expect = i < 0 ? i.ToString().Length - 1 : i.ToString().Length;
				long actual = i.GetLength();
				Assert.IsTrue(expect == actual);
			}


			var expectMaxValue = long.MaxValue.ToString().Length;
			var actualMaxValue = long.MaxValue.GetLength();
			var expectZero = 0.ToString().Length;
			var actualZero = 0.GetLength();

			Assert.IsTrue(expectZero == actualZero);
			Assert.IsTrue(expectMaxValue == actualMaxValue);
		}

		[TestMethod]
		public void Int64_GetDigits_test()
		{
			var testNumber = 12398564564353;
			var expected = new byte[] { 1, 2, 3, 9, 8, 5, 6, 4, 5, 6, 4, 3, 5, 3 };
			var actual = testNumber.GetDigits().ToArray();
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Int64_GetDigits_zero_test()
		{
			long testNumber = 0;
			var expected = new byte[] { 0 };
			var actual = testNumber.GetDigits().ToArray();
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Int64_GetDigits_minus_test()
		{
			var testNumber = -94564651554563813;
			var expected = new byte[] { 9, 4, 5, 6, 4, 6, 5, 1, 5, 5, 4, 5, 6, 3, 8, 1, 3 };
			var actual = testNumber.GetDigits().ToArray();
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void IsOdd_test()
		{
			for (var i = 10_000_000; i < 1_000_000_000; i += 2)
				Assert.IsTrue(i.IsOdd());
		}

		[TestMethod]
		public void IsEven_test()
		{
			for (var i = 10_000_001; i < 1_000_000_000; i += 2)
				Assert.IsTrue(i.IsEven());
		}

		[TestMethod]
		public void GCD_test()
		{
			var firstNumbers = new long[] { 205151, 5564561, 564351563, 5643516, 564131 };
			var secondNumbers = new long[] { 751451, 54646, 4564645, 545, 545 };
			var gcds = new long[] { 1, 1, 1, 1, 1 };
			for (var i = 0; i < firstNumbers.Length; i++)
				Assert.AreEqual(gcds[i], firstNumbers[i].GCD(secondNumbers[i]));
		}

		[TestMethod]
		public void LCM_test()
		{
			var firstNumbers = new long[] { 2556465, 5415, 545, 5451, 65456 };
			var secondNumbers = new long[] { 745, 4545, 5415, 463, 5456 };
			var lcms = new long[] { 380913285, 1640745, 590235, 2523813, 22320496 };
			for (var i = 0; i < firstNumbers.Length; i++)
				Assert.AreEqual(lcms[i], firstNumbers[i].LCM(secondNumbers[i]));
		}

		[TestMethod]
		public void Swap_test()
		{
			var firstNumber = 53432453453135;
			var secondNumber = 1212122453453135;
			firstNumber.Swap(ref secondNumber);
			Assert.AreEqual(firstNumber, 1212122453453135);
			Assert.AreEqual(secondNumber, 53432453453135);
		}

		[TestMethod]
		public void ToBinary_test()
		{
			var numbers = new[]
			              {
				              1564564534,
				              34354232,
				              3565645456,
				              53453454564,
				              654351354565,
				              6643212,
				              5454313431543213,
				              56453413256441
			              };

			var binaries = new[]
			               {
				               "1011101010000010101110000110110",
				               "10000011000011010000111000",
				               "11010100100001110110111010010000",
				               "110001110010000100101111110011100100",
				               "1001100001011010011000010100101011000101",
				               "11001010101111000001100",
				               "10011011000001010101101001110100101001010110110101101",
				               "1100110101100000010101101001001011110011111001"
			               };

			CollectionAssert.AreEqual(numbers.Select(num => num.ToBinary()).ToArray(), binaries);
		}

		[TestMethod]
		public void ToHEX_test()
		{
			var numbers = new[]
			              {
				              1654530,
				              254645314150,
				              3654563164321547,
				              5643521654435132,
				              45645645645,
				              77854354864533,
				              5623564423654423,
				              863523231234699999
			              };

			var hexes = new[]
			            {
				            "193F02",
				            "3B4A0B2266",
				            "CFBCE4B37F70B",
				            "140CC0C77EAD3C",
				            "AA0B1474D",
				            "46CEE12A7995",
				            "13FA9A1FD7EC17",
				            "BFBD9CDBD0AB2DF"
			            };

			CollectionAssert.AreEqual(numbers.Select(num => num.ToHex()).ToArray(),
			                          hexes.Select(x => x.ToLower()).ToArray());
		}
	}
}