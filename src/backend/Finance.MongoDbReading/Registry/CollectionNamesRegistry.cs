using Finance.DbReading.Models;

namespace Finance.MongoDbReading.Registry
{
    internal static class CollectionNamesRegistry
    {
        internal static string GetCollectionName<T>()
        {
            Dictionary<Type, string> collectionNames = new()
            {
                [typeof(CheckingAccount)] = "checkingAccounts",
            };

            return collectionNames[typeof(T)];
        }
    }
}