using System.Text.Json;
using Finance.Common.FixInc.ChkAcc;
using Finance.DbReading.Models;
using Finance.Domain.Events;
using Finance.Framework;
using Finance.MongoDb.Common;
using Finance.MongoDbReading.Registry;
using MongoDB.Driver;

namespace Finance.MongoDbReading
{
    public class MongoProjection : IDbProjection
    {
        private const string CouldNotDeserializeEventMessage = "Could not deserialize the event.";

        private readonly IEventTypeResolver _eventTypeResolver;
        private readonly IMongoDatabase _database;

        public MongoProjection(
            IEventTypeResolver eventTypeResolver,
            string? connectionString = default,
            string? dbName = default)
        {
            _eventTypeResolver = eventTypeResolver;

            var client = new MongoClient(connectionString ?? MongoDbSettings.Projection.ConnectionString);
            _database = client.GetDatabase(dbName ?? MongoDbSettings.Projection.DatabaseName);
        }

        public async Task ProjectEventsAsync(EventRecord<Guid>[] eventRecords, CancellationToken cancellationToken = default)
        {
            foreach (var eventRecord in eventRecords)
            {
                switch (GetEvent(eventRecord))
                {
                    case CheckingAccountEvents.V1.CheckingAccountCreated e:
                        await HandleEventAsync(e, cancellationToken: cancellationToken);
                        break;
                    case CheckingAccountEvents.V1.GrossValueUpdated e:
                        await HandleEventAsync(e, cancellationToken: cancellationToken);
                        break;
                }
            }
        }

        private async Task HandleEventAsync(CheckingAccountEvents.V1.CheckingAccountCreated @event, CancellationToken cancellationToken = default)
        {
            var model = new CheckingAccount
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                GrossValue = @event.GrossValue,
                NetValue = @event.NetValue,
                State = CheckingAccountState.Created,
            };

            await _database
                .GetCollection<CheckingAccount>(CollectionNamesRegistry.GetCollectionName<CheckingAccount>())
                .InsertOneAsync(model, cancellationToken: cancellationToken);
        }

        private async Task HandleEventAsync(CheckingAccountEvents.V1.GrossValueUpdated @event, CancellationToken cancellationToken = default)
        {
            var model = await _database
                .GetCollection<CheckingAccount>(CollectionNamesRegistry.GetCollectionName<CheckingAccount>())
                .Find(x => x.Id == @event.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var updatedModel = model with
            {
                GrossValue = @event.GrossValue,
                NetValue = @event.NetValue
            };

            await _database
                .GetCollection<CheckingAccount>(CollectionNamesRegistry.GetCollectionName<CheckingAccount>())
                .ReplaceOneAsync(x => x.Id == updatedModel.Id, updatedModel, cancellationToken: cancellationToken);
        }

        private IEvent GetEvent(EventRecord<Guid> eventRecord) =>
            JsonSerializer.Deserialize(
                eventRecord.Event,
                _eventTypeResolver.Resolve(eventRecord.EventType)) as IEvent
                ?? throw new ProjectionException(CouldNotDeserializeEventMessage);
    }
}