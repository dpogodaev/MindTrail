using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Enums;
using MindTrail.ApplicationContracts.Interfaces.Queries;
using MindTrail.ApplicationContracts.Requests.Queries;
using MindTrail.ApplicationContracts.Requests.Queries.Countries;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Entities;
using MindTrail.EfCore.Handlers.Queries.Base;
using MindTrail.EfCore.Mapping;

namespace MindTrail.EfCore.Handlers.Queries;

/// <summary>
/// Handles <see cref="GetCountriesQuery"/> requests.
/// </summary>
/// <param name="dbContext">Application database context.</param>
public class GetCountriesQueryHandler(AppDbContext dbContext)
    : BaseQueryHandler(dbContext), IQueryHandler<GetCountriesQuery, PagedDto<CountryDto>>
{
    /// <inheritdoc/>
    public async Task<PagedDto<CountryDto>> HandleAsync(
        GetCountriesQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        var entities = GetEntities<Country>();

        entities = ApplyFiltering(entities, query.Filter);
        entities = ApplySearch(entities, query.Search);
        entities = ApplySorting(entities, query.Sorting);
        var pagingResult = await ApplyPaging(entities, query.Pagination, cancellationToken: cancellationToken);

        return new PagedDto<CountryDto>
        {
            Total = pagingResult.Total,
            Items = await pagingResult.Query.Select(x => x.ToDto()).ToListAsync(cancellationToken),
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

        if (searchModel.CaseSensitive)
        {
            return query.Where(x =>
                x.Name.Contains(searchModel.Query) ||
                x.Code.Contains(searchModel.Query));
        }

        return query.Where(x =>
            x.Name.ToLower().Contains(searchModel.Query.ToLower()) ||
            x.Code.Contains(searchModel.Query));
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