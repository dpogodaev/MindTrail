using System.Threading;
using System.Threading.Tasks;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.Domain.Entities;
using MindTrail.DomainShared.Exceptions;

namespace MindTrail.Application.Helpers;

/// <summary>
/// Provides shared validation logic for person-related commands.
/// </summary>
public static class PersonValidationHelper
{
    /// <summary>
    /// Validates that the person's birth country exists.
    /// </summary>
    /// <param name="person">The person whose birth country is validated.</param>
    /// <param name="countryRepository">Provides access to country data.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <exception cref="CountryNotFoundException">The specified birth country does not exist.</exception>
    public static async Task ValidateCountryExistsAndThrowAsync(
        Person person,
        ICountryRepository countryRepository,
        CancellationToken cancellationToken = default)
    {
        if (person.BirthCountryId == null)
        {
            return;
        }

        if (!await countryRepository.ExistsByIdAsync(person.BirthCountryId.Value, cancellationToken))
        {
            throw new CountryNotFoundException(person.BirthCountryId.Value);
        }
    }

    /// <summary>
    /// Validates that no other person with the same name and year of birth already exists.
    /// </summary>
    /// <param name="person">The person to check for duplicates.</param>
    /// <param name="personRepository">Provides access to person data.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <exception cref="PersonDuplicateException">
    /// A person with the same name and date of birth already exists.
    /// </exception>
    public static async Task ValidatePersonDuplicatesAndThrowAsync(
        Person person,
        IPersonRepository personRepository,
        CancellationToken cancellationToken = default)
    {
        var birthYear = person.BirthYear ?? (int?)null;

        var duplicatePerson = await personRepository.GetPersonByNameAndBirthAsync(
            person.FullName, birthYear, cancellationToken);

        if (duplicatePerson == null)
        {
            return;
        }

        if (person.Id == duplicatePerson.Id)
        {
            return;
        }

        throw new PersonDuplicateException(duplicatePerson.Id, person.FullName, birthYear);
    }
}