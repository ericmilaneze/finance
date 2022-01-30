using Finance.Domain.ValueObjects;

namespace Finance.Domain.FixInc
{
    public interface IFixedIncome
    {
        Money GrossValue { get; }
        Money NetValue { get; }
    }
}