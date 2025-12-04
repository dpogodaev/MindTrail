using System;
using System.Linq;
using System.Threading.Tasks;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Filters;
using MindTrail.DomainServices.Interfaces.Storages.Repositories;
using EfEntities = MindTrail.EfCore.Entities;
using EfRepositories = MindTrail.EfCore.Interfaces.Repositories;

namespace MindTrail.EfCore.Adapters.Repositories;

public class PersonRepositoryAdapter(EfRepositories.IPersonRepository repository)
    : IPersonRepository
{
    public async Task<Person> GetPersonByIdAsync(Guid id)
    {
        return MapToDomainEntity(
            await repository.GetPersonByIdAsync(id));
    }

    public async Task<Person> GetPersonByIdAsReadOnlyAsync(Guid id)
    {
        return MapToDomainEntity(
            await repository.GetPersonByIdAsync(id));
    }

    public async Task<PagedResult<Person>> GetPersonsAsync(PersonFilter filter)
    {
        return MapToDomainEntities(
            await repository.GetPersonsAsync(filter));
    }

    public async Task<PagedResult<Person>> GetPersonsAsReadOnlyAsync(PersonFilter filter)
    {
        return MapToDomainEntities(
            await repository.GetPersonsAsReadOnlyAsync(filter));
    }

    public async Task<Person> CreatePersonAsync(Person entityToCreate)
    {
        return MapToDomainEntity(
            await repository.CreatePersonAsync(
                MapFromDomainEntity(entityToCreate)));
    }

    public async Task<Person> UpdatePersonAsync(Person entityToUpdate)
    {
        return MapToDomainEntity(
            await repository.UpdatePersonAsync(
                MapFromDomainEntity(entityToUpdate)));
    }

    public async Task<Person> DeletePersonAsync(Guid id)
    {
        return MapToDomainEntity(
            await repository.DeletePersonAsync(id));
    }

    private static PagedResult<Person> MapToDomainEntities(PagedResult<EfEntities.Person> efEntities)
    {
        return new PagedResult<Person>
        {
            Items = efEntities.Items.Select(MapToDomainEntity),
            PageNumber = efEntities.PageNumber,
            PageSize = efEntities.PageSize,
            TotalCount = efEntities.TotalCount,
        };
    }

    private static Person MapToDomainEntity(EfEntities.Person efEntity)
    {
        return new Person
        {
            Id = efEntity.Id,
            FullName = efEntity.FullName,
            BirthYear = efEntity.BirthYear,
            BirthCountryId = efEntity.BirthCountryId,
            BirthCountryName = efEntity.BirthCountry?.Name,
        };
    }

    private static EfEntities.Person MapFromDomainEntity(Person domainEntity)
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