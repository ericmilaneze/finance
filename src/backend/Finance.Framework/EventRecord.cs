namespace Finance.Framework
{
    public record EventRecord<TAggregateId>(
        string AggregateType,
        TAggregateId AggregateId,
        int Version,
        DateTime TimeStamp,
        string EventType,
        string Event) where TAggregateId : struct;
}