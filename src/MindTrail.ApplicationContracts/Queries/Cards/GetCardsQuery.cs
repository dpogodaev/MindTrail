using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.Queries;
using MindTrail.ApplicationContracts.Models.Base;
using MindTrail.ApplicationContracts.Models.Cards;

namespace MindTrail.ApplicationContracts.Queries.Cards;

/// <summary>
/// Query for retrieving a paged list of cards.
/// </summary>
public sealed record GetCardsQuery : PagedQueryModel, IQuery<PagedDto<CardDto>>
{
    /// <summary>
    /// Gets a model for filtering.
    /// </summary>
    /// <remarks>If <c>null</c>, filtering is not applied.</remarks>
    public CardFilterModel? Filter { get; init; }

    /// <summary>
    /// Gets a model for sorting.
    /// </summary>
    /// <remarks>
    /// If <c>null</c>, sorting is applied by the time an entry was added in descending order.
    /// </remarks>
    public CardSortingModel? Sorting { get; init; }
}