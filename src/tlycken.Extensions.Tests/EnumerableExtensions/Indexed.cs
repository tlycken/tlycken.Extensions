using System;
using System.Linq;
using Xunit;

namespace tlycken.Extensions.Tests.EnumerableExtensions
{
    public class Indexed
    {
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
    }
}
