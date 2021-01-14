using System;
using System.Collections.Generic;

namespace CyberMath.Primitives.Int32
{
    /// <summary>
    /// Extension methods of prime numbers for <see cref="Int32"/>
    /// </summary>
    public static class Int32PrimeNumbers
    {
        /// <summary>
        /// Checks <paramref name="number"/> for primality
        /// </summary>
        /// <param name="number"></param>
        /// <returns><see langword="true"/> if <paramref name="number"/> is prime; otherwise <see langword="false"/></returns>
        public static bool IsPrime(this int number)
        {
            if (number < 0) throw new Exception("Number was lower than zero");
            if (number == 0) return false;
            if (number == 2) return true;
            for (var i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Generating one random prime number between <paramref name="min"/> and <paramref name="max"/>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>Prime number</returns>
        public static int GenerateRandomPrimeNumber(int min = 3, int max = int.MaxValue)
        {
            if (min < 0) throw new Exception("Min was less than zero");
            if (max < 0) throw new Exception("Max was less than zero");
            if (min > max) throw new Exception("Min was bigger than max");
            var rnd = new Random();
            var stackCounter = 0;
            var number = rnd.Next(min, max);
            while (!IsPrime(number))
            {
                number = rnd.Next(min, max);
                stackCounter++;
                if (stackCounter == 5000)
                    throw new ArgumentException($"Can't find prime numbers between {min} and {max}");
            }
            return number;
        }

        /// <summary>
        /// Generates <see cref="IEnumerable{T}"/> collection of prime numbers which are less than <paramref name="max"/>
        /// </summary>
        /// <param name="max"></param>
        /// <returns><see cref="IEnumerable{T}"/> of prime numbers between 2 and <paramref name="max"/></returns>
        public static IEnumerable<int> GeneratePrimeNumbers(int max)
        {
            if (max <= 2) throw new Exception("Max was less or equal 2");
            for (var i = 2; i <= max; i++)
            {
                if (IsPrime(i))
                    yield return i;
            }
        }

        /// <summary>
        /// Generator of prime numbers until <seealso cref="int.MaxValue"/>
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<int> GeneratePrimeNumbers()
        {
            var number = 2;
            while (true)
            {
                if (IsPrime(number))
                    yield return number;
                number++;
                if (int.MaxValue == number)
                    yield break;
            }
        }
    }
}
