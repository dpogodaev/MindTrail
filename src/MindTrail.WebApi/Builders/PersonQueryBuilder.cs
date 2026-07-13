using System;
using MindTrail.ApplicationContracts.Requests.Queries;
using MindTrail.ApplicationContracts.Requests.Queries.Persons;
using MindTrail.WebApi.RequestModels;

namespace MindTrail.WebApi.Builders;

public static class PersonQueryBuilder
{
    public static GetPersonByIdQuery BuildGetPersonByIdQuery(Guid id)
    {
        return new GetPersonByIdQuery(id);
    }

    public static GetPersonsQuery BuildGetPersonsQuery(PersonQueryModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var paginationModel = new PaginationModel(model.PageNumber, model.PageSize);

        var filterModel = string.IsNullOrEmpty(model.FullName) && model.BirthYear == null
            ? null
            : new PersonFilterModel
            {
                FullName = model.FullName,
                BirthYear = model.BirthYear,
            };

        var searchModel = string.IsNullOrEmpty(model.TextSearchQuery)
            ? null
            : new TextSearchModel(model.TextSearchQuery, model.TextSearchCaseSensitive);

        var sortingModel = model.SortField == null
            ? null
            : new PersonSortingModel(model.SortField.Value, model.SortDirection);

        return new GetPersonsQuery
        {
            Pagination = paginationModel,
            Filter = filterModel,
            Search = searchModel,
            Sorting = sortingModel,
        };
    }
}