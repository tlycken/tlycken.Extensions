using System.Globalization;

namespace System
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string input)
            => input.ToTitleCase(CultureInfo.InvariantCulture);

        public static string ToTitleCase(this string input, CultureInfo culture)
            => culture.TextInfo.ToTitleCase(input);
    }
}
