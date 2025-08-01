using Microsoft.Extensions.Logging;

namespace MindTrail.AppServices.Logging;

/// <summary>
/// Log events.
/// </summary>
public class AppLogEvents
{
    private const int BaseEventId = 1100; // 1100-1099

    #region CRUD

    public static readonly EventId Read = CreateEvenId(1001, "Read");
    public static readonly EventId ReadNotFound = CreateEvenId(1002, "ReadNotFound");
    public static readonly EventId Create = CreateEvenId(1003, "Created");
    public static readonly EventId Update = CreateEvenId(1004, "Updated");
    public static readonly EventId UpdateNotFound = CreateEvenId(1005, "UpdateNotFound");
    public static readonly EventId Delete = CreateEvenId(1006, "Deleted");
    public static readonly EventId DeleteNotFound = CreateEvenId(1007, "DeleteNotFound");

    #endregion

    #region Private methods

    private static EventId CreateEvenId(int id, string name) => new(id, name);

    private static EventId CreateEvenIdRelativeToBase(int idOffset, string name) => new(BaseEventId + idOffset, name);

    #endregion
}