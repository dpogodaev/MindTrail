using System.Threading.Tasks;
using MindTrail.Application.Abstractions.QueryServices;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.Application.Mapping;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.AppServices;
using MindTrail.ApplicationContracts.RequestModels;
using MindTrail.Common.Interfaces.Providers;
using MindTrail.Domain.Entities;
using MindTrail.Domain.ValueObjects;
using MindTrail.DomainShared.Exceptions;

namespace MindTrail.Application.AppServices;

/// <inheritdoc/>
public class PersonAppService(
    ICurrentTimeProvider currentTimeProvider,
    IUnitOfWork unitOfWork,
    ICountryRepository countryRepository,
    IPersonRepository personRepository,
    IPersonQueryService personQueryService)
    : IPersonAppService
{
    /// <inheritdoc cref="IPersonAppService.GetPersonsAsync"/>
    public async Task<PagedDto<PersonDto>> GetPersonsAsync(PersonQueryModel filter)
    {
        return await personQueryService.GetPersonsAsync(filter);
    }

    /// <inheritdoc cref="IPersonAppService.CreatePersonAsync"/>
    public async Task<PersonDto> CreatePersonAsync(PersonCreationModel model)
    {
        var personToCreate = new Person
        {
            FullName = new PersonFullName(model.FullName),
            BirthCountryId = model.BirthCountryId,
            BirthYear = model.BirthYear != null
                ? new BirthYear(model.BirthYear.Value, currentTimeProvider.GetCurrentTime())
                : null,
        };

        await ValidatePersonDuplicatesAndThrowAsync(personToCreate);
        await ValidateCountryExistsAndThrowAsync(personToCreate);

        var createdPerson = await personRepository.CreatePersonAsync(personToCreate);
        await unitOfWork.SaveChangesAsync();

        return createdPerson.ToDto();
    }

    private async Task ValidateCountryExistsAndThrowAsync(Person personToCreate)
    {
        if (personToCreate.BirthCountryId == null)
        {
            return;
        }

        if (!await countryRepository.ExistsByIdAsync(personToCreate.BirthCountryId.Value))
        {
            throw new CountryNotFoundException(personToCreate.BirthCountryId.Value);
        }
    }

    private async Task ValidatePersonDuplicatesAndThrowAsync(Person person)
    {
        var birthYear = person.BirthYear ?? (int?)null;
        var duplicatePerson = await personRepository.GetPersonByNameAndBirthAsync(person.FullName, birthYear);

        if (duplicatePerson == null)
        {
            return;
        }

        throw new PersonDuplicateException(duplicatePerson.Id, person.FullName, birthYear);
    }
}