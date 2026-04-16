using System.Threading.Tasks;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.RequestModels;
using AppQueryServices = MindTrail.Application.Abstractions.QueryServices;
using EfQueryServices = MindTrail.EfCore.Interfaces.QueryServices;

namespace MindTrail.ApplicationConfigurator.Abstractions.Adapters.QueryServices;

public class CountryQueryServiceAdapter(
    EfQueryServices.ICountryQueryService countryQueryService)
    : AppQueryServices.ICountryQueryService
{
    public async Task<PagedDto<CountryDto>> GetCountriesAsync(CountryQueryModel queryModel)
    {
        return await countryQueryService.GetCountriesAsync(queryModel);
    }
}