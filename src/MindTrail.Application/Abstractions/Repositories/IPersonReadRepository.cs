using System.Threading.Tasks;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.RequestModels;

namespace MindTrail.Application.Abstractions.Repositories;

public interface IPersonReadRepository
{
    Task<PagedDto<PersonDto>> GetPersonsAsync(PersonFilterModel filter);
}