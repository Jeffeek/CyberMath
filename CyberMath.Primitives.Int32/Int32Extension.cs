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

        public static bool IsPalindrome(this int number) => number.ToString().IsPalindrome();

        /// <summary>
        /// Calculates the percent one integer of the second
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double PercentOf(this int a, int b) => (double)a / b;
    }
}
