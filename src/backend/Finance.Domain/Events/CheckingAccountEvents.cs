using Finance.Framework;

namespace Finance.Domain.Events
{
    public static class CheckingAccountEvents
    {
        public static class V1
        {
            public record CheckingAccountCreated(
                Guid Id,
                string Name,
                string Description,
                decimal GrossValue,
                decimal NetValue) : IEvent;

            public record GrossValueUpdated(Guid Id, decimal GrossValue, decimal NetValue) : IEvent;
        }
    }
}