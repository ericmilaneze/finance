using Finance.Common.FixInc;
using Finance.Domain.Events;
using Finance.Domain.ValueObjects;
using Finance.Framework;

namespace Finance.Domain.FixInc.ChkAcc
{
    public class CheckingAccount : AggregateRoot
    {
        private const string GrossValueNotSetMessage = "Gross value should be set.";
        private const string NetValueNotSetMessage = "Net value should be set.";

        public CheckingAccountName? Name { get; private set; }
        public string? Description { get; private set; }
        public string? BankCode { get; private set; }
        public Money GrossValue { get; private set; } = new Money(default);
        public Money NetValue { get; private set; } = new Money(default);
        public CheckingAccountState State { get; private set; }

        protected CheckingAccount(Guid id)
            : base(id)
        {
        }

        public CheckingAccount(
            Guid id,
            CheckingAccountName name,
            string description,
            string bankCode,
            Money initialGrossValue) : this(id)
        {
            RaiseEvent(
                new CheckingAccountEvents.V1.CheckingAccountCreated(
                    id,
                    name,
                    description,
                    bankCode,
                    initialGrossValue,
                    initialGrossValue));
        }

        public void UpdateGrossValue(Money grossValue)
        {
            RaiseEvent(
                new CheckingAccountEvents.V1.GrossValueUpdated(
                    Id,
                    grossValue,
                    grossValue));
        }

        public override void HandleEvent(IEvent @event)
        {
            switch (@event)
            {
                case CheckingAccountEvents.V1.CheckingAccountCreated e:
                    HandleEvent(e);
                    break;
                case CheckingAccountEvents.V1.GrossValueUpdated e:
                    HandleEvent(e);
                    break;
            }
        }

        private void HandleEvent(CheckingAccountEvents.V1.CheckingAccountCreated @event)
        {
            Name = new CheckingAccountName(@event.Name);
            Description = @event.Description;
            BankCode = @event.BankCode;
            GrossValue = new Money(@event.GrossValue);
            NetValue = new Money(@event.NetValue);
            State = CheckingAccountState.Created;
        }

        private void HandleEvent(CheckingAccountEvents.V1.GrossValueUpdated e)
        {
            GrossValue = new Money(e.GrossValue);
            NetValue = new Money(e.NetValue);
        }

        protected override void ValidateState()
        {
            switch (State)
            {
                case CheckingAccountState.Created:
                    ValidateStateCreated();
                    break;
            }
        }

        private void ValidateStateCreated()
        {
            if (GrossValue is null)
                throw new DomainException(GrossValueNotSetMessage);

            if (NetValue is null)
                throw new DomainException(NetValueNotSetMessage);
        }
    }
}