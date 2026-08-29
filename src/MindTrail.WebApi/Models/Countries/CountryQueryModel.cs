using MindTrail.ApplicationContracts.Enums;

namespace MindTrail.WebApi.Models.Countries;

/// <summary>
/// Model for querying a list of countries.
/// </summary>
public sealed record CountryQueryModel
{
    /// <summary>
    /// A page number. The default value is <c>1</c>.
    /// </summary>
    public int? PageNumber { get; init; } = 1;

    /// <summary>
    /// A page size. The default value is <c>10</c>.
    /// </summary>
    public int? PageSize { get; init; } = 10;

    /// <summary>
    /// A filter value by country code.
    /// </summary>
    /// <remarks>
    /// Performs a partial, case-insensitive match.
    /// Ignored if <c>null</c> or empty.
    /// </remarks>
    public string? Code { get; init; }

    /// <summary>
    /// A filter value by the name of the country.
    /// </summary>
    /// <remarks>
    /// Performs a partial, case-insensitive match.
    /// Ignored if <c>null</c> or empty.
    /// </remarks>
    public string? Name { get; init; }

    /// <summary>
    /// A query for text search across multiple fields.
    /// </summary>
    /// <remarks>
    /// Supports partial match by country name and country code.
    /// If <c>null</c>, no text search is applied.
    /// </remarks>
    public string? TextSearchQuery { get; init; }

    /// <summary>
    /// Whether the text search should be case-sensitive.
    /// </summary>
    /// <remarks>
    /// Ignored if <see cref="TextSearchQuery"/> is <c>null</c>.
    /// </remarks>
    public bool? TextSearchCaseSensitive { get; init; }

    /// <summary>
    /// A field for sorting countries.
    /// </summary>
    /// <remarks>
    /// If <c>null</c>, sorting is applied by name in ascending order.
    /// </remarks>
    public CountrySortingFieldType? SortField { get; init; }

    /// <summary>
    /// A direction for sorting operations.
    /// </summary>
    /// <remarks>
    /// Ignored if <see cref="SortField"/> is <c>null</c>.
    /// </remarks>
    public SortDirectionType? SortDirection { get; init; }
}