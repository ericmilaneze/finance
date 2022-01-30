using Finance.Domain.FixInc.ChkAcc;
using Finance.Framework;

namespace Finance.Application
{
    public class CheckingAccountApplication
    {
        private readonly IEventStore<CheckingAccount> _eventStore;

        public CheckingAccountApplication(IEventStore<CheckingAccount> eventStore)
        {
            _eventStore = eventStore;
        }

        public void CreateAccount(string name, string description, decimal grossValue)
        {
            var id = Guid.NewGuid();
            var account = new CheckingAccount(id, name, description, grossValue);
            _eventStore.Store(account);
        }
    }
}