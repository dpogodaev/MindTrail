using System;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.Domain.Entities;

namespace MindTrail.Application.Mapping;

public static class PersonMapping
{
    public static PersonDto ToDto(this Person domainEntity)
    {
        ArgumentNullException.ThrowIfNull(domainEntity, nameof(domainEntity));

        return new PersonDto
        {
            Id = domainEntity.Id,
            FullName = domainEntity.FullName,
            BirthYear = domainEntity.BirthYear?.Value,
            BirthCountryId = domainEntity.BirthCountryId,
        };
    }
}