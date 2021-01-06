using System;

namespace CyberMath.Primitives.Int64
{
    public static class Int64Extension
    {
        /// <summary>
        /// Checks is number odd
        /// </summary>
        /// <param name="number">number to check</param>
        /// <returns>is number odd</returns>
        public static bool IsOdd(this long number) => number % 2 == 0;
        
        /// <summary>
        /// Checks is number even
        /// </summary>
        /// <param name="number">number to check</param>
        /// <returns>is number even</returns>
        public static bool IsEven(this long number) => number % 2 != 0;

        /// <summary>
        /// Greatest common divisor
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long GCD(this long a, long b)
        {
            if (a < b)
                Swap(ref a, ref b);
            while (b > 0)
            {
                a %= b;
                Swap(ref a, ref b);
            }

            return a;
        }

        /// <summary>
        /// lowest common multiple
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long LCM(this long a, long b) => a / GCD(a, b) * b;

        /// <summary>
        /// swaps two integers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap(ref this long a, ref long b)
        {
            long temp = a;
            a = b;
            b = temp;
        }

        public static bool IsPalindrome(this long number)
        {
            number = Math.Abs(number);
            int div = 1;
            while (number / div >= 10)
                div *= 10;
            while (number != 0)
            {
                long leading = number / div;
                long trailing = number % 10;
                if (leading != trailing)
                    return false;
                number = number % div / 10;
                div /= 100;
            }
            return true;
        }

        /// <summary>
        /// Calculates the length of Int32 structure
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int GetLength(this long number)
        {
            if (number == long.MinValue || number == long.MaxValue) return 19;
            number = Math.Abs(number);
            return number >= 1_000_000_000_000_000_000 ? 19 :
                number >= 1_000_000_000_000_000_00 ? 18 :
                number >= 1_000_000_000_000_000_0 ? 17 :
                number >= 1_000_000_000_000_000 ? 16 :
                number >= 1_000_000_000_000_00 ? 15 :
                number >= 1_000_000_000_000_0 ? 14 :
                number >= 1_000_000_000_000 ? 13 :
                number >= 1_000_000_000_00 ? 12 :
                number >= 1_000_000_000_0 ? 11 :
                number >= 1_000_000_000 ? 10 :
                number >= 1_000_000_00 ? 9 :
                number >= 1_000_000_0 ? 8 :
                number >= 1_000_000 ? 7 :
                number >= 1_000_00 ? 6 :
                number >= 1_000_0 ? 5 :
                number >= 1_000 ? 4 :
                number >= 1_00 ? 3 :
                number < 10 ? 1 : 2;
        }
    }
}
