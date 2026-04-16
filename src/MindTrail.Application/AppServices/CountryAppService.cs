using System.Threading.Tasks;
using MindTrail.Application.Abstractions.QueryServices;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.AppServices;
using MindTrail.ApplicationContracts.RequestModels;

namespace MindTrail.Application.AppServices;

public class CountryAppService(
    ICountryQueryService countryRepository)
    : ICountryAppService
{
    public async Task<PagedDto<CountryDto>> GetCountriesAsync(CountryQueryModel filter)
    {
        return await countryRepository.GetCountriesAsync(filter);
    }
}