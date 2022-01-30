namespace Finance.Framework
{
    public interface IEventStore<TAggregate, in TId>
        where TAggregate : AggregateRoot<TId>
        where TId : struct
    {
        Task<TAggregate> GetAsync(TId id);
        Task StoreAsync(TAggregate aggregate);
    }

    public interface IEventStore<TAggregate> : IEventStore<TAggregate, Guid>
        where TAggregate : AggregateRoot
    {
        new Task<TAggregate> GetAsync(Guid id);
    }
}