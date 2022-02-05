using Finance.DbReading.Models;
using MongoDB.Bson.Serialization;

namespace Finance.MongoDbReading.Registry
{
    public static class ClassMapsRegistry
    {
        public static void RegisterClassMaps()
        {
            BsonClassMap.RegisterClassMap<CheckingAccount>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}