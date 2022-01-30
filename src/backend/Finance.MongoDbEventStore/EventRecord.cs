using Finance.Framework;

namespace Finance.MongoDbEventStore
{
    public record EventRecord(
        string AggregateType,
        Guid Id,
        int Version,
        DateTime TimeStamp,
        string EventType,
        string Event);
}