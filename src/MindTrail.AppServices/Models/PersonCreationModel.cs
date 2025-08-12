namespace MindTrail.AppServices.Models;

/// <summary>
/// Model for creating a person.
/// </summary>
public class PersonCreationModel
{
    /// <summary>
    /// Full name.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Year of birth.
    /// </summary>
    public int? BirthYear { get; set; }

    /// <summary>
    /// ID of the country of birth.
    /// </summary>
    public int? BirthCountryId { get; set; }
}