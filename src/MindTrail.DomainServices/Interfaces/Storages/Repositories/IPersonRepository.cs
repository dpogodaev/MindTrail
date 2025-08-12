using System;
using System.Threading.Tasks;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Filters;

namespace MindTrail.DomainServices.Interfaces.Storages.Repositories;

public interface IPersonRepository
{
    Task<Person> GetPersonByIdAsync(Guid id);

    Task<PagedResult<Person>> GetPersonsAsync(PersonFilter filter);

    Task<PagedResult<Person>> GetPersonsAsReadOnlyAsync(PersonFilter filter);

    Task<Person> CreatePersonAsync(Person entityToCreate);

    Task<Person> UpdatePersonAsync(Person entityToUpdate);

    Task<Person> DeletePersonAsync(Guid id);
}