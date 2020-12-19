using System;

namespace CyberMath.Structures.Extensions.NumberGenerators
{
    public class IntRandomGenerator : IRandomGenerator<int>
    {
        private readonly Random _random;

        public int Seed { get; }

        public IntRandomGenerator(int seed)
        {
            Seed = seed;
            _random = new Random(Seed);
        }

        public IntRandomGenerator()
        {
            Seed = DateTime.Now.Millisecond + DateTime.Now.DayOfYear * DateTime.Now.Second;
            _random = new Random(Seed);
        }

        public int GenerateOne(int min = -50, int max = 50)
        {
            return _random.Next(min, max + 1);
        }
    }
}
