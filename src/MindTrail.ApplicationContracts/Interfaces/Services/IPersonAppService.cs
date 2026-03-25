using System.Threading.Tasks;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.RequestModels;
using MindTrail.DomainShared.Exceptions;

namespace MindTrail.ApplicationContracts.Interfaces.Services;

/// <summary>
/// Service for managing person entities.
/// </summary>
public interface IPersonAppService
{
    Task<PagedDto<PersonDto>> GetPersonsAsync(PersonFilterModel filter);

    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="model">Model to create a person.</param>
    /// <returns>The created person.</returns>
    /// <exception cref="PersonNameTooLongException">The person's name is too long.</exception>
    /// <exception cref="PersonDuplicateException">The person with the specified name and date of birth already exists.</exception>
    Task<PersonDto> CreatePersonAsync(PersonCreationModel model);
}