using MindTrail.ApplicationContracts.Enums;

namespace MindTrail.WebApi.RequestModels;

/// <summary>
/// Model for querying a list of countries.
/// </summary>
public record CountryQueryModel
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
    /// Gets the filter value by country code.
    /// </summary>
    /// <remarks>
    /// Performs a partial, case-insensitive match.
    /// Ignored if <c>null</c> or empty.
    /// </remarks>
    public string? Code { get; init; }

    /// <summary>
    /// Gets the filter value by the name of the country.
    /// </summary>
    /// <remarks>
    /// Performs a partial, case-insensitive match.
    /// Ignored if <c>null</c> or empty.
    /// </remarks>
    public string? Name { get; init; }

    /// <summary>
    /// Gets a query for text search across multiple fields.
    /// </summary>
    /// <remarks>
    /// Supports partial match by country name and country code.<br/>
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
    /// Gets a field for sorting countries.
    /// </summary>
    /// <remarks>
    /// If <c>null</c>, sorting is applied by name in ascending order.
    /// </remarks>
    public CountrySortingFieldType? SortField { get; init; }

    /// <summary>
    /// Gets the direction for sorting operations.
    /// </summary>
    /// <remarks>
    /// Ignored if <see cref="SortField"/> is <c>null</c>.
    /// </remarks>
    public SortDirectionType? SortDirection { get; init; }
}