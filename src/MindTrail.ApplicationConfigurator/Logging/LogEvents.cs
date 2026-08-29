using Microsoft.Extensions.Logging;

namespace MindTrail.ApplicationConfigurator.Logging;

/// <summary>
/// Log events.
/// </summary>
public class LogEvents
{
    /// <summary>
    /// The base event ID used as an offset for event IDs created via <see cref="CreateEventIdRelativeToBase"/>.
    /// </summary>
    public const int BaseEventId = 1100; // 1100-1199

    private static EventId CreateEventId(int id, string name) => new(id, name);

    private static EventId CreateEventIdRelativeToBase(int idOffset, string name) => new(BaseEventId + idOffset, name);

    /// <summary>
    /// Event IDs for CRUD (Create, Read, Update, Delete) operations.
    /// </summary>
    public static class Crud
    {
        /// <summary>
        /// The event ID for a successful read operation.
        /// </summary>
        public static readonly EventId Read = CreateEventId(1001, "Read");

        /// <summary>
        /// The event ID for a read operation where the requested entity was not found.
        /// </summary>
        public static readonly EventId ReadNotFound = CreateEventId(1002, "ReadNotFound");

        /// <summary>
        /// The event ID for a successful create operation.
        /// </summary>
        public static readonly EventId Create = CreateEventId(1003, "Created");

        /// <summary>
        /// The event ID for a successful update operation.
        /// </summary>
        public static readonly EventId Update = CreateEventId(1004, "Updated");

        /// <summary>
        /// The event ID for an update operation where the target entity was not found.
        /// </summary>
        public static readonly EventId UpdateNotFound = CreateEventId(1005, "UpdateNotFound");

        /// <summary>
        /// The event ID for a successful delete operation.
        /// </summary>
        public static readonly EventId Delete = CreateEventId(1006, "Deleted");

        /// <summary>
        /// The event ID for a delete operation where the target entity was not found.
        /// </summary>
        public static readonly EventId DeleteNotFound = CreateEventId(1007, "DeleteNotFound");
    }
}