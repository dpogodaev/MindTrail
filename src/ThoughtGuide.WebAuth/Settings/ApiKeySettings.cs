using System.Collections.Generic;

namespace ThoughtGuide.WebAuth.Settings;

/// <summary>
/// API key settings.
/// </summary>
public class ApiKeySettings
{
    /// <summary>
    /// API key header name.
    /// </summary>
    public string HeaderName { get; init; }

    /// <summary>
    /// API key value.
    /// </summary>
    public string ApiKey { get; init; }

    /// <summary>
    /// Additional API keys.
    /// </summary>
    public Dictionary<string, string> AdditionalApiKeys { get; init; }
}