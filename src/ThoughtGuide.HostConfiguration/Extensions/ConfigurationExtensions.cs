using Microsoft.Extensions.Configuration;

namespace ThoughtGuide.HostConfiguration.Extensions;

/// <summary>
/// Application configuration extensions.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Indicates if the configuration section exists.
    /// </summary>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    /// <param name="key">The key of the configuration section. The colon is used as a separator.</param>
    /// <returns><c>true</c> if the section with the specified key is found; <c>false</c> otherwise.</returns>
    public static bool SectionExists(this IConfiguration configuration, string key)
    {
        return configuration.GetSection(key).Exists();
    }

    /// <summary>
    /// Binds the configuration section to a new instance of the specified type.
    /// </summary>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    /// <param name="key">The key of the configuration section. The colon is used as a separator.</param>
    /// <typeparam name="T">The type of the new instance to bind.</typeparam>
    /// <returns>
    /// The new <typeparamref name="T"/> instance if the section with the specified key is found and the binding was successful;
    /// <c>null</c> otherwise.
    /// </returns>
    /// <remarks>Binding is performed by recursively matching property names with configuration keys.</remarks>
    public static T BindSection<T>(this IConfiguration configuration, string key) where T : class
    {
        return configuration.GetSection(key).Get<T>();
    }

    /// <summary>
    /// Indicates if the configuration parameter exists and has a value other than <c><see cref="string.Empty"/></c> and <c>null</c>.
    /// </summary>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    /// <param name="key">The key of the configuration property. The colon is used as a separator.</param>
    /// <returns>
    /// <c>true</c> if a parameter with the specified key is found and has a value other than <c><see cref="string.Empty"/></c> and <c>null</c>;
    /// <c>false</c> otherwise.
    /// </returns>
    public static bool PropertyExists(this IConfiguration configuration, string key)
    {
        return !string.IsNullOrEmpty(GetProperty(configuration, key));
    }

    /// <summary>
    /// Checks for the presence of the parameter and gets it from the configuration.
    /// </summary>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    /// <param name="key">The key of the configuration property. The colon is used as a separator.</param>
    /// <param name="value">Parameter value obtained from the configuration.</param>
    /// <returns>
    /// <c>true</c> if the configuration parameter exists and is not equal to null or an empty string;
    /// <c>false</c> otherwise.
    /// </returns>
    public static bool TryGetProperty(this IConfiguration configuration, string key, out string value)
    {
        value = GetProperty(configuration, key);

        return !string.IsNullOrEmpty(value);
    }

    /// <summary>
    /// Returns the value of the configuration property.
    /// </summary>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    /// <param name="key">The key of the configuration property. The colon is used as a separator.</param>
    /// <returns>Parameter value in <c>string</c> format if the configuration parameter exists; <c>null</c> otherwise.</returns>
    public static string GetProperty(this IConfiguration configuration, string key)
    {
        return configuration[key];
    }
}