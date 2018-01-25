using System;
using System.Linq;
using Xunit;

namespace tlycken.Extensions.Tests.EnumerableExtensions
{
    public class SkipUpTo
    {
        [Fact]
        public void OnEmptyEnumerable_YieldsEmptyEnumerable()
        {
            var source = Enumerable.Empty<int>();

            var result = source.TakeUpTo(3);

            Assert.Empty(result);
        }

        [Fact]
        public void OnShorterEnumerable_YieldsNoElements()
        {
            var source = Enumerable.Range(0, 5);

            var result = source.SkipUpTo(10);

            Assert.Equal(Enumerable.Empty<int>(), result);
        }

        [Fact]
        public void OnEqualLengthEnumerable_YieldsNoElements()
        {
            var source = Enumerable.Range(0, 5);
            var result = source.SkipUpTo(5);
            Assert.Equal(Enumerable.Empty<int>(), result);
        }

        [Fact]
        public void OnLongerEnumerable_YieldsRemainingElements()
        {
            var source = Enumerable.Range(0, 15);
            var result = source.SkipUpTo(5).ToArray();
            Assert.Equal(source.Skip(5), result);
        }
    }
}
