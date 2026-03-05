using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppDtos = MindTrail.ApplicationContracts.Dtos;
using AppModels = MindTrail.ApplicationContracts.RequestModels;
using AppRepositories = MindTrail.Application.Abstractions.Repositories;
using EfEntities = MindTrail.EfCore.Entities;
using EfFilters = MindTrail.EfCore.Filters;
using EfRepositories = MindTrail.EfCore.Interfaces.Repositories;

namespace MindTrail.HostConfiguration.Abstractions.Adapters.Repositories;

public class PersonReadRepositoryAdapter(
    EfRepositories.IPersonRepository repository)
    : AppRepositories.IPersonReadRepository
{
    public async Task<AppDtos.PagedDto<AppDtos.PersonDto>> GetPersonsAsync(AppModels.PersonFilterModel filter)
    {
        return await ToAppDto(
            repository.GetPersons(ToEfFilter(filter), includeCountry: true));
    }

    private static AppDtos.PersonDto ToAppDto(EfEntities.Person efEntity)
    {
        return new AppDtos.PersonDto
        {
            Id = efEntity.Id,
            FullName = efEntity.FullName,
            BirthYear = (uint?)efEntity.BirthYear,
            BirthCountryId = efEntity.BirthCountryId,
            BirthCountryName = efEntity.BirthCountry?.Name,
        };
    }

    private static async Task<AppDtos.PagedDto<AppDtos.PersonDto>> ToAppDto(IQueryable<EfEntities.Person> query)
    {
        var total = await query.CountAsync();
        var items = query.Select(x => ToAppDto(x));

        return new AppDtos.PagedDto<AppDtos.PersonDto>
        {
            Total = total,
            Items = await items.ToListAsync(),
        };
    }

    private static EfFilters.PersonFilter ToEfFilter(AppModels.PersonFilterModel appFilter)
    {
        return new EfFilters.PersonFilter
        {
            PageNumber = appFilter.PageNumber,
            PageSize = appFilter.PageSize,
            Search = appFilter.Search,
            Sorting = appFilter.Sorting,
        };
    }
}