#region Using derectives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace CyberMath.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="string" />
    /// </summary>
    public static class StringExtensions
	{
        /// <summary>
        ///     Checks <paramref name="input" /> for palindromicity
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns><see cref="bool" /> true if <paramref name="input" /> is palindrome</returns>
        public static bool IsPalindrome(this string input)
		{
			if (ReferenceEquals(input, null)) return false;
			if (input.Length == 1) return true;
			for (var i = 0; i < input.Length / 2; i++)
			{
				if (input[i] != input[input.Length - i - 1])
					return false;
			}

			return true;
		}

        /// <summary>
        ///     Checks two string for anagramism
        /// </summary>
        /// <param name="inputOriginal">First string to check</param>
        /// <param name="testInput">Second string to check</param>
        /// <returns><see cref="bool" />: true if two string are anagrams of each other</returns>
        public static bool IsAnagramOf(this string inputOriginal, string testInput)
		{
			if (ReferenceEquals(inputOriginal, null)) throw new ArgumentNullException(nameof(inputOriginal));
			if (ReferenceEquals(testInput, null)) throw new ArgumentNullException(nameof(testInput));
			if (inputOriginal.Length != testInput.Length) return false;

			var originalFrequency = WordsFrequency(inputOriginal);
			var testFrequency = WordsFrequency(testInput);

			foreach (var key in originalFrequency.Keys)
			{
				if (!testFrequency.ContainsKey(key)) return false;
				if (originalFrequency[key] != testFrequency[key]) return false;
			}

			return true;
		}

        /// <summary>
        ///     Creates a <see cref="Dictionary{TKey,TValue}" /> where <see langword="TKey" /> is <see cref="char" /> and
        ///     <see langword="TValue" /> is <see cref="int" /> (count of <see langword="TKey" /> in input string)
        /// </summary>
        /// <param name="input"></param>
        /// <returns>New <see cref="Dictionary{TKey,TValue}" /> where Key is char in input string and Value is count of this char</returns>
        public static Dictionary<char, int> WordsFrequency(this string input)
		{
			if (ReferenceEquals(input, null)) throw new ArgumentNullException(nameof(input));
			var frequency = new Dictionary<char, int>();
			foreach (var c in input)
			{
				if (!frequency.ContainsKey(c))
					frequency.Add(c, 0);

				++frequency[c];
			}

			return frequency;
		}

        /// <summary>
        ///     Concatenates a string the <paramref name="count" /> of times and can append line if <paramref name="appendLine" />
        ///     is true
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="count">Count of repeating</param>
        /// <param name="appendLine">Setting for appending.</param>
        /// <returns>New concated string <paramref name="count" /> times</returns>
        public static string Concat(this string input, int count, bool appendLine = false)
		{
			if (ReferenceEquals(input, null)) count = 0;
			if (count == 0) return string.Empty;
			count = Math.Abs(count);
			if (count == 1) return input;
			var sb = new StringBuilder();
			for (var i = 0; i < count - 1; i++)
			{
				if (appendLine)
					sb.Append(input).Append(Environment.NewLine);
				else
					sb.Append(input);
			}

			sb.Append(input);
			return sb.ToString();
		}

        /// <summary>
        ///     Concatenates a string the <paramref name="count" /> of times, separated by a <paramref name="separator" />
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="count">Count of repeating</param>
        /// <param name="separator">Separator between strings</param>
        /// <returns>New concated string <paramref name="count" /> times with <paramref name="separator" /> between</returns>
        public static string Concat(this string input, int count, string separator)
		{
			if (ReferenceEquals(input, null)) count = 0;
			if (count == 0) return string.Empty;
			count = Math.Abs(count);
			if (count == 1) return input;
			var sb = new StringBuilder(input);
			for (var i = 0; i < count - 1; i++)
				sb.Append(separator).Append(input);

			return sb.ToString();
		}

        /// <summary>
        ///     Returns <paramref name="input" /> <seealso cref="string" /> converted to <see cref="int" />
        /// </summary>
        /// <param name="input">Input string, which is number</param>
        /// <returns><see cref="int" /> result number</returns>
        /// <exception cref="ArgumentException">When <paramref name="input" /> represents not <see cref="Int32" /></exception>
        public static int ToInt32(this string input)
		{
			if (ReferenceEquals(input, null)) throw new NullReferenceException(nameof(input));
			if (int.TryParse(input, out var result))
				return result;

			throw new ArgumentException(nameof(input));
		}

        /// <summary>
        ///     Returns <paramref name="input" /> <seealso cref="string" /> converted to <see cref="long" />
        /// </summary>
        /// <param name="input">Input string, which is number</param>
        /// <returns><see cref="long" /> result number</returns>
        /// <exception cref="ArgumentException">When <paramref name="input" /> represents not <see cref="Int64" /></exception>
        public static long ToInt64(this string input)
		{
			if (ReferenceEquals(input, null)) throw new NullReferenceException(nameof(input));
			if (long.TryParse(input, out var result))
				return result;

			throw new ArgumentException(nameof(input));
		}

        /// <summary>
        ///     Converts <paramref name="input" /> string to alternating case
        ///     <br />
        ///     <c>
        ///         <example>
        ///             <paramref name="input" /> = kEk<br />
        ///             output = KeK
        ///         </example>
        ///     </c>
        /// </summary>
        /// <param name="input">Input string to convert</param>
        /// <returns>New alternating string of <paramref name="input" /></returns>
        public static string ToAlternatingCase(this string input)
		{
			if (ReferenceEquals(input, null)) throw new ArgumentNullException(nameof(input));
			var length = input.Length;
			var output = new char[length];
			int i = 0, j = length - 1;
			while (i <= j)
			{
				output[i] = ShiftCase(input[i++]);
				output[j] = ShiftCase(input[j--]);
			}

			return string.Concat(output);
		}

		private static char ShiftCase(char c)
		{
			var i = (int)c;
			if (i < 65 ||
			    i > 122)
				return c;

			if (i < 97 &&
			    i > 90)
				return c;

			return (char)(i > 90 ? c & ~0x20 : c | 0x20);
		}
	}
}