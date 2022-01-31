namespace Finance.Framework
{
    public interface IAggregateNameResolver<TAggregate, TId>
        where TAggregate : AggregateRoot<TId>
        where TId : struct
    {
        string Resolve(TAggregate aggregate);
    }

    public interface IAggregateNameResolver<TAggregate> : IAggregateNameResolver<TAggregate, Guid>
        where TAggregate : AggregateRoot
    {
    }
}