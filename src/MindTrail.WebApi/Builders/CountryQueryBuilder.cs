using System;
using MindTrail.ApplicationContracts.Requests.Queries;
using MindTrail.ApplicationContracts.Requests.Queries.Countries;
using MindTrail.WebApi.RequestModels;

namespace MindTrail.WebApi.Builders;

public class CountryQueryBuilder
{
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