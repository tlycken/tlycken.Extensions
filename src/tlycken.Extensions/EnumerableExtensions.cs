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

        public static IEnumerable<(T,int)> Indexed<T>(this IEnumerable<T> source) => source.Select((x,i) => (value: x, index: i));
    }
}
