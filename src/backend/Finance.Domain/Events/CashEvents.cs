using Finance.Framework;

namespace Finance.Domain.Events
{
    public static class CashEvents
    {
        public static class V1
        {
            public record CashCreated(
                Guid Id,
                decimal Value) : IEvent;

            public record ValueUpdated(
                Guid Id,
                decimal Value) : IEvent;
        }
    }
}