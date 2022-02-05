using Finance.Common.FixInc;
using Finance.Domain.Events;
using Finance.Domain.ValueObjects;
using Finance.Framework;

namespace Finance.Domain.FixInc.Csh
{
    public class Cash : AggregateRoot
    {
        private const string ValueNotSetMessage = "Value should be set.";

        public Money Value { get; private set; } = new Money(default);
        public CashState State { get; private set; }

        protected Cash(Guid id)
            : base(id)
        {
        }

        public Cash(Guid id, Money value) : this(id) =>
            RaiseEvent(new CashEvents.V1.CashCreated(id, value));

        public void UpdateValue(Guid id, Money value) =>
            RaiseEvent(new CashEvents.V1.ValueUpdated(id, value));

        public override void HandleEvent(IEvent @event)
        {
            switch (@event)
            {
                case CashEvents.V1.CashCreated e:
                    HandleEvent(e);
                    break;
                case CashEvents.V1.ValueUpdated e:
                    HandleEvent(e);
                    break;
            }
        }

        public void HandleEvent(CashEvents.V1.CashCreated @event)
        {
            Value = @event.Value;
            State = CashState.Created;
        }

        public void HandleEvent(CashEvents.V1.ValueUpdated @event)
        {
            Value = @event.Value;
        }

        protected override void ValidateState()
        {
            switch (State)
            {
                case CashState.Created:
                    ValidateStateCreated();
                    break;
            }
        }

        private void ValidateStateCreated()
        {
            if (Value is null)
                throw new DomainException(ValueNotSetMessage);
        }
    }
}