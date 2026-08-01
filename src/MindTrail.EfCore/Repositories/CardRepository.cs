using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.DomainShared.Exceptions.Cards;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Repositories.Base;
using DomainEntities = MindTrail.Domain.Entities;
using EfEntities = MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Repositories;

/// <summary>
/// <inheritdoc cref="ICardRepository"/>
/// </summary>
/// <param name="dbContext">Application database context.</param>
public class CardRepository(AppDbContext dbContext)
    : BaseRepository(dbContext), ICardRepository
{
    public async Task<DomainEntities.Card?> GetCardByNumberAsync(
        int number,
        CancellationToken cancellationToken = default)
    {
        var card = await GetEntities<EfEntities.Card>()
            .FirstOrDefaultAsync(p => p.Id == number, cancellationToken);

        return card != null
            ? MapToDomainEntity(card)
            : null;
    }

    public async Task<DomainEntities.Card> GetRequiredCardByNumberAsync(
        int number,
        CancellationToken cancellationToken = default)
    {
        var card = await GetEntities<EfEntities.Card>()
            .FirstOrDefaultAsync(x => x.Id == number, cancellationToken);

        return card != null
            ? MapToDomainEntity(card)
            : throw new CardNotFoundException(number);
    }

    public async Task<int> CreateCardAsync(
        DomainEntities.Card cardToCreate,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(cardToCreate);

        var createdCard = await CreateEntityAsync(
            MapToEfEntity(cardToCreate),
            cancellationToken);

        return createdCard.Id;
    }

    public async Task<DomainEntities.Card> UpdateCardAsync(
        DomainEntities.Card cardToUpdate,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(cardToUpdate);

        var existingCard = await GetEntities<EfEntities.Card>()
            .FirstOrDefaultAsync(x => x.Id == cardToUpdate.Id, cancellationToken);

        if (existingCard == null)
        {
            throw new CardNotFoundException(cardToUpdate.Id);
        }

        UpdateProperties(existingCard, MapToEfEntity(cardToUpdate));
        await UpdateEntity(existingCard);

        return MapToDomainEntity(existingCard);
    }

    public async Task<DomainEntities.Card> DeleteCardAsync(
        int number,
        CancellationToken cancellationToken = default)
    {
        var existingCardToDelete = await GetEntities<EfEntities.Card>()
            .FirstOrDefaultAsync(x => x.Id == number, cancellationToken);

        if (existingCardToDelete == null)
        {
            throw new CardNotFoundException(number);
        }

        await DeleteEntity(existingCardToDelete);

        return MapToDomainEntity(existingCardToDelete);
    }

    private static void UpdateProperties(
        EfEntities.Card existingCard,
        EfEntities.Card newCard)
    {
        existingCard.Title = newCard.Title;
        existingCard.Content = newCard.Content;
        existingCard.EditedAt = newCard.EditedAt;
    }

    private static DomainEntities.Card MapToDomainEntity(EfEntities.Card efEntity)
    {
        return DomainEntities.Card.FromPersistence(
            efEntity.Id,
            efEntity.Title,
            efEntity.Content,
            efEntity.CreatedAt,
            efEntity.EditedAt);
    }

    private static EfEntities.Card MapToEfEntity(DomainEntities.Card domainEntity)
    {
        return new EfEntities.Card
        {
            Id = domainEntity.Id,
            Title = domainEntity.Title,
            Content = domainEntity.Content?.Value,
            CreatedAt = domainEntity.CreatedAt,
            EditedAt = domainEntity.EditedAt,
        };
    }
}