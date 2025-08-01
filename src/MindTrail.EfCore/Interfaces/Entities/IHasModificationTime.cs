using System;

namespace MindTrail.EfCore.Interfaces.Entities;

/// <summary>
/// Entity that should store information about the time of the last modification.
/// </summary>
/// <remarks>
/// <see cref="LastModificationTime"/> is set automatically when an entity is changed in the database context.
/// </remarks>
public interface IHasModificationTime
{
    /// <summary>
    /// The last modified time for this entity.
    /// </summary>
    DateTime? LastModificationTime { get; set; }
}