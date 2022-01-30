using Finance.Framework;

namespace Finance.MongoDbEventStore.Registry
{
    internal static class CollectionNamesRegistry
    {
        internal static string GetCollectionName<T>()
        {
            Dictionary<Type, string> collectionNames = new()
            {
                [typeof(EventRecord<Guid>)] = "eventRecords",
            };

            return collectionNames[typeof(T)];
        }
    }
}