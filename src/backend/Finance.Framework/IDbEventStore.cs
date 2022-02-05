namespace Finance.Framework
{
    public interface IDbEventStore<TId>
        where TId : struct
    {
        Task<IList<EventRecord<TId>>> GetEventRecordsAsync(string aggregateName, TId id, CancellationToken cancellationToken = default);
        Task<int> GetNextVersionAsync(string aggregateName, TId id, CancellationToken cancellationToken = default);
        Task StoreEventAsync(EventRecord<TId> eventRecord, CancellationToken cancellationToken = default);
        Task StoreEventsAsync(EventRecord<TId>[] eventRecords, CancellationToken cancellationToken = default);
    }

    public interface IDbEventStore : IDbEventStore<Guid>
    {
    }
}