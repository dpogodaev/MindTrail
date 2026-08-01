using System;
using MindTrail.ApplicationContracts.Models;
using MindTrail.ApplicationContracts.Models.Persons;
using MindTrail.ApplicationContracts.Queries.Persons;
using MindTrail.WebApi.Models.Persons;

namespace MindTrail.WebApi.Builders;

/// <summary>
/// Builds query objects for person operations from web API models.
/// </summary>
public static class PersonQueryBuilder
{
    /// <summary>
    /// Builds a <see cref="GetPersonByIdQuery"/> for the specified ID.
    /// </summary>
    /// <param name="id">The ID of the person to retrieve.</param>
    /// <returns>The <see cref="GetPersonByIdQuery"/> to send.</returns>
    public static GetPersonByIdQuery BuildGetPersonByIdQuery(Guid id)
    {
        return new GetPersonByIdQuery(id);
    }

    /// <summary>
    /// Builds a <see cref="GetPersonsQuery"/> from the specified model.
    /// </summary>
    /// <param name="model">The model to query a list of persons.</param>
    /// <returns>The <see cref="GetPersonsQuery"/> to send.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> is <c>null</c>.</exception>
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