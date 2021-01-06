using System;
using System.Text;

namespace CyberMath.Structures.Extensions.Extensions
{
    public static class StringExtensions
    {
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

        public static string Concat(this string input, int count, bool appendLine = false)
        {
            if (ReferenceEquals(input, null)) count = 0;
            if (count == 0) return String.Empty;
            count = Math.Abs(count);
            if (count == 1) return input;
            var sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                if (appendLine)
                    sb.AppendLine(input);
                else
                    sb.Append(input);
            }
            return sb.ToString();
        }

        public static string Concat(this string input, int count, string separator)
        {
            if (ReferenceEquals(input, null)) count = 0;
            if (count == 0) return String.Empty;
            count = Math.Abs(count);
            if (count == 1) return input;
            var sb = new StringBuilder(input);
            for (int i = 0; i < count - 1; i++)
                sb.Append(separator).Append(input);
            sb.Append(input);
            return sb.ToString();
        }
    }
}
