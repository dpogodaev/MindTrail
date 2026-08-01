using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.Queries;
using MindTrail.ApplicationContracts.Models.Base;
using MindTrail.ApplicationContracts.Models.Persons;

namespace MindTrail.ApplicationContracts.Queries.Persons;

/// <summary>
/// Query for retrieving a paged list of persons.
/// </summary>
public sealed record GetPersonsQuery : PagedQueryModel, IQuery<PagedDto<PersonDto>>
{
    /// <summary>
    /// Gets a model for filtering.
    /// </summary>
    /// <remarks>If <c>null</c>, filtering is not applied.</remarks>
    public PersonFilterModel? Filter { get; init; }

    /// <summary>
    /// Gets a model for sorting.
    /// </summary>
    /// <remarks>
    /// If <c>null</c>, sorting is applied by the time an entry was added in descending order.
    /// </remarks>
    public PersonSortingModel? Sorting { get; init; }
}