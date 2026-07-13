using MindTrail.ApplicationContracts.Enums;

namespace MindTrail.WebApi.RequestModels;

/// <summary>
/// Model for querying a list of persons.
/// </summary>
public record PersonQueryModel
{
    /// <summary>
    /// Gets the page number.<br/>
    /// The default value is <c>1</c>.
    /// </summary>
    public int? PageNumber { get; init; } = 1;

    /// <summary>
    /// Gets the page size.<br/>
    /// The default value is <c>10</c>.
    /// </summary>
    public int? PageSize { get; init; } = 10;

    /// <summary>
    /// Gets the filter value by full name.
    /// </summary>
    /// <remarks>
    /// Performs a partial, case-insensitive match.<br/>
    /// If <c>null</c> or empty, filtering is not applied.
    /// </remarks>
    public string? FullName { get; init; }

    /// <summary>
    /// Gets the filter value by year of birth.
    /// </summary>
    /// <remarks>
    /// Performs an exact match.<br/>
    /// If <c>null</c>, filtering is not applied.
    /// </remarks>
    public int? BirthYear { get; init; }

    /// <summary>
    /// Gets a query for text search across multiple fields.
    /// </summary>
    /// <remarks>
    /// Supports partial match by full name, year of birth, and country of birth.<br/>
    /// If <c>null</c>, no text search is applied.
    /// </remarks>
    public string? TextSearchQuery { get; init; }

    /// <summary>
    /// Gets a value indicating whether the text search should be case-sensitive.
    /// </summary>
    /// <remarks>
    /// Ignored if <see cref="TextSearchQuery"/> is <c>null</c>.
    /// </remarks>
    public bool? TextSearchCaseSensitive { get; init; }

    /// <summary>
    /// Gets a field for sorting persons.
    /// </summary>
    /// <remarks>
    /// If <c>null</c>, sorting is applied by the time an entry was added in descending order.
    /// </remarks>
    public PersonSortingFieldType? SortField { get; init; }

    /// <summary>
    /// Gets the direction for sorting operations.
    /// </summary>
    /// <remarks>
    /// Ignored if <see cref="SortField"/> is <c>null</c>.
    /// </remarks>
    public SortDirectionType? SortDirection { get; init; }
}