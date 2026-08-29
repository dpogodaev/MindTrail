using System.Collections.Generic;

namespace MindTrail.WebAuth.Settings;

/// <summary>
/// API key settings.
/// </summary>
public class ApiKeySettings
{
    /// <summary>
    /// The API key header name.
    /// </summary>
    public required string HeaderName { get; init; }

    /// <summary>
    /// The API key value.
    /// </summary>
    public required string ApiKey { get; init; }

    /// <summary>
    /// The additional API keys.
    /// </summary>
    public Dictionary<string, string>? AdditionalApiKeys { get; init; }
}