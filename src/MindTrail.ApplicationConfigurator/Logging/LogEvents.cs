using Microsoft.Extensions.Logging;

namespace MindTrail.ApplicationConfigurator.Logging;

/// <summary>
/// Log events.
/// </summary>
public class LogEvents
{
    public const int BaseEventId = 1100; // 1100-1099

    private static EventId CreateEventId(int id, string name) => new(id, name);

    private static EventId CreateEventIdRelativeToBase(int idOffset, string name) => new(BaseEventId + idOffset, name);

    public static class Crud
    {
        public static readonly EventId Read = CreateEventId(1001, "Read");
        public static readonly EventId ReadNotFound = CreateEventId(1002, "ReadNotFound");
        public static readonly EventId Create = CreateEventId(1003, "Created");
        public static readonly EventId Update = CreateEventId(1004, "Updated");
        public static readonly EventId UpdateNotFound = CreateEventId(1005, "UpdateNotFound");
        public static readonly EventId Delete = CreateEventId(1006, "Deleted");
        public static readonly EventId DeleteNotFound = CreateEventId(1007, "DeleteNotFound");
    }
}