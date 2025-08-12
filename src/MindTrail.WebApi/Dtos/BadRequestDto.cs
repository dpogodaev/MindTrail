namespace MindTrail.WebApi.Dtos;

/// <summary>
/// Information about a client error that caused the request not to be processed.
/// </summary>
public record BadRequestDto
{
    /// <summary>
    /// Name of a property with an invalid value.
    /// </summary>
    public string? PropertyName { get; init; }

    /// <summary>
    /// Invalid property value.
    /// </summary>
    public string? PropertyValue { get; init; }

    /// <summary>
    /// Description of the reason for the client's error.
    /// </summary>
    public required string Description { get; init; }
}