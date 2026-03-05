using System.Threading.Tasks;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.Application.Mapping;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.Services;
using MindTrail.ApplicationContracts.RequestModels;
using MindTrail.Common.Interfaces.Providers;
using MindTrail.Domain.Abstractions.Repositories;
using MindTrail.Domain.Interfaces.Services;
using MindTrail.Domain.ValueObjects;

namespace MindTrail.Application.Services;

/// <inheritdoc/>
public class PersonAppService(
    ICurrentTimeProvider currentTimeProvider,
    IUnitOfWork unitOfWork,
    IPersonRepository personRepository,
    IPersonService personService)
    : IPersonAppService
{
    /// <inheritdoc cref="IPersonAppService.CreatePersonAsync"/>
    public async Task<PersonDto> CreatePersonAsync(PersonCreationModel model)
    {
        var personToCreate = await personService.CreatePersonAsync(
            new PersonFullName(model.FullName),
            GetValidBirthYear(model),
            model.BirthCountryId);

        var createdPerson = await personRepository.CreatePersonAsync(personToCreate);

        await unitOfWork.SaveChangesAsync();

        return createdPerson.ToDto();
    }

    private BirthYear? GetValidBirthYear(PersonCreationModel model)
    {
        var birthYear = model.BirthYear != null
            ? new BirthYear(model.BirthYear.Value, currentTimeProvider.GetCurrentTime())
            : null;
        return birthYear;
    }
}