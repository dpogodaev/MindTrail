using System;
using System.Linq;
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
    public async Task<Person?> GetPersonByIdAsync(Guid id, bool includeCountry = false)
    {
        var query = GetEntities<Person>();

        if (includeCountry)
        {
            query = query.Include(p => p.BirthCountry);
        }

        return await query.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Person?> GetPersonByNameAndBirthAsync(
        string fullName, int? birthYear, bool includeCountry = false)
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

        return await query.FirstOrDefaultAsync();
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        ArgumentNullException.ThrowIfNull(person);

        return await CreateEntityAsync(person);
    }

    public async Task<Person?> UpdatePersonAsync(Person person, bool includeCountry = false)
    {
        ArgumentNullException.ThrowIfNull(person);

        var query = GetEntities<Person>();

        if (includeCountry)
        {
            query = query.Include(p => p.BirthCountry);
        }

        var existingPerson = await query.FirstOrDefaultAsync(x => x.Id == person.Id);

        if (existingPerson == null)
        {
            return null;
        }

        UpdateProperties(existingPerson, person);
        await UpdateEntity(existingPerson);

        return existingPerson;
    }

    public async Task<Person?> DeletePersonAsync(Guid id, bool includeCountry = false)
    {
        var query = GetEntities<Person>();

        if (includeCountry)
        {
            query = query.Include(p => p.BirthCountry);
        }

        var personToDelete = await query.FirstOrDefaultAsync(x => x.Id == id);

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
}