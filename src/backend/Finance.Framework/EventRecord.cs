namespace Finance.Framework
{
    public record EventRecord(
        string AggregateType,
        object Id,
        int Version,
        DateTime TimeStamp,
        string EventType,
        string Event);
}