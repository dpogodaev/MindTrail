using System;

namespace MindTrail.EfCore.Interfaces.Entities;

/// <summary>
/// Entity that should store information about the last modification time.
/// </summary>
/// <remarks>
/// <see cref="LastModificationTime"/> is set automatically when an entity is changed in the database context.
/// </remarks>
public interface IHasModificationTime
{
    /// <summary>
    /// Gets or sets the last modification time of this entity.
    /// </summary>
    DateTime? LastModificationTime { get; set; }
}