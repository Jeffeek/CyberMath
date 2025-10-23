#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace CyberMath.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="int"/>
    /// </summary>
    public static class Int32Extension
    {
        /// <summary>
        ///     Checks is number odd
        /// </summary>
        /// <param name="number">number to check</param>
        /// <returns><see langword="true"/> if number is odd; otherwise <see langword="false"/></returns>
        public static bool IsOdd(this int number) => (number & 1) != 0;

        /// <summary>
        ///     Checks is number even
        /// </summary>
        /// <returns><see langword="true"/> if number is even; otherwise <see langword="false"/></returns>
        /// <returns>is number even</returns>
        public static bool IsEven(this int number) => (number & 1) == 0;

        /// <summary>
        ///     Calculates greatest common divisor between two <see cref="int"/> numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Greatest common divisor</returns>
        // ReSharper disable once InconsistentNaming
        public static int GCD(this int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            if (a < b)
                a.Swap(ref b);

            while (b > 0)
            {
                a %= b;
                a.Swap(ref b);
            }

            return a;
        }

        /// <summary>
        ///     Calculates lowest common multiple between two <see cref="int"/> numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Lowest common multiple</returns>
        // ReSharper disable once InconsistentNaming
        public static int LCM(this int a, int b)
        {
            if (a == 0 || b == 0) return 0;

            var gcd = a.GCD(b);
            var absA = Math.Abs(a);
            var absB = Math.Abs(b);

            var quotient = absA / gcd;

            if (quotient != 0 && absB > int.MaxValue / quotient)
                throw new OverflowException($"LCM of {a} and {b} would overflow int.MaxValue");

            return quotient * absB;
        }

        /// <summary>
        /// Alternative safe LCM using long internally
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static int LCMSafe(this int a, int b)
        {
            if (a == 0 || b == 0) return 0;

            var gcd = a.GCD(b);
            var lcm = Math.Abs((long)a / gcd * b);

            if (lcm > int.MaxValue)
                throw new OverflowException($"LCM of {a} and {b} exceeds int.MaxValue");

            return (int)lcm;
        }

        /// <summary>
        ///     Swaps two integers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap(ref this int a, ref int b) => (a, b) = (b, a);

        /// <summary>
        /// Checks <see cref="int"/> for palindromicity
        /// </summary>
        /// <param name="number">Number to check</param>
        /// <returns><see langword="true"/> if number is palindrome; otherwise <see langword="false"/></returns>
        public static bool IsPalindrome(this int number)
        {
            // FIXED: Optimized version without string allocation
            if (number < 0) return false;
            if (number < 10) return true;

            // Count digits and build reversed number simultaneously
            var original = number;
            var reversed = 0;

            while (number > 0)
            {
                if (reversed > (int.MaxValue - number % 10) / 10)
                    return false;

                reversed = reversed * 10 + number % 10;
                number /= 10;
            }

            return original == reversed;
        }

        /// <summary>
        ///     Calculates the length of <see cref="int"/> number
        /// </summary>
        /// <param name="number"></param>
        /// <returns>Length of <paramref name="number"/></returns>
        public static int GetLength(this int number)
        {
            if (number == int.MinValue || number == int.MaxValue) return 10;

            number = Math.Abs(number);

            return
                number < 100000
                    ? number < 100
                          ? number < 10
                                ? 1
                                : 2
                          : number < 1000
                              ? 3
                              : number < 10000
                                  ? 4
                                  : 5
                    : number < 10000000
                        ? number < 1000000
                              ? 6
                              : 7
                        : number < 100000000
                            ? 8
                            : number < 1000000000
                                ? 9
                                : 10;
        }

        /// <summary>
        ///     Converts <see cref="int"/> <paramref name="number"/> to binary(2) format
        /// </summary>
        /// <param name="number">number to convert</param>
        /// <returns><see cref="string"/> representation of the binary form of a <paramref name="number"/></returns>
        public static string ToBinary(this int number) => Convert.ToString(number, 2);

        /// <summary>
        ///     Converts <see cref="Int32"/> <paramref name="number"/> to HEX(16) format
        /// </summary>
        /// <param name="number">number to convert</param>
        /// <returns><see cref="string"/> representation of the HEX form of a <paramref name="number"/></returns>
        public static string ToHex(this int number) => Convert.ToString(number, 16);

        /// <summary>
        ///     Return a <see cref="IEnumerable{T}"/> collection of all digits of <paramref name="number"/>
        /// </summary>
        /// <param name="number">Number to parse</param>
        /// <returns><see cref="IEnumerable{T}"/> collection of digits</returns>
        public static IEnumerable<byte> GetDigits(this int number)
        {
            if (number == 0)
                return new byte[]
                       {
                           0
                       };

            number = Math.Abs(number);
            var digits = new byte[number.GetLength()];
            var iterator = 0;

            while (number > 0)
            {
                digits[iterator] = (byte)(number % 10);
                number /= 10;
                iterator++;
            }

            return digits.Reverse();
        }
    }
}