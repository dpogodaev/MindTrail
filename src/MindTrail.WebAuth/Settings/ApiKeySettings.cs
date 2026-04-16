using System.Collections.Generic;

namespace MindTrail.WebAuth.Settings;

/// <summary>
/// API key settings.
/// </summary>
public class ApiKeySettings
{
    /// <summary>
    ///  Gets the API key header name.
    /// </summary>
    public required string HeaderName { get; init; }

    /// <summary>
    /// Gets the API key value.
    /// </summary>
    public required string ApiKey { get; init; }

    /// <summary>
    /// Gets additional API keys.
    /// </summary>
    public Dictionary<string, string>? AdditionalApiKeys { get; init; }
}