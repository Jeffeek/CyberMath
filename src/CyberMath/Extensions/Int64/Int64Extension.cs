﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberMath.Extensions.Int64
{
    /// <summary>
    /// Extension methods for <see cref="Int64"/>
    /// </summary>
    public static class Int64Extension
    {
        //TODO: unit-test
        /// <summary>
        /// Checks is number odd
        /// </summary>
        /// <param name="number">number to check</param>
        /// <returns><see langword="true"/> if number is odd; otherwise <see langword="false"/></returns>
        public static bool IsOdd(this long number) => number % 2 == 0;

        //TODO: unit-test
        /// <summary>
        /// Checks is number even
        /// </summary>
        /// <returns><see langword="true"/> if number is even; otherwise <see langword="false"/></returns>
        /// <returns>is number even</returns>
        public static bool IsEven(this long number) => number % 2 != 0;

        //TODO: unit-test
        /// <summary>
        /// Calculates greatest common divisor between two <see cref="Int64"/> numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Greatest common divisor</returns>
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

        //TODO: unit-test
        /// <summary>
        /// Calculates lowest common multiple between two <see cref="Int64"/> numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Lowest common multiple</returns>
        public static long LCM(this long a, long b) => a / GCD(a, b) * b;

        //TODO: unit-test
        /// <summary>
        /// Swaps two numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap(ref this long a, ref long b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        //TODO: unit-test
        /// <summary>
        /// Checks <see cref="Int64"/> for palindromicity
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsPalindrome(this long number)
        {
            number = Math.Abs(number);
            var div = 1;
            while (number / div >= 10)
                div *= 10;
            while (number != 0)
            {
                var leading = number / div;
                var trailing = number % 10;
                if (leading != trailing)
                    return false;
                number = number % div / 10;
                div /= 100;
            }
            return true;
        }

        //TODO: unit-test
        /// <summary>
        /// Calculates the length of <see cref="Int64"/> number
        /// </summary>
        /// <param name="number"></param>
        /// <returns>Length of <paramref name="number"/></returns>
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

        //TODO: unit-test
        /// <summary>
        /// Converts <see cref="Int64"/> <paramref name="number"/> to binary(2) format
        /// </summary>
        /// <param name="number">number to convert</param>
        /// <returns><see cref="string"/> representation of the binary form of a <paramref name="number"/></returns>
        public static string ToBinary(this long number) => Convert.ToString(number, 2);

        //TODO: unit-test
        /// <summary>
        /// Converts <see cref="Int64"/> <paramref name="number"/> to HEX(16) format
        /// </summary>
        /// <param name="number">number to convert</param>
        /// <returns><see cref="string"/> representation of the HEX form of a <paramref name="number"/></returns>
        public static string ToHex(this long number) => Convert.ToString(number, 16);

        //TODO: unit-test
        /// <summary>
        /// Return a <see cref="IEnumerable{T}"/> collection of all digits of <paramref name="number"/>
        /// </summary>
        /// <param name="number">Number to parse</param>
        /// <returns><see cref="IEnumerable{T}"/> collection of digits</returns>
        public static IEnumerable<byte> GetDigits(this long number)
        {
            if (number == 0) return new byte[] { 0 };
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
