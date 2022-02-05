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

        public async Task<Guid> CreateAccountAsync(
            string name,
            string description,
            string bankCode,
            decimal grossValue)
        {
            var id = Guid.NewGuid();
            var account = new CheckingAccount(
                id,
                name,
                description,
                bankCode,
                grossValue);
            await _eventStore.StoreAsync(account);
            return id;
        }

        public async Task UpdateAccountAsync(Guid id, decimal grossValue)
        {
            var account = await _eventStore.GetAsync(id);
            account.UpdateGrossValue(grossValue);
            await _eventStore.StoreAsync(account);
        }
    }
}