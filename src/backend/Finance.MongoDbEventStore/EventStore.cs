using System.Reflection;
using System.Text.Json;
using Finance.Framework;

namespace Finance.MongoDbEventStore
{
    public class EventStore<TAggregate> : IEventStore<TAggregate>
        where TAggregate : AggregateRoot
    {
        private const string TAggregateNullMessage = "TAggregate should never be null.";
        private const string TEventNullMessage = "Event should never be null.";

        public TAggregate Get(Guid id)
        {
            var aggregateConstructor = typeof(TAggregate)
                .GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance,
                    null,
                    new[] { typeof(Guid) },
                    null) ?? throw new Exception("Constructor with id not found.");

            var aggregate = aggregateConstructor.Invoke(new object[] { id }) as TAggregate
                ?? throw new Exception("Could not convert to TAggregate.");

            var eventRecords = GetEventRecords(
                typeof(TAggregate)?.FullName ?? throw new Exception(TAggregateNullMessage),
                id);

            foreach (var eventRecord in eventRecords)
            {
                var eventType = typeof(Domain.Info).Assembly.GetType(eventRecord.EventType)
                    ?? throw new Exception("Event type class not found.");

                var @event = JsonSerializer.Deserialize(
                    eventRecord.Event,
                    eventType) as IEvent ?? throw new Exception("Could not deserialize the event.");

                aggregate.HandleEvent(@event);
            }

            return aggregate;
        }

        public void Store(TAggregate obj)
        {
            var aggregateName = typeof(TAggregate)?.FullName ?? throw new Exception(TAggregateNullMessage);

            var nextVersion = GetNextVersion(aggregateName, obj.Id);

            foreach (var @event in obj.Events ?? Enumerable.Empty<IEvent>())
            {
                var eventRecord = new EventRecord(
                    typeof(TAggregate)?.FullName ?? throw new Exception(TAggregateNullMessage),
                    obj.Id,
                    nextVersion,
                    DateTime.UtcNow,
                    @event?.GetType()?.FullName ?? throw new Exception(TEventNullMessage),
                    JsonSerializer.Serialize(@event, @event.GetType()));

                StoreEvent(eventRecord);

                nextVersion++;
            }

            obj.ClearEvents();
        }

        private EventRecord[] GetEventRecords(string aggregateName, Guid id)
        {
            return new EventRecord[]
            {
                new EventRecord(
                    "",
                    Guid.NewGuid(),
                    1,
                    DateTime.UtcNow,
                    "Finance.Domain.Events.CheckingAccountEvents+V1+CheckingAccountCreated",
                    "{\"Name\":\"Account Test\",\"Description\":null,\"GrossValue\":10.23,\"NetValue\":10.23}")
            };
        }

        private int GetNextVersion(string aggregateName, Guid id)
        {
            return 1;
        }

        private void StoreEvent(EventRecord eventRecord)
        {
        }
    }
}