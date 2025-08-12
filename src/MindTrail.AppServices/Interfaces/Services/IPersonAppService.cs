using System.Threading.Tasks;
using MindTrail.AppServices.Exceptions;
using MindTrail.AppServices.Models;
using MindTrail.DomainEntities.Entities;

namespace MindTrail.AppServices.Interfaces.Services;

public interface IPersonAppService
{
    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="model">Model to create a person.</param>
    /// <returns>The created person.</returns>
    /// <exception cref="InvalidValueException">The property of model has an invalid value.</exception>
    /// <exception cref="InvalidStateException">The value of the model property conflicts with the current state of the application service.</exception>
    Task<Person> CreatePersonAsync(PersonCreationModel model);
}