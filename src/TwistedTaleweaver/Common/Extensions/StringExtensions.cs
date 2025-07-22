namespace TwistedTaleweaver.Common.Extensions;

public static class StringExtensions
{
    public static string Format(this string input, params object?[] args)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(args);
        return string.Format(input, args);
    }
}