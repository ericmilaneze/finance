using Finance.Framework;
using MongoDB.Bson.Serialization;

namespace Finance.MongoDbEventStore.Registry
{
    public static class ClassMapsRegistry
    {
        internal static void RegisterClassMaps()
        {
            BsonClassMap.RegisterClassMap<EventRecord<Guid>>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}