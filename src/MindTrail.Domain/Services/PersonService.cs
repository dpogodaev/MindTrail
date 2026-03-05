using System.Threading.Tasks;
using MindTrail.Domain.Abstractions.Repositories;
using MindTrail.Domain.Entities;
using MindTrail.Domain.Interfaces.Services;
using MindTrail.Domain.ValueObjects;
using MindTrail.DomainShared.Exceptions;

namespace MindTrail.Domain.Services;

/// <inheritdoc/>
public class PersonService(
    ICountryRepository countryRepository,
    IPersonRepository personRepository)
    : IPersonService
{
    /// <inheritdoc cref="IPersonService.CreatePersonAsync"/>
    public async Task<Person> CreatePersonAsync(
        PersonFullName fullName,
        BirthYear? birthYear,
        int? birthCountryId)
    {
        var personToCreate = new Person
        {
            FullName = fullName,
            BirthYear = birthYear,
            BirthCountryId = birthCountryId,
        };

        await ValidatePersonDuplicatesAndThrowAsync(personToCreate);
        await ValidateCountryExistsAndThrowAsync(personToCreate);

        return personToCreate;
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
        var birthYear = person.BirthYear ?? (uint?)null;

        var duplicatePerson = await personRepository.GetByNameAndBirthYearAsync(person.FullName, birthYear);

        if (duplicatePerson == null)
        {
            return;
        }

        throw new PersonDuplicateException(duplicatePerson.Id, person.FullName, birthYear);
    }
}