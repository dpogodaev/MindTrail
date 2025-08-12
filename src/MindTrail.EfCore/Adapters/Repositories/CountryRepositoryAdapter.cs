using System.Linq;
using System.Threading.Tasks;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Filters;
using MindTrail.DomainServices.Interfaces.Storages.Repositories;
using EfEntities = MindTrail.EfCore.Entities;
using EfRepositories = MindTrail.EfCore.Interfaces.Repositories;

namespace MindTrail.EfCore.Adapters.Repositories;

public class CountryRepositoryAdapter(EfRepositories.ICountryRepository repository) : ICountryRepository
{
    #region ICountryRepository

    public async Task<PagedResult<Country>> GetCountriesAsReadOnlyAsync(CountryFilter filter)
    {
        return MapToDomainEntities(
            await repository.GetCountriesAsReadOnlyAsync(filter));
    }

    #endregion

    #region Private methods

    private static PagedResult<Country> MapToDomainEntities(PagedResult<EfEntities.Country> efEntities)
    {
        return new PagedResult<Country>
        {
            Items = efEntities.Items.Select(MapToDomainEntity),
            PageNumber = efEntities.PageNumber,
            PageSize = efEntities.PageSize,
            TotalCount = efEntities.TotalCount
        };
    }

    private static Country MapToDomainEntity(EfEntities.Country efEntity)
    {
        return new Country
        {
            Id = efEntity.Id,
            Name = efEntity.Name,
            Code = efEntity.Code
        };
    }

    #endregion
}