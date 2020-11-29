using System.Collections.Generic;

namespace CyberMath.Structures.Generators.NumberGenerators
{
    public interface IRandomGenerator<T>
    {
        public int Seed { get; }
        T GenerateOne(T min, T max);

        IEnumerable<T> Generate(T min, T max)
        {
            while(true)
                yield return GenerateOne(min, max);
        }
    }
}
