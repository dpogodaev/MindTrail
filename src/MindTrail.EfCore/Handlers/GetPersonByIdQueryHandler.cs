using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.Queries;
using MindTrail.ApplicationContracts.Queries.Persons;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Entities;
using MindTrail.EfCore.Handlers.Base;
using MindTrail.EfCore.Handlers.Mapping;

namespace MindTrail.EfCore.Handlers;

/// <summary>
/// Handles <see cref="GetPersonByIdQuery"/> requests.
/// </summary>
/// <param name="dbContext">Application database context.</param>
public class GetPersonByIdQueryHandler(AppDbContext dbContext)
    : BaseQueryHandler(dbContext), IQueryHandler<GetPersonByIdQuery, PersonDto?>
{
    /// <inheritdoc/>
    public async Task<PersonDto?> HandleAsync(
        GetPersonByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        return await GetEntities<Person>()
            .Where(x => x.Id == query.Id)
            .Select(PersonMapping.ToDto())
            .FirstOrDefaultAsync(cancellationToken);
    }
}