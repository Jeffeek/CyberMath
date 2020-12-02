using System;

namespace CyberMath.Structures.Extensions
{
    public static class RandomExtensions
    {
        public static double NextDouble(this Random random, double min = -50.0d, double max = 50.0d)
        {
            return random.NextDouble() * (max - min) + min;
        }
    }
}
