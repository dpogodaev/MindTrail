using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Entities;
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
    public async Task<Person?> GetPersonByIdAsync(
        Guid id,
        bool includeCountry = false,
        CancellationToken cancellationToken = default)
    {
        var query = GetEntities<Person>();

        if (includeCountry)
        {
            query = query.Include(p => p.BirthCountry);
        }

        return await query.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Person?> GetPersonByNameAndBirthAsync(
        string fullName,
        int? birthYear,
        bool includeCountry = false,
        CancellationToken cancellationToken = default)
    {
        var query = GetEntities<Person>();

        if (includeCountry)
        {
            query = query.Include(p => p.BirthCountry);
        }

        query = query.Where(x => x.FullName.ToLower() == fullName.ToLower());

        if (birthYear != null)
        {
            query = query.Where(x => x.BirthYear == birthYear);
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Guid> CreatePersonAsync(Person person, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(person);

        var createdPerson = await CreateEntityAsync(person, cancellationToken);

        return createdPerson.Id;
    }

    public async Task<Person?> UpdatePersonAsync(
        Person person,
        bool includeCountry = false,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(person);

        var query = GetEntities<Person>();

        if (includeCountry)
        {
            query = query.Include(p => p.BirthCountry);
        }

        var existingPerson = await query.FirstOrDefaultAsync(x => x.Id == person.Id, cancellationToken);

        if (existingPerson == null)
        {
            return null;
        }

        var isCountryChanged = existingPerson.BirthCountryId != person.BirthCountryId;

        UpdateProperties(existingPerson, person);
        await UpdateEntity(existingPerson);

        if (includeCountry && isCountryChanged)
        {
            await DbContext.Entry(existingPerson)
                .Reference(p => p.BirthCountry)
                .LoadAsync(cancellationToken);
        }

        return existingPerson;
    }

    public async Task<Person?> DeletePersonAsync(
        Guid id,
        bool includeCountry = false,
        CancellationToken cancellationToken = default)
    {
        var query = GetEntities<Person>();

        if (includeCountry)
        {
            query = query.Include(p => p.BirthCountry);
        }

        var personToDelete = await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (personToDelete == null)
        {
            return null;
        }

        await DeleteEntity(personToDelete);

        return personToDelete;
    }

    private static void UpdateProperties(Person existingPerson, Person newPerson)
    {
        existingPerson.FullName = newPerson.FullName;
        existingPerson.BirthYear = newPerson.BirthYear;
        existingPerson.BirthCountryId = newPerson.BirthCountryId;
    }
}