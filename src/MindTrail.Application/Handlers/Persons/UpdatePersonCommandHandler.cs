using System.Threading;
using System.Threading.Tasks;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.Application.Helpers;
using MindTrail.ApplicationContracts.Commands.Common;
using MindTrail.ApplicationContracts.Commands.Persons;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.Common.Interfaces.Providers;
using MindTrail.Domain.ValueObjects;
using MindTrail.DomainShared.Exceptions;
using MindTrail.DomainShared.Exceptions.Persons;

namespace MindTrail.Application.Handlers.Persons;

/// <inheritdoc cref="ICommandHandler{UpdatePersonCommandHandler,VoidResult}"/>
/// <param name="currentTimeProvider">Provides the current time.</param>
/// <param name="unitOfWork">Coordinates persisting changes made during command handling.</param>
/// <param name="countryRepository">Provides access to country data, used to validate the birth country.</param>
/// <param name="personRepository">Provides access to person data, used to validate duplicates and update it.</param>
public class UpdatePersonCommandHandler(
    ICurrentTimeProvider currentTimeProvider,
    IUnitOfWork unitOfWork,
    ICountryRepository countryRepository,
    IPersonRepository personRepository)
    : ICommandHandler<UpdatePersonCommand, VoidResult>
{
    /// <inheritdoc cref="ICommandHandler{PersonCreationCommand,VoidResult}.HandleAsync"/>
    /// <exception cref="PersonNameTooLongException">The person's name is too long.</exception>
    /// <exception cref="PersonDuplicateException">A person with the same name and date of birth already exists.</exception>
    /// <exception cref="CountryNotFoundException">The specified birth country does not exist.</exception>
    /// <exception cref="PersonNotFoundException">The person with the specified ID was not found.</exception>
    public async Task<VoidResult> HandleAsync(
        UpdatePersonCommand command,
        CancellationToken cancellationToken = default)
    {
        var personToUpdate = await personRepository.GetRequiredPersonByIdAsync(command.Id, cancellationToken);

        personToUpdate.Rename(PersonFullName.Create(command.FullName));
        personToUpdate.ChangeBirthInformation(
            BirthYear.Create(command.BirthYear, currentTimeProvider.GetCurrentTime()),
            command.BirthCountryId);

        await PersonValidationHelper.ValidateCountryExistsAndThrowAsync(
            personToUpdate, countryRepository, cancellationToken);

        await PersonValidationHelper.ValidatePersonDuplicatesAndThrowAsync(
            personToUpdate, personRepository, cancellationToken);

        await personRepository.UpdatePersonAsync(personToUpdate, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return VoidResult.Value;
    }
}