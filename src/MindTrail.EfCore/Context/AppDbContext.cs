using Microsoft.EntityFrameworkCore;
using MindTrail.EfCore.Configs;
using MindTrail.EfCore.Entities;

namespace MindTrail.EfCore.Context;

/// <summary>
/// Application database context.
/// </summary>
/// <param name="options">The configuration options for the database context (connection, provider, etc.).</param>
public abstract class AppDbContext(DbContextOptions options)
    : DbContext(options)
{
    /// <summary>
    /// Gets or sets a value indicating whether changes should be saved automatically after entity operations.
    /// </summary>
    public bool IsAutoSaveEnabled { get; set; }

    /// <summary>
    /// Gets or sets the set of <see cref="Country"/> entities.
    /// </summary>
    public DbSet<Country> Countries { get; set; }

    /// <summary>
    /// Gets or sets the set of <see cref="Person"/> entities.
    /// </summary>
    public DbSet<Person> Persons { get; set; }

    /// <summary>
    /// Gets or sets the set of <see cref="Card"/> entities.
    /// </summary>
    public DbSet<Card> Cards { get; set; }

    /// <summary>
    /// Applies shared (base) configuration for the EF Core model
    /// such as entity mappings, relationships, constraints, and database schema settings
    /// that are common to all database providers.
    /// </summary>
    /// <param name="modelBuilder">The builder used to configure the EF Core model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CountryConfig());
        modelBuilder.ApplyConfiguration(new PersonConfig());
        modelBuilder.ApplyConfiguration(new CardConfig());
    }
}