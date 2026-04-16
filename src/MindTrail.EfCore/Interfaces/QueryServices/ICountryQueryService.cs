using System.Threading.Tasks;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.RequestModels;

namespace MindTrail.EfCore.Interfaces.QueryServices;

public interface ICountryQueryService
{
    public Task<PagedDto<CountryDto>> GetCountriesAsync(CountryQueryModel queryModel);
}