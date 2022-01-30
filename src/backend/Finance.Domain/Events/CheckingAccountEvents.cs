using Finance.Framework;

namespace Finance.Domain.Events
{
    public static class CheckingAccountEvents
    {
        public static class V1
        {
            public record CheckingAccountCreated(
                string Name,
                string Description,
                decimal GrossValue,
                decimal NetValue) : IEvent;

            public record GrossValueUpdated(decimal GrossValue, decimal NetValue) : IEvent;
        }
    }
}