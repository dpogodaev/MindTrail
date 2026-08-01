using System;
using MindTrail.ApplicationContracts.Models;
using MindTrail.ApplicationContracts.Models.Cards;
using MindTrail.ApplicationContracts.Queries.Cards;
using MindTrail.WebApi.Models.Cards;

namespace MindTrail.WebApi.Builders;

/// <summary>
/// Builds query objects for card operations from web API models.
/// </summary>
public static class CardQueryBuilder
{
    /// <summary>
    /// Builds a <see cref="GetCardByNumberQuery"/> for the specified number.
    /// </summary>
    /// <param name="number">The number of the card to retrieve.</param>
    /// <returns>The <see cref="GetCardByNumberQuery"/> to send.</returns>
    public static GetCardByNumberQuery BuildGetCardByNumberQuery(int number)
    {
        return new GetCardByNumberQuery(number);
    }

    /// <summary>
    /// Builds a <see cref="GetCardsQuery"/> from the specified model.
    /// </summary>
    /// <param name="model">The model to query a list of cards.</param>
    /// <returns>The <see cref="GetCardsQuery"/> to send.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> is <c>null</c>.</exception>
    public static GetCardsQuery BuildGetCardsQuery(CardQueryModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var paginationModel = new PaginationModel(model.PageNumber, model.PageSize);

        var filterModel = string.IsNullOrEmpty(model.Title) && model.Content == null
            ? null
            : new CardFilterModel
            {
                Number = model.CardNumber,
                Title = model.Title,
                Content = model.Content,
            };

        var searchModel = string.IsNullOrEmpty(model.TextSearchQuery)
            ? null
            : new TextSearchModel(model.TextSearchQuery, model.TextSearchCaseSensitive);

        var sortingModel = model.SortField == null
            ? null
            : new CardSortingModel(model.SortField.Value, model.SortDirection);

        return new GetCardsQuery
        {
            Pagination = paginationModel,
            Filter = filterModel,
            Search = searchModel,
            Sorting = sortingModel,
        };
    }
}