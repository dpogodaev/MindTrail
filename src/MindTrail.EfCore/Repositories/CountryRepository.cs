using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Entities;
using MindTrail.EfCore.Interfaces.Repositories;
using MindTrail.EfCore.Repositories.Base;

namespace MindTrail.EfCore.Repositories;

public class CountryRepository(AppDbContext dbContext)
    : BaseRepository(dbContext), ICountryRepository
{
    public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await GetEntities<Country>().AnyAsync(x => x.Id == id, cancellationToken);
    }
}