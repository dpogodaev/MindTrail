using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Enums;
using MindTrail.ApplicationContracts.RequestModels;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Entities;
using MindTrail.EfCore.Interfaces.QueryServices;
using MindTrail.EfCore.Mapping;
using MindTrail.EfCore.QueryServices.Base;

namespace MindTrail.EfCore.QueryServices;

/// <summary>
/// <inheritdoc cref="ICountryQueryService"/>
/// </summary>
/// <param name="dbContext">Application database context.</param>
public class CountryQueryService(AppDbContext dbContext)
    : BaseQueryService(dbContext), ICountryQueryService
{
    public async Task<PagedDto<CountryDto>> GetCountriesAsync(CountryQueryModel queryModel)
    {
        ArgumentNullException.ThrowIfNull(queryModel);

        var query = GetEntities<Country>();

        query = ApplyFiltering(query, queryModel.Filter);
        query = ApplySearch(query, queryModel.Search);
        query = ApplySorting(query, queryModel.Sorting);
        var pagingResult = await ApplyPaging(query, queryModel.Pagination);

        return new PagedDto<CountryDto>
        {
            Total = pagingResult.Total,
            Items = await pagingResult.Query.Select(x => x.ToDto()).ToListAsync(),
        };
    }

    private static IQueryable<Country> ApplyFiltering(IQueryable<Country> query, CountryFilterModel? filterModel)
    {
        if (filterModel == null)
        {
            return query;
        }

        if (!string.IsNullOrWhiteSpace(filterModel.Name))
        {
            query = query.Where(p => p.Name.Contains(filterModel.Name));
        }

        if (!string.IsNullOrWhiteSpace(filterModel.Code))
        {
            query = query.Where(p => p.Code.Contains(filterModel.Code));
        }

        return query;
    }

    [SuppressMessage(
        category: "Style",
        checkId: "CA1862: Prefer 'StringComparison' method overloads",
        Justification = "EF Core does not support StringComparison in SQL")]
    private static IQueryable<Country> ApplySearch(
        IQueryable<Country> query,
        TextSearchModel? searchModel)
    {
        if (searchModel == null)
        {
            return query;
        }

        if (!searchModel.CaseSensitive)
        {
            return query.Where(x =>
                x.Name.Contains(searchModel.Query) ||
                x.Code.ToString().Contains(searchModel.Query));
        }

        return query.Where(x =>
            x.Name.ToLower().Contains(searchModel.Query.ToLower()) ||
            x.Code.ToString().Contains(searchModel.Query));
    }

    private static IQueryable<Country> ApplySorting(
        IQueryable<Country> query,
        CountrySortingModel? sortingModel)
    {
        if (sortingModel == null)
        {
            return query.OrderBy(x => x.Name);
        }

        return sortingModel.Field switch
        {
            CountrySortingFieldType.Name =>
                sortingModel.Direction == SortDirectionType.Desc
                    ? query.OrderByDescending(x => x.Name)
                    : query.OrderBy(x => x.Name),
            CountrySortingFieldType.Code =>
                sortingModel.Direction == SortDirectionType.Desc
                    ? query.OrderByDescending(x => x.Code)
                    : query.OrderBy(x => x.Code),
            _ => query.OrderByDescending(x => x.Name),
        };
    }
}