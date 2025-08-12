using MindTrail.DomainEntities.Entities;

namespace MindTrail.DomainServices.Filters;

/// <summary>
/// Filter and pagination parameters for querying <see cref="Country"/> entities.
/// </summary>
public class CountryFilter
{
    /// <summary>
    /// Filter for the country name. Optional.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Page number.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Page size.
    /// </summary>
    public int PageSize { get; set; }
}