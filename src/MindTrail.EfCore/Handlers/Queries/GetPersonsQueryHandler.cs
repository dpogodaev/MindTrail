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
using MindTrail.ApplicationContracts.Requests.Queries.Persons;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Entities;
using MindTrail.EfCore.Handlers.Queries.Base;
using MindTrail.EfCore.Mapping;

namespace MindTrail.EfCore.Handlers.Queries;

/// <summary>
/// Handles <see cref="GetPersonsQuery"/> requests.
/// </summary>
/// <param name="dbContext">Application database context.</param>
public class GetPersonsQueryHandler(AppDbContext dbContext)
    : BaseQueryHandler(dbContext), IQueryHandler<GetPersonsQuery, PagedDto<PersonDto>>
{
    /// <inheritdoc/>
    public async Task<PagedDto<PersonDto>> HandleAsync(
        GetPersonsQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        IQueryable<Person> entities = GetEntities<Person>().Include(x => x.BirthCountry);

        entities = ApplyFiltering(entities, query.Filter);
        entities = ApplySearch(entities, query.Search);
        entities = ApplySorting(entities, query.Sorting);

        var pagingResult = await ApplyPaging(entities, query.Pagination, cancellationToken: cancellationToken);

        return new PagedDto<PersonDto>
        {
            Total = pagingResult.Total,
            Items = await pagingResult.Query.Select(x => x.ToDto()).ToListAsync(cancellationToken),
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
    private static IQueryable<Person> ApplySearch(IQueryable<Person> query, TextSearchModel? searchModel)
    {
        if (searchModel == null)
        {
            return query;
        }

        if (searchModel.CaseSensitive)
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

    private static IQueryable<Person> ApplySorting(IQueryable<Person> query, PersonSortingModel? sortingModel)
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