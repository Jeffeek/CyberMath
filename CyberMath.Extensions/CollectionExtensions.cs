using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberMath.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Swaps items in indexed collections
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="collection">Collection</param>
        /// <param name="firstIndex">First index for swapping
        /// <remarks><param name="firstIndex">&gt;0</param></remarks></param>
        /// <param name="secondIndex">Second index for swapping
        /// <remarks><param name="secondIndex">&gt;0</param></remarks></param>
        public static void Swap<T>(this IList<T> collection, int firstIndex, int secondIndex)
        {
            if (ReferenceEquals(collection, null)) return;
            if (firstIndex < 0 || firstIndex >= collection.Count) return;
            if (secondIndex < 0 || secondIndex >= collection.Count) return;
            if (firstIndex == secondIndex) return;
            var temp = collection[firstIndex];
            collection[firstIndex] = collection[secondIndex];
            collection[secondIndex] = temp;
        }

        /// <summary>
        /// Shuffles the items in an indexed collection
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="collection">The collection to shuffle</param>
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

        //TODO: unit-test
        /// <summary>
        /// Permute <see cref="IEnumerable{T}"> collection with all elements without repeating elements</see>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="list">List of elements</param>
        /// <param name="length">Max length for algorithm</param>
        /// <returns>New <see cref="IEnumerable{T}"/> collection with permutations</returns>
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return Permutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        //TODO: unit-test
        /// <summary>
        /// Returns a new <see cref="IEnumerable{T}"> collections with all permutations of elements without repeating elements</see>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="collection">List of elements</param>
        /// <returns>New <see cref="IEnumerable{T}"/> collection with permutations without repeating</returns>
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> collection)
        {
            if (ReferenceEquals(collection, null)) return Enumerable.Empty<IEnumerable<T>>();
            int length = collection.Count();
            if (length == 0) return Enumerable.Empty<IEnumerable<T>>();
            return Permutations(collection, length);
        }

        //TODO: unit-test
        public static IEnumerable<IEnumerable<T>> PermutationsWithRepeat<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return PermutationsWithRepeat(list, length - 1)
                .SelectMany(t => list,
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        //TODO: unit-test
        /// <summary>
        /// Returns a new <see cref="IEnumerable{T}"> collections with all permutations of elements with repeating elements</see>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="collection">List of elements</param>
        /// <returns>New <see cref="IEnumerable{T}"/> collection with permutations with repeating</returns>
        public static IEnumerable<IEnumerable<T>> PermutationsWithRepeat<T>(this IEnumerable<T> collection)
        {
            if (ReferenceEquals(collection, null)) return Enumerable.Empty<IEnumerable<T>>();
            int length = collection.Count();
            if (length == 0) return Enumerable.Empty<IEnumerable<T>>();
            return PermutationsWithRepeat(collection, length);
        }

        /// <summary>
        /// Gets a random item from <see cref="IEnumerable{T}"/> collection
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="collection">The collection to enumerate</param>
        /// <returns>Random item from enumerable collection</returns>
        public static T RandomItem<T>(this IEnumerable<T> collection)
        {
            if (ReferenceEquals(collection, null)) throw new ArgumentNullException(nameof(collection));
            int length = collection.Count();
            if (length == 0) throw new ArgumentException(nameof(collection) + " was empty");
            if (length == 1) return collection.ElementAt(0);
            var rnd = new Random();
            int randomIndex = rnd.Next(0, length);
            return collection.ElementAt(randomIndex);
        }
    }
}
