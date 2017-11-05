// ReSharper disable once CheckNamespace
namespace System
{
    public static class FuncExtensions
    {
        /// <summary>
        /// Fluently applies the function <paramref name="projection"/> to the argument <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="S">The element type of the queryable sequence</typeparam>
        /// <typeparam name="T">The element type of the result</typeparam>
        /// <param name="source">The source sequence</param>
        /// <param name="projection">A projection to apply to the <paramref name="source"/> sequence</param>
        /// <returns>projection(source)</returns>
        public static T Apply<S, T>(this S source, Func<S, T> projection) => projection(source);
    }
}
