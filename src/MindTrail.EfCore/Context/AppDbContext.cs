using Microsoft.EntityFrameworkCore;
using MindTrail.EfCore.Configs;
using MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Context;

/// <summary>
/// Application database context.
/// </summary>
/// <param name="options">Configuration options for the database context (connection, provider, etc.).</param>
public abstract class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    /// Indicates if changes should be saved automatically after entity operations.
    /// </summary>
    public bool IsAutoSaveEnabled { get; set; }

    #region DbContext

    /// <summary>
    /// Applies shared (base) configuration for the EF Core model
    /// such as entity mappings,  relationships, constraints, and database schema settings
    /// that are common to all database providers.
    /// </summary>
    /// <param name="modelBuilder">Used to configure the EF Core model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CountryConfig());
        modelBuilder.ApplyConfiguration(new PersonConfig());
    }

    #endregion

    #region Database entities (table names)

    public DbSet<Country> Countries { get; set; }
    public DbSet<Person> Persons { get; set; }

    #endregion
}