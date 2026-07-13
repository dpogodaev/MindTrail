using System;
using System.Threading;
using System.Threading.Tasks;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.ApplicationContracts.Requests.Commands;
using MindTrail.Common.Interfaces.Providers;
using MindTrail.Domain.Entities;
using MindTrail.Domain.ValueObjects;
using MindTrail.DomainShared.Exceptions;

namespace MindTrail.Application.Handlers;

/// <inheritdoc cref="ICommandHandler{PersonCreationCommand,PersonDto}"/>
/// <param name="currentTimeProvider">Provides the current time.</param>
/// <param name="unitOfWork">Coordinates persisting changes made during command handling.</param>
/// <param name="countryRepository">Provides access to country data, used to validate the birth country.</param>
/// <param name="personRepository">Provides access to person data, used to check for duplicates and persist the new person.</param>
public class CreatePersonCommandHandler(
    ICurrentTimeProvider currentTimeProvider,
    IUnitOfWork unitOfWork,
    ICountryRepository countryRepository,
    IPersonRepository personRepository)
    : ICommandHandler<CreatePersonCommand, Guid>
{
    /// <inheritdoc cref="ICommandHandler{PersonCreationCommand,PersonDto}.HandleAsync"/>
    /// <exception cref="PersonNameTooLongException">The person's name is too long.</exception>
    /// <exception cref="PersonDuplicateException">A person with the same name and date of birth already exists.</exception>
    /// <exception cref="CountryNotFoundException">The specified birth country does not exist.</exception>
    public async Task<Guid> HandleAsync(
        CreatePersonCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var personToCreate = new Person
        {
            FullName = new PersonFullName(command.FullName),
            BirthCountryId = command.BirthCountryId,
            BirthYear = command.BirthYear != null
                ? new BirthYear(command.BirthYear.Value, currentTimeProvider.GetCurrentTime())
                : null,
        };

        await ValidatePersonDuplicatesAndThrowAsync(personToCreate);
        await ValidateCountryExistsAndThrowAsync(personToCreate);

        var createdPersonId = await personRepository.CreatePersonAsync(personToCreate);
        await unitOfWork.SaveChangesAsync();

        return createdPersonId;
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