using System;
using System.Linq;
using Xunit;
using tlycken.Extensions;

namespace tlycken.Extensions.Tests
{
    public class Distinct
    {
        public class Foo
        {
            public int Bar { get; set; }
        }

        [Fact]
        public void OnEmptyEnumerable_ReturnsEmptyEnumerable()
        {
            var input = Enumerable.Empty<int>();

            var distinct = input.Distinct(x => x - 1);

            Assert.Empty(distinct);
        }

        [Fact]
        public void OnDistinctEnumerable_ReturnsSameCount()
        {
            var input = Enumerable.Range(0, 5).Select(i => new Foo { Bar = i });

            var distinct = input.Distinct(foo => foo.Bar);

            Assert.Equal(5, distinct.Count());
        }

        [Fact]
        public void OnEnumerableWithDuplicates_ReturnsOnlyDistinct()
        {
            var input = Enumerable.Range(0, 5).Concat(Enumerable.Range(1, 2)).Select(i => new Foo { Bar = i });

            var expected = input.Select(foo => foo.Bar).Distinct();

            var actual = input.Distinct(foo => foo.Bar).Select(foo => foo.Bar);

            Assert.Equal(expected, actual);
        }
    }
}
