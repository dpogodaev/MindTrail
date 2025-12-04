using System.Threading.Tasks;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Filters;

namespace MindTrail.AppServices.Interfaces.Services;

public interface ICountryAppService
{
    Task<PagedResult<Country>> GetCountriesAsync(CountryFilter filter);
}