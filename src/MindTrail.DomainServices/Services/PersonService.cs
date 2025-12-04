using System.Threading.Tasks;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Exceptions;
using MindTrail.DomainServices.Filters;
using MindTrail.DomainServices.Interfaces.Services;
using MindTrail.DomainServices.Interfaces.Storages.Repositories;

namespace MindTrail.DomainServices.Services;

/// <inheritdoc/>
public class PersonService(IPersonRepository personRepository)
    : IPersonService
{
    private const int MaxPersonNameLength = 64;

    /// <inheritdoc cref="IPersonService.CreatePersonAsync"/>
    public async Task<Person> CreatePersonAsync(Person personToCreate)
    {
        await ValidatePersonAndThrowAsync(personToCreate);

        return await personRepository.CreatePersonAsync(personToCreate);
    }

    private static void ValidatePersonNameLengthAndThrow(Person person)
    {
        if (person.FullName.Length > MaxPersonNameLength)
        {
            throw new PersonNameTooLongException(person.FullName, MaxPersonNameLength);
        }
    }

    private async Task ValidatePersonAndThrowAsync(Person person)
    {
        ValidatePersonNameLengthAndThrow(person);
        await ValidatePersonDuplicatesAndThrowAsync(person);
    }

    private async Task ValidatePersonDuplicatesAndThrowAsync(Person person)
    {
        var duplicateRules = await personRepository.GetPersonsAsReadOnlyAsync(
            new PersonFilter
            {
                FullName = person.FullName,
                BirthYear = person.BirthYear,
            });

        if (duplicateRules.TotalCount == 0)
        {
            return;
        }

        throw new PersonDuplicateException(person.FullName, person.BirthYear);
    }
}