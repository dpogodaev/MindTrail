using System;
using System.Threading.Tasks;
using MindTrail.DomainShared.Exceptions;
using MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Interfaces.Repositories;

/// <summary>
/// Database repository for <see cref="EfCore.Entities.Person"/> entities.
/// </summary>
public interface IPersonRepository
{
    Task<Person?> GetPersonByIdAsync(Guid id, bool includeCountry = false);

    Task<Person?> GetPersonByNameAndBirthAsync(string fullName, int? birthYear, bool includeCountry = false);

    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="person">The person to create.</param>
    /// <returns>The created person.</returns>
    Task<Person> CreatePersonAsync(Person person);

    /// <summary>
    /// Updates an existing person.
    /// </summary>
    /// <param name="person">The person to update.</param>
    /// <param name="includeCountry">If true, includes the <see cref="Country"/> navigation property.</param>
    /// <returns>The updated person.</returns>
    /// <exception cref="PersonNotFoundException">Thrown when the person was not found.</exception>
    Task<Person?> UpdatePersonAsync(Person person, bool includeCountry = false);

    /// <summary>
    /// Deletes an existing person.
    /// </summary>
    /// <param name="id">ID of the person to delete.</param>
    /// <param name="includeCountry">If true, includes the <see cref="Country"/> navigation property.</param>
    /// <returns>The deleted person.</returns>
    /// <exception cref="PersonNotFoundException">Thrown when the person was not found.</exception>
    Task<Person?> DeletePersonAsync(Guid id, bool includeCountry = false);
}