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
    /// <param name="source">Source object.</param>
    /// <typeparam name="T">Object type.</typeparam>
    /// <returns>
    /// Serialized string value if the <paramref name="source"/> is not <c>null</c>;
    /// <c><see cref="string.Empty"/></c> otherwise.
    /// </returns>
    public static string Serialize<T>(this T? source)
        where T : class
    {
        return source != null
            ? JsonSerializer.Serialize(source)
            : string.Empty;
    }
}