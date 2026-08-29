using System.ComponentModel.DataAnnotations;

namespace MindTrail.WebApi.Models.Persons;

/// <summary>
/// Model for creating a person.
/// </summary>
public sealed record PersonCreationModel
{
    /// <summary>
    /// The full name.
    /// </summary>
    [Required]
    public required string FullName { get; init; }

    /// <summary>
    /// The year of birth.
    /// </summary>
    public int? BirthYear { get; init; }

    /// <summary>
    /// The ID of the country in which the person was born.
    /// </summary>
    public int? BirthCountryId { get; init; }
}