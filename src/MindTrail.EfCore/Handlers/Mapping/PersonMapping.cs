using System;
using System.Linq.Expressions;
using MindTrail.ApplicationContracts.Dtos;
using EfEntities = MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Handlers.Mapping;

internal static class PersonMapping
{
    public static Expression<Func<EfEntities.Person, PersonDto>> ToDto()
    {
        return efEntity => new PersonDto
        {
            Id = efEntity.Id,
            FullName = efEntity.FullName,
            BirthYear = efEntity.BirthYear,
            BirthCountryId = efEntity.BirthCountryId,
            BirthCountryName = efEntity.BirthCountry != null
                ? efEntity.BirthCountry.Name
                : null,
        };
    }
}