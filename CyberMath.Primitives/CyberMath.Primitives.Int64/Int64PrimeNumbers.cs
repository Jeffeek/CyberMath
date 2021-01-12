using CyberMath.Extensions;
using System;
using System.Collections.Generic;

namespace CyberMath.Primitives.Int64
{
    public static class Int64PrimeNumbers
    {
        public static bool IsPrime(this long number)
        {
            if (number < 0) throw new Exception("Number was lower than zero");
            if (number == 0) return false;
            if (number == 2) return true;
            for (long i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }

        public static long GenerateRandomPrimeNumber(long min = 3, long max = long.MaxValue)
        {
            if (min < 0) throw new Exception("Min was less than zero");
            if (max < 0) throw new Exception("Max was less than zero");
            if (min > max) throw new Exception("Min was bigger than max");
            var rnd = new Random();
            long number = rnd.NextLong(min, max);
            while (!IsPrime(number))
                number = rnd.NextLong(min, max);
            return number;
        }

        public static IEnumerable<long> GeneratePrimeNumbers(long max)
        {
            if (max <= 2) throw new Exception("Max was less or equal 2");
            for (long i = 2; i <= max; i++)
            {
                if (IsPrime(i))
                    yield return i;
            }
        }

        public static IEnumerable<long> GeneratePrimeNumbers()
        {
            long number = 2;
            while (true)
            {
                if (IsPrime(number))
                    yield return number;
                number++;
                if (long.MaxValue == number)
                    yield break;
            }
        }
    }
}
