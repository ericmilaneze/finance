using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace Finance.MongoDbEventStore.Registry
{
    internal static class ConventionsRegistry
    {
        internal static void RegisterConventions()
        {
            ConventionRegistry.Register(
                "Custom Conventions",
                new ConventionPack
                {
                    new CamelCaseElementNameConvention(),
                    new IgnoreIfNullConvention(true),
                    new EnumRepresentationConvention(BsonType.String)
                },
                _ => true);
        }
    }
}