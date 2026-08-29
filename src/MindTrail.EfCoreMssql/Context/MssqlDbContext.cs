using Microsoft.EntityFrameworkCore;
using MindTrail.EfCore.Context;

namespace MindTrail.EfCoreMssql.Context;

/// <summary>
/// Application database context for SQL Server.
/// </summary>
/// <param name="options">The configuration options for the database context (connection, provider, etc.).</param>
public class MssqlDbContext(DbContextOptions options)
    : AppDbContext(options);