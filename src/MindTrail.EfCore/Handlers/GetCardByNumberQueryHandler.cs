using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.Queries;
using MindTrail.ApplicationContracts.Queries.Cards;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Entities;
using MindTrail.EfCore.Handlers.Base;
using MindTrail.EfCore.Handlers.Mapping;

namespace MindTrail.EfCore.Handlers;

/// <summary>
/// Handles <see cref="GetCardByNumberQuery"/> requests.
/// </summary>
/// <param name="dbContext">The application database context.</param>
public class GetCardByNumberQueryHandler(AppDbContext dbContext)
    : BaseQueryHandler(dbContext), IQueryHandler<GetCardByNumberQuery, CardDto?>
{
    /// <inheritdoc/>
    public async Task<CardDto?> HandleAsync(
        GetCardByNumberQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        return await GetEntities<Card>()
            .Where(x => x.Id == query.Number)
            .Select(CardMapping.ToDto())
            .FirstOrDefaultAsync(cancellationToken);
    }
}