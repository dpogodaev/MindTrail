using System;
using System.Threading.Tasks;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Exceptions;
using MindTrail.DomainServices.Filters;
using Person = MindTrail.EfCore.Entities.Person;

namespace MindTrail.EfCore.Interfaces.Repositories;

/// <summary>
/// Database repository for <see cref="EfCore.Entities.Person"/> entities.
/// </summary>
public interface IPersonRepository
{
    Task<Person> GetPersonByIdAsync(Guid id);

    Task<Person> GetPersonByIdAsReadOnlyAsync(Guid id);

    Task<PagedResult<Person>> GetPersonsAsync(PersonFilter filter);

    Task<PagedResult<Person>> GetPersonsAsReadOnlyAsync(PersonFilter filter);

    Task<Person> CreatePersonAsync(Person person);

    /// <summary>
    /// Updates an existing person.
    /// </summary>
    /// <param name="person">The person to update.</param>
    /// <returns>The updated person.</returns>
    /// <exception cref="PersonNotFoundException">Thrown when the person was not found.</exception>
    Task<Person> UpdatePersonAsync(Person person);

    /// <summary>
    /// Deletes an existing person.
    /// </summary>
    /// <param name="id">ID of the person to delete.</param>
    /// <returns>The deleted person.</returns>
    /// <exception cref="PersonNotFoundException">Thrown when the person was not found.</exception>
    Task<Person> DeletePersonAsync(Guid id);
}