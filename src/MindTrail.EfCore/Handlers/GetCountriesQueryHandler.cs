using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Enums;
using MindTrail.ApplicationContracts.Interfaces.Queries;
using MindTrail.ApplicationContracts.Models;
using MindTrail.ApplicationContracts.Models.Countries;
using MindTrail.ApplicationContracts.Queries.Countries;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Entities;
using MindTrail.EfCore.Handlers.Base;
using MindTrail.EfCore.Handlers.Mapping;

namespace MindTrail.EfCore.Handlers;

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

        var countries = GetEntities<Country>();

        countries = ApplyFiltering(countries, query.Filter);
        countries = ApplySearch(countries, query.Search);
        countries = ApplySorting(countries, query.Sorting);
        var (totalCountries, pagedCountries) = await ApplyPaging(
            countries,
            query.Pagination,
            cancellationToken: cancellationToken);

        return new PagedDto<CountryDto>
        {
            Total = totalCountries,
            Items = await pagedCountries
                .Select(CountryMapping.ToDto())
                .ToListAsync(cancellationToken),
        };
    }

    private static IQueryable<Country> ApplyFiltering(IQueryable<Country> countries, CountryFilterModel? filterModel)
    {
        if (filterModel == null)
        {
            return countries;
        }

        if (!string.IsNullOrWhiteSpace(filterModel.Name))
        {
            countries = countries.Where(p => p.Name.Contains(filterModel.Name));
        }

        if (!string.IsNullOrWhiteSpace(filterModel.Code))
        {
            countries = countries.Where(p => p.Code.Contains(filterModel.Code));
        }

        return countries;
    }

    [SuppressMessage(
        category: "Style",
        checkId: "CA1862: Prefer 'StringComparison' method overloads",
        Justification = "EF Core does not support StringComparison in SQL")]
    private static IQueryable<Country> ApplySearch(
        IQueryable<Country> countries,
        TextSearchModel? searchModel)
    {
        if (searchModel == null)
        {
            return countries;
        }

        if (searchModel.CaseSensitive)
        {
            return countries.Where(x =>
                x.Name.Contains(searchModel.Query) ||
                x.Code.Contains(searchModel.Query));
        }

        return countries.Where(x =>
            x.Name.ToLower().Contains(searchModel.Query.ToLower()) ||
            x.Code.Contains(searchModel.Query));
    }

    private static IQueryable<Country> ApplySorting(
        IQueryable<Country> countries,
        CountrySortingModel? sortingModel)
    {
        if (sortingModel == null)
        {
            return countries.OrderBy(x => x.Name);
        }

        return sortingModel.Field switch
        {
            CountrySortingFieldType.Name =>
                sortingModel.Direction == SortDirectionType.Desc
                    ? countries.OrderByDescending(x => x.Name)
                    : countries.OrderBy(x => x.Name),
            CountrySortingFieldType.Code =>
                sortingModel.Direction == SortDirectionType.Desc
                    ? countries.OrderByDescending(x => x.Code)
                    : countries.OrderBy(x => x.Code),
            _ => countries.OrderByDescending(x => x.Name),
        };
    }
}