using System.Threading.Tasks;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.Services;
using MindTrail.ApplicationContracts.RequestModels;

namespace MindTrail.Application.Services;

public class CountryAppService(
    ICountryReadRepository countryRepository)
    : ICountryAppService
{
    public async Task<PagedDto<CountryDto>> GetCountriesAsync(CountryFilterModel filter)
    {
        return await countryRepository.GetCountriesAsync(filter);
    }
}