using System;
using System.Threading;
using System.Threading.Tasks;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.Application.Helpers;
using MindTrail.ApplicationContracts.Commands.Persons;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.Common.Interfaces.Providers;
using MindTrail.Domain.Entities;
using MindTrail.Domain.ValueObjects;
using MindTrail.DomainShared.Exceptions;

namespace MindTrail.Application.Handlers.Persons;

/// <inheritdoc/>
/// <param name="currentTimeProvider">The provider of the current time.</param>
/// <param name="unitOfWork">The unit of work used to persist changes made during command handling.</param>
/// <param name="countryRepository">The repository providing access to country data, used to validate the birth country.</param>
/// <param name="personRepository">The repository providing access to person data, used to check for duplicates and create a person.</param>
public class CreatePersonCommandHandler(
    ICurrentTimeProvider currentTimeProvider,
    IUnitOfWork unitOfWork,
    ICountryRepository countryRepository,
    IPersonRepository personRepository)
    : ICommandHandler<CreatePersonCommand, Guid>
{
    /// <inheritdoc/>
    /// <exception cref="PersonNameTooLongException">The person's name is too long.</exception>
    /// <exception cref="PersonDuplicateException">The person with the same name and year of birth already exists.</exception>
    /// <exception cref="CountryNotFoundException">The specified birth country does not exist.</exception>
    public async Task<Guid> HandleAsync(
        CreatePersonCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var personToCreate = Person.Create(
            fullName: PersonFullName.Create(command.FullName),
            birthYear: BirthYear.Create(command.BirthYear, currentTimeProvider.GetCurrentTime()),
            birthCountryId: command.BirthCountryId);

        await PersonValidationHelper.ValidateCountryExistsAndThrowAsync(
            personToCreate, countryRepository, cancellationToken);

        await PersonValidationHelper.ValidatePersonDuplicatesAndThrowAsync(
            personToCreate, personRepository, cancellationToken);

        var createdPersonId = await personRepository.CreatePersonAsync(personToCreate, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return createdPersonId;
    }
}