#region Using namespaces

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace CyberMath.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="string"/>
    /// </summary>
    public static class StringExtensions
    {
        private const char UPPERCASE_A = 'A';
        private const char UPPERCASE_Z = 'Z';
        private const char LOWERCASE_A = 'a';
        private const char LOWERCASE_Z = 'z';
        private const int ASCII_CASE_DIFFERENCE = LOWERCASE_A - LOWERCASE_A;

        /// <summary>
        ///     Checks <paramref name="input"/> for palindromicity
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns><see cref="bool"/> true if <paramref name="input"/> is palindrome</returns>
        public static bool IsPalindrome(this string? input)
        {
            if (input is null) return false;
            if (input.Length == 1) return true;

            for (var i = 0; i < input.Length / 2; i++)
                if (input[i] != input[input.Length - i - 1])
                    return false;

            return true;
        }

        /// <summary>
        ///     Checks two string for anagramism
        /// </summary>
        /// <param name="inputOriginal">First string to check</param>
        /// <param name="testInput">Second string to check</param>
        /// <returns><see cref="bool"/>: true if two string are anagrams of each other</returns>
        public static bool IsAnagramOf(this string? inputOriginal, string? testInput)
        {
            if (inputOriginal == null || testInput == null)
                return false;

            if (inputOriginal.Length != testInput.Length)
                return false;

            var charCount = new int[256];

            foreach (var c in inputOriginal)
                charCount[c]++;

            foreach (var c in testInput)
            {
                charCount[c]--;
                if (charCount[c] < 0)
                    return false;
            }

            return true;
        }

        /// <summary>
        ///     Creates a <see cref="Dictionary{TKey,TValue}"/> where <see langword="TKey"/> is <see cref="char"/> and
        ///     <see langword="TValue"/> is <see cref="int"/> (count of <see langword="TKey"/> in input string)
        /// </summary>
        /// <param name="input"></param>
        /// <returns>New <see cref="Dictionary{TKey,TValue}"/> where Key is char in input string and Value is count of this char</returns>
        public static Dictionary<char, int> WordsFrequency(this string? input)
        {
            if (input is null) throw new ArgumentNullException(nameof(input));

            var frequency = new Dictionary<char, int>();

            foreach (var c in input)
            {
                frequency.TryAdd(c, 0);
                ++frequency[c];
            }

            return frequency;
        }

        /// <summary>
        ///     Concatenates a string the <paramref name="count"/> of times and can append line if <paramref name="appendLine"/>
        ///     is true
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="count">Count of repeating</param>
        /// <param name="appendLine">Setting for appending.</param>
        /// <returns>New concatenated string <paramref name="count"/> times</returns>
        public static string? Concat(this string? input, int count, bool appendLine = false)
        {
            if (input is null) count = 0;

            if (count == 0) return string.Empty;

            count = Math.Abs(count);

            if (count == 1) return input;

            var sb = new StringBuilder();

            for (var i = 0; i < count - 1; i++)
                if (appendLine)
                    sb.Append(input)
                      .Append(Environment.NewLine);
                else
                    sb.Append(input);

            sb.Append(input);

            return sb.ToString();
        }

        /// <summary>
        ///     Concatenates a string the <paramref name="count"/> of times, separated by a <paramref name="separator"/>
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="count">Count of repeating</param>
        /// <param name="separator">Separator between strings</param>
        /// <returns>New concatenated string <paramref name="count"/> times with <paramref name="separator"/> between</returns>
        public static string? Concat(this string? input, int count, string separator)
        {
            if (input is null) count = 0;

            if (count == 0) return string.Empty;

            count = Math.Abs(count);

            if (count == 1) return input;

            var sb = new StringBuilder(input);

            for (var i = 0; i < count - 1; i++)
                sb.Append(separator)
                  .Append(input);

            return sb.ToString();
        }

        /// <summary>
        ///     Returns <paramref name="input"/> <seealso cref="string"/> converted to <see cref="int"/>
        /// </summary>
        /// <param name="input">Input string, which is number</param>
        /// <returns><see cref="int"/> result number</returns>
        /// <exception cref="ArgumentException">When <paramref name="input"/> represents not <see cref="Int32"/></exception>
        public static int ToInt32(this string input)
        {
            if (input is null) throw new ArgumentNullException(nameof(input));

            if (int.TryParse(input, out var result))
                return result;

            throw new ArgumentException("Input was not in correct format", nameof(input));
        }

        /// <summary>
        ///     Returns <paramref name="input"/> <seealso cref="string"/> converted to <see cref="long"/>
        /// </summary>
        /// <param name="input">Input string, which is number</param>
        /// <returns><see cref="long"/> result number</returns>
        /// <exception cref="ArgumentException">When <paramref name="input"/> represents not <see cref="Int64"/></exception>
        public static long ToInt64(this string input)
        {
            if (input is null) throw new ArgumentNullException(nameof(input));

            if (long.TryParse(input, out var result))
                return result;

            throw new ArgumentException("Input was not in correct format", nameof(input));
        }

        /// <summary>
        ///     Converts <paramref name="input"/> string to alternating case
        ///     <br/>
        ///     <c>
        ///         <example>
        ///             <paramref name="input"/> = kEk<br/>
        ///             output = KeK
        ///         </example>
        ///     </c>
        /// </summary>
        /// <param name="input">Input string to convert</param>
        /// <returns>New alternating string of <paramref name="input"/></returns>
        public static string ToAlternatingCase(this string? input)
        {
            if (input is null) throw new ArgumentNullException(nameof(input));
            if (input.Length == 0) return input;

            return string.Create(input.Length, input, (chars, state) =>
            {
                for (var i = 0; i < chars.Length; i++)
                    chars[i] = ShiftCase(state[i]);
            });
        }

        private static char ShiftCase(char c)
        {
            if ((c >= UPPERCASE_A && c <= UPPERCASE_Z) || (c >= LOWERCASE_A && c <= LOWERCASE_Z))
                return (char)(c ^ ASCII_CASE_DIFFERENCE);

            return c;
        }
    }
}