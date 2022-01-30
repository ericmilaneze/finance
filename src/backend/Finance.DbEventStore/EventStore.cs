using System.Reflection;
using System.Text.Json;
using Finance.Framework;

namespace Finance.DbEventStore
{
    public class EventStore<TAggregate, TId> : IEventStore<TAggregate, TId>
        where TAggregate : AggregateRoot<TId>
        where TId : struct
    {
        private const string TEventNullMessage = "Event should never be null.";
        private const string ConstructorNotFoundMessage = "Constructor with id not found.";
        private const string CouldNotConvertAggregateMessage = "Could not convert to TAggregate.";
        private const string EventTypeClassNotFound = "Event type class not found.";
        private const string CouldNotDeserializeEventMessage = "Could not deserialize the event.";

        private readonly IDbEventStore<TId> _dbEventStore;
        private readonly IAggregateNameResolver<TAggregate, TId> _aggregateNameResolver;

        public EventStore(
            IAggregateNameResolver<TAggregate, TId> aggregateNameResolver,
            IDbEventStore<TId> dbEventStore)
        {
            _dbEventStore = dbEventStore;
            _aggregateNameResolver = aggregateNameResolver;
        }

        public async Task<TAggregate> Get(TId id)
        {
            var aggregate = CreateAggregate(id);

            foreach (var eventRecord in await _dbEventStore.GetEventRecords(_aggregateNameResolver.Resolve(aggregate), id))
                HandleEvent(aggregate, eventRecord);

            return aggregate;

            static TAggregate CreateAggregate(TId id)
            {
                var aggregateConstructor = typeof(TAggregate)
                    .GetConstructor(
                        BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance,
                        null,
                        new[] { typeof(Guid) },
                        null) ?? throw new EventStoreException(ConstructorNotFoundMessage);

                return aggregateConstructor.Invoke(new object[] { id }) as TAggregate
                    ?? throw new EventStoreException(CouldNotConvertAggregateMessage);
            }

            static void HandleEvent(TAggregate aggregate, EventRecord eventRecord)
            {
                var eventType = typeof(Domain.Info).Assembly.GetType(eventRecord.EventType)
                    ?? throw new EventStoreException(EventTypeClassNotFound);

                var @event = JsonSerializer.Deserialize(
                    eventRecord.Event,
                    eventType) as IEvent ?? throw new EventStoreException(CouldNotDeserializeEventMessage);

                aggregate.HandleEvent(@event);
            }
        }

        public async Task Store(TAggregate aggregate)
        {
            var nextVersion = await _dbEventStore.GetNextVersion(
                _aggregateNameResolver.Resolve(aggregate),
                aggregate.Id);

            foreach (var @event in aggregate.Events ?? Enumerable.Empty<IEvent>())
            {
                await StoreEvent(aggregate, nextVersion, @event);
                nextVersion++;
            }

            aggregate.ClearEvents();

            async Task StoreEvent(TAggregate aggregate, int nextVersion, IEvent @event)
            {
                var eventRecord = new EventRecord(
                    _aggregateNameResolver.Resolve(aggregate),
                    aggregate.Id,
                    nextVersion,
                    DateTime.UtcNow,
                    @event?.GetType()?.FullName ?? throw new EventStoreException(TEventNullMessage),
                    JsonSerializer.Serialize(@event, @event.GetType()));

                await _dbEventStore.StoreEvent(eventRecord);
            }
        }
    }

    public class EventStore<TAggregate> : EventStore<TAggregate, Guid>, IEventStore<TAggregate>
        where TAggregate : AggregateRoot
    {
        public EventStore(IAggregateNameResolver<TAggregate> aggregateNameResolver, IDbEventStore dbEventStore)
            : base(aggregateNameResolver, dbEventStore)
        {
        }
    }
}