namespace MindTrail.EfCore.Interfaces.Entities;

/// <summary>
/// Soft-deleted entities are not physically removed from the database — they are marked as deleted
/// and excluded from application queries.
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity is marked as deleted.
    /// </summary>
    bool IsDeleted { get; set; }
}