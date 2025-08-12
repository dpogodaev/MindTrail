using Microsoft.EntityFrameworkCore;
using MindTrail.EfCore.Context;

namespace MindTrail.EfCoreMssql.Context;

/// <summary>
/// Application database context for SQL Server.
/// </summary>
public class MssqlDbContext : AppDbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MssqlDbContext"/> class with the options provided.
    /// </summary>
    /// <param name="options">Configuration options for the database context (connection, provider, etc.).</param>
    public MssqlDbContext(DbContextOptions options) : base(options)
    {
    }
}