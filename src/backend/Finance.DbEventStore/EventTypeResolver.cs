using Finance.Framework;

namespace Finance.DbEventStore
{
    public class EventTypeResolver : IEventTypeResolver
    {
        private const string EventTypeClassNotFound = "Event type class not found.";

        public Type Resolve(string eventType) =>
            typeof(Domain.Info).Assembly.GetType(eventType)
                ?? throw new EventStoreException(EventTypeClassNotFound);
    }
}