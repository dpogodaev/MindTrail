using Microsoft.EntityFrameworkCore;

namespace MindTrail.EfCore.Interfaces.Context;

/// <summary>
/// Defines the configuration for the EF Core model.
/// </summary>
public interface IModelConfiguration
{
    /// <summary>
    /// Configures the EF Core model, including entity mappings, relationships, constraints, and database schema settings.
    /// </summary>
    void ConfigureModel(ModelBuilder modelBuilder);
}