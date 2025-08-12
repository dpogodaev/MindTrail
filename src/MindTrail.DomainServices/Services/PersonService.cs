using System.Threading.Tasks;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Exceptions;
using MindTrail.DomainServices.Filters;
using MindTrail.DomainServices.Interfaces.Services;
using MindTrail.DomainServices.Interfaces.Storages.Repositories;
using MindTrail.DomainServices.ValueObjects;

namespace MindTrail.DomainServices.Services;

public class PersonService(IPersonRepository personRepository) : IPersonService
{
    private const int MaxPersonNameLength = 64;

    #region IPersonService

    /// <inheritdoc cref="IPersonService.CreatePersonAsync"/>
    public async Task<Person> CreatePersonAsync(Person personToCreate)
    {
        await ValidatePersonAndThrowAsync(personToCreate);

        return await personRepository.CreatePersonAsync(personToCreate);
    }

    #endregion

    #region Private methods

    private async Task ValidatePersonAndThrowAsync(Person person)
    {
        var fullNameValidation = ValidatePersonName(person.FullName);
        if (!fullNameValidation.IsValid)
        {
            throw new PersonNameException(fullNameValidation.ErrorInfo!, person.FullName);
        }

        var duplicatesValidation = await ValidatePersonDuplicates(person);
        if (!duplicatesValidation.IsValid)
        {
            throw new PersonDuplicateException(duplicatesValidation.ErrorInfo!, person.FullName, person.BirthYear);
        }
    }

    private static ValidationResult ValidatePersonName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return ValidationResult.WithUnsuccessful("Name is required");
        }

        if (name.Length > MaxPersonNameLength)
        {
            return ValidationResult.WithUnsuccessful(
                $"The maximum length of the name is {MaxPersonNameLength} characters");
        }

        return ValidationResult.WithSuccessful();
    }

    private async Task<ValidationResult> ValidatePersonDuplicates(Person person)
    {
        var duplicateRules = await personRepository.GetPersonsAsReadOnlyAsync(
            new PersonFilter
            {
                FullName = person.FullName,
                BirthYear = person.BirthYear
            });

        if (duplicateRules.TotalCount == 0)
        {
            return ValidationResult.WithSuccessful();
        }

        return ValidationResult.WithUnsuccessful(person.BirthYear == null
            ? "The person with the specified name already exists, try to set his date of birth"
            : "The person with the specified name and date of birth already exists");
    }

    #endregion
}