using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberMath.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IEnumerable{T}"/> and <see cref="IList{T}"/>
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Swaps items in indexed collections
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="source">Collection</param>
        /// <param name="firstIndex">First index for swapping</param>
        /// <param name="secondIndex">Second index for swapping</param>
        public static void Swap<T>(this IList<T> source, int firstIndex, int secondIndex)
        {
            if (ReferenceEquals(source, null)) return;
            if (firstIndex < 0 || firstIndex >= source.Count) return;
            if (secondIndex < 0 || secondIndex >= source.Count) return;
            if (firstIndex == secondIndex) return;
            var temp = source[firstIndex];
            source[firstIndex] = source[secondIndex];
            source[secondIndex] = temp;
        }

        /// <summary>
        /// Shuffles the items in an indexed collection
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="source">The collection to shuffle</param>
        public static void Shuffle<T>(this IList<T> source)
        {
            if (ReferenceEquals(source, null)) return;
            if (source.Count < 2) return;
            var rnd = new Random();
            for (var i = source.Count - 1; i > 0; i--)
            {
                var randomIndex = rnd.Next(0, i + 1);
                while (randomIndex == i)
                    randomIndex = rnd.Next(0, i + 1);
                source.Swap(i, randomIndex);
            }
        }

        //TODO: unit-test
        /// <summary>
        /// Permute <see cref="IEnumerable{T}"> collection with all elements without repeating elements</see>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="source">List of elements</param>
        /// <param name="length">Max length for algorithm</param>
        /// <returns>New <see cref="IEnumerable{T}"/> collection with permutations</returns>
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> source, int length)
        {
            if (length == 1) return source.Select(t => new[] { t });
            var enumerable = source as T[] ?? source.ToArray();
            return Permutations(enumerable, length - 1)
                .SelectMany(t => enumerable.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }

        //TODO: unit-test
        /// <summary>
        /// Returns a new <see cref="IEnumerable{T}"> collections with all permutations of elements without repeating elements</see>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="source">List of elements</param>
        /// <returns>New <see cref="IEnumerable{T}"/> collection with permutations without repeating</returns>
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> source)
        {
            if (ReferenceEquals(source, null)) return Enumerable.Empty<IEnumerable<T>>();
            var enumerable = source as T[] ?? source.ToArray();
            var length = enumerable.Count();
            if (length == 0) return Enumerable.Empty<IEnumerable<T>>();
            return Permutations(enumerable, length);
        }

        /// <summary>
        /// Returns a new <see cref="IEnumerable{T}"> collections with all permutations of elements with repeating elements</see>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="source">List of elements</param>
        /// /// <param name="length">Initial length</param>
        /// <returns>New <see cref="IEnumerable{T}"/> collection with permutations with repeating</returns>
        public static IEnumerable<IEnumerable<T>> PermutationsWithRepeat<T>(IEnumerable<T> source, int length)
        {
            if (length == 1) return source.Select(t => new[] { t });
            var enumerable = source as T[] ?? source.ToArray();
            return PermutationsWithRepeat(enumerable, length - 1)
                .SelectMany(t => enumerable,
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }

        /// <summary>
        /// Returns a new <see cref="IEnumerable{T}"> collections with all permutations of elements with repeating elements</see>
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="source">List of elements</param>
        /// <returns>New <see cref="IEnumerable{T}"/> collection with permutations with repeating</returns>
        public static IEnumerable<IEnumerable<T>> PermutationsWithRepeat<T>(this IEnumerable<T> source)
        {
            if (ReferenceEquals(source, null)) return Enumerable.Empty<IEnumerable<T>>();
            var enumerable = source as T[] ?? source.ToArray();
            var length = enumerable.Count();
            if (length == 0) return Enumerable.Empty<IEnumerable<T>>();
            return PermutationsWithRepeat(enumerable, length);
        }

        /// <summary>
        /// Gets a random item from <see cref="IEnumerable{T}"/> collection
        /// </summary>
        /// <typeparam name="T">ANY</typeparam>
        /// <param name="source">The collection to enumerate</param>
        /// <returns>Random item from enumerable collection</returns>
        public static T RandomItem<T>(this IEnumerable<T> source)
        {
            if (ReferenceEquals(source, null)) throw new ArgumentNullException(nameof(source));
            var enumerable = source as T[] ?? source.ToArray();
            var length = enumerable.Count();
            if (length == 0) throw new ArgumentException(nameof(source) + " was empty");
            if (length == 1) return enumerable.ElementAt(0);
            var rnd = new Random();
            var randomIndex = rnd.Next(0, length);
            return enumerable.ElementAt(randomIndex);
        }
    }
}
