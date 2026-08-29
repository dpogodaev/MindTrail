using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Enums;
using MindTrail.ApplicationContracts.Interfaces.Queries;
using MindTrail.ApplicationContracts.Models;
using MindTrail.ApplicationContracts.Models.Cards;
using MindTrail.ApplicationContracts.Queries.Cards;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Entities;
using MindTrail.EfCore.Handlers.Base;
using MindTrail.EfCore.Handlers.Mapping;

namespace MindTrail.EfCore.Handlers;

/// <summary>
/// Handles <see cref="GetCardsQuery"/> requests.
/// </summary>
/// <param name="dbContext">The application database context.</param>
public class GetCardsQueryHandler(AppDbContext dbContext)
    : BaseQueryHandler(dbContext), IQueryHandler<GetCardsQuery, PagedDto<CardDto>>
{
    /// <inheritdoc/>
    public async Task<PagedDto<CardDto>> HandleAsync(
        GetCardsQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        var cards = GetEntities<Card>();

        cards = ApplyFiltering(cards, query.Filter);
        cards = ApplySearch(cards, query.Search);
        cards = ApplySorting(cards, query.Sorting);
        var (totalCards, pagedCards) = await ApplyPaging(
            cards,
            query.Pagination,
            cancellationToken: cancellationToken);

        return new PagedDto<CardDto>
        {
            Total = totalCards,
            Items = await pagedCards
                .Select(CardMapping.ToDto())
                .ToListAsync(cancellationToken),
        };
    }

    [SuppressMessage(
        category: "Style",
        checkId: "CA1862: Prefer 'StringComparison' method overloads",
        Justification = "EF Core does not support StringComparison in SQL")]
    private static IQueryable<Card> ApplyFiltering(IQueryable<Card> cards, CardFilterModel? filterModel)
    {
        if (filterModel == null)
        {
            return cards;
        }

        if (filterModel.Number != null)
        {
            cards = cards.Where(x => x.Id == filterModel.Number.Value);
        }

        if (!string.IsNullOrWhiteSpace(filterModel.Title))
        {
            cards = cards.Where(x => x.Title.ToLower().Contains(filterModel.Title.ToLowerInvariant()));
        }

        if (!string.IsNullOrWhiteSpace(filterModel.Content))
        {
            cards = cards.Where(x =>
                x.Content != null &&
                x.Content.ToLower().Contains(filterModel.Content.ToLowerInvariant()));
        }

        return cards;
    }

    [SuppressMessage(
        category: "Style",
        checkId: "CA1862: Prefer 'StringComparison' method overloads",
        Justification = "EF Core does not support StringComparison in SQL")]
    private static IQueryable<Card> ApplySearch(IQueryable<Card> cards, TextSearchModel? searchModel)
    {
        if (searchModel == null)
        {
            return cards;
        }

        if (searchModel.CaseSensitive)
        {
            return cards.Where(x =>
                x.Title.Contains(searchModel.Query) ||
                (x.Content != null && x.Content.Contains(searchModel.Query)));
        }

        return cards.Where(x =>
            x.Title.ToLower().Contains(searchModel.Query.ToLowerInvariant()) ||
            (x.Content != null && x.Content.ToLower().Contains(searchModel.Query.ToLowerInvariant())));
    }

    private static IQueryable<Card> ApplySorting(IQueryable<Card> cards, CardSortingModel? sortingModel)
    {
        if (sortingModel == null)
        {
            return cards.OrderByDescending(x => x.CreationTime);
        }

        return sortingModel.Field switch
        {
            CardSortingFieldType.Number =>
                sortingModel.Direction == SortDirectionType.Desc
                    ? cards.OrderByDescending(x => x.Id)
                    : cards.OrderBy(x => x.Id),
            CardSortingFieldType.Title =>
                sortingModel.Direction == SortDirectionType.Desc
                    ? cards.OrderByDescending(x => x.Title)
                    : cards.OrderBy(x => x.Title),
            _ => cards.OrderByDescending(x => x.CreationTime),
        };
    }
}