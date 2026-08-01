using System;
using System.Linq.Expressions;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Handlers.Mapping;

internal static class CardMapping
{
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