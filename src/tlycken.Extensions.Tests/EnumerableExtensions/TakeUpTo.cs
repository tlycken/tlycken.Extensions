using System;
using System.Linq;
using Xunit;

namespace tlycken.Extensions.Tests.EnumerableExtensions
{
    public class TakeUpTo
    {
        [Fact]
        public void OnEmptyEnumerable_YieldsEmptyEnumerable()
        {
            var source = Enumerable.Empty<int>();

            var result = source.TakeUpTo(3);

            Assert.Empty(result);
        }

        [Fact]
        public void OnShorterEnumerable_YieldsAllElements()
        {
            var source = Enumerable.Range(0, 5);

            var result = source.TakeUpTo(10);

            Assert.Equal(source, result);
        }

        [Fact]
        public void OnEqualLengthEnumerable_YieldsAllElements()
        {
            var source = Enumerable.Range(0, 5);
            var result = source.TakeUpTo(5);
            Assert.Equal(source, result);
        }

        [Fact]
        public void OnLongerEnumerable_YieldsFirstElements()
        {
            var source = Enumerable.Range(0, 15);
            var result = source.TakeUpTo(5).ToArray();
            Assert.Equal(source.Take(5), result);
        }
    }
}
