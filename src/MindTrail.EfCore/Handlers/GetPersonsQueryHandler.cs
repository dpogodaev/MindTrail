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
using MindTrail.ApplicationContracts.Models.Persons;
using MindTrail.ApplicationContracts.Queries.Persons;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Entities;
using MindTrail.EfCore.Handlers.Base;
using MindTrail.EfCore.Handlers.Mapping;

namespace MindTrail.EfCore.Handlers;

/// <summary>
/// Handles <see cref="GetPersonsQuery"/> requests.
/// </summary>
/// <param name="dbContext">The application database context.</param>
public class GetPersonsQueryHandler(AppDbContext dbContext)
    : BaseQueryHandler(dbContext), IQueryHandler<GetPersonsQuery, PagedDto<PersonDto>>
{
    /// <inheritdoc/>
    public async Task<PagedDto<PersonDto>> HandleAsync(
        GetPersonsQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);

        IQueryable<Person> persons = GetEntities<Person>().Include(x => x.BirthCountry);

        persons = ApplyFiltering(persons, query.Filter);
        persons = ApplySearch(persons, query.Search);
        persons = ApplySorting(persons, query.Sorting);
        var (totalPersons, pagedPersons) = await ApplyPaging(
            persons,
            query.Pagination,
            cancellationToken: cancellationToken);

        return new PagedDto<PersonDto>
        {
            Total = totalPersons,
            Items = await pagedPersons
                .Select(PersonMapping.ToDto())
                .ToListAsync(cancellationToken),
        };
    }

    [SuppressMessage(
        category: "Style",
        checkId: "CA1862: Prefer 'StringComparison' method overloads",
        Justification = "EF Core does not support StringComparison in SQL")]
    private static IQueryable<Person> ApplyFiltering(IQueryable<Person> persons, PersonFilterModel? filterModel)
    {
        if (filterModel == null)
        {
            return persons;
        }

        if (!string.IsNullOrWhiteSpace(filterModel.FullName))
        {
            persons = persons.Where(x => x.FullName.ToLower().Contains(filterModel.FullName.ToLower()));
        }

        if (filterModel.BirthYear.HasValue)
        {
            persons = persons.Where(x => x.BirthYear == filterModel.BirthYear.Value);
        }

        return persons;
    }

    [SuppressMessage(
        category: "Style",
        checkId: "CA1862: Prefer 'StringComparison' method overloads",
        Justification = "EF Core does not support StringComparison in SQL")]
    private static IQueryable<Person> ApplySearch(IQueryable<Person> persons, TextSearchModel? searchModel)
    {
        if (searchModel == null)
        {
            return persons;
        }

        if (searchModel.CaseSensitive)
        {
            return persons.Where(x =>
                x.FullName.Contains(searchModel.Query) ||
                (x.BirthYear != null && x.BirthYear.ToString()!.Contains(searchModel.Query)) ||
                (x.BirthCountry != null && x.BirthCountry.Name.Contains(searchModel.Query)));
        }

        return persons.Where(x =>
            x.FullName.ToLower().Contains(searchModel.Query.ToLower()) ||
            (x.BirthYear != null && x.BirthYear.ToString()!.Contains(searchModel.Query)) ||
            (x.BirthCountry != null && x.BirthCountry.Name.ToLower().Contains(searchModel.Query.ToLower())));
    }

    private static IQueryable<Person> ApplySorting(IQueryable<Person> persons, PersonSortingModel? sortingModel)
    {
        if (sortingModel == null)
        {
            return persons.OrderByDescending(x => x.CreationTime);
        }

        return sortingModel.Field switch
        {
            PersonSortingFieldType.FullName =>
                sortingModel.Direction == SortDirectionType.Desc
                    ? persons.OrderByDescending(x => x.FullName)
                    : persons.OrderBy(x => x.FullName),
            PersonSortingFieldType.BirthYear =>
                sortingModel.Direction == SortDirectionType.Desc
                    ? persons.OrderByDescending(x => x.BirthYear)
                    : persons.OrderBy(x => x.BirthYear),
            PersonSortingFieldType.CreatedAt =>
                sortingModel.Direction == SortDirectionType.Desc
                    ? persons.OrderByDescending(x => x.CreationTime)
                    : persons.OrderBy(x => x.CreationTime),
            _ => persons.OrderByDescending(x => x.CreationTime),
        };
    }
}