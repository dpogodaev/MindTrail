using System.Threading.Tasks;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Filters;
using Country = MindTrail.EfCore.Entities.Country;

namespace MindTrail.EfCore.Interfaces.Repositories;

/// <summary>
/// Database repository for <see cref="EfCore.Entities.Country"/> entities.
/// </summary>
public interface ICountryRepository
{
    Task<PagedResult<Country>> GetCountriesAsReadOnlyAsync(CountryFilter filter);
}