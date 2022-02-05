// See https://aka.ms/new-console-template for more information

using Finance.Application;
using Finance.Domain.FixInc.ChkAcc;
using Finance.DbEventStore;
using Finance.MongoDbEventStore;
using Finance.MongoDbReading;
using Finance.MongoDb.Common.Registry;
using Finance.Framework;
using Finance.MongoDb.Common;

Settings.Projection.IsEnabled = true;
MongoDbSettings.EventStore.ConnectionString = "mongodb://root:123456@localhost:27017/";
MongoDbSettings.EventStore.DatabaseName = "finance";
MongoDbSettings.Projection.ConnectionString = "mongodb://root:123456@localhost:27017/";
MongoDbSettings.Projection.DatabaseName = "finance-reading";

ConventionsRegistry.RegisterConventions();
SerializersRegistry.RegisterSerializers();
Finance.MongoDbEventStore.Registry.ClassMapsRegistry.RegisterClassMaps();
Finance.MongoDbReading.Registry.ClassMapsRegistry.RegisterClassMaps();

var eventTypeResolver = new EventTypeResolver();

var eventStore = new MongoEventStore();
var projection = new MongoProjection(eventTypeResolver);

var app = new CheckingAccountApplication(
    new EventStore<CheckingAccount>(
        new AggregateNameResolver<CheckingAccount>(),
        eventTypeResolver,
        eventStore,
        projection));

var id = await app.CreateAccountAsync("Account Test", null, 10.23M);

Console.WriteLine("Created");

await app.UpdateAccountAsync(id, 1.10M);

//  var eventStore = new EventStore<CheckingAccount>(
//     new AggregateNameResolver<CheckingAccount>(),
//     new EventTypeResolver(),
//     new MongoEventStore("mongodb://root:123456@localhost:27017/", "finance"));
// var aggregate = await eventStore.GetAsync(Guid.Parse("192ab642-7421-4d09-9215-1e63a9d7d8ec"));

// var eventStore = new EventStore<CheckingAccount>();
// var aggregate = eventStore.Get(Guid.NewGuid());

 //Console.WriteLine(aggregate);