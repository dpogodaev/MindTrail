using System;
using System.Threading;
using System.Threading.Tasks;
using MindTrail.Domain.ValueObjects;
using MindTrail.DomainShared.Exceptions;
using DomainEntities = MindTrail.Domain.Entities;
using DomainRepositories = MindTrail.Application.Abstractions.Repositories;
using EfEntities = MindTrail.EfCore.Entities;
using EfRepositories = MindTrail.EfCore.Interfaces.Repositories;

namespace MindTrail.ApplicationConfigurator.Abstractions.Adapters.Repositories;

public class PersonRepositoryAdapter(
    EfRepositories.IPersonRepository repository)
    : DomainRepositories.IPersonRepository
{
    public async Task<DomainEntities.Person?> GetPersonByNameAndBirthAsync(
        string fullName,
        int? birthYear,
        CancellationToken cancellationToken = default)
    {
        var person = await repository.GetPersonByNameAndBirthAsync(
            fullName,
            birthYear,
            cancellationToken: cancellationToken);

        return person != null ? ToDomainEntity(person) : null;
    }

    public async Task<DomainEntities.Person> GetRequiredPersonByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return ToDomainEntity(
            await repository.GetPersonByIdAsync(id, cancellationToken: cancellationToken) ??
            throw new PersonNotFoundException(id));
    }

    public async Task<Guid> CreatePersonAsync(
        DomainEntities.Person entityToCreate,
        CancellationToken cancellationToken = default)
    {
        return await repository.CreatePersonAsync(
            ToEfEntity(entityToCreate),
            cancellationToken);
    }

    public async Task<DomainEntities.Person> UpdatePersonAsync(
        DomainEntities.Person entityToUpdate,
        CancellationToken cancellationToken = default)
    {
        return ToDomainEntity(
            await repository.UpdatePersonAsync(ToEfEntity(entityToUpdate), cancellationToken: cancellationToken) ??
            throw new PersonNotFoundException(entityToUpdate.Id));
    }

    public async Task<DomainEntities.Person> DeletePersonAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return ToDomainEntity(
            await repository.DeletePersonAsync(id, cancellationToken: cancellationToken) ??
            throw new PersonNotFoundException(id));
    }

    private static EfEntities.Person ToEfEntity(DomainEntities.Person domainEntity)
    {
        return new EfEntities.Person
        {
            Id = domainEntity.Id,
            FullName = domainEntity.FullName,
            BirthYear = domainEntity.BirthYear,
            BirthCountryId = domainEntity.BirthCountryId,
        };
    }

    private static DomainEntities.Person ToDomainEntity(EfEntities.Person efEntity)
    {
        return new DomainEntities.Person(
            efEntity.Id,
            PersonFullName.FromPersistence(efEntity.FullName),
            BirthYear.FromPersistence(efEntity.BirthYear),
            efEntity.BirthCountryId);
    }
}