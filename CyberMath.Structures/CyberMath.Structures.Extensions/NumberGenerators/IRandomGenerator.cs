using System.Collections.Generic;

namespace CyberMath.Structures.Extensions.NumberGenerators
{
    public interface IRandomGenerator<T> where T : struct
    {
        public int Seed { get; }
        T GenerateOne(T min, T max);
        IEnumerable<T> GenerateMany(T min, T max);
    }
}
