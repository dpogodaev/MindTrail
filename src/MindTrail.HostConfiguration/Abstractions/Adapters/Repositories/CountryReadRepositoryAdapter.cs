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

public class CountryReadRepositoryAdapter(
    EfRepositories.ICountryRepository repository)
    : AppRepositories.ICountryReadRepository
{
    public async Task<AppDtos.PagedDto<AppDtos.CountryDto>> GetCountriesAsync(AppModels.CountryFilterModel filter)
    {
        return await ToAppDto(
            repository.GetCountriesAsReadOnly(ToEfFilter(filter)));
    }

    private static AppDtos.CountryDto ToAppDto(EfEntities.Country efEntity)
    {
        return new AppDtos.CountryDto
        {
            Id = efEntity.Id,
            Name = efEntity.Name,
            Code = efEntity.Code,
        };
    }

    private static async Task<AppDtos.PagedDto<AppDtos.CountryDto>> ToAppDto(IQueryable<EfEntities.Country> query)
    {
        var total = await query.CountAsync();
        var items = query.Select(x => ToAppDto(x));

        return new AppDtos.PagedDto<AppDtos.CountryDto>
        {
            Total = total,
            Items = await items.ToListAsync(),
        };
    }

    private static EfFilters.CountryFilter ToEfFilter(AppModels.CountryFilterModel appFilter)
    {
        return new EfFilters.CountryFilter
        {
            PageNumber = appFilter.PageNumber,
            PageSize = appFilter.PageSize,
            Search = appFilter.Search,
            Sorting = appFilter.Sorting,
        };
    }
}