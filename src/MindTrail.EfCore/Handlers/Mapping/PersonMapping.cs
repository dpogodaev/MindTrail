using System;
using System.Linq.Expressions;
using MindTrail.ApplicationContracts.Dtos;
using EfEntities = MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Handlers.Mapping;

/// <summary>
/// Maps <see cref="EfEntities.Person"/> entities to <see cref="PersonDto"/> objects.
/// </summary>
internal static class PersonMapping
{
    /// <summary>
    /// Returns an expression that maps a <see cref="EfEntities.Person"/> entity to a <see cref="PersonDto"/>.
    /// </summary>
    /// <returns>The mapping expression.</returns>
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