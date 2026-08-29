namespace MindTrail.WebApi.Dtos;

/// <summary>
/// Information about the application.
/// </summary>
public record BuildInfoDto
{
    /// <summary>
    /// The version number.
    /// </summary>
    public required string Version { get; init; }

    /// <summary>
    /// The build date (UTC).
    /// </summary>
    public string? BuildDate { get; init; }

    /// <summary>
    /// The build configuration (<c>"Debug"</c> or <c>"Release"</c>).
    /// </summary>
    public string? Configuration { get; init; }

    /// <summary>
    /// The application name.
    /// </summary>
    public string? AppName { get; init; }
}