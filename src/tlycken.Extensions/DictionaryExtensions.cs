using System.Collections.Generic;

namespace System.Linq
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue @default = default(TValue))
            => key != null
                ? source.ContainsKey(key)
                    ? source[key]
                    : @default
                : throw new ArgumentNullException(nameof(key));
    }
}
