using System.Threading.Tasks;
using MindTrail.AppServices.Models;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Exceptions;

namespace MindTrail.AppServices.Interfaces.Services;

/// <summary>
/// Service for managing person entities.
/// </summary>
public interface IPersonAppService
{
    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="model">Model to create a person.</param>
    /// <returns>The created person.</returns>
    /// <exception cref="PersonNameTooLongException">The person's name is too long.</exception>
    /// <exception cref="PersonDuplicateException">The person with the specified name and date of birth already exists.</exception>
    Task<Person> CreatePersonAsync(PersonCreationModel model);
}