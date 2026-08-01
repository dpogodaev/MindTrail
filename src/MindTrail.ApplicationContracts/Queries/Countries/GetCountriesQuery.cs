using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.Queries;
using MindTrail.ApplicationContracts.Models.Base;
using MindTrail.ApplicationContracts.Models.Countries;

namespace MindTrail.ApplicationContracts.Queries.Countries;

/// <summary>
/// Query for retrieving a paged list of countries.
/// </summary>
public sealed record GetCountriesQuery : PagedQueryModel, IQuery<PagedDto<CountryDto>>
{
    /// <summary>
    /// Gets a model for filtering.
    /// </summary>
    /// <remarks>If <c>null</c>, filtering is not applied.</remarks>
    public CountryFilterModel? Filter { get; init; }

    /// <summary>
    /// Gets a model for sorting.
    /// </summary>
    /// <remarks>
    /// If <c>null</c>, sorting is applied by name in ascending order.
    /// </remarks>
    public CountrySortingModel? Sorting { get; init; }
}