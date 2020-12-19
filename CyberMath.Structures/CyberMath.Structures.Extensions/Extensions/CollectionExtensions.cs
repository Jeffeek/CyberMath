using System;
using System.Collections.Generic;

namespace CyberMath.Structures.Extensions.Extensions
{
    public static class CollectionExtensions
    {
        public static void Swap<T>(this IList<T> collection, int firstIndex, int secondIndex)
        {
            if (ReferenceEquals(collection, null)) return;
            if (firstIndex < 0 || firstIndex >= collection.Count) return;
            if (secondIndex < 0 || secondIndex >= collection.Count) return;
            if (firstIndex == secondIndex) return;
            T temp = collection[firstIndex];
            collection[firstIndex] = collection[secondIndex];
            collection[secondIndex] = temp;
        }

        public static void Shuffle<T>(this IList<T> collection)
        {
            if (ReferenceEquals(collection, null)) return;
            if (collection.Count == 1) return;
            var rnd = new Random();
            for (int i = 0; i < collection.Count; i++)
            {
                int randomIndex = rnd.Next(0, collection.Count);
                while (randomIndex != i)
                    randomIndex = rnd.Next(0, collection.Count);
                collection.Swap(i, randomIndex);
            }
        }

        public static IEnumerable<int> GetRepeatedIntEnumerable(int value)
        {
            yield return value;
        }
    }
}
