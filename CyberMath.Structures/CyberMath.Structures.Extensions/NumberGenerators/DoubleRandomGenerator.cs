using System;
using System.Collections.Generic;

namespace CyberMath.Structures.Extensions.NumberGenerators
{
    public class DoubleRandomGenerator : IRandomGenerator<double>
    {
        private readonly Random _random;
        public int Seed { get; }

        public DoubleRandomGenerator(int seed)
        {
            Seed = seed;
            _random = new Random(seed);
        }

        public DoubleRandomGenerator()
        {
            _random = new Random();
        }

        public double GenerateOne(double min = -50.0d, double max = -50.0d)
        {
            return _random.NextDouble() * (max - min) + min;
        }

        public IEnumerable<double> GenerateMany(double min = -50.0d, double max = 50.0d)
        {
            while (true)
                yield return GenerateOne(min, max);
        }
    }
}
