using System;

namespace MindTrail.EfCore.Interfaces.Entities;

/// <summary>
/// Entity that should store information about creation time.
/// </summary>
/// <remarks>
/// <see cref="CreationTime"/> is set automatically when an entity is added to the database context.
/// </remarks>
public interface IHasCreationTime
{
    /// <summary>
    /// Creation time of this entity.
    /// </summary>
    DateTime CreationTime { get; set; }
}