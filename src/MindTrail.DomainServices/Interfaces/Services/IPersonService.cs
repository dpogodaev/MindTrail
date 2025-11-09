using System.Threading.Tasks;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Exceptions;

namespace MindTrail.DomainServices.Interfaces.Services;

/// <summary>
/// Service for managing person entities.
/// </summary>
public interface IPersonService
{
    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="personToCreate">Person to create.</param>
    /// <returns>The created person.</returns>
    /// <exception cref="PersonNameTooLongException">The person's name is too long.</exception>
    /// <exception cref="PersonDuplicateException">The person with the specified name and date of birth already exists.</exception>
    Task<Person> CreatePersonAsync(Person personToCreate);
}