// See https://aka.ms/new-console-template for more information

using Finance.Application;
using Finance.Domain.FixInc.ChkAcc;
using Finance.DbEventStore;
using Finance.MongoDbEventStore;

var app = new CheckingAccountApplication(
    new EventStore<CheckingAccount>(
        new AggregateNameResolver<CheckingAccount>(),
        new EventTypeResolver(),
        new MongoEventStore("mongodb://root:123456@localhost:27017/", "finance")));
await app.CreateAccount("Account Test", null, 10.23M);

Console.WriteLine("Created");

//  var eventStore = new EventStore<CheckingAccount>(
//     new AggregateNameResolver<CheckingAccount>(),
//     new EventTypeResolver(),
//     new MongoEventStore("mongodb://root:123456@localhost:27017/", "finance"));
// var aggregate = await eventStore.GetAsync(Guid.Parse("192ab642-7421-4d09-9215-1e63a9d7d8ec"));

// var eventStore = new EventStore<CheckingAccount>();
// var aggregate = eventStore.Get(Guid.NewGuid());

 //Console.WriteLine(aggregate);