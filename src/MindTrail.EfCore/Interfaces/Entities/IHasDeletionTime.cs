using System;

namespace MindTrail.EfCore.Interfaces.Entities;

/// <summary>
/// Entity that should store information about the deletion time.
/// </summary>
/// <remarks>
/// <see cref="DeletionTime"/> is set automatically when an entity is marked as deleted in the database context.
/// </remarks>
public interface IHasDeletionTime : ISoftDelete
{
    /// <summary>
    /// Gets or sets the deletion time of this entity.
    /// </summary>
    DateTime? DeletionTime { get; set; }
}