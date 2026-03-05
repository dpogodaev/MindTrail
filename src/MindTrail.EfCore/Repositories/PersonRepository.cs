using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Entities;
using MindTrail.EfCore.Filters;
using MindTrail.EfCore.Interfaces.Repositories;
using MindTrail.EfCore.Repositories.Base;

namespace MindTrail.EfCore.Repositories;

/// <summary>
/// <inheritdoc cref="IPersonRepository"/>
/// </summary>
/// <param name="dbContext">Application database context.</param>
public class PersonRepository(AppDbContext dbContext)
    : BaseRepository(dbContext), IPersonRepository
{
    public async Task<Person?> GetPersonByIdAsync(Guid id)
    {
        return await DbContext.Persons
            .Include(x => x.BirthCountry)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<Person> GetPersons(PersonFilter filter, bool includeCountry)
    {
        ArgumentNullException.ThrowIfNull(filter);

        var query = GetEntities<Person>();

        if (includeCountry)
        {
            query = query.Include(x => x.BirthCountry);
        }

        query = ApplyFiltering(query, filter);
        query = ApplySearch(query, filter.Search);
        query = ApplySorting(query, filter.Sorting);
        query = ApplyPaging(query, filter.PageNumber, filter.PageSize);

        return query;
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        ArgumentNullException.ThrowIfNull(person);

        return await CreateEntityAsync(person);
    }

    public async Task<Person?> UpdatePersonAsync(Person person)
    {
        ArgumentNullException.ThrowIfNull(person);

        var existingPerson = await DbContext.Persons
            .Include(x => x.BirthCountry)
            .FirstOrDefaultAsync(x => x.Id == person.Id);

        if (existingPerson == null)
        {
            return null;
        }

        UpdateProperties(existingPerson, person);
        await UpdateEntity(existingPerson);

        return existingPerson;
    }

    public async Task<Person?> DeletePersonAsync(Guid id)
    {
        var personToDelete = await DbContext.Persons
            .Include(x => x.BirthCountry)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (personToDelete == null)
        {
            return null;
        }

        await DeleteEntity(personToDelete);

        return personToDelete;
    }

    private static void UpdateProperties(Person source, Person target)
    {
        target.FullName = source.FullName;
        target.BirthYear = source.BirthYear;
        target.BirthCountryId = source.BirthCountryId;
        target.BirthCountry = source.BirthCountry;
    }

    private static IQueryable<Person> ApplyFiltering(IQueryable<Person> query, PersonFilter filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.FullName))
        {
            query = query.Where(p => p.FullName.Contains(filter.FullName));
        }

        if (filter.BirthYear.HasValue)
        {
            query = query.Where(p => p.BirthYear == filter.BirthYear.Value);
        }

        return query;
    }

    private static IQueryable<Person> ApplySearch(
        IQueryable<Person> query,
        string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return query;
        }

        return query.Where(x =>
            x.FullName.Contains(search));
    }

    private static IQueryable<Person> ApplySorting(
        IQueryable<Person> query,
        string? sorting)
    {
        var (propName, isDescending) = GetSortingOptions(sorting);

        if (propName != null)
        {
            if (propName.Equals(nameof(Person.FullName), StringComparison.InvariantCultureIgnoreCase))
            {
                return isDescending
                    ? query.OrderByDescending(x => x.FullName)
                    : query.OrderBy(x => x.FullName);
            }

            if (propName.Equals(nameof(Person.BirthYear), StringComparison.InvariantCultureIgnoreCase))
            {
                return isDescending
                    ? query.OrderByDescending(x => x.BirthYear)
                    : query.OrderBy(x => x.BirthYear);
            }
        }

        return query.OrderBy(x => x.Id);
    }
}