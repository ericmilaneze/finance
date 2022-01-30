namespace Finance.Framework
{
    public record EventRecord<TId>(
        string AggregateType,
        TId Id,
        int Version,
        DateTime TimeStamp,
        string EventType,
        string Event) where TId : struct;
}