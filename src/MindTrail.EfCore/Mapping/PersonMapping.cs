using MindTrail.ApplicationContracts.Dtos;
using MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Mapping;

internal static class PersonMapping
{
    public static PersonDto ToDto(this Person efEntity)
    {
        return new PersonDto
        {
            Id = efEntity.Id,
            FullName = efEntity.FullName,
            BirthYear = efEntity.BirthYear,
            BirthCountryId = efEntity.BirthCountryId,
            BirthCountryName = efEntity.BirthCountry?.Name,
        };
    }
}