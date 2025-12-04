namespace MindTrail.EfCore.Interfaces.Entities;

/// <summary>
/// Soft-delete entities are not actually deleted, are marked as deleted in the database,
/// but cannot be retrieved to the application.
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// Gets or sets a value indicating whether used to mark an entity as 'Deleted'.
    /// </summary>
    bool IsDeleted { get; set; }
}