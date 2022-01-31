namespace Finance.Framework
{
    public interface IDbEventStore<TId>
        where TId : struct
    {
        Task<IList<EventRecord<TId>>> GetEventRecordsAsync(string aggregateName, TId id);
        Task<int> GetNextVersionAsync(string aggregateName, TId id);
        Task StoreEventAsync(EventRecord<TId> eventRecord);
        Task StoreEventsAsync(EventRecord<TId>[] eventRecords);
    }
}