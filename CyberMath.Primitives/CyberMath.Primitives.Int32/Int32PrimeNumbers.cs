using System;
using System.Collections.Generic;

namespace CyberMath.Primitives.Int32
{
    public static class Int32PrimeNumbers
    {
        public static bool IsPrime(this int number)
        {
            if (number < 0) throw new Exception("Number was lower than zero");
            if (number == 0) return false;
            if (number == 2) return true;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }

        public static int GenerateRandomPrimeNumber(int min = 3, int max = int.MaxValue)
        {
            if (min < 0) throw new Exception("Min was less than zero");
            if (max < 0) throw new Exception("Max was less than zero");
            if (min > max) throw new Exception("Min was bigger than max");
            var rnd = new Random();
            int number = rnd.Next(min, max);
            while (!IsPrime(number))
                number = rnd.Next(min, max);
            return number;
        }

        public static IEnumerable<int> GeneratePrimeNumbers(int max)
        {
            if (max <= 2) throw new Exception("Max was less or equal 2");
            for (int i = 2; i <= max; i++)
            {
                if (IsPrime(i))
                    yield return i;
            }
        }

        public static IEnumerable<int> GeneratePrimeNumbers()
        {
            int number = 2;
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
