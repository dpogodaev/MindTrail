using Microsoft.EntityFrameworkCore;
using MindTrail.EfCore.Context;

namespace MindTrail.EfCorePostgreSql.Context;

/// <summary>
/// Application database context for PostgreSQL.
/// </summary>
public class PostgreSqlDbContext : AppDbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PostgreSqlDbContext"/> class with the options provided.
    /// </summary>
    /// <param name="options">Configuration options for the database context (connection, provider, etc.).</param>
    public PostgreSqlDbContext(DbContextOptions options) : base(options)
    {
    }
}