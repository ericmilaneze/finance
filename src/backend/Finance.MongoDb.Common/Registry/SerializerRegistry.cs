using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Finance.MongoDb.Common.Registry
{
    public static class SerializersRegistry
    {
        public static void RegisterSerializers()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));
        }
    }
}