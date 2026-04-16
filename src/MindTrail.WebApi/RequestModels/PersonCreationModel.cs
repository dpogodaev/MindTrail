using System.ComponentModel.DataAnnotations;

namespace MindTrail.WebApi.RequestModels;

/// <summary>
/// Model for creating a person.
/// </summary>
public record PersonCreationModel
{
    /// <summary>
    /// Gets the full name.
    /// </summary>
    [Required]
    public required string FullName { get; init; }

    /// <summary>
    /// Gets the year of birth.
    /// </summary>
    public int? BirthYear { get; init; }

    /// <summary>
    /// Gets the ID of the country in which the person was born.
    /// </summary>
    public int? BirthCountryId { get; init; }
}