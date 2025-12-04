namespace MindTrail.WebApi.Dtos;

/// <summary>
/// Information about the conflict.
/// </summary>
public record ConflictDto
{
    /// <summary>
    /// Gets the name of the property that the conflict is associated with.
    /// </summary>
    public string? PropertyName { get; init; }

    /// <summary>
    /// Gets the value of the property that the conflict is associated with.
    /// </summary>
    public string? PropertyValue { get; init; }

    /// <summary>
    /// Gets a description of the conflict.
    /// </summary>
    public required string Description { get; init; }
}