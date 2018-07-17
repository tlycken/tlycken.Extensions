using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace tlycken.Extensions.Tests
{
    public class DictionaryGetOrDefaultTests
    {
        private readonly IDictionary<string, string> _strings;
        private readonly Dictionary<int, int> _ints;

        public DictionaryGetOrDefaultTests()
        {
            _strings = new Dictionary<string, string> {
                { "foo", "bar" },
                { "boo", "far" }
            };

            _ints = new Dictionary<int, int> {
                { 17, 4711 },
                { 42, 1337 }
            };
        }

        [Fact]
        public void ExistingKeyReturnsValue()
        {
            foreach (var key in _ints.Keys)
            {
                var expected = _ints[key];
                var actual = _ints.GetOrDefault(key);

                Assert.Equal(expected, actual);
            }

            foreach (var key in _strings.Keys)
            {
                var expected = _strings[key];
                var actual = _strings.GetOrDefault(key);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void NonexistingKeyWithNoDefaultProvidedReturnsTypeDefault()
        {
            Assert.Equal(default(string), _strings.GetOrDefault("baz"));
            Assert.Equal(default(int), _ints.GetOrDefault(7));
        }

        [Fact]
        public void NonexistingKeyWithDefaultProvidedReturnsTypeDefault()
        {
            Assert.Equal("qux", _strings.GetOrDefault("baz", "qux"));
            Assert.Equal(12, _ints.GetOrDefault(7, 12));
        }
    }
}
