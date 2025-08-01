using Microsoft.EntityFrameworkCore;

namespace MindTrail.EfCoreMssql.Context;

/// <summary>
/// Application database context for SQL Server.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class with the options provided.
    /// </summary>
    /// <param name="options">Data context options for connect to the database server.</param>
    /// <param name="tenantProvider">Used to get the tenant ID.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    #region DbContext

    /// <inheritdoc cref="DbContext.OnModelCreating"/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new PersonConfig());
    }

    #endregion

    #region Database entities (table names)

    //public DbSet<Person> Persons { get; set; }

    #endregion
}