namespace Finance.MongoDb.Common
{
    public static class MongoDbSettings
    {
        public static class EventStore
        {
            public static string? ConnectionString { get; set; }
            public static string? DatabaseName { get; set; }
        }

        public static class Projection
        {
            public static string? ConnectionString { get; set; }
            public static string? DatabaseName { get; set; }
        }
    }
}