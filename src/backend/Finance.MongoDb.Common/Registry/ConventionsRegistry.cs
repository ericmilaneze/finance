using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace Finance.MongoDb.Common.Registry
{
    public static class ConventionsRegistry
    {
        public static void RegisterConventions()
        {
            ConventionRegistry.Register(
                "Custom Conventions",
                new ConventionPack
                {
                    new CamelCaseElementNameConvention(),
                    new IgnoreIfNullConvention(true),
                    new EnumRepresentationConvention(BsonType.Int32)
                },
                _ => true);
        }
    }
}