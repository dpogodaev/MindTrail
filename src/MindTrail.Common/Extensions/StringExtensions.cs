namespace MindTrail.Common.Extensions;

/// <summary>
/// Provides extension methods for manipulating string values.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Makes the first character lower case.
    /// </summary>
    /// <param name="source">The source string.</param>
    /// <returns>The source string with the first character in lowercase.</returns>
    public static string FirstCharToLowerCase(this string? source)
    {
        if (string.IsNullOrEmpty(source))
        {
            return string.Empty;
        }

        return source.Length == 1
            ? char.ToLower(source[0]).ToString()
            : char.ToLower(source[0]) + source[1..];
    }
}