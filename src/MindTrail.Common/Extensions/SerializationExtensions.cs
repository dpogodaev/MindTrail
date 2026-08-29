using System.Text.Json;

namespace MindTrail.Common.Extensions;

/// <summary>
/// Provides extension methods for serializing objects into JSON strings.
/// </summary>
public static class SerializationExtensions
{
    /// <summary>
    /// Serializes an object of the specified type into a string.
    /// </summary>
    /// <param name="source">The source object.</param>
    /// <typeparam name="T">The type of the object being serialized.</typeparam>
    /// <returns>
    /// The serialized string, or <c><see cref="string.Empty"/></c> if <paramref name="source"/> is <c>null</c>.
    /// </returns>
    public static string Serialize<T>(this T? source)
        where T : class
    {
        return source != null
            ? JsonSerializer.Serialize(source)
            : string.Empty;
    }
}