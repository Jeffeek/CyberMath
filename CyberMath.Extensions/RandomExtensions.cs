﻿using System;

namespace CyberMath.Extensions
{
    public static class RandomExtensions
    {
        //TODO: summary
        public static double NextDouble(this Random random, double min, double max) => random.NextDouble() * (max - min) + min;

        //TODO: summary
        public static long NextLong(this Random random, long min, long max)
        {
            byte[] buf = new byte[8];
            random.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            return Math.Abs(longRand % (max - min)) + min;
        }
    }
}
