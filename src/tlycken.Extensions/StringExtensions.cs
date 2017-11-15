using System.Globalization;

namespace System
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string input)
            => input.ToTitleCase(CultureInfo.InvariantCulture);

        public static string ToTitleCase(this string input, CultureInfo culture)
            => culture.TextInfo.ToTitleCase(input);

        public static string ToCamelCase(this string input)
            => input.ToCamelCase(CultureInfo.InvariantCulture);

        public static string ToCamelCase(this string input, CultureInfo culture)
            => string.IsNullOrEmpty(input)
                ? input
                : culture.TextInfo.ToLower(input[0]) + culture.TextInfo
                    .ToTitleCase(input)
                    .Replace(" ", string.Empty)
                    .Substring(1);
    }
}
