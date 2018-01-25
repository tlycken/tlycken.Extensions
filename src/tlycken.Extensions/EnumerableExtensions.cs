using System.Collections.Generic;

namespace System.Linq
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Select distinct elements by a key specified by the provided selector.
        /// </summary>
        /// <param name="source">The source sequence of elements</param>
        /// <param name="keySelector">Determines how to compare elements</param>
        /// <returns>A sequence of elements for which <paramref name="keySelector"/>(item) is unique</returns>
        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
            => source.GroupBy(keySelector).Select(group => group.First());

        /// <summary>
        /// Return a tuple of each value and its index
        /// </summary>
        /// <param name="source">The source sequence of elements</param>
        /// <returns>A sequence of elements of type <code>(T, int)</code>. The first item is the value, and the second is the index.</returns>
        public static IEnumerable<(T, int)> Indexed<T>(this IEnumerable<T> source) => source.Select((x, i) => (value: x, index: i));

        /// <summary>
        /// Same as <code>Take()</code>, but does not throw if the sequence is shorter.
        /// </summary>
        /// <param name="source">The source sequence of elements</param>
        /// <param name="max">The number of elements to return, at the most.</param>
        /// <returns>The first <paramref name="max" /> elements, or the entire sequence if it is shorter.</returns>
        public static IEnumerable<T> TakeUpTo<T>(this IEnumerable<T> source, int max)
        {
            int current = 0;
            foreach (var item in source)
            {
                if (current++ == max)
                {
                    yield break;
                }
                yield return item;
            }
        }

        /// <summary>
        /// Same as <code>Skip()</code>, but does not throw if the sequence is shorter.
        /// </summary>
        /// <param name="source">The source sequence of elements</param>
        /// <param name="max">The number of elements to skip, at the most.</param>
        /// <returns>All except the first <paramref name="max" /> elements, or an empty sequence if the source is shorter.</returns>
        public static IEnumerable<T> SkipUpTo<T>(this IEnumerable<T> source, int max)
        {
            int current = 0;
            foreach (var item in source)
            {
                if (current++ < max)
                {
                    continue;
                }
                yield return item;
            }
        }
    }
}
