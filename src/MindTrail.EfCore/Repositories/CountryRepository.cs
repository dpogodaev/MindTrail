using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Filters;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Interfaces.Repositories;
using MindTrail.EfCore.Repositories.Base;
using Country = MindTrail.EfCore.Entities.Country;

namespace MindTrail.EfCore.Repositories;

/// <summary>
/// <inheritdoc cref="ICountryRepository"/>
/// </summary>
/// <param name="dbContext">Application database context.</param>
public class CountryRepository(AppDbContext dbContext) : BaseRepository(dbContext), ICountryRepository
{
    #region ICountryRepository

    public async Task<PagedResult<Country>> GetCountriesAsReadOnlyAsync(CountryFilter filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        var query = DbContext.Countries.AsNoTracking();

        return await GetAllPersonsImpl(filter, query);
    }

    #endregion

    #region Private methods

    private static async Task<PagedResult<Country>> GetAllPersonsImpl(CountryFilter filter, IQueryable<Country> query)
    {
        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(p => p.Name.Contains(filter.Name));
        }

        return await GetPagedResult(query, filter.PageNumber, filter.PageSize);
    }

    #endregion
}