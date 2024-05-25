namespace ThoughtGuide.WebApi.Dtos;

/// <summary>
/// Information about the application.
/// </summary>
public record BuildInfoDto
{
    /// <summary>
    /// Version number.
    /// </summary>
    public string Version { get; init; }

    /// <summary>
    /// Build date (UTC).
    /// </summary>
    public string BuildDate { get; init; }

    /// <summary>
    /// Build configuration (Debug or Release).
    /// </summary>
    public string Configuration { get; init; }

    /// <summary>
    /// Application name.
    /// </summary>
    public string AppName { get; init; }
}