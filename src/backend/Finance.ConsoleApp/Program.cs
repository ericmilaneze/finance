// See https://aka.ms/new-console-template for more information

using Finance.Application;
using Finance.Domain.FixInc.ChkAcc;
using Finance.DbEventStore;
using Finance.MongoDbEventStore;

var app = new CheckingAccountApplication(new EventStore<CheckingAccount>(new AggregateNameResolver<CheckingAccount>(), new MongoEventStore()));
await app.CreateAccount("Account Test", null, 10.23M);

Console.WriteLine("Created");

// var eventStore = new EventStore<CheckingAccount, Guid>();
// var aggregate = eventStore.Get(Guid.NewGuid());

// var eventStore = new EventStore<CheckingAccount>();
// var aggregate = eventStore.Get(Guid.NewGuid());

// Console.WriteLine(aggregate);