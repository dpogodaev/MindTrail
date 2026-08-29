using System;
using System.Linq.Expressions;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Handlers.Mapping;

/// <summary>
/// Maps <see cref="Card"/> entities to <see cref="CardDto"/> objects.
/// </summary>
internal static class CardMapping
{
    /// <summary>
    /// Returns an expression that maps a <see cref="Card"/> entity to a <see cref="CardDto"/>.
    /// </summary>
    /// <returns>The mapping expression.</returns>
    public static Expression<Func<Card, CardDto>> ToDto()
    {
        return efEntity => new CardDto
        {
            Number = efEntity.Id,
            Title = efEntity.Title,
            Content = efEntity.Content,
            CreatedAt = efEntity.CreatedAt,
            EditedAt = efEntity.EditedAt,
        };
    }
}