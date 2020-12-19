using System;
using System.Linq;
using System.Text;

namespace CyberMath.Structures.Extensions.Extensions
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string input)
        {
            if (ReferenceEquals(input, null)) return false;
            var reversed = String.Concat(input.Reverse());
            return input == reversed;
        }

        public static string Concat(this string input, int count, bool appendLine = false)
        {
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
            var sb = new StringBuilder(input);
            for (int i = 0; i < count; i++)
            {
                sb.Append(separator + input);
            }
            return sb.ToString();
        }
    }
}
