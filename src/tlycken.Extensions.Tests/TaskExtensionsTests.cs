using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace tlycken.Extensions.Tests
{
    public class TaskExtensionsTests
    {
        [Fact]
        public async Task CanAwaitIEnumerableAsList()
        {
            var task = Task.FromResult(new[] { 1, 2, 3, 4 }.AsEnumerable());

            List<int> list = await task.ToList();

            Assert.IsType<List<int>>(list);
            Assert.Collection(list,
                i => Assert.Equal(1, i),
                i => Assert.Equal(2, i),
                i => Assert.Equal(3, i),
                i => Assert.Equal(4, i));
        }

        [Fact]
        public async Task CanAwaitIEnumerableAsReadOnlyCollection()
        {
            var task = Task.FromResult(new[] { 1, 2, 3, 4 }.AsEnumerable());

            IReadOnlyCollection<int> roc = await task.ToReadOnlyCollection();

            Assert.IsAssignableFrom<IReadOnlyCollection<int>>(roc);
            Assert.Collection(roc,
                i => Assert.Equal(1, i),
                i => Assert.Equal(2, i),
                i => Assert.Equal(3, i),
                i => Assert.Equal(4, i));
        }

        [Fact]
        public async Task CanAwaitListAsIEnumerable()
        {
            var task = Task.FromResult(new List<int> { 1, 2, 3, 4, });

            Task<IEnumerable<int>> GetResults() => task.AsEnumerable();

            var result = await GetResults();

            Assert.Collection(result,
                i => Assert.Equal(1, i),
                i => Assert.Equal(2, i),
                i => Assert.Equal(3, i),
                i => Assert.Equal(4, i));
        }
    }
}
