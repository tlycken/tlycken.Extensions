using System;
using System.Globalization;
using Xunit;

namespace tlycken.Extensions.Tests
{
    public class DateTimeExtensionsTests
    {
        [Theory]
        [InlineData("2017-01-01", "January")]
        [InlineData("2017-02-01", "February")]
        [InlineData("2017-03-01", "March")]
        [InlineData("2017-04-01", "April")]
        [InlineData("2017-05-01", "May")]
        [InlineData("2017-06-01", "June")]
        [InlineData("2017-07-01", "July")]
        [InlineData("2017-08-01", "August")]
        [InlineData("2017-09-01", "September")]
        [InlineData("2017-10-01", "October")]
        [InlineData("2017-11-01", "November")]
        [InlineData("2017-12-01", "December")]
        public void MonthNamesWithDefaultCulture(string date, string expected)
        {
            var dateTime = DateTime.Parse(date);

            var monthName = dateTime.MonthName();

            Assert.Equal(expected, monthName);
        }

        [Theory]
        [InlineData("2017-01-01", "January")]
        [InlineData("2017-02-01", "February")]
        [InlineData("2017-03-01", "March")]
        [InlineData("2017-04-01", "April")]
        [InlineData("2017-05-01", "May")]
        [InlineData("2017-06-01", "June")]
        [InlineData("2017-07-01", "July")]
        [InlineData("2017-08-01", "August")]
        [InlineData("2017-09-01", "September")]
        [InlineData("2017-10-01", "October")]
        [InlineData("2017-11-01", "November")]
        [InlineData("2017-12-01", "December")]
        public void MonthNamesInEnglish(string date, string expected)
        {
            var dateTime = DateTime.Parse(date);

            var monthName = dateTime.MonthName(new CultureInfo("en-US"));

            Assert.Equal(expected, monthName);
        }

        [Theory]
        [InlineData("2017-01-01", "januari")]
        [InlineData("2017-02-01", "februari")]
        [InlineData("2017-03-01", "mars")]
        [InlineData("2017-04-01", "april")]
        [InlineData("2017-05-01", "maj")]
        [InlineData("2017-06-01", "juni")]
        [InlineData("2017-07-01", "juli")]
        [InlineData("2017-08-01", "augusti")]
        [InlineData("2017-09-01", "september")]
        [InlineData("2017-10-01", "oktober")]
        [InlineData("2017-11-01", "november")]
        [InlineData("2017-12-01", "december")]
        public void MonthNamesInSwedish(string date, string expected)
        {
            var dateTime = DateTime.Parse(date);

            var monthName = dateTime.MonthName(new CultureInfo("sv-SE"));

            Assert.Equal(expected, monthName);
        }
    }
}
