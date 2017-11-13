using Xunit;
using System;
using System.Globalization;

namespace tlycken.Extensions.Tests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void TitleCaseWithDefaultCulture() {
            var input    = "this is a string";
            var expected = "This Is A String";

            var output = input.ToTitleCase();

            Assert.Equal(expected, output);
        }

        [Fact]
        public void TitleCaseWithSwedishCulture() {
            var input    = "detta är en sträng";
            var expected = "Detta Är En Sträng";

            var output = input.ToTitleCase(new CultureInfo("sv-SE"));

            Assert.Equal(expected, output);
        }

        [Fact]
        public void TitleCaseWithTurkishCulture() {
            var input    = "iota ıota";
            var expected = "İota Iota";

            var output = input.ToTitleCase(new CultureInfo("tr-TR"));

            Assert.Equal(expected, output);
        }
    }
}
