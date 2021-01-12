using System;

namespace CyberMath.Primitives.Int32
{
    public static class Int32Extension
    {
        /// <summary>
        /// Checks is number odd
        /// </summary>
        /// <param name="number">number to check</param>
        /// <returns>is number odd</returns>
        public static bool IsOdd(this int number) => number % 2 == 0;
        /// <summary>
        /// Checks is number even
        /// </summary>
        /// <param name="number">number to check</param>
        /// <returns>is number even</returns>
        public static bool IsEven(this int number) => number % 2 != 0;

        /// <summary>
        /// Greatest common divisor
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int GCD(this int a, int b)
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
        public static int LCM(this int a, int b) => a / GCD(a, b) * b;

        /// <summary>
        /// swaps two integers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap(ref this int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        public static bool IsPalindrome(this int number)
        {
            number = Math.Abs(number);
            int div = 1;
            while (number / div >= 10)
                div *= 10;
            while (number != 0)
            {
                int leading = number / div;
                int trailing = number % 10;
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
        public static int GetLength(this int number)
        {
            if (number == int.MinValue || number == int.MaxValue) return 10;
            number = Math.Abs(number);
            return
                number < 100000 ?
                    number < 100 ?
                    number < 10 ? 1 : 2 :
                    number < 1000 ? 3 :
                        number < 10000 ? 4 : 5 :
                    number < 10000000 ?
                        number < 1000000 ? 6 : 7 :
                        number < 100000000 ? 8 :
                            number < 1000000000 ? 9 : 10;
        }
    }
}
