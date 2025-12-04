namespace MindTrail.WebApi.Dtos;

/// <summary>
/// Information about the application.
/// </summary>
public record BuildInfoDto
{
    /// <summary>
    /// Gets the version number.
    /// </summary>
    public required string Version { get; init; }

    /// <summary>
    /// Gets the build date (UTC).
    /// </summary>
    public string? BuildDate { get; init; }

    /// <summary>
    /// Gets the build configuration (<c>"Debug"</c> or <c>"Release"</c>).
    /// </summary>
    public string? Configuration { get; init; }

    /// <summary>
    /// Gets the application name.
    /// </summary>
    public string? AppName { get; init; }
}