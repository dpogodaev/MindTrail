using System;
using System.Linq.Expressions;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Handlers.Mapping;

/// <summary>
/// Maps <see cref="Country"/> entities to <see cref="CountryDto"/> objects.
/// </summary>
internal static class CountryMapping
{
    /// <summary>
    /// Returns an expression that maps a <see cref="Country"/> entity to a <see cref="CountryDto"/>.
    /// </summary>
    /// <returns>The mapping expression.</returns>
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