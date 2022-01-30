using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Finance.MongoDbEventStore.Registry
{
    internal static class SerializersRegistry
    {
        internal static void RegisterSerializers()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));
        }
    }
}