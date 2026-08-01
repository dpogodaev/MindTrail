using System;
using System.Threading;
using System.Threading.Tasks;
using MindTrail.Domain.Entities;

namespace MindTrail.Application.Abstractions.Repositories;

public interface IPersonRepository
{
    Task<Person?> GetPersonByNameAndBirthAsync(
        string fullName,
        int? birthYear,
        CancellationToken cancellationToken = default);

    Task<Person> GetRequiredPersonByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Guid> CreatePersonAsync(
        Person personToCreate,
        CancellationToken cancellationToken = default);

    Task<Person> UpdatePersonAsync(
        Person personToUpdate,
        CancellationToken cancellationToken = default);

    Task<Person> DeletePersonAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}