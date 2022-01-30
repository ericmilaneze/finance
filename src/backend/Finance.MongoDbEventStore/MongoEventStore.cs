using Finance.Framework;

namespace Finance.MongoDbEventStore
{
    public class MongoEventStore : IDbEventStore
    {
        public Task<EventRecord[]> GetEventRecords(string aggregateName, object id)
        {
            return Task.FromResult(new EventRecord[]
            {
                new EventRecord(
                    "",
                    Guid.NewGuid(),
                    1,
                    DateTime.UtcNow,
                    "Finance.Domain.Events.CheckingAccountEvents+V1+CheckingAccountCreated",
                    "{\"Name\":\"Account Test\",\"Description\":null,\"GrossValue\":10.23,\"NetValue\":10.23}")
            });
        }

        public Task<int> GetNextVersion(string aggregateName, object id)
        {
            return Task.FromResult(1);
        }

        public Task StoreEvent(EventRecord eventRecord)
        {
            throw new NotImplementedException();
        }
    }
}