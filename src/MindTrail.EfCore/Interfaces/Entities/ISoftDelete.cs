namespace MindTrail.EfCore.Interfaces.Entities;

/// <summary>
/// Soft-delete entities are not actually deleted, are marked as deleted in the database,
/// but cannot be retrieved to the application.
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// Used to mark an entity as 'Deleted'. 
    /// </summary>
    bool IsDeleted { get; set; }
}