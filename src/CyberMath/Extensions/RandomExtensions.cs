#region Using namespaces

using System;

#endregion

namespace CyberMath.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="Random"/>
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        ///     Generates a <see cref="double"/> number between <paramref name="min"/> and <paramref name="max"/>
        /// </summary>
        /// <param name="random">Random instance</param>
        /// <param name="min">Minimal value to generate</param>
        /// <param name="max">Maximal value to generate</param>
        /// <returns><see cref="double"/> value between <paramref name="min"/> and <paramref name="max"/></returns>
        public static double NextDouble(this Random random, double min, double max) => random.NextDouble() * (max - min) + min;

        /// <summary>
        ///     Generates a <see cref="long"/> number between <paramref name="min"/> and <paramref name="max"/>
        /// </summary>
        /// <param name="random">Random instance</param>
        /// <param name="min">Minimal value to generate</param>
        /// <param name="max">Maximal value to generate</param>
        /// <returns><see cref="long"/> value between <paramref name="min"/> and <paramref name="max"/></returns>
        public static long NextLong(this Random random, long min, long max)
        {
            var buf = new byte[8];
            random.NextBytes(buf);
            var longRand = BitConverter.ToInt64(buf, 0);

            return Math.Abs(longRand % (max - min)) + min;
        }
    }
}