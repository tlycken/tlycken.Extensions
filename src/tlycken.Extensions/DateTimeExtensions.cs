using System.Globalization;

namespace System
{
    public static class DateTimeExtensions
    {
        public static string MonthName(this DateTime dateTime)
            => dateTime.MonthName(CultureInfo.InvariantCulture);

        public static string MonthName(this DateTime dateTime, CultureInfo culture)
            => culture.DateTimeFormat.GetMonthName(dateTime.Month);
    }
}
