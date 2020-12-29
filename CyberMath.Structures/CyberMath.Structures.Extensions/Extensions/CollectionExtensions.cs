using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

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
            if (collection.Count < 2) return;
            var rnd = new Random();
            for (int i = 0; i < collection.Count; i++)
            {
                int randomIndex = rnd.Next(0, collection.Count);
                while (randomIndex != i)
                    randomIndex = rnd.Next(0, collection.Count);
                collection.Swap(i, randomIndex);
            }
        }

        public static T GetRandomItem<T>(IList<T> collection)
        {
            if (ReferenceEquals(collection, null)) return default;
            if (collection.Count == 0) return default;
            if (collection.Count == 1) return collection[0];
            var rnd = new Random();
            return collection[rnd.Next(0, collection.Count)];
        }
    }
}
