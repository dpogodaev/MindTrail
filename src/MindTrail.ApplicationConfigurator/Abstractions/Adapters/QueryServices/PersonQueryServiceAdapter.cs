using System.Threading.Tasks;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.RequestModels;
using AppQueryServices = MindTrail.Application.Abstractions.QueryServices;
using EfQueryServices = MindTrail.EfCore.Interfaces.QueryServices;

namespace MindTrail.ApplicationConfigurator.Abstractions.Adapters.QueryServices;

public class PersonQueryServiceAdapter(
    EfQueryServices.IPersonQueryService personQueryService)
    : AppQueryServices.IPersonQueryService
{
    public async Task<PagedDto<PersonDto>> GetPersonsAsync(PersonQueryModel filter)
    {
        return await personQueryService.GetPersonsAsync(filter);
    }
}