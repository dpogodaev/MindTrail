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
/// <inheritdoc cref="IPersonQueryService"/>
/// </summary>
/// <param name="dbContext">Application database context.</param>
public class PersonQueryService(AppDbContext dbContext)
    : BaseQueryService(dbContext), IPersonQueryService
{
    public async Task<PagedDto<PersonDto>> GetPersonsAsync(PersonQueryModel queryModel)
    {
        ArgumentNullException.ThrowIfNull(queryModel);

        IQueryable<Person> query = GetEntities<Person>().Include(x => x.BirthCountry);

        query = ApplyFiltering(query, queryModel.Filter);
        query = ApplySearch(query, queryModel.Search);
        query = ApplySorting(query, queryModel.Sorting);
        var pagingResult = await ApplyPaging(query, queryModel.Pagination);

        return new PagedDto<PersonDto>
        {
            Total = pagingResult.Total,
            Items = await pagingResult.Query.Select(x => x.ToDto()).ToListAsync(),
        };
    }

    private static IQueryable<Person> ApplyFiltering(IQueryable<Person> query, PersonFilterModel? filterModel)
    {
        if (filterModel == null)
        {
            return query;
        }

        if (!string.IsNullOrWhiteSpace(filterModel.FullName))
        {
            query = query.Where(p => p.FullName.ToLower().Contains(filterModel.FullName.ToLower()));
        }

        if (filterModel.BirthYear.HasValue)
        {
            query = query.Where(p => p.BirthYear == filterModel.BirthYear.Value);
        }

        return query;
    }

    [SuppressMessage(
        category: "Style",
        checkId: "CA1862: Prefer 'StringComparison' method overloads",
        Justification = "EF Core does not support StringComparison in SQL")]
    private static IQueryable<Person> ApplySearch(
        IQueryable<Person> query,
        TextSearchModel? searchModel)
    {
        if (searchModel == null)
        {
            return query;
        }

        if (!searchModel.CaseSensitive)
        {
            return query.Where(x =>
                x.FullName.Contains(searchModel.Query) ||
                (x.BirthYear != null && x.BirthYear.ToString()!.Contains(searchModel.Query)) ||
                (x.BirthCountry != null && x.BirthCountry.Name.Contains(searchModel.Query)));
        }

        return query.Where(x =>
            x.FullName.ToLower().Contains(searchModel.Query.ToLower()) ||
            (x.BirthYear != null && x.BirthYear.ToString()!.Contains(searchModel.Query)) ||
            (x.BirthCountry != null && x.BirthCountry.Name.ToLower().Contains(searchModel.Query.ToLower())));
    }

    private static IQueryable<Person> ApplySorting(
        IQueryable<Person> query,
        PersonSortingModel? sortingModel)
    {
        if (sortingModel == null)
        {
            return query.OrderByDescending(x => x.CreationTime);
        }

        return sortingModel.Field switch
        {
            PersonSortingFieldType.FullName =>
                sortingModel.Direction == SortDirectionType.Desc
                    ? query.OrderByDescending(x => x.FullName)
                    : query.OrderBy(x => x.FullName),
            PersonSortingFieldType.BirthYear =>
                sortingModel.Direction == SortDirectionType.Desc
                    ? query.OrderByDescending(x => x.BirthYear)
                    : query.OrderBy(x => x.BirthYear),
            PersonSortingFieldType.CreatedAt =>
                sortingModel.Direction == SortDirectionType.Desc
                    ? query.OrderByDescending(x => x.CreationTime)
                    : query.OrderBy(x => x.CreationTime),
            _ => query.OrderByDescending(x => x.CreationTime),
        };
    }
}