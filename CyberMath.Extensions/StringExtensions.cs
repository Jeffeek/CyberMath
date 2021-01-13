using System;
using System.Collections.Generic;
using System.Text;

namespace CyberMath.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks <paramref name="input"/> for palindromicity
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns><see cref="Boolean"/> true if <paramref name="input"/> is palindrome</returns>
        public static bool IsPalindrome(this string input)
        {
            if (ReferenceEquals(input, null)) return false;
            if (input.Length == 1) return true;
            for (int i = 0; i < input.Length / 2; i++)
            {
                if (input[i] != input[input.Length - i - 1])
                    return false;
            }

            return true;
        }

        //TODO: unit-test
        /// <summary>
        /// Checks two string for anagramism
        /// </summary>
        /// <param name="inputOriginal">First string to check</param>
        /// <param name="testInput">Second string to check</param>
        /// <returns><see cref="Boolean"/>: true if two string are anagrams of each other</returns>
        public static bool IsAnagram(this string inputOriginal, string testInput)
        {
            if (ReferenceEquals(inputOriginal, null)) throw new ArgumentNullException(nameof(inputOriginal));
            if (ReferenceEquals(testInput, null)) throw new ArgumentNullException(nameof(testInput));
            if (inputOriginal.Length != testInput.Length) return false;
            
            var originalFrequency = CalculateFrequency(inputOriginal);
            var testFrequency = CalculateFrequency(testInput);

            foreach (var key in originalFrequency.Keys)
            {
                if (!testFrequency.ContainsKey(key)) return false;
                if (originalFrequency[key] != testFrequency[key]) return false;
            }

            return true;
        }

        //TODO: unit-test
        /// <summary>
        /// Creates a <see cref="Dictionary{TKey,TValue}"/> where <paramref name="TKey"/> is <see cref="Char"/> and <paramref name="TValue"/> is <see cref="Int32"/> (count of <paramref name="TKey"/> in input string)
        /// </summary>
        /// <param name="input"></param>
        /// <returns>New <see cref="Dictionary{TKey,TValue}"/> where Key is char in input string and Value is count of this char</returns>
        public static Dictionary<char, int> CalculateFrequency(this string input)
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
        /// Concatenates a string the <paramref name="count"/> of times and can append line if <paramref name="appendLine"/> is true
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="count">Count of repeating</param>
        /// <param name="appendLine">Setting for appending.</param>
        /// <returns>New concated string <paramref name="count"/> times</returns>
        public static string Concat(this string input, int count, bool appendLine = false)
        {
            if (ReferenceEquals(input, null)) count = 0;
            if (count == 0) return string.Empty;
            count = Math.Abs(count);
            if (count == 1) return input;
            var sb = new StringBuilder();
            for (int i = 0; i < count - 1; i++)
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
        /// Concatenates a string the <paramref name="count"/> of times, separated by a <paramref name="separator"/>
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="count">Count of repeating</param>
        /// <param name="separator">Separator between strings</param>
        /// <returns>New concated string <paramref name="count"/> times with <paramref name="separator"/> between</returns>
        public static string Concat(this string input, int count, string separator)
        {
            if (ReferenceEquals(input, null)) count = 0;
            if (count == 0) return string.Empty;
            count = Math.Abs(count);
            if (count == 1) return input;
            var sb = new StringBuilder(input);
            for (int i = 0; i < count - 1; i++)
                sb.Append(separator).Append(input);
            return sb.ToString();
        }
    }
}
