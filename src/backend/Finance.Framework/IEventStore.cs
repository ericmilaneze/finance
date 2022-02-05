namespace Finance.Framework
{
    public interface IEventStore<TAggregate, in TId>
        where TAggregate : AggregateRoot<TId>
        where TId : struct
    {
        Task<TAggregate> GetAsync(TId id, CancellationToken cancellationToken = default);
        Task StoreAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
    }

    public interface IEventStore<TAggregate> : IEventStore<TAggregate, Guid>
        where TAggregate : AggregateRoot
    {
    }
}