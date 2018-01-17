using System;
using System.Linq;
using Xunit;

namespace tlycken.Extensions.Tests
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void DistinctWithComparisonFunc_OnEmptyEnumerable_ReturnsEmptyEnumerable()
        {
            var input = Enumerable.Empty<int>();

            var distinct = input.Distinct(x => x - 1);

            Assert.Empty(distinct);
        }

        [Fact]
        public void DistinctWithComparisonFunc_OnDistinctEnumerable_ReturnsSameCount()
        {
            var input = Enumerable.Range(0, 5).Select(i => new Foo { Bar = i });

            var distinct = input.Distinct(foo => foo.Bar);

            Assert.Equal(5, distinct.Count());
        }

        [Fact]
        public void DistinctWithComparisonFunc_OnEnumerableWithDuplicates_ReturnsOnlyDistinct()
        {
            var input = Enumerable.Range(0, 5).Concat(Enumerable.Range(1, 2)).Select(i => new Foo { Bar = i });

            var expected = input.Select(foo => foo.Bar).Distinct();

            var actual = input.Distinct(foo => foo.Bar).Select(foo => foo.Bar);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Indexed_OnEnumerable_ReturnsTupleWithValueInFirstPos()
        {
            var random = new Random();
            var input = Enumerable.Range(0, 5).Select(i => random.Next()).ToList();

            Assert.Equal(input, input.Indexed().Select(t => t.Item1));
        }

        [Fact]
        public void Indexed_OnEnumerable_ReturnsTupleWithIndicesInSecondPos()
        {
            var random = new Random();
            var input = Enumerable.Range(0, 5).Select(i => random.Next());

            Assert.Equal(Enumerable.Range(0, 5), input.Indexed().Select(t => t.Item2));
        }

        private class Foo
        {
            public int Bar { get; set; }
        }
    }
}
