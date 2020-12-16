using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PrimeNumbersTests
{
    [TestClass]
    public class PrimeNumberCheck
    {
        [TestMethod]
        public void PrimeNumsCheck_10_000()
        {
            string text;
            using (var stream = new StreamReader("10_000_primes.txt"))
                text = stream.ReadToEnd();

            var nums = text.Split('\n').Select(int.Parse).ToArray();
            for (int i = 0; i < nums.Length; i++)
                Assert.IsTrue(CyberMath.Structures.Extensions.NumberGenerators.CyberMath.IsPrime(nums[i]));
        }

        [TestMethod]
        public void GeneratePrimeNums_1_000_000()
        {
            string text = String.Join(Environment.NewLine, CyberMath.Structures.Extensions.NumberGenerators.CyberMath.GeneratePrimeNumbers().Take(1_000_000));
            using (var writer = new StreamWriter("1_000_000_primes.txt"))
                writer.Write(text);
        }

        [TestMethod]
        public void GeneratePrimeNums_10_000_000()
        {
            string text = String.Join(Environment.NewLine, CyberMath.Structures.Extensions.NumberGenerators.CyberMath.GeneratePrimeNumbers().Take(10_000_000));
            using (var writer = new StreamWriter("10_000_000_primes.txt"))
                writer.Write(text);
        }
    }
}
