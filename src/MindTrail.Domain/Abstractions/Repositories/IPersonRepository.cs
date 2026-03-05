using System;
using System.Threading.Tasks;
using MindTrail.Domain.Entities;

namespace MindTrail.Domain.Abstractions.Repositories;

public interface IPersonRepository
{
    Task<Person?> GetByNameAndBirthYearAsync(string fullName, uint? birthYear);

    Task<Person> GetRequiredBPersonByIdAsync(Guid id);

    Task<Person> CreatePersonAsync(Person entityToCreate);

    Task<Person> UpdatePersonAsync(Person entityToUpdate);

    Task<Person> DeletePersonAsync(Guid id);
}