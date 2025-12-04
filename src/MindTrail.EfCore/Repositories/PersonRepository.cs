using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Exceptions;
using MindTrail.DomainServices.Filters;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Interfaces.Repositories;
using MindTrail.EfCore.Repositories.Base;
using Person = MindTrail.EfCore.Entities.Person;

namespace MindTrail.EfCore.Repositories;

/// <summary>
/// <inheritdoc cref="IPersonRepository"/>
/// </summary>
/// <param name="dbContext">Application database context.</param>
public class PersonRepository(AppDbContext dbContext)
    : BaseRepository(dbContext), IPersonRepository
{
    public async Task<Person> GetPersonByIdAsync(Guid id)
    {
        return await DbContext.Persons
            .Include(x => x.BirthCountry)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new PersonNotFoundException(id);
    }

    public async Task<Person> GetPersonByIdAsReadOnlyAsync(Guid id)
    {
        return await DbContext.Persons
            .AsNoTracking()
            .Include(x => x.BirthCountry)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new PersonNotFoundException(id);
    }

    public async Task<PagedResult<Person>> GetPersonsAsync(PersonFilter filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        IQueryable<Person> query = DbContext.Persons;

        return await GetAllPersonsImpl(filter, query);
    }

    public async Task<PagedResult<Person>> GetPersonsAsReadOnlyAsync(PersonFilter filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        var query = DbContext.Persons.AsNoTracking();

        return await GetAllPersonsImpl(filter, query);
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        ArgumentNullException.ThrowIfNull(person);

        return await CreateEntityAsync(person);
    }

    public async Task<Person> UpdatePersonAsync(Person person)
    {
        ArgumentNullException.ThrowIfNull(person);

        var dbPerson = await DbContext.Persons
            .Include(x => x.BirthCountry)
            .FirstOrDefaultAsync(x => x.Id == person.Id) ?? throw new PersonNotFoundException(person.Id);

        UpdateProperties(dbPerson, person);
        await UpdateEntity(dbPerson);

        return dbPerson;
    }

    public async Task<Person> DeletePersonAsync(Guid id)
    {
        var dbPerson = await DbContext.Persons
            .Include(x => x.BirthCountry)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new PersonNotFoundException(id);

        await DeleteEntity(dbPerson);

        return dbPerson;
    }

    private static async Task<PagedResult<Person>> GetAllPersonsImpl(PersonFilter filter, IQueryable<Person> query)
    {
        if (!string.IsNullOrWhiteSpace(filter.FullName))
        {
            query = query.Where(p => p.FullName.Contains(filter.FullName));
        }

        if (filter.BirthYear.HasValue)
        {
            query = query.Where(p => p.BirthYear == filter.BirthYear.Value);
        }

        return await GetPagedResult(query, filter.PageNumber, filter.PageSize);
    }

    private static void UpdateProperties(Person source, Person target)
    {
        target.FullName = source.FullName;
        target.BirthYear = source.BirthYear;
        target.BirthCountryId = source.BirthCountryId;
        target.BirthCountry = source.BirthCountry;
    }
}