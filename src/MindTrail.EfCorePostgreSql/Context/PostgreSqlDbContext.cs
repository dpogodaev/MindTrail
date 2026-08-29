using Microsoft.EntityFrameworkCore;
using MindTrail.EfCore.Context;

namespace MindTrail.EfCorePostgreSql.Context;

/// <summary>
/// Application database context for PostgreSQL.
/// </summary>
/// <param name="options">The configuration options for the database context (connection, provider, etc.).</param>
public class PostgreSqlDbContext(DbContextOptions options)
    : AppDbContext(options);