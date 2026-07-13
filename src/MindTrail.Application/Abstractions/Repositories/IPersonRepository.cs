using System;
using System.Threading.Tasks;
using MindTrail.Domain.Entities;

namespace MindTrail.Application.Abstractions.Repositories;

public interface IPersonRepository
{
    Task<Person?> GetPersonByNameAndBirthAsync(string fullName, int? birthYear);

    Task<Person> GetRequiredPersonByIdAsync(Guid id);

    Task<Guid> CreatePersonAsync(Person entityToCreate);

    Task<Person> UpdatePersonAsync(Person entityToUpdate);

    Task<Person> DeletePersonAsync(Guid id);
}