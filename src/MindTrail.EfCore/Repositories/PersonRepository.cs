using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.DomainShared.Exceptions.Persons;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Repositories.Base;
using DomainEntities = MindTrail.Domain.Entities;
using EfEntities = MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Repositories;

/// <inheritdoc/>
/// <param name="dbContext">The application database context.</param>
public class PersonRepository(AppDbContext dbContext)
    : BaseRepository(dbContext), IPersonRepository
{
    /// <inheritdoc/>
    public async Task<DomainEntities.Person> GetRequiredPersonByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var person = await GetEntities<EfEntities.Person>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return person != null
            ? MapToDomainEntity(person)
            : throw new PersonNotFoundException(id);
    }

    /// <inheritdoc/>
    public async Task<DomainEntities.Person?> GetPersonByNameAndBirthAsync(
        string fullName,
        int? birthYear,
        CancellationToken cancellationToken = default)
    {
        var filteredPersons = GetEntities<EfEntities.Person>()
            .Where(x => x.FullName.ToLower() == fullName.ToLower());

        if (birthYear != null)
        {
            filteredPersons = filteredPersons.Where(x => x.BirthYear == birthYear);
        }

        var person = await filteredPersons.FirstOrDefaultAsync(cancellationToken);

        return person != null
            ? MapToDomainEntity(person)
            : null;
    }

    /// <inheritdoc/>
    public async Task<Guid> CreatePersonAsync(
        DomainEntities.Person personToCreate,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(personToCreate);

        var createdPerson = await CreateEntityAsync(
            MapToEfEntity(personToCreate),
            cancellationToken);

        return createdPerson.Id;
    }

    /// <inheritdoc/>
    public async Task<DomainEntities.Person> UpdatePersonAsync(
        DomainEntities.Person personToUpdate,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(personToUpdate);

        var existingPerson = await GetEntities<EfEntities.Person>()
            .FirstOrDefaultAsync(x => x.Id == personToUpdate.Id, cancellationToken);

        if (existingPerson == null)
        {
            throw new PersonNotFoundException(personToUpdate.Id);
        }

        UpdateProperties(existingPerson, MapToEfEntity(personToUpdate));
        await UpdateEntity(existingPerson);

        return MapToDomainEntity(existingPerson);
    }

    /// <inheritdoc/>
    public async Task<DomainEntities.Person> DeletePersonAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var existingPersonToDelete = await GetEntities<EfEntities.Person>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (existingPersonToDelete == null)
        {
            throw new PersonNotFoundException(id);
        }

        await DeleteEntity(existingPersonToDelete);

        return MapToDomainEntity(existingPersonToDelete);
    }

    private static void UpdateProperties(
        EfEntities.Person existingPerson,
        EfEntities.Person newPerson)
    {
        existingPerson.FullName = newPerson.FullName;
        existingPerson.BirthYear = newPerson.BirthYear;
        existingPerson.BirthCountryId = newPerson.BirthCountryId;
    }

    private static DomainEntities.Person MapToDomainEntity(EfEntities.Person efEntity)
    {
        return DomainEntities.Person.FromPersistence(
            efEntity.Id,
            efEntity.FullName,
            efEntity.BirthYear,
            efEntity.BirthCountryId);
    }

    private static EfEntities.Person MapToEfEntity(DomainEntities.Person domainEntity)
    {
        return new EfEntities.Person
        {
            Id = domainEntity.Id,
            FullName = domainEntity.FullName,
            BirthYear = domainEntity.BirthYear,
            BirthCountryId = domainEntity.BirthCountryId,
        };
    }
}