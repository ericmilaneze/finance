using Finance.Framework;
using Finance.MongoDb.Common;
using Finance.MongoDbEventStore.Registry;
using MongoDB.Driver;

namespace Finance.MongoDbEventStore
{
    public class MongoEventStore : IDbEventStore
    {
        private readonly IMongoDatabase _database;

        public MongoEventStore(
            string? connectionString = default,
            string? dbName = default)
        {
            var client = new MongoClient(connectionString ?? MongoDbSettings.EventStore.ConnectionString);
            _database = client.GetDatabase(dbName ?? MongoDbSettings.EventStore.DatabaseName);
        }

        public async Task<IList<EventRecord<Guid>>> GetEventRecordsAsync(
            string aggregateName,
            Guid id,
            CancellationToken cancellationToken = default
        ) =>
            await _database
                .GetCollection<EventRecord<Guid>>(CollectionNamesRegistry.GetCollectionName<EventRecord<Guid>>())
                .Find(x => x.AggregateType == aggregateName && x.AggregateId == id)
                .SortBy(x => x.Version)
                .ToListAsync(cancellationToken);

        public async Task<int> GetNextVersionAsync(
            string aggregateName,
            Guid id,
            CancellationToken cancellationToken = default
        ) =>
            await _database
                .GetCollection<EventRecord<Guid>>(CollectionNamesRegistry.GetCollectionName<EventRecord<Guid>>())
                .Find(x => x.AggregateType == aggregateName && x.AggregateId == id)
                .SortByDescending(x => x.Version)
                .Limit(1)
                .Project(x => x.Version)
                .FirstOrDefaultAsync(cancellationToken) + 1;

        public async Task StoreEventAsync(
            EventRecord<Guid> eventRecord,
            CancellationToken cancellationToken = default
        ) =>
            await _database
                .GetCollection<EventRecord<Guid>>(CollectionNamesRegistry.GetCollectionName<EventRecord<Guid>>())
                .InsertOneAsync(eventRecord, cancellationToken: cancellationToken);

        public async Task StoreEventsAsync(
            EventRecord<Guid>[] eventRecords,
            CancellationToken cancellationToken = default
        ) =>
            await _database
                .GetCollection<EventRecord<Guid>>(CollectionNamesRegistry.GetCollectionName<EventRecord<Guid>>())
                .InsertManyAsync(eventRecords, cancellationToken: cancellationToken);
    }
}