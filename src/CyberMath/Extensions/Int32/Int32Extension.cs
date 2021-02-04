using System;
using System.Collections.Generic;

namespace CyberMath.Extensions.Int32
{
    /// <summary>
    /// Extension methods for <see cref="Int32"/>
    /// </summary>
    public static class Int32Extension
    {
        /// <summary>
        /// Checks is number odd
        /// </summary>
        /// <param name="number">number to check</param>
        /// <returns><see langword="true"/> if number is odd; otherwise <see langword="false"/></returns>
        public static bool IsOdd(this int number) => number % 2 == 0;

        /// <summary>
        /// Checks is number even
        /// </summary>
        /// <returns><see langword="true"/> if number is even; otherwise <see langword="false"/></returns>
        /// <returns>is number even</returns>
        public static bool IsEven(this int number) => number % 2 != 0;

        /// <summary>
        /// Calculates greatest common divisor between two <see cref="Int32"/> numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Greatest common divisor</returns>
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
        /// Calculates lowest common multiple between two <see cref="Int32"/> numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Lowest common multiple</returns>
        public static int LCM(this int a, int b) => a / GCD(a, b) * b;

        /// <summary>
        /// Swaps two integers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap(ref this int a, ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// Checks <see cref="Int32"/> for palindromicity
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsPalindrome(this int number)
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

        /// <summary>
        /// Calculates the length of <see cref="Int32"/> number
        /// </summary>
        /// <param name="number"></param>
        /// <returns>Length of <paramref name="number"/></returns>
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

        /// <summary>
        /// Converts <see cref="Int32"/> <paramref name="number"/> to binary(2) format
        /// </summary>
        /// <param name="number">number to convert</param>
        /// <returns><see cref="string"/> representation of the binary form of a <paramref name="number"/></returns>
        public static string ToBinary(this int number) => Convert.ToString(number, 2);

        /// <summary>
        /// Converts <see cref="Int32"/> <paramref name="number"/> to HEX(16) format
        /// </summary>
        /// <param name="number">number to convert</param>
        /// <returns><see cref="string"/> representation of the HEX form of a <paramref name="number"/></returns>
        public static string ToHex(this int number) => Convert.ToString(number, 16);
    }
}
		/// <summary>
		///     Converts <see cref="Int32" /> <paramref name="number" /> to HEX(16) format
		/// </summary>
		/// <param name="number">number to convert</param>
		/// <returns><see cref="string" /> representation of the HEX form of a <paramref name="number" /></returns>
		public static string ToHex(this int number) => Convert.ToString(number, 16);

		/// <summary>
		/// Return a <see cref="IEnumerable{T}"/> collection of all digits of <paramref name="number"/>
		/// </summary>
		/// <param name="number">Number to parse</param>
		/// <returns><see cref="IEnumerable{T}"/> collection of digits</returns>
		public static IEnumerable<byte> GetDigits(this int number)
		{
			if (number == 0) return new byte[] { 0 };
			number = Math.Abs(number);
			var digits = new byte[number.GetLength()];
			var iterator = 0;
			while (number > 0)
			{
				digits[iterator] = (byte)(number % 10);
				number /= 10;
			}

			return digits;
		}
	}
}