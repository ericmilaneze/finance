using Finance.Domain.FixInc.ChkAcc;
using Finance.Framework;

namespace Finance.DbEventStore
{
    public class AggregateNameResolver<TAggregate, TId> : IAggregateNameResolver<TAggregate, TId>
        where TAggregate : AggregateRoot<TId>
        where TId : struct
    {
        public string Resolve(TAggregate aggregate)
        {
            return aggregate switch
            {
                CheckingAccount _ => "CheckingAccount",
                _ => throw new EventStoreException("Could not resolve aggregate name."),
            };
        }
    }

    public class AggregateNameResolver<TAggregate> : AggregateNameResolver<TAggregate, Guid>, IAggregateNameResolver<TAggregate>
        where TAggregate : AggregateRoot
    {
    }
}