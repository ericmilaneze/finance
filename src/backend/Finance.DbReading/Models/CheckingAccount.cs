using Finance.Common.FixInc.ChkAcc;

namespace Finance.DbReading.Models
{
    public record CheckingAccount
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public decimal GrossValue { get; init; }
        public decimal NetValue { get; init; }
        public CheckingAccountState State { get; init; }
    }
}