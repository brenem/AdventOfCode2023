namespace AdventOfCode2023.Extensions
{
    public static class StringExtensions
    {
        public static string[] SplitTrim(this string str, params char[]? separator)
            => str.Split(separator, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        public static string[] SplitTrim(this string str, string? separator)
            => str.Split(separator, options: StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        public static string[] SplitTrim(this string str, string[]? separator)
            => str.Split(separator, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }
}
