using MindTrail.DomainEntities.Entities;

namespace MindTrail.DomainServices.Filters;

/// <summary>
/// Filter and pagination parameters for querying <see cref="Person"/> entities.
/// </summary>
public class PersonFilter
{
    /// <summary>
    /// Filter for the full name. Optional.
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Filter for the birth year. Optional.
    /// </summary>
    public int? BirthYear { get; set; }

    /// <summary>
    /// Page number.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Page size.
    /// </summary>
    public int PageSize { get; set; }
}