using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.Queries;
using MindTrail.ApplicationContracts.Requests.Queries.Persons;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Entities;
using MindTrail.EfCore.Handlers.Queries.Base;
using MindTrail.EfCore.Mapping;

namespace MindTrail.EfCore.Handlers.Queries;

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

        var entity = await GetEntities<Person>()
            .Include(x => x.BirthCountry)
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        return entity?.ToDto();
    }
}