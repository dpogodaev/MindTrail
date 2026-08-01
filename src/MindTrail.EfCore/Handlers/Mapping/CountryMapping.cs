using System;
using System.Linq.Expressions;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Handlers.Mapping;

internal static class CountryMapping
{
    public static Expression<Func<Country, CountryDto>> ToDto()
    {
        return country => new CountryDto
        {
            Id = country.Id,
            Name = country.Name,
            Code = country.Code,
        };
    }
}