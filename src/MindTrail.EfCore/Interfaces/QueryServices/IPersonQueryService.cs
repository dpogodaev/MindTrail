using System.Threading.Tasks;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.RequestModels;

namespace MindTrail.EfCore.Interfaces.QueryServices;

public interface IPersonQueryService
{
    public Task<PagedDto<PersonDto>> GetPersonsAsync(PersonQueryModel queryModel);
}