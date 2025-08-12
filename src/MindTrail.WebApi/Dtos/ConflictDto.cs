namespace MindTrail.WebApi.Dtos;

/// <summary>
/// Information about the conflict.
/// </summary>
public record ConflictDto
{
    /// <summary>
    /// The name of the property that the conflict is associated with.
    /// </summary>
    public string? PropertyName { get; init; }

    /// <summary>
    /// Value of the property that the conflict is associated with.
    /// </summary>
    public string? PropertyValue { get; init; }

    /// <summary>
    /// Description of the conflict.
    /// </summary>
    public required string Description { get; init; }
}