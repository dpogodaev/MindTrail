using System.Threading.Tasks;
using MindTrail.AppServices.Interfaces.Services;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Filters;
using MindTrail.DomainServices.Interfaces.Storages.Repositories;

namespace MindTrail.AppServices.Services;

public class CountryAppService(ICountryRepository countryRepository)
    : ICountryAppService
{
    public async Task<PagedResult<Country>> GetCountriesAsync(CountryFilter filter)
    {
        return await countryRepository.GetCountriesAsReadOnlyAsync(filter);
    }
}