using System;
using System.Collections.Generic;
using System.Text;

namespace CyberMath.Extensions
{
    public static class StringExtensions
    {
        //TODO: summary
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
        
        //TODO: unit-test & summary
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

        //TODO: unit-test & summary
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

        //TODO: summary
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
        
        //TODO: summary
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
