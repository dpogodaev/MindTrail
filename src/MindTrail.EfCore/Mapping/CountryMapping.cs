using MindTrail.ApplicationContracts.Dtos;
using MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Mapping;

internal static class CountryMapping
{
    public static CountryDto ToDto(this Country efEntity)
    {
        return new CountryDto
        {
            Id = efEntity.Id,
            Name = efEntity.Name,
            Code = efEntity.Code,
        };
    }
}