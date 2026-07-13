using System;
using System.Threading.Tasks;
using MindTrail.Common.Interfaces.Providers;
using MindTrail.Domain.ValueObjects;
using MindTrail.DomainShared.Exceptions;
using DomainEntities = MindTrail.Domain.Entities;
using DomainRepositories = MindTrail.Application.Abstractions.Repositories;
using EfEntities = MindTrail.EfCore.Entities;
using EfRepositories = MindTrail.EfCore.Interfaces.Repositories;

namespace MindTrail.ApplicationConfigurator.Abstractions.Adapters.Repositories;

public class PersonRepositoryAdapter(
    ICurrentTimeProvider currentTimeProvider,
    EfRepositories.IPersonRepository repository)
    : DomainRepositories.IPersonRepository
{
    public async Task<DomainEntities.Person?> GetPersonByNameAndBirthAsync(string fullName, int? birthYear)
    {
        var person = await repository.GetPersonByNameAndBirthAsync(fullName, birthYear);

        return person != null ? ToDomainEntity(person) : null;
    }

    public async Task<DomainEntities.Person> GetRequiredPersonByIdAsync(Guid id)
    {
        return ToDomainEntity(
            await repository.GetPersonByIdAsync(id) ??
            throw new PersonNotFoundException(id));
    }

    public async Task<Guid> CreatePersonAsync(DomainEntities.Person entityToCreate)
    {
        return await repository.CreatePersonAsync(
            ToEfEntity(entityToCreate));
    }

    public async Task<DomainEntities.Person> UpdatePersonAsync(DomainEntities.Person entityToUpdate)
    {
        return ToDomainEntity(
            await repository.UpdatePersonAsync(ToEfEntity(entityToUpdate)) ??
            throw new PersonNotFoundException(entityToUpdate.Id));
    }

    public async Task<DomainEntities.Person> DeletePersonAsync(Guid id)
    {
        return ToDomainEntity(
            await repository.DeletePersonAsync(id) ??
            throw new PersonNotFoundException(id));
    }

    private static EfEntities.Person ToEfEntity(DomainEntities.Person domainEntity)
    {
        return new EfEntities.Person
        {
            Id = domainEntity.Id,
            FullName = domainEntity.FullName,
            BirthYear = domainEntity.BirthYear == null ? null : (int?)domainEntity.BirthYear,
            BirthCountryId = domainEntity.BirthCountryId,
        };
    }

    private DomainEntities.Person ToDomainEntity(EfEntities.Person efEntity)
    {
        var birthYear = efEntity.BirthYear != null
            ? new BirthYear(efEntity.BirthYear.Value, currentTimeProvider.GetCurrentTime())
            : null;

        return new DomainEntities.Person
        {
            Id = efEntity.Id,
            FullName = new PersonFullName(efEntity.FullName),
            BirthYear = birthYear,
            BirthCountryId = efEntity.BirthCountryId,
        };
    }
}