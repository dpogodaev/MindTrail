using System.Threading.Tasks;
using MindTrail.Domain.Entities;
using MindTrail.Domain.ValueObjects;

namespace MindTrail.Domain.Interfaces.Services;

/// <summary>
/// Service for performing operations with persons.
/// </summary>
public interface IPersonService
{
    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="fullName">The person's full name.</param>
    /// <param name="birthYear">Year of birth.</param>
    /// <param name="birthCountryId">The ID of the country in which the person was born.</param>
    /// <returns>The created person.</returns>
    /// <exception cref="PersonDuplicateException">The person with the specified name and date of birth already exists.</exception>
    Task<Person> CreatePersonAsync(
        PersonFullName fullName,
        BirthYear? birthYear,
        int? birthCountryId);
}