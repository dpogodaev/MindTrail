using MindTrail.ApplicationContracts.Dtos;

namespace MindTrail.WebApi.RequestModels;

/// <summary>
/// Model for querying a list of persons.
/// </summary>
public record PersonFilterModel
{
    /// <summary>
    /// Gets the page number.
    /// The default value is <c>1</c>.
    /// </summary>
    public uint PageNumber { get; init; } = 1;

    /// <summary>
    /// Gets the page size.
    /// The default value is <c>10</c>.
    /// </summary>
    public uint PageSize { get; init; } = 10;

    /// <summary>
    /// Gets the search text used to filter countries by partial match.
    /// </summary>
    /// <remarks>
    /// If <c>null</c> or empty, no text filtering is applied.
    /// </remarks>
    public string? Search { get; init; }

    /// <summary>
    /// Gets the sorting order of the result list in SQL ORDER BY format,
    /// e.g.: <c>FullName ASC</c>, <c>BirthYear DESC</c> (case insensitive).
    /// </summary>
    /// <remarks>
    /// Supports fields: <see cref="PersonDto.FullName"/>, <see cref="PersonDto.BirthYear"/>.
    /// Default sorting is ASC by <see cref="PersonDto.Id"/>.
    /// </remarks>
    public string? Sorting { get; init; }
}