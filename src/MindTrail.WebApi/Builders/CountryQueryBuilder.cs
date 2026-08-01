using System;
using MindTrail.ApplicationContracts.Models;
using MindTrail.ApplicationContracts.Models.Countries;
using MindTrail.ApplicationContracts.Queries.Countries;
using MindTrail.WebApi.Models.Countries;

namespace MindTrail.WebApi.Builders;

/// <summary>
/// Builds query objects for country operations from web API models.
/// </summary>
public static class CountryQueryBuilder
{
    /// <summary>
    /// Builds a <see cref="GetCountriesQuery"/> from the specified model.
    /// </summary>
    /// <param name="model">The model to query a list of countries.</param>
    /// <returns>The <see cref="GetCountriesQuery"/> to send.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> is <c>null</c>.</exception>
    public static GetCountriesQuery BuildGetCountriesQuery(CountryQueryModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var paginationModel = new PaginationModel(model.PageNumber, model.PageSize);

        var filterModel = string.IsNullOrEmpty(model.Code) && string.IsNullOrEmpty(model.Name)
            ? null
            : new CountryFilterModel(model.Code, model.Name);

        var searchModel = string.IsNullOrEmpty(model.TextSearchQuery)
            ? null
            : new TextSearchModel(model.TextSearchQuery, model.TextSearchCaseSensitive);

        var sortingModel = model.SortField == null
            ? null
            : new CountrySortingModel(model.SortField.Value, model.SortDirection);

        return new GetCountriesQuery
        {
            Pagination = paginationModel,
            Filter = filterModel,
            Search = searchModel,
            Sorting = sortingModel,
        };
    }
}