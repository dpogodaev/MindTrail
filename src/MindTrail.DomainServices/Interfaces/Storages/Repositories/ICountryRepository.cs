using System.Threading.Tasks;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Filters;

namespace MindTrail.DomainServices.Interfaces.Storages.Repositories;

public interface ICountryRepository
{
    Task<PagedResult<Country>> GetCountriesAsReadOnlyAsync(CountryFilter filter);
}