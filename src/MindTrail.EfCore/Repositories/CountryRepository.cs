using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Entities;
using MindTrail.EfCore.Filters;
using MindTrail.EfCore.Interfaces.Repositories;
using MindTrail.EfCore.Repositories.Base;

namespace MindTrail.EfCore.Repositories;

public class CountryRepository(AppDbContext dbContext)
    : BaseRepository(dbContext), ICountryRepository
{
    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await GetEntities<Country>()
            .AnyAsync(x => x.Id == id);
    }

    public async Task<PagedEntity<Country>> GetCountriesAsync(CountryFilter filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        var query = GetEntities<Country>();

        query = ApplySearch(query, filter.Search);
        query = ApplySorting(query, filter.Sorting);
        return await ApplyPaging(query, filter.PageNumber, filter.PageSize);
    }

    private static IQueryable<Country> ApplySearch(
        IQueryable<Country> query,
        string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return query;
        }

        return query.Where(x =>
            x.Name.Contains(search) ||
            x.Code.Contains(search));
    }

    private IQueryable<Country> ApplySorting(
        IQueryable<Country> query,
        string? sorting)
    {
        var (propName, isDescending) = GetSortingOptions(sorting);

        if (propName != null)
        {
            if (propName.Equals(nameof(Country.Name), StringComparison.InvariantCultureIgnoreCase))
            {
                return isDescending
                    ? query.OrderByDescending(x => x.Name)
                    : query.OrderBy(x => x.Name);
            }

            if (propName.Equals(nameof(Country.Code), StringComparison.InvariantCultureIgnoreCase))
            {
                return isDescending
                    ? query.OrderByDescending(x => x.Code)
                    : query.OrderBy(x => x.Code);
            }
        }

        return query.OrderBy(x => x.Id);
    }
}