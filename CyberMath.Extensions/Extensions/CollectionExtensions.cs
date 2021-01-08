using System;
using System.Collections.Generic;

namespace CyberMath.Extensions.Extensions
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
            if (collection.Count < 2) return;
            var rnd = new Random();
            for (int i = collection.Count - 1; i > 0; i--)
            {
                int randomIndex = rnd.Next(0, i + 1);
                while (randomIndex == i)
                    randomIndex = rnd.Next(0, i + 1);
                collection.Swap(i, randomIndex);
            }
        }

        public static T GetRandomItem<T>(IList<T> collection)
        {
            if (ReferenceEquals(collection, null)) return default;
            if (collection.Count == 0) return default;
            if (collection.Count == 1) return collection[0];
            var rnd = new Random();
            int randomIndex = rnd.Next(0, collection.Count);
            return collection[randomIndex];
        }
    }
}
