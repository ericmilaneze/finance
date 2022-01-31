using Finance.Framework;
using Finance.MongoDbEventStore.Registry;
using MongoDB.Driver;

namespace Finance.MongoDbEventStore
{
    public class MongoEventStore : IDbEventStore<Guid>
    {
        private readonly IMongoDatabase _database;

        static MongoEventStore() =>
            MongoDbSettings.RegiterSettings();

        public MongoEventStore(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(dbName);
        }

        public async Task<IList<EventRecord<Guid>>> GetEventRecordsAsync(string aggregateName, Guid id)
        {
            return await _database
                .GetCollection<EventRecord<Guid>>(CollectionNamesRegistry.GetCollectionName<EventRecord<Guid>>())
                .Find(x => x.AggregateType == aggregateName && x.Id == id)
                .SortBy(x => x.Version)
                .ToListAsync();
        }

        public async Task<int> GetNextVersionAsync(string aggregateName, Guid id)
        {
            var lastVersion = await _database
                .GetCollection<EventRecord<Guid>>(CollectionNamesRegistry.GetCollectionName<EventRecord<Guid>>())
                .Find(x => x.AggregateType == aggregateName && x.Id == id)
                .SortByDescending(x => x.Version)
                .Limit(1)
                .Project(x => x.Version)
                .FirstOrDefaultAsync();

            return lastVersion + 1;
        }

        public async Task StoreEventAsync(EventRecord<Guid> eventRecord)
        {
            await _database
                .GetCollection<EventRecord<Guid>>(CollectionNamesRegistry.GetCollectionName<EventRecord<Guid>>())
                .InsertOneAsync(eventRecord);
        }

        public async Task StoreEventsAsync(EventRecord<Guid>[] eventRecords)
        {
            await _database
                .GetCollection<EventRecord<Guid>>(CollectionNamesRegistry.GetCollectionName<EventRecord<Guid>>())
                .InsertManyAsync(eventRecords);
        }
    }
}