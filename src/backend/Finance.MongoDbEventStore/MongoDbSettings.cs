using Finance.MongoDbEventStore.Registry;

namespace Finance.MongoDbEventStore
{
    public static class MongoDbSettings
    {
        public static void RegiterSettings()
        {
            ConventionsRegistry.RegisterConventions();
            SerializersRegistry.RegisterSerializers();
            ClassMapsRegistry.RegisterClassMaps();
        }
    }
}