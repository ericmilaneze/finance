namespace Finance.Framework
{
    public interface IDbEventStore<TId>
        where TId : struct
    {
        Task<EventRecord[]> GetEventRecords(string aggregateName, object id);
        Task<int> GetNextVersion(string aggregateName, object id);
        Task StoreEvent(EventRecord eventRecord);
    }

    public interface IDbEventStore : IDbEventStore<Guid>
    {
    }
}