using System;
using System.Threading;
using System.Threading.Tasks;
using MindTrail.Domain.Entities;
using MindTrail.DomainShared.Exceptions.Persons;

namespace MindTrail.Application.Abstractions.Repositories;

/// <summary>
/// Provides data access operations for <see cref="Person"/> entities.
/// </summary>
public interface IPersonRepository
{
    /// <summary>
    /// Returns a person by full name and year of birth.
    /// </summary>
    /// <param name="fullName">The full name of the person to retrieve.</param>
    /// <param name="birthYear">The year of birth of the person to retrieve. If <c>null</c>, matching is not applied.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The matching person, or <c>null</c> if not found.</returns>
    Task<Person?> GetPersonByNameAndBirthAsync(
        string fullName,
        int? birthYear,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the person with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the person to retrieve.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The person with the specified ID.</returns>
    /// <exception cref="PersonNotFoundException">The person with the specified ID was not found.</exception>
    Task<Person> GetRequiredPersonByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="personToCreate">The person to create.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The ID of the created person.</returns>
    Task<Guid> CreatePersonAsync(
        Person personToCreate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing person.
    /// </summary>
    /// <param name="personToUpdate">The person with the new values to persist.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The updated person.</returns>
    /// <exception cref="PersonNotFoundException">The person to update was not found.</exception>
    Task<Person> UpdatePersonAsync(
        Person personToUpdate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a person by ID.
    /// </summary>
    /// <param name="id">The ID of the person to delete.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The deleted person.</returns>
    /// <exception cref="PersonNotFoundException">The person with the specified ID was not found.</exception>
    Task<Person> DeletePersonAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}