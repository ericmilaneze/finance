namespace Finance.Framework
{
    public interface IEventStore<TAggregate, in TId>
        where TAggregate : AggregateRoot<TId>
        where TId : struct
    {
        TAggregate Get(TId id);
        void Store(TAggregate obj);
    }

    public interface IEventStore<TAggregate> : IEventStore<TAggregate, Guid>
        where TAggregate : AggregateRoot
    {
        new TAggregate Get(Guid id);
    }
}