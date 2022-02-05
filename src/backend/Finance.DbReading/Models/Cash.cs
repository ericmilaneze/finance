using Finance.Common.FixInc;

namespace Finance.DbReading.Models
{
    public record Cash
    {
        public Guid Id { get; init; }
        public decimal Value { get; init; }
        public CashState State { get; init; }
    }
}