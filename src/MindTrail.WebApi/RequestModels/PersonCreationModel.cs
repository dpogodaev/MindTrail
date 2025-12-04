using System.ComponentModel.DataAnnotations;

namespace MindTrail.WebApi.RequestModels;

/// <summary>
/// Model for creating a person.
/// </summary>
public record PersonCreationModel
{
    /// <summary>
    /// Gets or sets the full name.
    /// </summary>
    [Required]
    public required string FullName { get; set; }

    /// <summary>
    /// Gets or sets the year of birth.
    /// </summary>
    public int? BirthYear { get; set; }

    /// <summary>
    /// Gets or sets the ID of the country in which the person was born.
    /// </summary>
    public int? BirthCountryId { get; set; }
}